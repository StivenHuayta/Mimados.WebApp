namespace Mimados.WebApp.Models
{
    public class Atencion
    {
        public int AtencionId { get; set; }
        public int CitaId { get; set; }
        public int EmpleadoId { get; set; }
        public int MascotaId { get; set; }
        public DateTime FechaHoraRegistro { get; set; } = DateTime.Now;
        public decimal? PesoActual { get; set; }
        public decimal? Temperatura { get; set; }
        public string Sintomas { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public string TratamientoRecetado { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; } = 0.00m;

        // Datos complementarios para visualización
        public string NombreVeterinario { get; set; } = string.Empty;
        public string CodigoCmvp { get; set; } = string.Empty;
        public string SedeNombre { get; set; } = string.Empty;
        public string NombreMascota { get; set; } = string.Empty;
        public string ServicioRealizado { get; set; } = string.Empty;
    }
}
