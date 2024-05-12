using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class Cliente
    {
        private int _id;
        public int Id { get { return _id; } }
        private string _nombre;
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        private string _dni;
        public string Dni { get { return _dni; } }

        public Cliente(int id, string nombre, string dni)
        {
            _id = id;
            _nombre = nombre;
            _dni = dni;
        }

    }
}
