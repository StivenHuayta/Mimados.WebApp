namespace Mimados.WebApp.Models;

public class Mascota
{
    public int MascotaId { get; set; }
    public int ClienteId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especie { get; set; } = "CANINO"; // CANINO, FELINO, AVE, CONEJO, OTRO
    public string Raza { get; set; } = string.Empty;
    public string Sexo { get; set; } = "MACHO"; // MACHO, HEMBRA
    public DateTime? FechaNacimiento { get; set; }
    public decimal? PesoReferencial { get; set; }
    public bool Activo { get; set; } = true;

    // Datos complementarios del cliente / propietario
    public string NombreCliente { get; set; } = string.Empty;
    public string DniCliente { get; set; } = string.Empty;
    public string TelefonoCliente { get; set; } = string.Empty;
    public string TelefonoEmergencia { get; set; } = string.Empty;
    public string DireccionCliente { get; set; } = string.Empty;
    public string Alergias { get; set; } = "Ninguna conocida";

    public int EdadAnios
    {
        get
        {
            if (!FechaNacimiento.HasValue) return 0;
            var anios = DateTime.Today.Year - FechaNacimiento.Value.Year;
            if (FechaNacimiento.Value.Date > DateTime.Today.AddYears(-anios)) anios--;
            return Math.Max(0, anios);
        }
    }
}

