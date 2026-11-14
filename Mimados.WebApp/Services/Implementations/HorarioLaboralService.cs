using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class HorarioLaboralService : IHorarioLaboralService
{
    private const string PATH = "HorarioLaboralRS";
    private readonly HttpClient _http;
    private readonly ILogger<HorarioLaboralService> _logger;

    public HorarioLaboralService(HttpClient http, ILogger<HorarioLaboralService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<ApiResponse<object>?> RegistrarHorarioAsync(HorarioRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync($"{PATH}/registrar", request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar horario laboral");
            return new ApiResponse<object> { Success = false, Message = "Error de conexión al registrar horario." };
        }
    }

    public async Task<ApiResponse<List<HorarioRequest>>?> ListarPorEmpleadoAsync(int idEmpleado)
    {
        try
        {
            return await _http.GetFromJsonAsync<ApiResponse<List<HorarioRequest>>>($"{PATH}/listarPorEmpleado/{idEmpleado}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar horarios del empleado {EmpleadoId}", idEmpleado);
            return new ApiResponse<List<HorarioRequest>>
            {
                Success = false,
                Message = "Error de conexión al listar horarios.",
                Data = new List<HorarioRequest>()
            };
        }
    }

    public async Task<ApiResponse<object>?> EliminarHorarioAsync(int idHorario)
    {
        try
        {
            var request = new HorarioRequest { Id = idHorario };
            var response = await _http.PostAsJsonAsync($"{PATH}/eliminar", request);
            return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar horario {HorarioId}", idHorario);
            return new ApiResponse<object> { Success = false, Message = "Error de conexión al eliminar horario." };
        }
    }
}
