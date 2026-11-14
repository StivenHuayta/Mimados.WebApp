using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces;

public interface IVeterinarioService
{
    Task<List<VeterinarioResumenDto>> ListarAsync();
}
