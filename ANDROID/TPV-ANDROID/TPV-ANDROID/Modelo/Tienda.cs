using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV_WINDOWS.Controlador;

namespace TPV_WINDOWS.Modelo
{
    public class Tienda
    {
        private int _id;
        public int Id { get => _id; }
        private int _codTienda;
        public int CodTienda { get => _codTienda; set => _codTienda = value;  }
        private int _tpvMaster;
        public int TPVMaster { get => _tpvMaster; set => _tpvMaster = value; }
        private string _ipTPVMaster;
        public string IPTPVMaster { get => _ipTPVMaster; set => _ipTPVMaster = value; }      
        
        private string? _descripcion;
        public string? Descripcion { get => _descripcion; set => _descripcion = value; }
        private int _tarifaDefecto;
        public int TarifaDefecto { get => _tarifaDefecto; set => _tarifaDefecto = value; }

        public Tienda(int codTienda, int tpvMaster, string ipTPVMaster, string descripcion, int tarifaDefecto)
        {
            _id = ControladorComun.BD!.SelectMAXInt("Tienda", "_id") + 1;
            _codTienda = codTienda;
            _tpvMaster = tpvMaster;
            _ipTPVMaster = ipTPVMaster;
            _descripcion = descripcion;
            _tarifaDefecto = tarifaDefecto;
        }
        public Tienda(int id, int codTienda, int tpvMaster, string ipTPVMaster, string descripcion, int tarifaDefecto)
        {
            _id = id;
            _codTienda = codTienda;
            _tpvMaster = tpvMaster;
            _ipTPVMaster = ipTPVMaster;
            _descripcion = descripcion;
            _tarifaDefecto = tarifaDefecto;
        }

    }
}
