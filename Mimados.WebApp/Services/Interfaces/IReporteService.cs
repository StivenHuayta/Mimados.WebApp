using Mimados.WebApp.Models;
using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services
{
    public interface IReporteService
    {
        Task<List<ReporteServiciosDTO?>> ObtenerReporteServiciosPorFechas(DateOnly? fechaInicio, DateOnly? fechaFin);
    }
}
