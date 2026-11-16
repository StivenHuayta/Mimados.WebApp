
using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces;

public interface ICitaService
{
    Task<List<Cita>> ListarPorMascotaAsync(int mascotaId);
    Task<Cita?> ObtenerPorIdAsync(int id);
    Task<bool> RegistrarAsync(Cita cita);
    Task<bool> CancelarAsync(int id);
    Task<bool> ModificarAsync(Cita cita);
}
