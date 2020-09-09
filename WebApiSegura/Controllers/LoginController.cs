using System;
using System.Net;
using System.Threading;
using System.Web.Http;
using VeciHelp.Models;
using VeciHelp.Security;

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
            string rolUser = string.Empty;

            if (lgn.Validarlogin(login, out rolUser)==1)
            {
                    var token = TokenGenerator.GenerateTokenJwt(login.correo, rolUser);
                    return Ok(token);
            }     
            // Acceso denegado
            return Unauthorized();
        }
    }
}
