using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces;

public interface IEmpleadoService
{
    Task<ApiResponse<object>?> RegistrarEmpleadoAsync(EmpleadoRequest request);
    Task<ApiResponse<object>?> ModificarEmpleadoAsync(EmpleadoRequest request);
    Task<ApiResponse<List<EmpleadoRequest>>?> ListarEmpleadosAsync();
}
