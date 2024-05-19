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
    public class TPVBase
    {
        private TPV _tpvCFG;
        //private BDMongo? _bd;
        //public BDMongo? BD { get => _bd; set => _bd = value; }
        private bool isTPVMaster = true; //Esto se define a mano desde aquí. Determina si esta TPV es Master
        private TPVMaster? procesoMaster=null;
        private VentanaTecladoNumericoUsuario tecladoWindow = new VentanaTecladoNumericoUsuario();
        private Tarifa? _tarifaActual;
        private PosicionVenta? _posicionVentaActual;
        public PosicionVenta? PosicionVentaActual { get => _posicionVentaActual; set => _posicionVentaActual = value; }

        public void InicioTPV()
        {
            if (isTPVMaster) { 
                procesoMaster = new Controlador.TPVMaster();
                procesoMaster.Iniciar();
            }
            this._tarifaActual = ControladorComun.ListaTarifas!.FirstOrDefault(t => t.Id == _tpvCFG.TarifaDefecto)!;
            MessageBox.Show("Hemos cargado TPVBase");
            BloqueaTPV();
            
        }

        public bool GeneraTicket()
        {
            return true;
        }

        public bool FinTPV()
        {
            if (this.isTPVMaster || this.procesoMaster != null)
            {
                if (!procesoMaster!.IsRunning)
                {
                    return false;
                }
                else
                {
                    this.procesoMaster.Parar();
                }
            }
            return false;
        }
        public void BloqueaTPV()
        {
            tecladoWindow.ShowDialog();
        }

        public bool CompruebaCierre()
        {
            return true;
        }

    }
}
