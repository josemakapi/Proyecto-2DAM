using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class Tarifa
    {
        private int _id;
        public int Id { get => _id;}
        private double _iva;
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        public double Iva { get => _iva; set => _iva = value; }
        private Dictionary<int, double> _productos; //IdProducto, Precio
        public Dictionary<int, double> Productos { get => _productos; set => _productos = value; }

        public Tarifa(int id, string nombre, double iva)
        {
            _id = id;
            _iva = iva;
            _nombre = nombre;
            _productos = new Dictionary<int, double>();
        }

    }
}
