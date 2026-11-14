namespace Mimados.WebApp.Models;

public class AuthValidationResult
{
    public bool Exito { get; set; }
    public string? MensajeError { get; set; }
    public int UsuarioId { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty; // "Admin", "Veterinario", "Cliente"
    public bool EsAdmin { get; set; }
    public bool Activo { get; set; }
    public string? CodigoCmvp { get; set; }
    public int? SedeId { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }

    public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
}
