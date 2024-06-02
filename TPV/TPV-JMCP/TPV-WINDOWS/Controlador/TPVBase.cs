using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TPV_WINDOWS.Datos;
using TPV_WINDOWS.Modelo;
using TPV_WINDOWS.Vista;
namespace TPV_WINDOWS.Controlador
{
    /// <summary>
    /// Contiene la lógica de los aspectos específicos del TPV y su uso
    /// </summary>
    public class TPVBase
    {
        private int _numTPV = 1; //Esto se define a mano desde aquí. Determina el número de TPV inicial si no se ha definido
        private int _numerosTeclado;
        public int NumerosTeclado { get => _numerosTeclado; set => _numerosTeclado = value; }
        private List<Usuario> _listaUsuarios;
        public List<Usuario> ListaUsuarios { get => _listaUsuarios; set => _listaUsuarios = value; }
        private Usuario? _usuarioActual;
        public Usuario? UsuarioActual { get => _usuarioActual; set => _usuarioActual = value; }
        private TPV _tpvCFG = null!;
        private TPVMaster _procesoMaster = null!;
        //private VentanaTecladoNumericoUsuario _tecladoClave = new VentanaTecladoNumericoUsuario(true);
        //private VentanaTecladoNumericoUsuario _tecladoNumeros = new VentanaTecladoNumericoUsuario(false);
        private Tarifa? _tarifaActual;
        private PosicionVenta? _posicionVentaActual;
        public PosicionVenta? PosicionVentaActual { get => _posicionVentaActual; set => _posicionVentaActual = value; }
        private VentanaPrincipal? _ventanaPrincipal;
        private List<Seccion>? _secciones;
        public List<Seccion>? Secciones { get => _secciones; set => _secciones = value; }
        private List<Producto>? _productos;

        /// <summary>
        /// Método que coordina las comprobaciones y operaciones de inicio del TPV
        /// </summary>
        public void InicioTPV()
        {
            CargarCfgTPV();
            CargarProductos();
            CargarTarifas();   
            CargarSecciones();
            CargarUsuarioPredeterminado();
            CompruebaCierreCaja();
            CompruebaAperturaCaja();
            if (_tpvCFG.IsTPVMaster)
            {
                Task.Run(() =>
                {
                    _procesoMaster = new TPVMaster();
                    _procesoMaster.Iniciar();
                });
            }
            
            
            _ventanaPrincipal = new VentanaPrincipal();
            BloqueaTPV();
            _ventanaPrincipal.Show();
        }

        private bool CompruebaAperturaCaja()
        {
            return true; 
        }

        private void CargarCfgTPV()
        {
            if (ControladorComun.BD!.ContarObjetos<TPV>() < 1)
            {
                this._tpvCFG = new TPV(0,1,true,0,0);
                ControladorComun.BD!.PersistirObjeto(_tpvCFG);
            }
            else
            {
                this._tpvCFG = ControladorComun.BD!.BuscarObjetosIntAndInt<TPV>("CodTienda",ControladorComun.TiendaActual!.CodTienda,"NumTPV", _numTPV)[0];
            }
        }

        private void CargarUsuarioPredeterminado()
        {
            if (ControladorComun.BD!.ContarObjetos<Usuario>() < 1)
            {
                ControladorComun.BD!.PersistirObjeto(new Usuario(0,"7777",0,true,"admin",new BitmapImage(new Uri("C:\\Proyecto 2DAM\\TPV\\TPV-JMCP\\TPV-WINDOWS\\Recursos\\Imagenes\\profile.png"))));
                ControladorComun.BD!.PersistirObjeto(new Usuario(1, "1111", 0, false, "user", new BitmapImage(new Uri("C:\\Proyecto 2DAM\\TPV\\TPV-JMCP\\TPV-WINDOWS\\Recursos\\Imagenes\\pngegg48_.png"))));
            }
        }

        public void CargarTarifas()
        {
            if (ControladorComun.BD!.ContarObjetos<Tarifa>() < 1)
            {
                this._tarifaActual = new Tarifa(0,"Tarifa Base PVP", 0, 21, "PVP sin IVA incluido");
                this._tarifaActual.AnadirProducto(0, 600);
                ControladorComun.BD!.PersistirObjeto(_tarifaActual);
            }
            else
            {
                this._tarifaActual = ControladorComun.BD!.BuscarObjetosIntAndInt<Tarifa>("_id", _tpvCFG.TarifaDefecto, "CodTienda", _tpvCFG.CodTienda)[0];
            }
        }

