using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPV_WINDOWS.Datos;
using TPV_WINDOWS.Modelo;
using TPV_WINDOWS.Vista;

namespace TPV_WINDOWS.Controlador
{
    /// <summary>
    /// Superclase estática que controla el inicio y fin del programa y tiene recursos comunes a todos los elementos 
    /// del programa para evitar crear instancias innecesarias.
    /// </summary>
    public static class ControladorComun
    {
        public static TPVBase? TpvBase;
        public static BDMongo? BD;
        public static Tienda? TiendaActual;
        public static List<Tienda>? Tiendas;

        public static bool IniciarBD(string host, int puerto, string user, string pass)
        {
            BD = new BDMongo(host, puerto, user, pass);
            if (!BD.ConectarBD("TPVJMCP"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Llamado desde el botón Conectar de la ventana de PreInicio para inicializar los parámetros de la tienda
        /// </summary>
        public static void PreInicializaTienda()
        {   
            CargarTiendas();
        }

        public static bool CargarPantallaVentas()
        {
            TpvBase = new TPVBase();
            //await Task.Run(() => TpvBase.InicioTPV()); //Da petes
            TpvBase.InicioTPV();
            return true;
        }
        public static void CargarTiendas()
        {
            if (BD!.ContarObjetos<Tienda>() < 1)
            {
                Tiendas = [new Tienda(0,0, 1, "192.168.1.200", "Tienda de audio de JMCP", 0)];
                BD!.PersistirObjeto(Tiendas[0]);
            }
            else
            {
                Tiendas = BD!.LeerObjetosTipo<Tienda>();
            }
        }

        public static void CerrarPrograma()
        {
            if (TpvBase != null)
            {
                TpvBase!.FinTPV();
                BD!.DesconectarBD();
                Application.Current.Shutdown();
            }
        }
        
        //public static LineaPantalla DimeUltimaLinea(List<LineaPantalla> listaLineas)
        //{
        //    List<LineaPantalla> lineas = BD!.LeerObjetosLista<LineaPantalla>(listaLineas, "NumLinea");
        //    int numLinea = lineas.Last().NumeroLinea;
            
        //    return numLinea;
        //}

    }
}
