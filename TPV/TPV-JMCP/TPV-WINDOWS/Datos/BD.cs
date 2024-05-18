using Orient.Client;
using Orient.Client.API.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV_WINDOWS.Modelo;

namespace TPV_WINDOWS.Datos
{
    public class BD
    {
        private string _hostBD = "127.0.0.1";
        private int _port = 2480;
        private string _nombreBD = "Tienda00";
        private string _userBD = "admin";
        private string _passBD = "nonohay";
        private string _connectBDString;
        private ODatabase _db;

        public BD()
        {
            //this._connectBDString = $"Host={this._hostBD};Port={this._port};Database={this._nombreBD};User={_userBD};Password={this._passBD}";
            //OClient.CreateDatabasePool(_connectBDString, 10, ODatabaseType.Document, OStorageType.PLocal);
            this._db = new ODatabase(_hostBD,_port,_nombreBD,ODatabaseType.Document,_userBD,_passBD);
        }

        public bool GrabaTicket(Ticket ticket)
        {
            try
            {
                ODocument doc = new ODocument();
                //doc.OClassName = "Ticket";
                doc.SetField("NumTicket", ticket.NumTicket);
                doc.SetField("Rid",doc.ORID);
                this._db.Insert(doc);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        public void LeeObjetoBD(string tipoObjeto, string nombrePropiedad, string valorPropiedad)
        {
            List<ODocument> miLista = null;
            Dictionary<string, string> condiciones = new Dictionary<string, string>();
            condiciones.Add(nombrePropiedad, valorPropiedad);
            // BUILD QUERY
            List<string> baseQuery = [
              String.Format($"SELECT FROM {0}", tipoObjeto)];

            // CHECK FOR CONDITIONAL VALUES
            if (condiciones.Count > 0)
            {
                // ADD WHERE
                baseQuery.Add("WHERE");

                // ADD CONDITIONS
                foreach (KeyValuePair<string, string> condition in condiciones)
                {
                    string entry = String.Format($"{0}={1}",
                      condition.Key, condition.Value);

                    baseQuery.Add(entry);
                }

                // JOIN QUERY
                string query = String.Join(' ', baseQuery);

                // RUN QUERY
                miLista = _db.Query(query);

            }
        }

        public Ticket GetTicket(String numTicket)
        {
            Ticket ticket = null;
            ticket = new Ticket(numTicket);
            try
            {
                List<Ticket> list = new List<Ticket>();
                List<ODocument> list2 = new List<ODocument>();
                //ticket = _db.Select().From("Ticket").Where("NumTicket").Equals(numTicket).ToList().Select(doc => new Ticket
                //{
                //    NumTicket = doc.GetField<string>("NumTicket")
                //});
                list2 = _db.Select().From("Ticket").Where("NumTicket").Equals(numTicket).ToList();
                list = list2.Select(x => x.GetField<Ticket>("NumTicket")).ToList();
                return ticket;
            }
            catch (Exception ex) { }
            return ticket;
        }
        
    }
}
