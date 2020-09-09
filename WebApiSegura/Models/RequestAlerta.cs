using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class RequestAlerta
    {
        public int idUsuaro { get; set; }
        public int idVecino { get; set; }
        public int idAlerta { get; set; }
        public string coordenadas { get; set; }
        public string texto { get; set; }
    }
}