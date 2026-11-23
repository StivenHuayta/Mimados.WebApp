
using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces;

public interface ICitaService
{
    Task<List<Cita>> ListarPorMascotaAsync(int mascotaId);
    Task<Cita?> ObtenerPorIdAsync(int id);
    Task<bool> RegistrarAsync(Cita cita);
    Task<bool> CancelarAsync(int id);
    Task<bool> ModificarAsync(Cita cita);
    Task<List<CitaListadoEmpleadoDTO>> ListarTodasAsync();
    Task<bool> RegistrarEmpleadoAsync(Cita cita, int sedeId, int sedeServicioId);
    Task<PageResultDto<CitaVeterinarioDto>> ListarPorVeterinarioAsync(
        int empleadoId, string? searchText, DateTime? fecha, string? estado,
        int pageNumber, int recordsPerPage);
    Task<List<CitaReporteDto>> ListarParaReporteAsync(DateOnly? fechaInicio, DateOnly? fechaFin, string? estado);
    Task<byte[]?> DescargarReportePdfAsync(DateOnly? fechaInicio, DateOnly? fechaFin, string? estado);
}
