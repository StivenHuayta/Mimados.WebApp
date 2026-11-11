using System.Text.Json.Serialization;
namespace Mimados_WebApp.Models
{
    public class Cita
    {
        [JsonPropertyName("id")]
        public int CitaId { get; set; }
        public int EmpleadoId { get; set; }
        public int MascotaId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public string Estado { get; set; } = "PROGRAMADA"; // PROGRAMADA, CONFIRMADA, CANCELADA, ATENDIDA, NO_ASISTIO
        public string Motivo { get; set; } = string.Empty;

        // Datos de presentación y navegación
        public string SedeNombre { get; set; } = "Sede Central";
        public string NombreMascota { get; set; } = string.Empty;
        public string EspecieMascota { get; set; } = string.Empty;
        public string RazaMascota { get; set; } = string.Empty;
        public string SexoMascota { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string TelefonoCliente { get; set; } = string.Empty;
        public string NombreVeterinario { get; set; } = "Dr. Carlos Mendoza";
        public string CodigoCmvp { get; set; } = "CMVP 08421";
        public string ServicioSolicitado { get; set; } = "Consulta General";
        public decimal MontoEstimado { get; set; } = 60.00m;
    }
}

