using System.Text;
using System.Text.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class HorarioLaboralService : IHorarioLaboralService
{
    private const string PATH = "horarios";
    private readonly HttpClient _httpClient;
    private readonly ILogger<HorarioLaboralService> _logger;

    public HorarioLaboralService(HttpClient httpClient, ILogger<HorarioLaboralService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<HorarioLaboral>> ListarPorEmpleadoAsync(int empleadoId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{PATH}/listarPorEmpleado/{empleadoId}");
            if (!response.IsSuccessStatusCode) return new List<HorarioLaboral>();

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var apiResponse = JsonSerializer.Deserialize<ApiResponseJava<List<HorarioLaboral>>>(jsonString, options);

            if (apiResponse == null || !apiResponse.Success || apiResponse.Data == null)
                return new List<HorarioLaboral>();

            return apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar horarios del empleado {Id}", empleadoId);
            return new List<HorarioLaboral>();
        }
    }

    public async Task<bool> RegistrarHorarioAsync(HorarioLaboral horario)
    {
        try
        {
            // Enviamos el objeto plano que espera HorarioLaboralRS.HorarioRequest en Java
            var payload = new
            {
                empleadoId = horario.EmpleadoId,
                diaSemana = horario.DiaSemana,
                horaInicio = horario.HoraInicio,
                horaFin = horario.HoraFin,
                activo = horario.Activo
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{PATH}/registrar", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar horario");
            return false;
        }
    }

    public async Task<bool> ModificarHorarioAsync(HorarioLaboral horario)
    {
        try
        {
            var payload = new
            {
                id = horario.Id,
                diaSemana = horario.DiaSemana,
                horaInicio = horario.HoraInicio,
                horaFin = horario.HoraFin,
                activo = horario.Activo
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{PATH}/modificar", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al modificar horario {Id}", horario.Id);
            return false;
        }
    }

    public async Task<bool> EliminarHorarioAsync(int horarioId)
    {
        try
        {
            // Tu backend Java espera un POST a /eliminar con un objeto que contenga el id
            var payload = new { id = horarioId };
            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{PATH}/eliminar", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar horario {Id}", horarioId);
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

    private class HorarioJavaResponse
    {
        public int Id { get; set; }
        public EmpleadoJavaResponse? Empleado { get; set; }
        public string? DiaSemana { get; set; }
        public string? HoraInicio { get; set; }
        public string? HoraFin { get; set; }
        public bool Activo { get; set; }
    }

    private class EmpleadoJavaResponse
    {
        public UsuarioJavaResponse? Usuario { get; set; }
    }

    private class UsuarioJavaResponse
    {
        public int Id { get; set; }
    }
}