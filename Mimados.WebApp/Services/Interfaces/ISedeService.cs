using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces
{
    public interface ISedeService
{
        Task<Sede?> ObtenerSedePorIdAsync(int id);
        Task<List<Sede>> ObtenerSedesAsync();
        Task<List<Sede>> ObtenerSedesConParamatrosAsync(string nombre,string direccion);
        Task<bool> CrearSedeAsync(Sede sede);
        Task<bool> ActualizarSedeAsync(Sede sede);
        //Task<bool> EliminarSedeAsync(int id);
    }
}
