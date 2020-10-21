using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeciHelp.BD;

namespace WebApiSegura.Models
{
    public class Alerta
    {
        public int idAlerta { get; set; }
        public DateTime fechaAlerta { get; set; }
        public DateTime horaAlerta { get; set; }
        public string TipoAlerta { get; set; }
        public string nombreGenerador { get; set; }
        public string apellidoGenerador { get; set; }
        public string nombreAyuda { get; set; }
        public string apellidoAyuda { get; set; }
        public string coordenadaSospecha { get; set; }
        public string textoSospecha { get; set; }
        public string direccion { get; set; }
        public string organizacion { get; set; }
        public int participantes { get; set; }
        public string foto { get; set; }

        private BaseDatos bd;

        public Alerta()
        {

        }


        public List<string> M_AlertaSospechaIns(int idUsuario, string coordenadas, byte[] texto)
        {
            bd = new BaseDatos();

            var fotoSospecha = Convert.ToBase64String(texto);

            return bd.P_AlertaSospechaIns(idUsuario, coordenadas, fotoSospecha);
        }

        //sirve para las alertas en casa propia y para las alertas en casa vecino
        public List<string> M_AlertaSOSIns(int idUsuario, int idvecino)
        {
            bd = new BaseDatos();
            return bd.P_AlertaSOSIns(idUsuario, idvecino);
        }

        //sirve para las alertas en casa propia y para las alertas en casa vecino
        public List<string> M_AlertaEmergenciaIns(int idUsuario, int idvecino)
        {
            bd = new BaseDatos();
            return bd.P_AlertaEmergenciaIns(idUsuario, idvecino);
        }

        public bool M_AcudirLlamadoIns(int idUsuario,int idAlerta,out string mensaje)
        {
            mensaje = string.Empty;
            bd = new BaseDatos();
            return bd.P_AcudirLlamadoIns(idUsuario, idAlerta, out mensaje);
        }

        public List<Alerta> M_AlertaLst(int idUsuario)
        {
            bd = new BaseDatos();
            return bd.P_AlertaLst(idUsuario);
        }

        public Alerta M_AlertaById(int idAlerta, int idUsuario)
        {
            bd = new BaseDatos();
            return bd.P_AlertaById(idAlerta,idUsuario);
        }






    }
}