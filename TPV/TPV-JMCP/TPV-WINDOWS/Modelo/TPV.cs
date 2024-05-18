using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class TPV
    {
        private int _numTPV;
        public int NumTPV { get => _numTPV; set => _numTPV = value; }
        private int _isTPVMaster;
        public int IsTPVMaster { get => _isTPVMaster; }
        private int tarifaDefecto;
        public int TarifaDefecto { get => tarifaDefecto; set => tarifaDefecto = value; }


    }
}
