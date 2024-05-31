using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV_WINDOWS.Controlador;

namespace TPV_WINDOWS.Modelo
{
    public class Tarifa
    {
        private int _id;
        public int Id { get => _id;}
        private double _iva;
        public double Iva { get => _iva; set => _iva = value; }
        private int _codTienda;
        public int CodTienda { get => _codTienda; set => _codTienda = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        private string _descripcion;
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        private Dictionary<int, double>? _productos; //IdProducto, Precio
        public Dictionary<int, double> Productos { get => _productos!; set => _productos = value; }

        public Tarifa(string nombre, int codTienda,  double iva, string descripcion)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("Tarifa", "_id") + 1;
            this._iva = iva;
            this._codTienda = codTienda;
            this._nombre = nombre;
            this._descripcion = descripcion;
            this._productos = new Dictionary<int, double>();
        }
        public Tarifa(int id, string nombre, int codTienda, double iva, string descripcion)
        {
            this._id = id;
            this._iva = iva;
            this._codTienda = codTienda;
            this._nombre = nombre;
            this._descripcion = descripcion;
            this._productos = new Dictionary<int, double>();
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
