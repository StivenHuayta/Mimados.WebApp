using System.Text.Json.Serialization;

namespace Mimados.WebApp.Models
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
        public string SedeNombre { get; set; } = string.Empty;
        public string NombreMascota { get; set; } = string.Empty;
        public string EspecieMascota { get; set; } = string.Empty;
        public string RazaMascota { get; set; } = string.Empty;
        public string SexoMascota { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string TelefonoCliente { get; set; } = string.Empty;
        public string NombreVeterinario { get; set; } = string.Empty;
        public string CodigoCmvp { get; set; } = string.Empty;
        public string ServicioSolicitado { get; set; } = string.Empty;
        public decimal MontoEstimado { get; set; } = 0.00m;
    }
}
