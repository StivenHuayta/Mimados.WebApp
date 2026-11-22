using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces
{
    public interface IServicioService
{
        Task<Servicio?> ObtenerServicioPorIdAsync(int id);
        Task<List<Servicio>> ObtenerServiciosAsync(string nombre,double precioMinimo,double precioMaximo,int duracionMinima,int duracionMaxima);
        Task<bool> CrearServicioAsync(Servicio servicio);
        Task<bool> ActualizarServicioAsync(Servicio servicio);
        //Task<bool> EliminarServicioAsync(int id);
    }
}
