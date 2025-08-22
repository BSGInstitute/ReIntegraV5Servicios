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
    /// Controlador: CongelamientoPeriodoReporteFlujoController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/09/2023
    /// <summary>
    /// Gestión de CongelamientoPeriodoReporteFlujo
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CongelamientoPeriodoReporteFlujoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CongelamientoPeriodoReporteFlujoController(IUnitOfWork unitOfWork)
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
        [Route("GenerarCongelamientoReporte")]
        [HttpPost]
        public ActionResult GenerarCongelamientoReporte(List<FlujoCongelamientoPeriodoDTO> FlujoCongelamientoPeriodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CongelamientoPeriodoReporteFlujoService(unitOfWork);
                return Ok(servicio.GenerarCongelamientoReporte(FlujoCongelamientoPeriodo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

    }
}