        private void CargarSecciones()
        {
            if (ControladorComun.BD!.ContarObjetos<Seccion>() < 1)
            {
                _secciones = new List<Seccion>();
                _secciones.Add(new Seccion(0, 0, "Auriculares", 0));
                AgregarProductoASeccion(_productos[0], 0);
                ControladorComun.BD!.PersistirObjeto(_secciones[0]);
                
                ControladorComun.BD!.PersistirObjeto(new Seccion(1,1, "DACs", 0));
            }
            _secciones = ControladorComun.BD!.BuscarObjetosInt<Seccion>("CodTienda", _tpvCFG.CodTienda).ToList();
        }



        private void CargarProductos()
        {
            if (ControladorComun.BD!.ContarObjetos<Producto>() < 1)
            {
                ControladorComun.BD!.PersistirObjeto(new Producto(0,0,"Stax SR-507","Auricular electrostático", _tpvCFG.CodTienda,new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}Recursos\\Imagenes\\sr507.png"))));   
            }
            _productos = ControladorComun.BD!.BuscarObjetosInt<Producto>("CodTienda", _tpvCFG.CodTienda).ToList();
        }

        public bool GeneraTicket()
        {
            return true;
        }

        public bool FinTPV()
        {
            if (this._tpvCFG.IsTPVMaster && this._procesoMaster != null)
            {
                if (!_procesoMaster!.IsRunning)
                {
                    return false;
                }
                else
                {
                    this._procesoMaster.Parar();
                }
            }
            return false;
        }

        public void BloqueaTPV()
        {
            new VentanaTecladoNumericoUsuario(true).ShowDialog();
            //_usuarioActual = ControladorComun.BD!.BuscarObjetosIntAndInt<Usuario>("Clave", _numerosTeclado, "CodTienda", _tpvCFG.CodTienda)[0]; //kk
            _ventanaPrincipal!.ActualizaInfoUsuario();
        }

        public bool CompruebaCierreCaja()
        {
            return true;
        }

        public void InsertarProductoTest()
        {     
            //public Ticket(string numTicket, int numTPV, int ejercicio, string tienda)
            //Ticket t1 = new Ticket('T', 1, 2024, "AudioCuenca00");
            
            //Producto prod1 = new Producto(1,"T037", 10);
            //Producto prod2 = new Producto("T098", 20);
            //Producto prod3 = new Producto("D87435", 30);
            //_tarifaActual.AnadirProducto(1, 200);
            //prod3.Descripcion = "Descripción de prueba";
            //ControladorComun.BD!.PersistirObjeto<Producto>(prod1);
            //ControladorComun.BD!.PersistirObjeto<Producto>(prod2);
            //ControladorComun.BD!.PersistirObjeto<Producto>(prod3);
            //ControladorComun.BD!.PersistirObjeto<Ticket>(t1);
            //ControladorComun.BD!.PersistirObjeto<Tarifa>(_tarifaActual);
            //string maxID= ControladorComun.BD!.SelectMAXTicketT(2024,1);
            //MessageBox.Show("Hemos insertado 3 productos. El ID máximo es: " + maxID);
        }
        public bool ExistenTicketCerrados()
        {
            List<Ticket> ticketsCerrados = ControladorComun.BD!.BuscarObjetosBool<Ticket>("Cerrado", true).ToList();
            return ticketsCerrados.Any();
        }

        public bool CompruebaClave(string clave)
        {
            List<Usuario> usuarios = ControladorComun.BD!.BuscarObjetosStringAndInt<Usuario>("Clave", clave, "CodTienda",_tpvCFG.CodTienda).ToList();
            if (usuarios.Count > 0)
            {
                return usuarios.Any(x => x.EsActivo);
            }
            return false;
        }

        public string ElegirImagenAvatar()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Selecciona imagen de avatar";

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return string.Empty;
        }

        public BitmapImage? CargarImagen(string rutaImagen) //Para que el usuario suba su avatar
        {
            if (!string.IsNullOrEmpty(rutaImagen))
            {
                BitmapImage imagen = new BitmapImage();
                imagen.BeginInit();
                imagen.UriSource = new Uri(rutaImagen);
                imagen.EndInit();
                return imagen;
            }
            return null;
        }
        public void AgregarProductoASeccion(Producto producto, int CodSeccion)
        {
            Seccion seccion = _secciones?.FirstOrDefault(s => s.CodSeccion == CodSeccion);
            if (seccion != null)
            {
                seccion.AddProducto(producto);
                ControladorComun.BD!.PersistirObjeto(seccion);
            }
        }
        public void AgregarProductoASeccion(int CodSeccion, int CodProducto)
        {
            Seccion seccion = _secciones?.FirstOrDefault(s => s.CodSeccion == CodSeccion);
            if (seccion != null)
            {
                seccion.AddProducto(ControladorComun.BD!.BuscarObjetosInt<Producto>("CodProducto", CodProducto)[0]);
                ControladorComun.BD!.PersistirObjeto(seccion);
            }
        }
    }
}
