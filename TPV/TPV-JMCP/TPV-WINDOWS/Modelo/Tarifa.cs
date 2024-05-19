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
        public double Iva { get => _iva; set => _iva = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        private string _descripcion;
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        private Dictionary<int, double>? _productos; //IdProducto, Precio
        public Dictionary<int, double> Productos { get => _productos!; set => _productos = value; }

        public Tarifa(int id, string nombre, double iva, string descripcion)
        {
            _id = id;
            _iva = iva;
            _nombre = nombre;
            _descripcion = descripcion;
        }

        public void AnadirProducto(int idProducto, double precio)
        {
            _productos!.Add(idProducto, precio);
        }
        public void EliminarProducto(int idProducto)
        {
            _productos!.Remove(idProducto);
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Iva: {Iva}, Descripcion: {Descripcion}";
        }


    }
}
