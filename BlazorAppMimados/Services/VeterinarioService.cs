
using System.Net.Http.Json;
using BlazorAppMimados.Models;

namespace Mimados_WebApp.Services;

// Datos del veterinario que devuelve Java
public class VeterinarioResumenDto
{
    public int EmpleadoId { get; set; }
    public string Nombre { get; set; } = "";
    public int SedeId { get; set; }
    public string Sede { get; set; } = "";
    public string Puesto { get; set; } = "";
}

public interface IVeterinarioService
{
    Task<List<VeterinarioResumenDto>> ListarAsync();
}

public class VeterinarioService : IVeterinarioService
{
    private readonly HttpClient _http;

    public VeterinarioService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<VeterinarioResumenDto>> ListarAsync()
    {
        var response = await _http.GetAsync(
            "VeterinarioRS/listar");

        var resultado = await response.Content
            .ReadFromJsonAsync<ApiResponse<List<VeterinarioResumenDto>>>();

        if (!response.IsSuccessStatusCode ||
            resultado == null ||
            !resultado.Success)
        {
            throw new Exception(
                resultado?.Message ?? "Error al listar veterinarios");
        }

        return resultado.Data ?? new List<VeterinarioResumenDto>();
    }
}
