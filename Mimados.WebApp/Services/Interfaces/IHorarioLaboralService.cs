using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces;

public interface IHorarioLaboralService
{
    Task<ApiResponse<object>?> RegistrarHorarioAsync(HorarioRequest request);
    Task<ApiResponse<List<HorarioRequest>>?> ListarPorEmpleadoAsync(int idEmpleado);
    Task<ApiResponse<object>?> EliminarHorarioAsync(int idHorario);
}
