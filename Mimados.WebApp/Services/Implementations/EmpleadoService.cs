using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class EmpleadoService : IEmpleadoService
{
    private const string PATH = "EmpleadoRS";
    private readonly HttpClient _http;
    private readonly ILogger<EmpleadoService> _logger;

    public EmpleadoService(HttpClient http, ILogger<EmpleadoService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<ApiResponse<object>?> RegistrarEmpleadoAsync(EmpleadoRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync($"{PATH}/registrar", request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar empleado {Email}", request.Email);
            return new ApiResponse<object> { Success = false, Message = "Error de conexión al registrar empleado." };
        }
    }

    public async Task<ApiResponse<object>?> ModificarEmpleadoAsync(EmpleadoRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync($"{PATH}/modificar", request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al modificar empleado {EmpleadoId}", request.Id);
            return new ApiResponse<object> { Success = false, Message = "Error de conexión al modificar empleado." };
        }
    }

    public async Task<ApiResponse<List<EmpleadoRequest>>?> ListarEmpleadosAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<ApiResponse<List<EmpleadoRequest>>>($"{PATH}/listar");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar empleados");
            return new ApiResponse<List<EmpleadoRequest>>
            {
                Success = false,
                Message = "Error de conexión al listar empleados.",
                Data = new List<EmpleadoRequest>()
            };
        }
    }
}
