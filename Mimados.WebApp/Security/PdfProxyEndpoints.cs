using Mimados.WebApp.Services;
using Mimados.WebApp.Services.Interfaces;

namespace Mimados.WebApp.Security;

public static class PdfProxyEndpoints
{
    public static IEndpointRouteBuilder MapPdfEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/pdf/mascotas/{id:int}/historial", async (int id, IMascotaService mascotaService) =>
        {
            var pdfBytes = await mascotaService.DescargarPdfHistorialAsync(id);
            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return Results.Problem("No se pudo generar el reporte PDF del historial.", statusCode: 502);
            }
            return Results.File(pdfBytes, "application/pdf", $"Historial_Mascota_{id}.pdf");
        }).RequireAuthorization();

        endpoints.MapGet("/api/pdf/reportes/citas", async (
            string? fechaInicio,
            string? fechaFin,
            string? estado,
            ICitaService citaService) =>
        {
            DateOnly? fi = DateOnly.TryParse(fechaInicio, out var d1) ? d1 : null;
            DateOnly? ff = DateOnly.TryParse(fechaFin, out var d2) ? d2 : null;

            var pdfBytes = await citaService.DescargarReportePdfAsync(fi, ff, estado);
            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return Results.Problem("No se pudo generar el reporte PDF de citas.", statusCode: 502);
            }
            return Results.File(pdfBytes, "application/pdf", "ReporteCitas.pdf");
        }).RequireAuthorization();

        endpoints.MapGet("/api/pdf/reportes/servicios", async (
            string? fechaInicio,
            string? fechaFin,
            IReporteService reporteService) =>
        {
            DateOnly? fi = DateOnly.TryParse(fechaInicio, out var d1) ? d1 : null;
            DateOnly? ff = DateOnly.TryParse(fechaFin, out var d2) ? d2 : null;

            var pdfBytes = await reporteService.DescargarReportePdfAsync(fi, ff);
            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return Results.Problem("No se pudo generar el reporte PDF de servicios.", statusCode: 502);
            }
            return Results.File(pdfBytes, "application/pdf", "ReporteServicios.pdf");
        }).RequireAuthorization();

        return endpoints;
    }
}
