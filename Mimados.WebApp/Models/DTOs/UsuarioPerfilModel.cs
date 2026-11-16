using System.ComponentModel.DataAnnotations;

namespace Mimados.WebApp.Models
{
    public class UsuarioPerfilModel
    {
        
        public int Id { get; set; }
        public string? Dni { get; set; }
        public string? Email { get; set; }

        
        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; } = string.Empty;

       
        [Required(ErrorMessage = "El contacto de emergencia es obligatorio.")]
        public string ContactoEmergencia { get; set; } = string.Empty;
    }
}