using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private int _numerosTeclado;
        public int NumerosTeclado { get => _numerosTeclado; set => _numerosTeclado = value; }
        private Usuario? _usuarioActual;
        public Usuario? UsuarioActual { get => _usuarioActual; set => _usuarioActual = value; }
        private TPV _tpvCFG = null!;
        private bool isTPVMaster = true; //Esto se define a mano desde aquí. Determina si esta TPV es Master
        private TPVMaster _procesoMaster = null!;
        private VentanaTecladoNumericoUsuario _tecladoClave = new VentanaTecladoNumericoUsuario(true);
        private VentanaTecladoNumericoUsuario _tecladoNumeros = new VentanaTecladoNumericoUsuario(false);
        private Tarifa? _tarifaActual;
        private PosicionVenta? _posicionVentaActual;
        public PosicionVenta? PosicionVentaActual { get => _posicionVentaActual; set => _posicionVentaActual = value; }

        public void InicioTPV()
        {
            if (isTPVMaster)
            { 
                Task.Run(() =>
                {
                    _procesoMaster = new Controlador.TPVMaster();
                    _procesoMaster.Iniciar();
                });
            }
            //this._tarifaActual = ControladorComun.ListaTarifas!.FirstOrDefault(t => t.Id == _tpvCFG.TarifaDefecto)!;
            //MessageBox.Show("Hemos cargado TPVBase");
            //BloqueaTPV();
            CargaTarifas();
        }

        public void CargaTarifas()
        {
            if (ControladorComun.BD!.ContarObjetos<Tarifa>() < 1)
            {
                this._tarifaActual = new Tarifa("Tarifa Base PVP", 1, 21, "Venta habitual con IVA");
                ControladorComun.BD!.PersistirObjeto(_tarifaActual);
            }
            else
            {
                int numTarifa = ControladorComun.BD!.BuscarObjeto<Tienda>(ControladorComun.TiendaActual!, "TarifaDefecto")[0].TarifaDefecto;
                this._tarifaActual = ControladorComun.BD!.BuscarObjetosInt<Tarifa>("Id", numTarifa)[0];
            }
            this._tpvCFG = new TPV(1, true, 1, 1);
        }

        public bool GeneraTicket()
        {
            return true;
        }

        public bool FinTPV()
        {
            if (this.isTPVMaster && this._procesoMaster != null)
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
            _tecladoClave.ShowDialog();
            _numerosTeclado = 0;
        }

        public bool CompruebaCierre()
        {
            return true;
        }

        public void InsertarProductoTest()
        {     
            //public Ticket(string numTicket, int numTPV, int ejercicio, string tienda)
            Ticket t1 = new Ticket('T', 1, 2024, "AudioCuenca00");
            
            Producto prod1 = new Producto(1,"T037", 10);
            Producto prod2 = new Producto("T098", 20);
            Producto prod3 = new Producto("D87435", 30);
            _tarifaActual.AnadirProducto(1, 200);
            prod3.Descripcion = "Descripción de prueba";
            ControladorComun.BD!.PersistirObjeto<Producto>(prod1);
            ControladorComun.BD!.PersistirObjeto<Producto>(prod2);
            ControladorComun.BD!.PersistirObjeto<Producto>(prod3);
            ControladorComun.BD!.PersistirObjeto<Ticket>(t1);
            ControladorComun.BD!.PersistirObjeto<Tarifa>(_tarifaActual);
            string maxID= ControladorComun.BD!.SelectMAXTicketT(2024,1);
            MessageBox.Show("Hemos insertado 3 productos. El ID máximo es: " + maxID);
        }
        public bool ExistenTicketCerrados()
        {
            List<Ticket> ticketsCerrados = ControladorComun.BD!.BuscarObjetosBool<Ticket>("Cerrado", true).ToList();
            return ticketsCerrados.Any();
        }

        public bool CompruebaClave(string clave)
        {
            List<Usuario> usuarios = ControladorComun.BD!.BuscarObjetosString<Usuario>("Clave", clave).ToList();
            return usuarios.Any();
        }
    }
}
