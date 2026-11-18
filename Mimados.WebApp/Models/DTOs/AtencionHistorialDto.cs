namespace Mimados.WebApp.Models;

public class AtencionHistorialDto
{
    public int AtencionId { get; set; }
    public int CitaId { get; set; }
    public DateTime FechaHoraRegistro { get; set; }
    public decimal? PesoActual { get; set; }
    public decimal? Temperatura { get; set; }
    public string? Sintomas { get; set; }
    public string? Diagnostico { get; set; }
    public string? TratamientoRecetado { get; set; }
    public string? Observaciones { get; set; }
    public decimal MontoTotal { get; set; }
    public string? ServicioRealizado { get; set; }
    public string NombreVeterinario { get; set; } = string.Empty;
    public string? CodigoCmvp { get; set; }
}
