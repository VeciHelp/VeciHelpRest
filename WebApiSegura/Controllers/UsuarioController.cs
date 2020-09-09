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
        //metodo para crear usuarios
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

        [HttpGet]
        [Route("GetUserId")]
        //metodo que retorna los datos de un usuario by id
        public IHttpActionResult GetUserId(int id_user)
        {
            Usuario usr = new Usuario();

            usr = usr.M_UsuarioGet(id_user);

            if (usr!=null)
            {
                return Ok(usr);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        //metodo para actualizar datos del usuario
        public IHttpActionResult UpdateUser(Usuario usuario)
        {

            var respuesta = "error";

            if (usuario.M_UsuarioUpd())
            {
                respuesta = "usuario Modificado Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }

        [HttpPut]
        [Route("UpdatePhoto")]
        //metodo para actualizar foto de perfil
        public IHttpActionResult UpdatePhoto(FotoUpdRequest fotoUpd)
        {
            Usuario usr = new Usuario();
            var respuesta = "error";

            if (usr.M_FotoUsuarioUpd(fotoUpd.id_TipoUsuario,fotoUpd.Foto))
            {
                respuesta = "Foto Modificada Correctamente";

                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }


    }
}
