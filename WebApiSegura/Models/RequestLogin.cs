using System;
using VeciHelp.BD;
using WebApiSegura.Models;

namespace VeciHelp.Models
{
    public class RequestLogin
    {
        public string correo { get; set; }
        public string clave { get; set; }

        public Usuario Validarlogin(RequestLogin login)
        {
            BaseDatos bd = new BaseDatos();

            return bd.P_Login(login.correo, login.clave);
        }
    }

}