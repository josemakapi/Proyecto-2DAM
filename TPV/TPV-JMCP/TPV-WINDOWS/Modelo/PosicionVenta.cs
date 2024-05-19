using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class PosicionVenta
    {
        private int _numPosicion;
        public int NumPosicion { get { return _numPosicion; } }
        private List<LineaPantalla> _lineasPantalla;
        public List<LineaPantalla> LineasPantalla { get => _lineasPantalla; set => _lineasPantalla = value; }

    }
}
