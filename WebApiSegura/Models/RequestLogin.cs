﻿using System;
using VeciHelp.BD;

namespace VeciHelp.Models
{
    public class RequestLogin
    {
        public string correo { get; set; }
        public string clave { get; set; }

        public int Validarlogin(RequestLogin login,out string rolUser)
        {
            BaseDatos bd = new BaseDatos();
            int retorna = 0;

            bd.P_Login(login.correo, login.clave, out  retorna,out string rolename);
            rolUser = rolename;

            return retorna;
        }
    }

}