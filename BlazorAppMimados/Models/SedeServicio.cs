using Mimados_WebApp.Models;

namespace Mimados_WebApp.Models
{
    public class SedeServicio
{
    private int _id;
    private Sede _Sede;
    private Servicio _Servicio;
    private Boolean _activo;
    private string _usuarioCreacion;
    private string _usuarioModificacion;
    private DateTime _fechaCreacion;
    private DateTime _fechaModificacion;

        public int Id { get => _id; set => _id = value; }
        public Sede Sede { get => _Sede; set => _Sede = value; }
        public Servicio Servicio { get => _Servicio; set => _Servicio = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public string UsuarioCreacion { get => _usuarioCreacion; set => _usuarioCreacion = value; }
        public string UsuarioModificacion { get => _usuarioModificacion; set => _usuarioModificacion = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }

    }
}
