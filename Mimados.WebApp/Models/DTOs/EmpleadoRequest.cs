using System.Text.Json.Serialization;

namespace Mimados.WebApp.Models;

public class EmpleadoRequest
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("dni")]
    public string Dni { get; set; } = string.Empty;

    [JsonPropertyName("nombres")]
    public string Nombres { get; set; } = string.Empty;

    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("telefono")]
    public string Telefono { get; set; } = string.Empty;

    [JsonPropertyName("sedeId")]
    public int SedeId { get; set; }

    [JsonPropertyName("codigoCmvp")]
    public string? CodigoCmvp { get; set; }

    [JsonPropertyName("esAdmin")]
    public bool EsAdmin { get; set; }

    [JsonPropertyName("activo")]
    public bool Activo { get; set; } = true;
}
