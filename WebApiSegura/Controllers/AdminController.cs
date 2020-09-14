﻿using System.Collections.Generic;
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
        [AllowAnonymous]
        [HttpPost]
        [Route("CrearAdmin")]
        //metodo para crear usuarios  **Listo
        public IHttpActionResult CrearAdmin(Administrador adminis)
        {

            var respuesta = "error";

            if (adminis.M_UsuarioAdministradorIns(out respuesta))
            {
                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }

        
        [HttpPost]
        [Route("EnrolarUsr")]
        //metodo para enrolar usuarios     **Listo
        public IHttpActionResult EnrolarUsr(Administrador admin)
        {
            var respuesta = "error";

            if (admin.M_CodigoVerificacionUsuarioGenera(admin.Correo,admin.IdUsuarioCreador, out respuesta))
            {
                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }


        [HttpPost]
        [Route("InsAsocVecino")]
        //metodo que crea asociaciones de vecinos   **Listo
        public IHttpActionResult InsAsocVecino(RequestAsoc asoc)
        {
            Administrador adminis = new Administrador();
            var respuesta = "error";

            if (adminis.M_AsociacionVecinoIns(asoc.idUsuario, asoc.idVecino, asoc.idAdmin, out respuesta))
            {
                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }

        [HttpDelete]
        [Route("DelAsocVecino")]
        //metodo que elimina asociaciones **Listo
        public IHttpActionResult DelAsocVecino(RequestAsoc asoc)
        {
            Administrador adminis = new Administrador();
            var respuesta = "error";

            if (adminis.M_AsociacionVecinoDel(asoc.idUsuario, asoc.idVecino, asoc.idAdmin, out respuesta))
            {
                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }


        [HttpGet]
        [Route("GetListaVecinoId")]
        //metodo que retorna listado de vecinos asociados a un usuario  **Listo
        public IHttpActionResult GetListaVecinoId(int id)
        {
            var usrLst = new List<Usuario>();

            Administrador adm = new Administrador();

            usrLst = adm.M_AsociacionVecinosLst(id);

            return Ok(usrLst);
        }

    }
}
