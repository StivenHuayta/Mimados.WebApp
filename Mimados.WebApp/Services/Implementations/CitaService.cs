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

    public async Task<List<CitaListadoEmpleadoDTO>> ListarTodasAsync()
    {
        try
        {
            var respuesta = await _http.GetFromJsonAsync<
                ApiResponse<List<CitaListadoEmpleadoDTO>>>(
                    $"{PATH}/listar");

            if (respuesta?.Success != true)
            {
                throw new InvalidOperationException(
                    respuesta?.Message ?? "No se pudieron obtener las citas.");
            }

            return respuesta.Data ?? new List<CitaListadoEmpleadoDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar todas las citas");
            throw;
        }
    }

    public async Task<bool> RegistrarEmpleadoAsync(Cita cita, int sedeId, int sedeServicioId)
    {
        try
        {
            var datos = new
            {
                empleadoId = cita.EmpleadoId,
                mascotaId = cita.MascotaId,
                sedeId,
                sedeServicioId,
                fechaHoraInicio = cita.FechaHoraInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                fechaHoraFin = cita.FechaHoraFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                motivo = cita.Motivo
            };

            var response = await _http.PostAsJsonAsync($"{PATH}/registrarEmpleado", datos);
            var resultado = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

            if (!response.IsSuccessStatusCode || resultado?.Success != true)
                throw new InvalidOperationException(resultado?.Message ?? "No se pudo registrar la cita.");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al registrar cita del empleado para mascota {MascotaId}", cita.MascotaId);
            throw;
        }
    }

    public async Task<PageResultDto<CitaVeterinarioDto>> ListarPorVeterinarioAsync(
        int empleadoId, string? searchText, DateTime? fecha, string? estado,
        int pageNumber, int recordsPerPage)
    {
        try
        {
            var queryParams = new List<string>
            {
                $"pageNumber={pageNumber}",
                $"recordsPerPage={recordsPerPage}"
            };

            if (!string.IsNullOrWhiteSpace(searchText))
                queryParams.Add($"searchText={Uri.EscapeDataString(searchText.Trim())}");

            if (fecha.HasValue)
                queryParams.Add($"fecha={fecha.Value:yyyy-MM-dd}");

            if (!string.IsNullOrWhiteSpace(estado))
                queryParams.Add($"estado={Uri.EscapeDataString(estado.Trim())}");

            var url = $"{PATH}/veterinario/{empleadoId}?{string.Join("&", queryParams)}";
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<PageResultDto<CitaVeterinarioDto>>>(url);

            return respuesta?.Data ?? new PageResultDto<CitaVeterinarioDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al listar citas del veterinario {EmpleadoId}", empleadoId);
            return new PageResultDto<CitaVeterinarioDto>();
        }
    }

    public async Task<List<CitaReporteDto>> ListarParaReporteAsync(
    DateOnly? fechaInicio, DateOnly? fechaFin, string? estado)
{
    try
    {
        var queryParams = new List<string>();

        if (fechaInicio.HasValue)
            queryParams.Add($"fechaInicio={fechaInicio.Value:yyyy-MM-dd}");

        if (fechaFin.HasValue)
            queryParams.Add($"fechaFin={fechaFin.Value:yyyy-MM-dd}");

        if (!string.IsNullOrWhiteSpace(estado))
            queryParams.Add($"estado={Uri.EscapeDataString(estado.Trim())}");

        var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;
        var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<CitaReporteDto>>>($"{PATH}/reporte{queryString}");

        return respuesta?.Data ?? new List<CitaReporteDto>();
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error al listar citas para reporte");
        return new List<CitaReporteDto>();
    }
}

public async Task<byte[]?> DescargarReportePdfAsync(
    DateOnly? fechaInicio, DateOnly? fechaFin, string? estado)
{
    var queryParams = new List<string>();

    if (fechaInicio.HasValue)
        queryParams.Add($"fechaInicio={fechaInicio.Value:yyyy-MM-dd}");

    if (fechaFin.HasValue)
        queryParams.Add($"fechaFin={fechaFin.Value:yyyy-MM-dd}");

    if (!string.IsNullOrWhiteSpace(estado))
        queryParams.Add($"estado={Uri.EscapeDataString(estado.Trim())}");

    var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;
    var response = await _http.GetAsync($"{PATH}/reporte/pdf{queryString}");

    if (!response.IsSuccessStatusCode)
    {
        try
        {
            var errorApi = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            if (!string.IsNullOrWhiteSpace(errorApi?.Message))
                throw new InvalidOperationException(errorApi.Message);
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch
        {
        }

        var detalle = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"Error {(int)response.StatusCode} al generar el PDF: {detalle}");
    }

    return await response.Content.ReadAsByteArrayAsync();
}
}
