namespace Mimados.WebApp.Models;

public class PacienteVeterinarioDto
{
    public int MascotaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string Raza { get; set; } = string.Empty;
    public string Sexo { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public double? PesoReferencial { get; set; }
    public int ClienteId { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string DniCliente { get; set; } = string.Empty;
    public string TelefonoCliente { get; set; } = string.Empty;
    public string? TelefonoEmergencia { get; set; }
    public string? DireccionCliente { get; set; }

    public int EdadAnios => FechaNacimiento.HasValue
        ? Math.Max(0, DateTime.Today.Year - FechaNacimiento.Value.Year -
            (DateTime.Today.DayOfYear < FechaNacimiento.Value.DayOfYear ? 1 : 0))
        : 0;
}
