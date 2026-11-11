
using System.Net.Http.Json;
using BlazorAppMimados.Models;
using Mimados_WebApp.Models;
using Mimados_WebApp.Services.CitaServices;

namespace Mimados_WebApp.Services;



public class CitaService : ICitaService
{
    private readonly HttpClient _http;

    public CitaService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Cita>> ListarPorMascotaAsync(int mascotaId)
    {
        var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Cita>>>(
            $"CitaRS/listarPorMascota/{mascotaId}");

        return respuesta?.Data ?? new List<Cita>();
    }

    public async Task<Cita?> ObtenerPorIdAsync(int id)
    {
        var respuesta = await _http.GetFromJsonAsync<ApiResponse<Cita>>(
            $"CitaRS/{id}");

        return respuesta?.Data;
    }

    public async Task<bool> RegistrarAsync(Cita cita)
    {
        var datos = new
        {
            empleadoId = cita.EmpleadoId,
            mascotaId = cita.MascotaId,
            fechaHoraInicio = cita.FechaHoraInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
            fechaHoraFin = cita.FechaHoraFin.ToString("yyyy-MM-ddTHH:mm:ss"),
            motivo = cita.Motivo
        };

        var response = await _http.PostAsJsonAsync("CitaRS/registrar", datos);

        if (!response.IsSuccessStatusCode)
            return false;

        var resultado = await response.Content
            .ReadFromJsonAsync<ApiResponse<object>>();

        return resultado?.Success == true;
    }

    public async Task<bool> CancelarAsync(int id)
    {
        var response = await _http.DeleteAsync($"CitaRS/{id}");

        if (!response.IsSuccessStatusCode)
            return false;

        var resultado = await response.Content
            .ReadFromJsonAsync<ApiResponse<object>>();

        return resultado?.Success == true;
    }


    public async Task<bool> ModificarAsync(Cita cita)
    {
        var datos = new
        {
            empleadoId = cita.EmpleadoId,
            fechaHoraInicio = cita.FechaHoraInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
            fechaHoraFin = cita.FechaHoraFin.ToString("yyyy-MM-ddTHH:mm:ss"),
            motivo = cita.Motivo
        };

        var response = await _http.PutAsJsonAsync(
            $"CitaRS/{cita.CitaId}", datos);

        var resultado = await response.Content
            .ReadFromJsonAsync<ApiResponse<object>>();

        if (!response.IsSuccessStatusCode || resultado?.Success != true)
        {
            throw new Exception(
                resultado?.Message ?? "No se pudo modificar la cita.");
        }

        return true;
    }

}
