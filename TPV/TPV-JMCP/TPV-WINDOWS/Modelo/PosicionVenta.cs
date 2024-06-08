using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV_WINDOWS.Controlador;

namespace TPV_WINDOWS.Modelo
{
    public class PosicionVenta //No es necesario persistir en la BD. Pero sí que sea volcada al ticket
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
        private int _numPosicion;
        public int NumPosicion { get => _numPosicion; set => _numPosicion = value; }
        private List<LineaPantalla> _lineasPantalla;
        public List<LineaPantalla> LineasPantalla { get => _lineasPantalla; set => _lineasPantalla = value; }
        private int _tarifaUsada;
        public int TarifaUsada { get => _tarifaUsada; set => _tarifaUsada = value; }

        public PosicionVenta(int numPosicion, int tarifaUsada)
        {
            _id = _id = ControladorComun.BD!.SelectMAXInt("PosicionVenta", "_id") + 1;
            _numPosicion = numPosicion;
            _tarifaUsada = tarifaUsada;
            _lineasPantalla = new List<LineaPantalla>();
        }

        public PosicionVenta(int id, int numPosicion, int tarifaUsada)
        {
            _id = id;
            _numPosicion = numPosicion;
            _tarifaUsada = tarifaUsada;
            _lineasPantalla = new List<LineaPantalla>();
        }

        public void AddLineaPantalla(LineaPantalla linea)
        {
            _lineasPantalla.Add(linea);
        }
    }
}
