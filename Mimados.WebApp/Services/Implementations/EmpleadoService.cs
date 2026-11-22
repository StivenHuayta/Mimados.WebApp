using System.Text;
using System.Text.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class EmpleadoService : IEmpleadoService
{
    private const string PATH = "empleados";
    private readonly HttpClient _httpClient;
    private readonly ILogger<EmpleadoService> _logger;

    public EmpleadoService(HttpClient httpClient, ILogger<EmpleadoService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Empleado>> ListarEmpleadosAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{PATH}/listar");
            if (!response.IsSuccessStatusCode) return new List<Empleado>();

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var apiResponse = JsonSerializer.Deserialize<ApiResponseJava<List<Empleado>>>(jsonString, options);

            if (apiResponse == null || !apiResponse.Success || apiResponse.Data == null)
                return new List<Empleado>();

            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar empleados");
            return new List<Empleado>();
        }
    }

    public async Task<bool> RegistrarEmpleadoAsync(Empleado empleado)
    {
        try
        {
            // Enviamos el objeto plano exacto que espera EmpleadoRS.EmpleadoRequest en Java
            var payload = new
            {
                dni = empleado.Dni,
                nombres = empleado.Nombres,
                apellidos = empleado.Apellidos,
                email = empleado.Email,
                password = empleado.Password,
                telefono = empleado.Telefono,
                sedeId = empleado.SedeId,
                codigoCmvp = empleado.CodigoCmvp,
                esAdmin = empleado.EsAdmin,
                activo = empleado.Activo
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{PATH}/registrar", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar empleado");
            return false;
        }
    }

    public async Task<bool> ModificarEmpleadoAsync(Empleado empleado)
    {
        try
        {
            var payload = new
            {
                id = empleado.Id,
                sedeId = empleado.SedeId,
                codigoCmvp = empleado.CodigoCmvp,
                esAdmin = empleado.EsAdmin,
                activo = empleado.Activo
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{PATH}/modificar", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al modificar empleado {Id}", empleado.Id);
            return false;
        }
    }

    // Clases privadas para mapear la respuesta de Java
    private class ApiResponseJava<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }

    private class EmpleadoJavaResponse
    {
        public UsuarioJavaResponse? Usuario { get; set; }
        public SedeJavaResponse? Sede { get; set; }
        public string? CodigoCmvp { get; set; }
        public bool EsAdmin { get; set; }
        public bool Activo { get; set; }
    }

    private class UsuarioJavaResponse
    {
        public int Id { get; set; }
        public string? Dni { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }

    private class SedeJavaResponse
    {
        public int Id { get; set; }
    }
}