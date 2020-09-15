using System;
using System.Net;
using System.Threading;
using System.Web.Http;
using VeciHelp.Models;
using VeciHelp.Security;
using WebApiSegura.Models;

namespace VeciHelp.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/v1/login")]
    public class LoginController : ApiController
    {
       
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(RequestLogin login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //instancio la clase loginrequest para llamar a metodo que valida el usuario
            RequestLogin lgn = new RequestLogin();
            ResponseLogin response = new ResponseLogin();

            response = lgn.Validarlogin(login);

            if (response!=null)
            {
                    response.token = TokenGenerator.GenerateTokenJwt(login.correo, response.roleName);
                    return Ok(response);
            }     
            // Acceso denegado
            return Unauthorized();
        }
    }
}
