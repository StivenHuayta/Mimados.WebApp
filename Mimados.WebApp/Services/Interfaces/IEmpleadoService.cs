using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services;

public interface IEmpleadoService
{
    Task<List<Empleado>> ListarEmpleadosAsync();
    Task<(bool Found, Empleado? Usuario, string Message)> BuscarUsuarioPorDniAsync(string dni);
    Task<(bool Success, string Message)> RegistrarEmpleadoAsync(Empleado empleado);
    Task<bool> ModificarEmpleadoAsync(Empleado empleado);
}   