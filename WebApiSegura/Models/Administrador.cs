using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using VeciHelp.BD;
using VeciHelp.Models;
using WebApiSegura.Models;

namespace VeciHelp.Models
{
    public class Administrador
    {
        public int IdUser { get; set; }
        public string Correo { get; set; }
        public string CodigoVerificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Rut { get; set; }
        public char Digito { get; set; }
        public string AntecedentesSalud { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Celular { get; set; }
        public string Direccion { get; set; }
        public string Clave { get; set; }
        public int IdUsuarioCreador { get; set; }

        private BaseDatos bd;

        public Administrador()
        {

        }

        public Administrador(string correo,int idUsuarioCreador)
        {
            this.Correo = correo;
            this.IdUsuarioCreador = idUsuarioCreador;
        }

        public bool M_UsuarioAdministradorIns()
        {
            bd = new BaseDatos();

            if (bd.p_UsuarioAdministradorIns(this.Correo,this.CodigoVerificacion,this.Nombre,this.Apellido,this.Rut,this.Digito,this.AntecedentesSalud,this.FechaNacimiento,this.Celular,this.Direccion,this.Clave))
            {
                return true;
            }
            return false;
        }

        public bool M_CodigoVerificacionUsuarioGenera(string correo, int idUsuarioCreador)
        {
            bd = new BaseDatos();

            return bd.p_CodigoVerificacionUsuarioGenera(correo, idUsuarioCreador);
        }

        public bool M_AsociacionVecinoIns(int idUsuario, int idVecino, int idAdmin)
        {
            bd = new BaseDatos();

            return bd.p_AsociacionVecinoIns(idUsuario, idVecino, idAdmin);
        }


        public bool M_AsociacionVecinoDel(int idUsuario, int idVecino, int idAdmin)
        {
            bd = new BaseDatos();

            return bd.p_AsociacionVecinoDel(idUsuario, idVecino, idAdmin);
        }

        public List<Usuario> M_AsociacionVecinosLst(int idUsuario)
        {
            bd = new BaseDatos();
 
            return  bd.p_AsociacionVecinosLst(idUsuario);
        }


    }
}