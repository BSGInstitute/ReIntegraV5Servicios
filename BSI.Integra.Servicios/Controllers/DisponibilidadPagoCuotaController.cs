using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: DisponibilidadPagoCuotaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de DisponibilidadPagoCuota
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class DisponibilidadPagoCuotaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public DisponibilidadPagoCuotaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Crea la Matricula
        /// </summary>
        /// <param name="filtro">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("GenerarReporteDisponibilidadCuota")]
        public IActionResult GenerarReporteDisponibilidadCuota([FromBody]FiltroReportePagoDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteService(unitOfWork);
                var respuesta = servicio.GenerarReporteDisponibilidadCuota(filtro);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Cambia las fechas segun el tipo
        /// </summary>
        /// <param name="filtro">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("CambiarFechaProcesos")]
        public IActionResult CambiarFechaProcesos([FromBody]ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaPagoDetalleFinalService(unitOfWork);
                var respuesta = servicio.CambiarFechaProcesos(listaEnteros);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Cambia las fechas segun el tipo
        /// </summary>
        /// <param name="filtro">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("CambiarFechaProcesoCronograma")]
        public IActionResult CambiarFechaProcesoCronograma([FromBody] FechaCronogramaDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaPagoDetalleFinalService(unitOfWork);
                var respuesta = servicio.CambiarFechaProcesoCronograma(data);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
