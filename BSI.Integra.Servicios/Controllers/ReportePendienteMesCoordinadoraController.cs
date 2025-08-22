using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReportePendienteMesCoordinadoraController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ReportePendienteMesCoordinadora
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class ReportePendienteMesCoordinadoraController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReportePendienteMesCoordinadoraController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Autor: Griselberto Huaman C.
        /// Fecha: 29/09/2023
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        [HttpPost("ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador")]
        public IActionResult ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador([FromBody] ReportePendienteMesCoordinadorFiltroDTO filtro)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new ReportePendienteMesCoordinadoraService(unitOfWork);
                    var respuesta = servicio.ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador(filtro);
                    return Ok(respuesta);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }


        }
        /// Autor: Griselberto Huaman C.
        /// Fecha: 29/09/2023
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        [HttpPost("ObtenerReportePendienteCierrePorMesCoordinador")]
        public IActionResult ObtenerReportePendienteCierrePorMesCoordinador([FromBody] ReportePendienteMesCoordinadorFiltroDTO filtro)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new ReportePendienteMesCoordinadoraService(unitOfWork);
                    var respuesta = servicio.ObtenerReportePendienteCierrePorMesCoordinador(filtro);
                    return Ok(respuesta);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }


        }

        /// Autor: Griselberto Huaman C.
        /// Fecha: 29/09/2023
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        [HttpPost("ProcesarReporteMesCoordinadora")]
        public IActionResult ProcesarReporteMesCoordinadora(ResultadoGeneralMesCoordinadoraDTO respuestaGeneral)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new ReportePendienteMesCoordinadoraService(unitOfWork);
                    var respuesta = servicio.ProcesarReporteMesCoordinadora(respuestaGeneral);
                    return Ok(respuesta);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
