namespace Mimados.WebApp.Models;

public class CitaVeterinarioDto
{
    public int CitaId { get; set; }
    public int MascotaId { get; set; }
    public string NombreMascota { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public int ClienteId { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string? Motivo { get; set; }
}
