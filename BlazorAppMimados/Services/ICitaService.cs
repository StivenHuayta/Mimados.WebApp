
using Mimados_WebApp.Models;

namespace Mimados_WebApp.Services.CitaServices;

public interface ICitaService
{
    Task<List<Cita>> ListarPorMascotaAsync(int mascotaId);
    Task<Cita?> ObtenerPorIdAsync(int id);
    Task<bool> RegistrarAsync(Cita cita);
    Task<bool> CancelarAsync(int id);
    Task<bool> ModificarAsync(Cita cita);
}
