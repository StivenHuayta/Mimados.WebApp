using System.Text.Json.Serialization;

namespace Mimados.WebApp.Models;

/// <summary>
/// Modelo que mapea la respuesta del servicio REST de Java (mimados-model: AuthResultado).
/// Basado en el documento "Creación de Servicios Web RESTFul con Jakarta REST" (Sección 9.1).
/// </summary>
public class AuthResultado
{
    private int _usuarioId;
    private string _dni = string.Empty;
    private string _nombres = string.Empty;
    private string _apellidos = string.Empty;
    private string _email = string.Empty;
    private string _rol = string.Empty;
    private bool _esAdmin;
    private bool _activo;
    private string? _codigoCmvp;
    private int? _sedeId;

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get => _usuarioId; set => _usuarioId = value; }

    [JsonPropertyName("dni")]
    public string Dni { get => _dni; set => _dni = value; }

    [JsonPropertyName("nombres")]
    public string Nombres { get => _nombres; set => _nombres = value; }

    [JsonPropertyName("apellidos")]
    public string Apellidos { get => _apellidos; set => _apellidos = value; }

    [JsonPropertyName("email")]
    public string Email { get => _email; set => _email = value; }

    [JsonPropertyName("rol")]
    public string Rol { get => _rol; set => _rol = value; }

    [JsonPropertyName("esAdmin")]
    public bool EsAdmin { get => _esAdmin; set => _esAdmin = value; }

    [JsonPropertyName("activo")]
    public bool Activo { get => _activo; set => _activo = value; }

    [JsonPropertyName("codigoCmvp")]
    public string? CodigoCmvp { get => _codigoCmvp; set => _codigoCmvp = value; }

    [JsonPropertyName("sedeId")]
    public int? SedeId { get => _sedeId; set => _sedeId = value; }

    public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
}

/// <summary>
/// DTO para la petición de login enviada al servicio REST de Java.
/// </summary>
public class LoginRequest
{
    private string _email = string.Empty;
    private string _password = string.Empty;

    public LoginRequest() { }

    public LoginRequest(string email, string password)
    {
        _email = email;
        _password = password;
    }

    [JsonPropertyName("email")]
    public string Email { get => _email; set => _email = value; }

    [JsonPropertyName("password")]
    public string Password { get => _password; set => _password = value; }
}

/// <summary>
/// DTO para la petición de creación de nueva cuenta de cliente.
/// </summary>
public class RegistroClienteRequest
{
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

    [JsonPropertyName("direccion")]
    public string Direccion { get; set; } = string.Empty;

    [JsonPropertyName("contactoEmergencia")]
    public string? ContactoEmergencia { get; set; }
}

/// <summary>
/// Contenedor de la respuesta estándar del servicio REST UsuarioRS.
/// </summary>
public class UsuarioApiResponse
{
    private bool _success;
    private string? _message;
    private AuthResultado? _data;

    [JsonPropertyName("success")]
    public bool Success { get => _success; set => _success = value; }

    [JsonPropertyName("message")]
    public string? Message { get => _message; set => _message = value; }

    [JsonPropertyName("data")]
    public AuthResultado? Data { get => _data; set => _data = value; }
}
