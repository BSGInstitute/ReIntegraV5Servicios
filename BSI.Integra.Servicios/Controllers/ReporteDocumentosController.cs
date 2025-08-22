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
    /// Controlador: ReporteDocumentosController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/09/2023
    /// <summary>
    /// Gestión de ReporteDocumentos
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteDocumentosController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteDocumentosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("ObtenerReporteDocumentos")]
        [HttpPost]
        public ActionResult ObtenerReporteDocumentos(ReporteDocumentosFiltroDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteDocumentosService(unitOfWork);
                return Ok(servicio.ObtenerReporteDocumentos(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                PersonalService servicioPersonal = new PersonalService(unitOfWork);
                ReporteTasaConversionConsolidadaDTO resultado = new ReporteTasaConversionConsolidadaDTO();

                resultado.Asesores = servicioPersonal.ObtenerPersonalAsesoresFiltro();
                resultado.Coordinadores = servicioPersonal.ObtenerPersonalCoordinadoresFiltro();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFrecuenciaReporteDocumentos()
        {
            try
            {
                var servicio = new FrecuenciaService(unitOfWork);
                return Ok(servicio.ObtenerFrecuenciaReporteDocumentos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



    }
}
