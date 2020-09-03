using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [Authorize(Roles = "Usuario")]
    [RoutePrefix("api/v1/user")]
    public class UsuarioController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("CrearUser")]
        //metodo para crear usuarios  **Listo
        public IHttpActionResult CrearUser(Usuario usuario)
        {

            var respuesta = "error";

            if (usuario.M_UsuarioIns())
            {
                respuesta = "usuario Creado Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }
    }
}
