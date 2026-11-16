using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class CitaService : ICitaService
{
    private const string PATH = "CitaRS";
    private readonly HttpClient _http;
    private readonly ILogger<CitaService> _logger;

    public CitaService(HttpClient http, ILogger<CitaService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<List<Cita>> ListarPorMascotaAsync(int mascotaId)
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Cita>>>(
                $"{PATH}/listarPorMascota/{mascotaId}");

            return respuesta?.Data ?? new List<Cita>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar citas de la mascota {MascotaId}", mascotaId);
            return new List<Cita>();
        }
    }

    public async Task<Cita?> ObtenerPorIdAsync(int id)
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<Cita>>($"{PATH}/{id}");
            return respuesta?.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la cita {CitaId}", id);
            return null;
        }
    }

    public async Task<bool> RegistrarAsync(Cita cita)
    {
        try
        {
            var datos = new
            {
                empleadoId = cita.EmpleadoId,
                mascotaId = cita.MascotaId,
                fechaHoraInicio = cita.FechaHoraInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                fechaHoraFin = cita.FechaHoraFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                motivo = cita.Motivo
            };

            var response = await _http.PostAsJsonAsync($"{PATH}/registrar", datos);
            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return resultado?.Success == true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar cita para mascota {MascotaId}", cita.MascotaId);
            return false;
        }
    }

    public async Task<bool> CancelarAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"{PATH}/{id}");
            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return resultado?.Success == true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cancelar la cita {CitaId}", id);
            return false;
        }
    }

    public async Task<bool> ModificarAsync(Cita cita)
    {
        try
        {
            var datos = new
            {
                empleadoId = cita.EmpleadoId,
                fechaHoraInicio = cita.FechaHoraInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                fechaHoraFin = cita.FechaHoraFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                motivo = cita.Motivo
            };

            var response = await _http.PutAsJsonAsync($"{PATH}/{cita.CitaId}", datos);
            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

            if (!response.IsSuccessStatusCode || resultado?.Success != true)
            {
                throw new InvalidOperationException(resultado?.Message ?? "No se pudo modificar la cita.");
            }

            return true;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al modificar la cita {CitaId}", cita.CitaId);
            throw new Exception("No se pudo conectar con el servidor. Inténtalo nuevamente.");
        }
    }
}
