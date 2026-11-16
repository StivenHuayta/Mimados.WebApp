namespace Mimados.WebApp.Models;

public class Empleado
{
    public int Id { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int SedeId { get; set; } = 1;
    public string? CodigoCmvp { get; set; }
    public bool EsAdmin { get; set; }
    public bool Activo { get; set; } = true;
}