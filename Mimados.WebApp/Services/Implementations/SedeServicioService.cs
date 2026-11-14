using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class SedeServicioService : ISedeServicioService
{
    private const string PATH = "SedeServicioRS";
    private readonly HttpClient _http;
    private readonly ILogger<SedeServicioService> _logger;

    public SedeServicioService(HttpClient http, ILogger<SedeServicioService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<List<SedeServicioDTO>> ObtenerServiciosPorSedeAsync(int sedeid)
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<SedeServicioDTO>>>(
                $"{PATH}/listarSedeServicios/{sedeid}");

            return respuesta?.Data ?? new List<SedeServicioDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar servicios de la sede {SedeId}", sedeid);
            return new List<SedeServicioDTO>();
        }
    }

    public async Task<bool> EliminarSedeServicioAsync(int sedeServicioId)
    {
        try
        {
            var response = await _http.DeleteAsync($"{PATH}/{sedeServicioId}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar sede-servicio {SedeServicioId}", sedeServicioId);
            return false;
        }
    }

    public async Task<List<Servicio>> ObtenerServiciosDisponiblesAsync(int sedeid)
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Servicio>>>(
                $"{PATH}/listarServiciosDisponibles/{sedeid}");

            return respuesta?.Data ?? new List<Servicio>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar servicios disponibles de la sede {SedeId}", sedeid);
            return new List<Servicio>();
        }
    }

    public async Task<bool> CrearSedeServicioAsync(SedeServicio sedeServicio)
    {
        try
        {
            var datos = new
            {
                id_sede = sedeServicio.Sede.Id,
                id_servicio = sedeServicio.Servicio.Id
            };

            var response = await _http.PostAsJsonAsync($"{PATH}/registrar", datos);
            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return resultado?.Success == true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al asignar servicio {ServicioId} a sede {SedeId}", sedeServicio.Servicio.Id, sedeServicio.Sede.Id);
            return false;
        }
    }
}
