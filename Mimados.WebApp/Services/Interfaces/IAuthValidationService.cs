using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces;

public interface IAuthValidationService
{
    Task<AuthValidationResult> ValidarCredencialesAsync(string usuarioOCorreo, string contrasena);
    Task<(bool Exito, string? Mensaje)> RegistrarClienteAsync(RegistroClienteRequest model);
}
