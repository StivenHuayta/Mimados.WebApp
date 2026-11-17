
namespace Mimados.WebApp.Models
{
    public class CitaListadoEmpleadoDTO
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public int MascotaId { get; set; }

        public string NombreCliente { get; set; } = "";
        public string NombreMascota { get; set; } = "";
        public string NombreServicio { get; set; } = "";

        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }

        public string Estado { get; set; } = "";
        public string Motivo { get; set; } = "";
    }
}
