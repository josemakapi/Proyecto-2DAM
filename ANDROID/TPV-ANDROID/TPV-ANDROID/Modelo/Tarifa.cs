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
        public int Id { get => _id; set => _id = value; }
        private int _codTarifa;
        public int CodTarifa { get => _codTarifa; set => _codTarifa = value; }
        private double _iva;
        public double Iva { get => _iva; set => _iva = value; }
        private int _codTienda;
        public int CodTienda { get => _codTienda; set => _codTienda = value; }
        private string _nombre;
        public string Nombre { get => _nombre; set => _nombre = value; }
        private string _descripcion;
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        private List<Dictionary<int,double>> _listaProductos; //IdProducto, Precio
        public List<Dictionary<int, double>> ListaProductos { get => _listaProductos; set => _listaProductos = value; }

        public Tarifa(int codTarifa, string nombre, int codTienda,  double iva, string descripcion)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("Tarifa", "_id") + 1;
            this._codTarifa = codTarifa;
            this._iva = iva;
            this._codTienda = codTienda;
            this._nombre = nombre;
            this._descripcion = descripcion;
            this._listaProductos = new List<Dictionary<int, double>>();
        }
        public Tarifa(int id, int codTarifa, string nombre, int codTienda, double iva, string descripcion)
        {
            this._id = id;
            this._codTarifa = codTarifa;
            this._iva = iva;
            this._codTienda = codTienda;
            this._nombre = nombre;
            this._descripcion = descripcion;
            this._listaProductos = new List<Dictionary<int, double>>();
        }


        public void AnadirProducto(Producto idProducto, double precio, bool persistencia)
        {
            _listaProductos!.Add(new Dictionary<int, double>() { { idProducto.Id, precio } });
            if (persistencia)
            {
                ControladorComun.BD!.ActualizarObjeto(this);
            }
        }
        public void AnadirProducto(Producto producto, double precio)
        {
            _listaProductos!.Add(producto.Id, precio);
        }
        public void AnadirProducto(IDictionary() productos)
        {
            _listaProductos=productos;
        }

        public bool EliminarProducto(int idProducto)
        {
            return _listaProductos!.Remove(ControladorComun.BD!.BuscarObjetosInt<Producto>("CodProducto", idProducto)[0]);
        }
        public bool EliminarProducto(Producto producto)
        {
            return _listaProductos!.Remove(producto);
        }

    }
}
