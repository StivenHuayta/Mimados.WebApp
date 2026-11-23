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
        
        public string ObtenerUrlPdfReporteServivios(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            return new Uri(_http.BaseAddress!, $"ReporteRS/descargarServicioReporte?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}").ToString();
        }

        public async Task<byte[]?> DescargarReportePdfAsync(DateOnly? fechaInicio, DateOnly? fechaFin)
        {
            var query = new List<string>();
            if (fechaInicio.HasValue) query.Add($"fechaInicio={fechaInicio.Value:yyyy-MM-dd}");
            if (fechaFin.HasValue) query.Add($"fechaFin={fechaFin.Value:yyyy-MM-dd}");
            var qs = query.Count > 0 ? "?" + string.Join("&", query) : "";

            var response = await _http.GetAsync($"ReporteRS/descargarServicioReporte{qs}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadAsByteArrayAsync();
        }

    }
}
