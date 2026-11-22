namespace Mimados.WebApp.Models;

public class RegistrarAtencionDto
{
    public int? CitaId { get; set; }
    public int? MascotaId { get; set; }
    public int EmpleadoId { get; set; }
    public string? Motivo { get; set; }
    public double? PesoActual { get; set; }
    public double? Temperatura { get; set; }
    public string? Sintomas { get; set; }
    public string? Diagnostico { get; set; }
    public string? TratamientoRecetado { get; set; }
    public string? Observaciones { get; set; }
    public decimal MontoTotal { get; set; }
}
