using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: VerificacionOportunidadISMController
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión de Información en la verificación de Oportunidad ISM
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class VerificacionOportunidadISMController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public VerificacionOportunidadISMController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Insera Oportunidad Verificada
        /// </summary>
        /// <param name="oportunidadVerificada"></param>
        /// <returns></returns>

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarOportunidadVerificadaV3(OportunidadVerificadaDTO OportunidadVerificada)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var usuario = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity).RegistroClaimToken.UserName;
                return Ok(new OportunidadIsVerificadaService(unitOfWork).InsertarOportunidadVerificadaV3(OportunidadVerificada, usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor:Margiory Ramirez
        /// Fecha: 07/02/2023
        /// Versión: 1.0
        /// <summary>
        /// ObtiENE Obtine Oportinidades verificadas 
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerOportunidadesVerificadas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OportunidadIsVerificadaService(unitOfWork);
                return Ok(servicio.ObtenerOportunidadesVerificadas());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerOportunidadesISM()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OportunidadIsVerificadaService(unitOfWork);
                return Ok(servicio.ObtenerOportunidadesISM());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosVerificacionOportunidadISM()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try

            {
                var servicio = new OportunidadIsVerificadaService(unitOfWork);
                return Ok(servicio.ObtenerCombosVerificacionOportunidadISM());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
