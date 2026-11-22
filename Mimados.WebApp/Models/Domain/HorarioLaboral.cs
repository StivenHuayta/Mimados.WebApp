namespace Mimados.WebApp.Models;

public class HorarioLaboral
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; }
    public string DiaSemana { get; set; } = string.Empty;
    public string HoraInicio { get; set; } = string.Empty;
    public string HoraFin { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}