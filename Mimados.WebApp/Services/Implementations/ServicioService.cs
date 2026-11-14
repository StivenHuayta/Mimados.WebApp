using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class ServicioService : IServicioService
{
    private const string PATH = "ServicioRS";
    private readonly HttpClient _http;
    private readonly ILogger<ServicioService> _logger;

    public ServicioService(HttpClient http, ILogger<ServicioService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<Servicio?> ObtenerServicioPorIdAsync(int id)
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<Servicio>>($"{PATH}/{id}");
            return respuesta?.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener servicio {ServicioId}", id);
            return null;
        }
    }

    public async Task<List<Servicio>> ObtenerServiciosAsync()
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Servicio>>>($"{PATH}/listarServicios");
            return respuesta?.Data ?? new List<Servicio>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar servicios");
            return new List<Servicio>();
        }
    }

    public async Task<bool> CrearServicioAsync(Servicio servicio)
    {
        try
        {
            var datos = new
            {
                nombre = servicio.Nombre,
                descripcion = servicio.Descripcion,
                precioReferencial = servicio.PrecioReferencial,
                duracionMinutos = servicio.DuracionMinutos
            };

            var response = await _http.PostAsJsonAsync($"{PATH}/registrar", datos);
            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return resultado?.Success == true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear servicio {Nombre}", servicio.Nombre);
            return false;
        }
    }

    public async Task<bool> ActualizarServicioAsync(Servicio servicio)
    {
        try
        {
            var datos = new
            {
                id = servicio.Id,
                nombre = servicio.Nombre,
                descripcion = servicio.Descripcion,
                precioReferencial = servicio.PrecioReferencial,
                duracionMinutos = servicio.DuracionMinutos,
                activo = servicio.Activo
            };

            var response = await _http.PutAsJsonAsync(PATH, datos);
            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(string.IsNullOrWhiteSpace(errorMsg) || errorMsg.TrimStart().StartsWith("<")
                    ? "No se pudo actualizar el servicio."
                    : errorMsg);
            }

            return true;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar servicio {ServicioId}", servicio.Id);
            throw new Exception("No se pudo guardar los cambios. Inténtalo nuevamente.");
        }
    }
}
