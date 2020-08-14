using System.Web.Http;
using VeciHelp.Models;

namespace VeciHelp.Controllers
{
    /// <summary>
    /// customer controller class for testing security token 
    /// </summary>
    [Authorize(Roles = "Administrador")]
    [RoutePrefix("api/v1/admin")]
    public class AdminController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var customerFake = "customer-fake: " + id;
            return Ok(customerFake);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
            return Ok(customersFake);
        }

        [HttpPost]
        [Route("CrearUsr")]
        public IHttpActionResult CrearUsr(Administrador adminis)
        {
            Administrador admin = new Administrador();
            var respuesta = "error";

            if (admin.m_CodigoVerificacionUsuarioGenera(adminis.correo, adminis.idUsuarioCreador))
            {
                respuesta = "usuario Enrolado Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }

    }
}
