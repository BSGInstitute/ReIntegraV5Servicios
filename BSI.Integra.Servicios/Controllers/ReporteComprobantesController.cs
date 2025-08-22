using BSI.Integra.Aplicacion.DTO;
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
    /// Controlador: ReporteComprobantesController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/09/2023
    /// <summary>
    /// Gestión de ReporteComprobantes
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteComprobantesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteComprobantesController(IUnitOfWork unitOfWork)
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
        [Route("ObtenerReporteComprobantes")]
        [HttpPost]
        public ActionResult ObtenerReporteComprobantes(IdDTO valor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteComprobantesService(unitOfWork);
                return Ok(servicio.ObtenerReporteComprobantes(valor.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("ObtenerTipoAsociado")]
        [HttpGet]
        public ActionResult ObtenerTipoAsociado()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteComprobantesService(unitOfWork);
                return Ok(servicio.ObtenerTipo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
