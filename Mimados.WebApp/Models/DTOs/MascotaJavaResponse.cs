namespace Mimados.WebApp.Models;

public class MascotaJavaResponse
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Especie { get; set; }
    public string? Raza { get; set; }
    public string? Sexo { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public decimal? PesoReferencial { get; set; }
    public bool Activo { get; set; }
    public ClienteJavaResponse? Cliente { get; set; }
}

public class ClienteJavaResponse
{
    public UsuarioJavaResponse? Usuario { get; set; }
}

public class UsuarioJavaResponse
{
    public int Id { get; set; }
}
