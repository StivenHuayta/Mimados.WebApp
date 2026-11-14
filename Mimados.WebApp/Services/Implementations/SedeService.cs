using System.Net.Http.Json;
using Mimados.WebApp.Models;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Services.Implementations;

public class SedeService : ISedeService
{
    private const string PATH = "SedeRS";
    private readonly HttpClient _http;
    private readonly ILogger<SedeService> _logger;

    public SedeService(HttpClient http, ILogger<SedeService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<Sede?> ObtenerSedePorIdAsync(int id)
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<Sede>>($"{PATH}/{id}");
            return respuesta?.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener sede {SedeId}", id);
            return null;
        }
    }

    public async Task<List<Sede>> ObtenerSedesAsync()
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Sede>>>($"{PATH}/listarSede");
            return respuesta?.Data ?? new List<Sede>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar sedes");
            return new List<Sede>();
        }
    }

    public async Task<bool> CrearSedeAsync(Sede sede)
    {
        try
        {
            var datos = new
            {
                nombre = sede.Nombre,
                direccion = sede.Direccion,
                telefono = sede.Telefono
            };

            var response = await _http.PostAsJsonAsync($"{PATH}/registrar", datos);
            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return resultado?.Success == true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear sede {Nombre}", sede.Nombre);
            return false;
        }
    }

    public async Task<bool> ActualizarSedeAsync(Sede sede)
    {
        try
        {
            var datos = new
            {
                id = sede.Id,
                nombre = sede.Nombre,
                direccion = sede.Direccion,
                telefono = sede.Telefono,
                activo = sede.Activo
            };

            var response = await _http.PutAsJsonAsync(PATH, datos);
            if (!response.IsSuccessStatusCode)
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(string.IsNullOrWhiteSpace(errorMsg) || errorMsg.TrimStart().StartsWith("<")
                    ? "No se pudo actualizar la sede."
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
            _logger.LogError(ex, "Error al actualizar sede {SedeId}", sede.Id);
            throw new Exception("No se pudo guardar los cambios. Inténtalo nuevamente.");
        }
    }
}
