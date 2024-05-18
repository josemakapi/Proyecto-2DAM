using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class LineaPantalla
    {
        private int _numLinea;
        public int NumLinea { get { return _numLinea; } }
        private Producto _producto;
        public Producto Producto { get => _producto; set => _producto = value; }
        private double _precio;
        public double Precio { get => _precio; set => _precio = value; }

    }
}
