using System.Collections.Generic;
using System.Web.Http;
using VeciHelp.Models;
using WebApiSegura.Models;

namespace VeciHelp.Controllers
{
    /// <summary>
    /// customer controller class for testing security token 
    /// </summary>
    [Authorize(Roles = "Administrador")]
    [RoutePrefix("api/v1/admin")]
    public class AdminController : ApiController
    {
        [HttpPost]
        [Route("CrearUsuario")]
        //metodo para crear usuarios
        public IHttpActionResult CrearUsuario(Administrador adminis)
        {

            var respuesta = "error";

            if (adminis.M_UsuarioAdministradorIns())
            {
                respuesta = "usuario Enrolado Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }

        [HttpPost]
        [Route("EnrolarUsr")]
        //metodo para enrolar usuarios
        public IHttpActionResult EnrolarUsr(Administrador adminis)
        {
            var respuesta = "error";

            if (adminis.M_CodigoVerificacionUsuarioGenera(adminis.correo, adminis.idUsuarioCreador))
            {
                respuesta = "usuario Enrolado Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }


        [HttpPost]
        [Route("InsAsocVecino")]
        //metodo que crea asociaciones de vecinos
        public IHttpActionResult InsAsocVecino(int idUsuario,int idVecino,int idAdmin)
        {
            Administrador adminis = new Administrador();
            var respuesta = "error";

            if (adminis.M_AsociacionVecinoIns(idUsuario,idVecino,idAdmin))
            {
                respuesta = "usuario Enrolado Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }

        [HttpDelete]
        [Route("DelAsocVecino")]
        //metodo que elimina asociaciones
        public IHttpActionResult DelAsocVecino(int idUsuario, int idVecino, int idAdmin)
        {
            Administrador adminis = new Administrador();
            var respuesta = "error";

            if (adminis.M_AsociacionVecinoDel(idUsuario, idVecino, idAdmin))
            {
                respuesta = "usuario Enrolado Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }


        [HttpGet]
        [Route("GetListaVecinoId")]
        //metodo que retorna listado de vecinos asociados a un usuario
        public IHttpActionResult GetListaVecinoId(int idUsuario)
        {
            var usrLst = new List<Usuario>();

            Administrador adm = new Administrador();

            usrLst = adm.M_AsociacionVecinosLst(idUsuario);

            return Ok(usrLst);
        }
        

    }
}
