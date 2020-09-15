using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class ResponseLogin
    {
        public int existe { get; set; }
        public int id_Usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string roleName { get; set; }
        public string token { get; set; }
        public string mensaje { get; set; }
    }
}