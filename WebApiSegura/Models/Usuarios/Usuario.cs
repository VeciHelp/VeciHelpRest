using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeciHelp.Models.Usuarios
{
    public class Usuario
    {
        public string nombre {get; set;}
        public string apellido {get; set;}
        public string direccion {get; set;}
        public string celular { get; set; }
    }

    public class Usuario2
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
    }
}