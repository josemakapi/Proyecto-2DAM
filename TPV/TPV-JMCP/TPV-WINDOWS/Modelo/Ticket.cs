using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV_WINDOWS.Modelo
{
    public class Ticket
    {
        private string _numTicket;
        private string _numTPV;

        public string NumTicket { get { return _numTicket; } set {  _numTicket = value; } }
        public string NumTPV { get { return _numTPV; } set { _numTPV = value; } }



        public Ticket(string numTicket)
        {
            this._numTicket = numTicket;

        }

    }
}
