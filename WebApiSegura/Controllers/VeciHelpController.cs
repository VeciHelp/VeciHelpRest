using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeciHelp.Models;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    //[Authorize(Roles = "VeciHelp")]
    [AllowAnonymous]
    [RoutePrefix("api/v1/vecihelp")]
    public class VeciHelpController : ApiController
    {
        [HttpPost]
        [Route("EnrolarAdmin")]
        //metodo para enrolar administradores
        public IHttpActionResult EnrolarUsr(RequestEnrolar enrolar)
        {
            var respuesta = "error";

            if (enrolar.M_CodigoVerificacionAdministradorGenera(enrolar.correo, enrolar.idOrganizacion, out respuesta))
            {
                return Ok(respuesta);
            }
            else
                return Ok(respuesta);
        }

        [HttpGet]
        [Route("GetPais")]
        //metodo para enrolar administradores
        public IHttpActionResult GetPais()
        {
            var paisVar= Pais.M_PaisLst();

            if (paisVar.Count != 0)
            {
                return Ok(paisVar);
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("GetRegion")]
        //metodo para enrolar administradores
        public IHttpActionResult GetRegion(int id)
        {
            var regionVar = RegionModel.M_RegionLst(id);

            if (regionVar.Count != 0)
            {
                return Ok(regionVar);
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("GetProvincia")]
        //metodo para enrolar administradores
        public IHttpActionResult GetProvincia(int id)
        {
            var provinciaVar = Provincia.M_ProvinciaLst(id);

            if (provinciaVar.Count != 0)
            {
                return Ok(provinciaVar);
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("GetCiudad")]
        //metodo para enrolar administradores
        public IHttpActionResult GetCiudad(int id)
        {
            var ciudadVar = Ciudad.M_CiudadLst(id);

            if (ciudadVar.Count != 0)
            {
                return Ok(ciudadVar);
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("GetComuna")]
        //metodo para enrolar administradores
        public IHttpActionResult GetComuna(int id)
        {
            var comunaVar = Comuna.M_ComunaLst(id);

            if (comunaVar.Count != 0)
            {
                return Ok(comunaVar);
            }
            else
                return NotFound();
        }
    }
}
