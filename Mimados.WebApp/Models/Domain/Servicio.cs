namespace Mimados.WebApp.Models
{
    public class Servicio
{
    private int _id;
    private string _nombre;
    private string _descripcion;
    private double _precioReferencial;
    private int _duracionMinutos;
    private Boolean _activo;
    private string _usuarioCreacion;
    private string _usuarioModificacion;
    private DateTime _fechaCreacion;
    private DateTime _fechaModificacion;

        public Servicio(int id, string nombre, string descripcion, double precioReferencial, int duracionMinutos, bool activo)
        {
            _id = id;
            _nombre = nombre;
            _descripcion = descripcion;
            _precioReferencial = precioReferencial;
            _duracionMinutos = duracionMinutos;
            _activo = activo;
        }
        public Servicio() { }
        public int Id { get => _id; set => _id = value; }
    public string Nombre { get => _nombre; set => _nombre = value; }
    public string Descripcion { get => _descripcion; set => _descripcion = value; }
    public double PrecioReferencial { get => _precioReferencial; set => _precioReferencial = value; }
    public int DuracionMinutos { get => _duracionMinutos; set => _duracionMinutos = value; }

    public string UsuarioCreacion { get => _usuarioCreacion; set => _usuarioCreacion = value; }
    public string UsuarioModificacion { get => _usuarioModificacion; set => _usuarioModificacion = value; }
    public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
    public DateTime FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }
    public bool Activo { get => _activo; set => _activo = value; }
}
}
