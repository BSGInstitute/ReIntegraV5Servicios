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
    /// Controlador: ReporteIndicadoresProductividadController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ReporteIndicadoresProductividad
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteIndicadoresProductividadController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteIndicadoresProductividadController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("GenerarReporte")]
        [HttpPost]
        public ActionResult GenerarReporte(ReporteIndicadoresProductividadVentasGeneralDTO respuestaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIndicadoresProductividadService(unitOfWork);
                return Ok(servicio.GenerarReporte(respuestaGeneral));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("ObtenerReporteProductividadVentasHorasTrabajadas")]
        [HttpPost]
        public ActionResult ObtenerReporteProductividadVentasHorasTrabajadas(FiltroFechaDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIndicadoresProductividadService(unitOfWork);
                return Ok(servicio.ObtenerReporteProductividadVentasHorasTrabajadas(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("ObtenerReporteProductividadVentasIndicadores")]
        [HttpPost]
        public ActionResult ObtenerReporteProductividadVentasIndicadores(FiltroFechaDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIndicadoresProductividadService(unitOfWork);
                return Ok(servicio.ObtenerReporteProductividadVentasIndicadores(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
