using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services.Interfaces
{
    public interface ISedeServicioService
{
        Task<List<SedeServicioDTO>> ObtenerServiciosPorSedeAsync(int sedeid);

        Task<List<Servicio>> ObtenerServiciosDisponiblesAsync(int sedeid);

        Task<bool> CrearSedeServicioAsync(SedeServicio sedeServicio);



        Task<bool> EliminarSedeServicioAsync(int sedeServicioId);
    }
}
