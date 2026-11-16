using System.Text.Json.Serialization;

namespace Mimados.WebApp.Models;

public class HorarioRequest
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("empleadoId")]
    public int EmpleadoId { get; set; }

    [JsonPropertyName("diaSemana")]
    public string DiaSemana { get; set; } = string.Empty;

    [JsonPropertyName("horaInicio")]
    public string HoraInicio { get; set; } = string.Empty;

    [JsonPropertyName("horaFin")]
    public string HoraFin { get; set; } = string.Empty;

    [JsonPropertyName("activo")]
    public bool Activo { get; set; } = true;
}
