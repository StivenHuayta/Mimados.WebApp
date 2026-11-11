
using System.Net.Http.Json;
using BlazorAppMimados.Models;

namespace Mimados_WebApp.Services;

public interface IDisponibilidadService
{
    Task<List<string>> ConsultarAsync(
        int empleadoId,
        DateTime fecha,
        int duracion);
}

public class DisponibilidadService : IDisponibilidadService
{
    private readonly HttpClient _http;

    public DisponibilidadService(HttpClient http)
    {
        _http = http;
    }

    // Consultar horarios libres desde Java
    public async Task<List<string>> ConsultarAsync(
        int empleadoId,
        DateTime fecha,
        int duracion)
    {
        string url =
            $"DisponibilidadRS/consultar?empleadoId={empleadoId}" +
            $"&fecha={fecha:yyyy-MM-dd}&duracion={duracion}";

        var response = await _http.GetAsync(url);

        var resultado = await response.Content
            .ReadFromJsonAsync<ApiResponse<List<string>>>();

        if (!response.IsSuccessStatusCode ||
            resultado == null ||
            !resultado.Success)
        {
            throw new Exception(
                resultado?.Message ??
                "Error al consultar disponibilidad.");
        }

        return resultado.Data ?? new List<string>();
    }
}
