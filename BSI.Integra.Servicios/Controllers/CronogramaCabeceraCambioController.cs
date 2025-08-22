using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CronogramaCabeceraCambioController
    /// Autor: Margiory Ramirez
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Cronograma
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CronogramaCabeceraCambioController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CronogramaCabeceraCambioController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 11/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpGet("[Action]/{idPersonal}")]
        public ActionResult ObtenerSolicitudesCambios(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaCabeceraCambioService(unitOfWork);
                var respuesta = servicio.ObtenerSolicitudesCambios(idPersonal);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GER
        /// Autor: Margiory Ramirez
        /// Fecha: 11/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>

        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaFinal(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaPagoDetalleFinalService(unitOfWork);
                var respuesta = servicio.ObtenerCronogramaFinal(idMatriculaCabecera);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("[Action]")]
        [HttpPost]
        public ActionResult Aprobar([FromBody] CronogramaCabeceraCambioAprobarDTO cronogramaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaCabeceraCambioService(unitOfWork);
                var respuesta = servicio.Aprobar(cronogramaDTO);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult Rechazar([FromBody] CronogramaCabeceraCambioAprobarDTO cronogramaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaCabeceraCambioService(unitOfWork);
                var respuesta = servicio.Rechazar(cronogramaDTO);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
