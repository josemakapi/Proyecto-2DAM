using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class PosicionVenta //No es necesario persistir en la BD. Pero sí que sea volcada al ticket
    {
        private int _numPosicion;
        public int NumPosicion { get { return _numPosicion; } }
        private List<LineaPantalla> _lineasPantalla;
        public List<LineaPantalla> LineasPantalla { get => _lineasPantalla; set => _lineasPantalla = value; }
        private int _tarifaUsada;
        public int TarifaUsada { get => _tarifaUsada; set => _tarifaUsada = value; }
    }
}
