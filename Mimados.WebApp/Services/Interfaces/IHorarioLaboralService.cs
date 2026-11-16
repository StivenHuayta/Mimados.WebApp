using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services;

public interface IHorarioLaboralService
{
    Task<List<HorarioLaboral>> ListarPorEmpleadoAsync(int empleadoId);
    Task<bool> RegistrarHorarioAsync(HorarioLaboral horario);
    Task<bool> ModificarHorarioAsync(HorarioLaboral horario);
    Task<bool> EliminarHorarioAsync(int horarioId);
}