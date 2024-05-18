using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Controlador
{
    /// <summary>
    /// Superclase estática que controla el inicio y fin del programa y tiene recursos comunes a todos los elementos del programa para evitar
    /// crear instancias innecesarias.
    /// </summary>
    public static class ControladorComun
    {
        public static TPVBase? TpvBase;
        public static bool IniciarPrograma()
        {
            if (TpvBase == null)
            {
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
        
        
    }
}
