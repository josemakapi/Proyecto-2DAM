using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV_WINDOWS.Controlador;

namespace TPV_WINDOWS.Modelo
{
    public class LineaPantalla
    {
        private int _numLinea;
        public int NumLinea { get { return _numLinea; } }
        private Producto _producto;
        public Producto Producto { get => _producto; set => _producto = value; }
        private double _precio;
        public double Precio { get => _precio; set => _precio = value; } //Aunque lo tendrá Producto, permitirá en el futuro cambiar el precio desde el grid de ventas manualmente
        private int _tarifaUsada;
        public int TarifaUsada { get => _tarifaUsada; set => _tarifaUsada = value; }


        public int UltimoNumLinea() //para NumLinea en el constructor
        {
            int maxNumLinea = 0;
            foreach (LineaPantalla linea in ControladorComun.TpvBase!.PosicionVentaActual!.LineasPantalla)
            {
                if (linea.NumLinea > maxNumLinea)
                {
                    maxNumLinea = linea.NumLinea;
                }
            }
            return maxNumLinea;
        }
    }
}
