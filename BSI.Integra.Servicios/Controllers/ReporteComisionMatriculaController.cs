using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteComisionMatriculaBancoController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ReporteComisionMatriculaBanco
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteComisionMatriculaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteComisionMatriculaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de Subestados de seguimiento comsiones matricula
        /// </summary>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpGet("ObtenerListaSubEstadosParaSeguimientoComisiones")]
        public IActionResult ObtenerListaSubEstadosParaSeguimientoComisiones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteComisionMatriculaService(unitOfWork);
                var respuesta = servicio.ObtenerListaSubEstadosParaSeguimientoComisiones();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor : Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte de Comisiones Por Matricula para Grilla
        /// </summary>
        /// <returns> List<ReporteComisionMatriculaDTO> </returns>
        [HttpPost("ObtenerDatosReporteSeguimientoComisiones")]
        public IActionResult ObtenerDatosReporteSeguimientoComisiones([FromBody] FiltroReporteSeguimientoComisionesDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteComisionMatriculaService(unitOfWork);
                var respuesta = servicio.ObtenerDatosReporteSeguimientoComisiones(filtro);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
