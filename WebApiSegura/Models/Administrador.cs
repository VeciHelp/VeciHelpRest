using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeciHelp.BD;

namespace VeciHelp.Models
{
    public class Administrador
    {
        public string correo { get; set; }

        public int idUsuarioCreador { get; set; }

        public bool m_CodigoVerificacionUsuarioGenera(string correo, int idUsuarioCreador)
        {
            BaseDatos bd = new BaseDatos();

            if (bd.p_CodigoVerificacionUsuarioGenera(correo, idUsuarioCreador))
            {
                return true;
            }
            else
                return false;
        }

    }
}