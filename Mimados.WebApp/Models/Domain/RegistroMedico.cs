namespace Mimados.WebApp.Models;

public class RegistroMedico
{
    public string Fecha { get; set; } = string.Empty;
    public string Veterinario { get; set; } = string.Empty;
    public string Servicio { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public decimal Peso { get; set; }
}

