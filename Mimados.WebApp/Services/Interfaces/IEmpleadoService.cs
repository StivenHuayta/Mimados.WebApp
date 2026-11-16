using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services;

public interface IEmpleadoService
{
    Task<List<Empleado>> ListarEmpleadosAsync();
    Task<bool> RegistrarEmpleadoAsync(Empleado empleado);
    Task<bool> ModificarEmpleadoAsync(Empleado empleado);
}   