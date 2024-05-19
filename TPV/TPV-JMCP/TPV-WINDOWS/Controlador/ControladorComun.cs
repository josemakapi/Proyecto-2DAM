using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV_WINDOWS.Datos;
using TPV_WINDOWS.Modelo;

namespace TPV_WINDOWS.Controlador
{
    /// <summary>
    /// Superclase estática que controla el inicio y fin del programa y tiene recursos comunes a todos los elementos del programa para evitar
    /// crear instancias innecesarias.
    /// </summary>
    public static class ControladorComun
    {
        public static TPVBase? TpvBase;
        public static BDMongo? BD;
        public static List<Tarifa>? ListaTarifas;
        public static bool IniciarPrograma()
        {
            if (TpvBase == null)
            {
                BD = new BDMongo("192.168.1.200", 27017, "root", "nonotiene");
                ListaTarifas = BD.LeerObjetosTipo<Tarifa>();
                TpvBase = new TPVBase();
                
                TpvBase.InicioTPV();
                return true;
            }
            return false;
        }
        public static bool DetenerPrograma()
        {
            if (TpvBase == null)
            {
                TpvBase!.FinTPV();
                return true;
            }
            return false;
        }
        
        public static LineaPantalla DimeUltimaLinea(List<LineaPantalla> listaLineas)
        {
            List<LineaPantalla> lineas = BD!.LeerObjetosLista<LineaPantalla>(listaLineas, "NumLinea");
            int numLinea = lineas.Last().NumeroLinea;
            
            return numLinea;
        }

    }
}
