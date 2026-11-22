using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces;

public interface IAtencionService
{
    Task<(bool Success, string Message)> RegistrarAsync(RegistrarAtencionDto request);
}
