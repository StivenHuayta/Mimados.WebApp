namespace Mimados.WebApp.Services.Interfaces;
using Mimados.WebApp.Models;


    public interface IMascotaService
{

    Task<Mascota?> ObtenerMascotaPorIdAsync(int mascotaId);
    Task<List<Mascota>> ObtenerMascotasPorClienteAsync(int clienteId);
    Task<bool> CrearMascotaAsync(Mascota mascota);
    Task<bool> ActualizarMascotaAsync(Mascota mascota);
    Task<bool> EliminarMascotaAsync(int mascotaId);
    Task<List<RegistroMedico>> ObtenerHistorialAsync(int mascotaId);
    string ObtenerUrlPdfHistorial(int mascotaId);
}

