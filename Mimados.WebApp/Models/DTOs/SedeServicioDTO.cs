namespace Mimados.WebApp.Models
{
    public class SedeServicioDTO
{
        private int _idSedeServicio;
        private int _idSede;
        private int _idServicio;
        private string _nombre;
        private string _descripcion;
        private decimal _precioReferencial;
        private int _duracionMinutos;
        private bool _activo;

        public int IdSedeServicio { get => _idSedeServicio; set => _idSedeServicio = value; }
        public int IdSede { get => _idSede; set => _idSede = value; }
        public int IdServicio { get => _idServicio; set => _idServicio = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public decimal PrecioReferencial { get => _precioReferencial; set => _precioReferencial = value; }
        public int DuracionMinutos { get => _duracionMinutos; set => _duracionMinutos = value; }
        public bool Activo { get => _activo; set => _activo = value; }
    }
}
