using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TPV_WINDOWS.Modelo
{
    public class Usuario
    {
        private int _id;
        private bool _esEncargado;
        private string _nombre;
        private BitmapImage? _avatar;
        private bool _esActivo;

        public bool EsEncargado { get { return _esEncargado; } set { _esEncargado = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public int ID { get { return _id; } }
        public bool EsActivo { get { return _esActivo; } set { _esActivo = value; } }
        public BitmapImage? Avatar { get { return _avatar; } set { _avatar = value; } }

        public Usuario(int id, bool esEncargado, string nombre, BitmapImage avatar)
        {
            _id = id;
            _esEncargado = esEncargado;
            _nombre = nombre;
            _avatar = avatar;
            _esActivo = true;
        }

        public Usuario(int id, bool esEncargado, string nombre)
        {
            _id = id;
            _esEncargado = esEncargado;
            _nombre = nombre;
            _esActivo = true;
        }
    }
}
