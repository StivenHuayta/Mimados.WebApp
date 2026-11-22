namespace Mimados.WebApp.Models;

public class CitaReporteDto
{
    public int CitaId { get; set; }
    public string? FechaHoraInicio { get; set; }
    public string? FechaHoraFin { get; set; }
    public string NombreSede { get; set; } = string.Empty;
    public string NombreMascota { get; set; } = string.Empty;
    public string NombreVeterinario { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string? Motivo { get; set; }
    public string Estado { get; set; } = string.Empty;
}
