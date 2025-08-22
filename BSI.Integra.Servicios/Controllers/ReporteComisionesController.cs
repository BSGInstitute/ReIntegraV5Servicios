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
    /// Controlador: ReporteComisionesController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ReporteComisiones
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteComisionesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteComisionesController(IUnitOfWork unitOfWork)
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
        [Route("ObtenerReporteComisiones")]
        [HttpPost]
        public ActionResult ObtenerReporteComisiones([FromBody] FiltroReporteComisionDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteService(unitOfWork);
                return Ok(servicio.ObtenerReporteComisiones(filtro));
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
        /// Actualiza el reporte comsiiones.
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("ActualizarReporteComisiones")]
        [HttpPost]
        public ActionResult ActualizarReporteComisiones([FromBody] FiltroGenerarCierreDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteService(unitOfWork);
                return Ok(servicio.ActualizarReporteComisiones(filtro));
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
        /// Persnal Ventas V4
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("ObtenerPersonalVentasV4")]
        [HttpGet]
        public ActionResult  ObtenerPersonalVentasV4()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PersonalService(unitOfWork);
                return Ok(servicio.ObtenerPersonalVentasV4());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
