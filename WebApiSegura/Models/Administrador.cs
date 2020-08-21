using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using VeciHelp.BD;
using VeciHelp.Models.Usuarios;

namespace VeciHelp.Models
{
    public class Administrador
    {
        public string correo { get; set; }
        public string codigoVerificacion { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string rut { get; set; }
        public char digito { get; set; }
        public string antecedentesSalud { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int celular { get; set; }
        public string direccion { get; set; }
        public string clave { get; set; }
        public int idUsuarioCreador { get; set; }

        private BaseDatos bd;

        public Administrador()
        {

        }

        public bool m_UsuarioAdministradorIns()
        {
            bd = new BaseDatos();

            if (bd.p_UsuarioAdministradorIns(this.correo,this.codigoVerificacion,this.nombre,this.apellido,this.rut,this.digito,this.antecedentesSalud,this.fechaNacimiento,this.celular,this.direccion,this.clave))
            {
                return true;
            }
            return false;
           
        }

        public bool m_CodigoVerificacionUsuarioGenera(string correo, int idUsuarioCreador)
        {
            bd = new BaseDatos();

            return bd.p_CodigoVerificacionUsuarioGenera(correo, idUsuarioCreador);
        }

        public bool m_AsociacionVecinoIns(int idUsuario,int idVecino,int idAdmin)
        {
            bd = new BaseDatos();

            if (bd.p_AsociacionVecinoIns(idUsuario,idVecino,idAdmin))
            {
                return true;
            }
            return false;
        }

        public bool m_AsociacionVecinoDel(int idUsuario, int idVecino, int idAdmin)
        {
            bd = new BaseDatos();

            if (bd.p_AsociacionVecinoDel(idUsuario, idVecino, idAdmin))
            {
                return true;
            }
            return false;
        }

        public List<Usuario> m_AsociacionVecinosLst(int idUsuario)
        {
            bd = new BaseDatos();
            List<Usuario> usrList;

            try
            {
                usrList = bd.p_AsociacionVecinosLst(idUsuario);
            }
            catch (Exception)
            {

                throw;
            }
          
            return usrList;
        }


    }
}