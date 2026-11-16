using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class DisponibilidadService : IDisponibilidadService
{
    private const string PATH = "DisponibilidadRS";
    private readonly HttpClient _http;
    private readonly ILogger<DisponibilidadService> _logger;

    public DisponibilidadService(HttpClient http, ILogger<DisponibilidadService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<List<string>> ConsultarAsync(int empleadoId, DateTime fecha, int duracion)
    {
        try
        {
            string url = $"{PATH}/consultar?empleadoId={empleadoId}&fecha={fecha:yyyy-MM-dd}&duracion={duracion}";
            var response = await _http.GetAsync(url);
            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<List<string>>>();

            if (!response.IsSuccessStatusCode || resultado == null || !resultado.Success)
            {
                throw new InvalidOperationException(resultado?.Message ?? "No se pudo consultar la disponibilidad.");
            }

            return resultado.Data ?? new List<string>();
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar disponibilidad del empleado {EmpleadoId}", empleadoId);
            throw new Exception("No se pudo consultar la disponibilidad en este momento. Inténtalo nuevamente.");
        }
    }
}
