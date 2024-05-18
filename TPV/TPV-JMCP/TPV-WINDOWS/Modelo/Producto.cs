using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TPV_WINDOWS.Modelo
{
    public class Producto
    {
        private int _id;
        public int Id { get { return _id; } }
        private string _nombre;
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        private double _precio;
        public double Precio { get { return _precio; } set { _precio = value; } }
        private string? _descripcion;
        public string? Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        private BitmapImage? _imagen;
        public BitmapImage? Imagen { get { return _imagen; } set { _imagen = value; } }

        public Producto(int id, string nombre, double precio)
        {
            _id = id;
            _nombre = nombre;
            _precio = precio;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Precio: {Precio}, Descripcion: {Descripcion}";
        }
    }
}
