using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingHere.BO
{
    public class FiltroResult
    {
        public string Endereco {get; set;}
        public int TipoV { get; set; }
        public string Placa { get; set; }
        public string TipoVe { get; set; }
        public string DataAt { get; set; }
        public string HoraInicial { get; set; }
    }
}
