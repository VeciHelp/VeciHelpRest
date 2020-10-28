using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
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
        public string nroEmergencia { get; set; }
        public int participantes { get; set; }
        public string foto { get; set; }
        public string opcionBoton { get; set; }

        private BaseDatos bd;

        public Alerta()
        {

        }


        public List<string> M_AlertaSospechaIns(int idUsuario, string texto, byte[] foto)
        {
            bd = new BaseDatos();

            var fotoSospecha = Convert.ToBase64String(foto);

            return bd.P_AlertaSospechaIns(idUsuario, texto, fotoSospecha);
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

        public bool M_CancelaAcudirLlamadoUpd(int idUsuario, int idAlerta, out string mensaje)
        {
            mensaje = string.Empty;
            bd = new BaseDatos();
            return bd.p_CancelaAcudirLlamadoUpd(idUsuario, idAlerta, out mensaje);
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




        //metodos para enviar las alertas
        public  string SendNotification(string[] tokenList, string titulo, string mensaje)
        {
            string webAddr = "https://fcm.googleapis.com/fcm/send";
            string serverKey = "AAAAJsP8Zq8:APA91bG4UU1OvFpwQRq3Xl_uw4JC7MMYHcGm8d2mVwkF45Km0L0ztw3Gt1hjbInUweqWd9NqdV8OQmlOxa440aw4snOcUsDq0Ty8eDQg5KSe-IzI1GbLMPDDBlXIo1jTIwG-smyl_eTd";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Method = "POST";

            var payload = new
            {
                registration_ids = tokenList,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = mensaje,
                    title = titulo
                },
            };
            var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                // aqui se revisa el resultado de la peticion
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
                return "Alerta enviada Correctamente";
            }
            else
            {
                return "Error al enviar alerta";
            }

          
            
        }
    }
}