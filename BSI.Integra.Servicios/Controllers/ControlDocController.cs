using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class ControlDocController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ControlDocController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerDocumentosPorMatricula(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ControlDocService _repControlDoc = new ControlDocService(unitOfWork);
                return Ok(_repControlDoc.ObtenerDocumentosPorMatriculaCabeceraControl(IdMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarControlDocumento([FromBody] ControlDocumentoDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                ControlDocService _repControlDoc = new ControlDocService(unitOfWork);
                return Ok(_repControlDoc.ActualizarControlDocumento(entidad, usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarControlDocumentoAlumno([FromBody] ControlDocumentoAlumnoDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                ControlDocAlumnoService _repControlDoc = new ControlDocAlumnoService(unitOfWork);
                return Ok(_repControlDoc.ActualizarControlDocAlumno(entidad, usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
