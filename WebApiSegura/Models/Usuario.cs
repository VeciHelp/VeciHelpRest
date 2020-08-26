using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using VeciHelp.BD;

namespace WebApiSegura.Models
{
    public class Usuario
    {
        public int id_Usuario { get; set; }
        public string organizacion { get; set; }
        public string correo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string rut { get; set; }
        public char digito { get; set; }
        public string foto { get; set; }
        public string antecedentesSalud { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int celular { get; set; }
        public string direccion { get; set; }
        public string clave { get; set; }
        public string codigoVerificacion { get; set; }
        public int id_TipoUsuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string nombreCreador { get; set; }

        private BaseDatos bd;

        public Usuario()
        {

        }


        private Usuario M_UsuarioGet()
        {
            bd = new BaseDatos();
            return bd.p_UsuarioGet(this.id_Usuario);
        }

        private bool M_UsuarioIns()
        {
            bd = new BaseDatos();
            return bd.p_UsuarioIns(this.correo, this.codigoVerificacion, this.nombre, this.apellido, this.rut, this.digito, this.antecedentesSalud, this.fechaNacimiento, this.celular, this.direccion, this.clave);
        }

        private bool M_UsuarioUpd()
        {
            bd = new BaseDatos();
            return bd.p_UsuarioUpd(this.id_Usuario, this.nombre, this.apellido, this.rut, this.digito, this.antecedentesSalud, this.fechaNacimiento, this.celular, this.direccion, this.clave);
        }

        private bool M_FotoUsuarioUpd()
        {
            bd = new BaseDatos();
            return bd.p_FotoUsuarioUpd(this.id_TipoUsuario, this.foto);
        }
    }
}