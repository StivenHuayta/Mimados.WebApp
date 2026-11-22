using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class AtencionService : IAtencionService
{
    private const string PATH = "AtencionRS";
    private readonly HttpClient _http;
    private readonly ILogger<AtencionService> _logger;

    public AtencionService(HttpClient http, ILogger<AtencionService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<(bool Success, string Message)> RegistrarAsync(RegistrarAtencionDto request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync($"{PATH}/registrar", request);
            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

            if (!response.IsSuccessStatusCode || resultado?.Success != true)
            {
                return (false, resultado?.Message ?? "No se pudo registrar la atención clínica.");
            }

            return (true, resultado.Message ?? "Atención registrada correctamente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar atención para la cita {CitaId}", request.CitaId);
            return (false, "No se pudo conectar con el servidor. Inténtalo nuevamente.");
        }
    }
}
