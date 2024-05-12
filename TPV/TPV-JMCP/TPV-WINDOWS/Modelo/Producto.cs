using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class Producto
    {
        private int _id;
        public int Id { get { return _id; } }
        private string _nombre;
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
    }
}
