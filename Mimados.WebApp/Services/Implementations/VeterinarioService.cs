using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class VeterinarioService : IVeterinarioService
{
    private const string PATH = "VeterinarioRS";
    private readonly HttpClient _http;
    private readonly ILogger<VeterinarioService> _logger;

    public VeterinarioService(HttpClient http, ILogger<VeterinarioService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<List<VeterinarioResumenDto>> ListarAsync()
    {
        try
        {
            var response = await _http.GetAsync($"{PATH}/listar");
            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<List<VeterinarioResumenDto>>>();

            if (!response.IsSuccessStatusCode || resultado == null || !resultado.Success)
            {
                throw new InvalidOperationException(resultado?.Message ?? "No se pudieron cargar los veterinarios.");
            }

            return resultado.Data ?? new List<VeterinarioResumenDto>();
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar veterinarios");
            throw new Exception("No se pudieron cargar los veterinarios en este momento. Inténtalo nuevamente.");
        }
    }
}
