namespace Mimados.WebApp.Models
{
    public class ReporteServiciosDTO
    {
        private int _orden;
        private string _nombre;
        private int _cantidad;
        private double _precio;
        private double _total;
        private double _proporcion;

        public int Orden { get => _orden; set => _orden = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public double Precio { get => _precio; set => _precio = value; }
        public double Total { get => _total; set => _total = value; }
        public double Proporcion { get => _proporcion; set => _proporcion = value; }
    }
}
