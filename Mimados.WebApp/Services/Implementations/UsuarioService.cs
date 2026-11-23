using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class UsuarioService : IUsuarioService
{
    private const string PATH = "UsuarioRS";
    private readonly HttpClient _httpClient;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(HttpClient httpClient, ILogger<UsuarioService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<UsuarioPerfilModel?> ObtenerPerfilAsync(int idUsuario)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<UsuarioPerfilModel>($"{PATH}/perfil/{idUsuario}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el perfil del usuario {IdUsuario}", idUsuario);
            return null;
        }
    }

    public async Task<bool> ActualizarPerfilAsync(UsuarioPerfilModel perfil)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/perfil", perfil);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el perfil del usuario {IdUsuario}", perfil.Id);
            return false;
        }
    }

    public async Task<bool> EliminarCuentaLogicaAsync(int idUsuario)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{PATH}/eliminar/{idUsuario}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la cuenta del usuario {IdUsuario}", idUsuario);
            return false;
        }
    }

    public async Task<bool> EsClienteActivoAsync(int idUsuario)
    {
        try
        {
            var res = await _httpClient.GetFromJsonAsync<ApiResponse<bool>>($"{PATH}/es-cliente/{idUsuario}");
            return res != null && res.Success && res.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar perfil de cliente para {IdUsuario}", idUsuario);
            return false;
        }
    }

    public async Task<(bool Success, string Message)> ActivarPerfilClienteAsync(int idUsuario, string? direccion, string? contactoEmergencia)
    {
        try
        {
            var payload = new
            {
                usuarioId = idUsuario,
                direccion = direccion ?? string.Empty,
                contactoEmergencia = contactoEmergencia ?? string.Empty
            };
            var response = await _httpClient.PostAsJsonAsync($"{PATH}/activar-cliente", payload);
            var res = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            if (response.IsSuccessStatusCode && res != null && res.Success)
            {
                return (true, res.Message ?? "Perfil de cliente activado correctamente.");
            }
            return (false, res?.Message ?? "No se pudo activar el perfil de cliente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al activar perfil de cliente para {IdUsuario}", idUsuario);
            return (false, "No se pudo conectar con el servidor.");
        }
    }

    public async Task<List<ClienteOpcionDTO>> ListarClientesAsync()
    {
        try
        {
            var respuesta = await _httpClient.GetFromJsonAsync<
                ApiResponse<List<ClienteOpcionDTO>>>(
                    $"{PATH}/clientes");

            if (respuesta?.Success != true)
            {
                throw new InvalidOperationException(
                    respuesta?.Message ?? "No se pudieron obtener los clientes.");
            }

            return respuesta.Data ?? new List<ClienteOpcionDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar los clientes");
            throw;
        }
    }

}
