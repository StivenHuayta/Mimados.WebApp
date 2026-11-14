using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

/// <summary>
/// Servicio de autenticación y registro que consume el servicio REST de usuarios.
/// </summary>
public class AuthValidationService : IAuthValidationService
{
    private const string PATH = "UsuarioRS";

    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthValidationService> _logger;

    public AuthValidationService(
        HttpClient httpClient,
        ILogger<AuthValidationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<AuthValidationResult> ValidarCredencialesAsync(string usuarioOCorreo, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(usuarioOCorreo) || string.IsNullOrWhiteSpace(contrasena))
        {
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = "Por favor, ingresa tu correo y contraseña."
            };
        }

        var input = usuarioOCorreo.Trim();
        var loginReq = new LoginRequest(input, contrasena);

        var endpoints = new[]
        {
            $"{PATH}/autenticar",
            $"{PATH}/login"
        };

        HttpResponseMessage? response = null;

        foreach (var url in endpoints)
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(loginReq),
                    Encoding.UTF8,
                    "application/json");

                var res = await _httpClient.PostAsync(url, jsonContent, cts.Token);

                if (res.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    response = res;
                    break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Intento de conexión a {Url} no exitoso", url);
            }
        }

        if (response == null)
        {
            _logger.LogError("No se pudo conectar con el servicio de autenticación.");
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = "No se pudo conectar con el sistema en este momento. Inténtalo nuevamente en unos minutos."
            };
        }

        try
        {
            var contentString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(contentString) || contentString.TrimStart().StartsWith("<"))
            {
                _logger.LogError("El servicio de autenticación devolvió una respuesta HTML inesperada (Código {StatusCode}): {Body}",
                    (int)response.StatusCode, contentString);

                return new AuthValidationResult
                {
                    Exito = false,
                    MensajeError = "Ocurrió un problema temporal en el servidor. Inténtalo nuevamente más tarde."
                };
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiResult = JsonSerializer.Deserialize<UsuarioApiResponse>(contentString, options);

            if (response.IsSuccessStatusCode && apiResult != null && apiResult.Success && apiResult.Data != null)
            {
                var data = apiResult.Data;
                var rol = string.IsNullOrWhiteSpace(data.Rol) ? "Cliente" : data.Rol;

                return new AuthValidationResult
                {
                    Exito = true,
                    UsuarioId = data.UsuarioId,
                    Dni = data.Dni ?? string.Empty,
                    Nombres = data.Nombres ?? string.Empty,
                    Apellidos = data.Apellidos ?? string.Empty,
                    Email = string.IsNullOrWhiteSpace(data.Email) ? input : data.Email,
                    Rol = rol,
                    EsAdmin = data.EsAdmin,
                    Activo = data.Activo,
                    CodigoCmvp = data.CodigoCmvp,
                    SedeId = data.SedeId
                };
            }

            var mensaje = apiResult?.Message;
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                mensaje = response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => "Correo o contraseña incorrectos.",
                    System.Net.HttpStatusCode.BadRequest => "Por favor, completa todos los datos para iniciar sesión.",
                    _ => "No se pudo iniciar sesión en este momento. Inténtalo nuevamente."
                };
            }

            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = mensaje
            };
        }
        catch (JsonException jEx)
        {
            _logger.LogError(jEx, "Error al deserializar la respuesta del servicio de autenticación");
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = "Ocurrió un problema al procesar la respuesta. Inténtalo nuevamente más tarde."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al procesar la respuesta de autenticación");
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = "Ocurrió un error inesperado al iniciar sesión. Inténtalo nuevamente."
            };
        }
    }

    public async Task<(bool Exito, string? Mensaje)> RegistrarClienteAsync(RegistroClienteRequest model)
    {
        if (model == null)
        {
            return (false, "Por favor, completa los datos del formulario.");
        }

        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(7));

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{PATH}/registrarCliente", jsonContent, cts.Token);
            var contentString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(contentString) || contentString.TrimStart().StartsWith("<"))
            {
                _logger.LogError("El servicio de registro devolvió una respuesta no válida (Código {StatusCode})",
                    (int)response.StatusCode);
                return (false, "Ocurrió un problema temporal en el servidor. Inténtalo nuevamente más tarde.");
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiResult = JsonSerializer.Deserialize<UsuarioApiResponse>(contentString, options);

            if (response.IsSuccessStatusCode && apiResult != null && apiResult.Success)
            {
                return (true, apiResult.Message ?? "Cuenta registrada con éxito.");
            }

            return (false, apiResult?.Message ?? "No se pudo completar el registro. Verifica tus datos.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar cliente");
            return (false, "No se pudo conectar con el sistema en este momento. Inténtalo nuevamente más tarde.");
        }
    }
}
