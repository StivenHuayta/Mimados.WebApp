using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services
{
    public class ReporteService : IReporteService
    {
        private readonly HttpClient _http;

        public ReporteService(HttpClient http)
        {
            _http = http;
        }

         public async Task<List<ReporteServiciosDTO?>> ObtenerReporteServiciosPorFechas(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
    
            Console.
                WriteLine($"Obteniendo reporte de servicios desde {fechaInicio:yyyy-MM-dd} hasta {fechaFin}");
            var response = await _http.GetFromJsonAsync<ApiResponse<List<ReporteServiciosDTO>>>
                ($"ReporteRS/listarServiciosSolicitados?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}");

            return response.Data;
        }
    }
}
