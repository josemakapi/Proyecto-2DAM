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
        public int Id { get { return _id; } }
        private int _codTienda;
        public int CodTienda { get { return _codTienda; } set { _codTienda = value; } }
        private int _tpvMaster;
        private string _ipTPVMaster;
        public int TPVMaster { get { return _tpvMaster;} }
        public string IPTPVMaster { get { return _ipTPVMaster; } }
        private string? _descripcion;
        public string? Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        private int _tarifaDefecto;
        public int TarifaDefecto { get { return _tarifaDefecto; } set { _tarifaDefecto = value; } }

        public Tienda(int codTienda, int tpvMaster, string ipTPVMaster, string descripcion, int tarifaDefecto)
        {
            this._id = ControladorComun.BD!.SelectMAXInt("Tienda", "_id") + 1;
            this.CodTienda = codTienda;
            this._tpvMaster = tpvMaster;
            this._ipTPVMaster = ipTPVMaster;
            this._descripcion = descripcion;
            this._tarifaDefecto = tarifaDefecto;
        }
        public Tienda(int id, int codTienda, int tpvMaster, string ipTPVMaster, string descripcion, int tarifaDefecto)
        {
            this._id = id;
            this.CodTienda = codTienda;
            this._tpvMaster = tpvMaster;
            this._ipTPVMaster = ipTPVMaster;
            this._descripcion = descripcion;
            this._tarifaDefecto = tarifaDefecto;
        }

    }
}
