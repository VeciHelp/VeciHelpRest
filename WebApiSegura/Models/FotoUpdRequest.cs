using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class FotoUpdRequest
    {
        public int id_TipoUsuario { get; set; }
        public byte[] Foto { get; set; }
    }
}