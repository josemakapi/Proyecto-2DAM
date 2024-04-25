using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    /// <summary>
    /// Si esta TPV está configurada como Master,
    /// Este proceso residente atiende las peticiones de creación de tickets de TPVs secundarias.
    /// </summary>
    public class TPVMaster
    {
        private bool isRunning;
        private Thread? hilo;
        private readonly object bloqueoInstancia = new object();

        public TPVMaster()
        {
            this.isRunning = false;
        }

        public bool Iniciar()
        {
            if (!isRunning)
            {
                isRunning = true;
                hilo = new Thread(Main);
                hilo.Start();
                return true;
            }
            return false;
        }

        public bool Parar()
        {
            if (isRunning)
            {
                isRunning = false;
                hilo?.Join();
                return true;
            }
            return false;
        }

        private void Main()
        {
            while (isRunning && hilo != null)
            {
                lock(bloqueoInstancia)
                {
                    //Console.WriteLine("Proceso residente en ejecución...");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
