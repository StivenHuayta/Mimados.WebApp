namespace Mimados.WebApp.Models;

// Datos del veterinario que devuelve Java
public class VeterinarioResumenDto
{
    public int EmpleadoId { get; set; }
    public string Nombre { get; set; } = "";
    public int SedeId { get; set; }
    public string Sede { get; set; } = "";
    public string Puesto { get; set; } = "";
}
