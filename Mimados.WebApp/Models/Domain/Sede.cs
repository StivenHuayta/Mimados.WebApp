namespace Mimados.WebApp.Models
{
    public class Sede
{
    private int _id;
    private string _nombre;
    private string _direccion;
    private string _telefono;
    private Boolean _activo;
    private string _usuarioCreacion;
    private string _usuarioModificacion;
    private DateTime _fechaCreacion;
    private DateTime _fechaModificacion;

        public Sede()
        {
            
        }

        public Sede(int id, string nombre, string direccion, string telefono, bool activo)
        {
            _id = id;
            _nombre = nombre;
            _direccion = direccion;
            _telefono = telefono;
            _activo = activo;
        }


        public int Id { get => _id; set => _id = value; }
    public string Nombre { get => _nombre; set => _nombre = value; }
    public string Direccion { get => _direccion; set => _direccion = value; }
    public string Telefono { get => _telefono; set => _telefono = value; }

    public string UsuarioCreacion { get => _usuarioCreacion; set => _usuarioCreacion = value; }
    public string UsuarioModificacion { get => _usuarioModificacion; set => _usuarioModificacion = value; }
    public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
    public DateTime FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
    public bool Activo { get => _activo; set => _activo = value; }
}
}
