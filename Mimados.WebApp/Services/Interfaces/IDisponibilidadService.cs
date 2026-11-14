namespace Mimados.WebApp.Services.Interfaces;

public interface IDisponibilidadService
{
    Task<List<string>> ConsultarAsync(
        int empleadoId,
        DateTime fecha,
        int duracion);
}
