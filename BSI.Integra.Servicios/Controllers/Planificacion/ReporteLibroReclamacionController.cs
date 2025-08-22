using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ReporteLibroReclamacionController
    /// Autor: Jonathan Caipo
    /// Fecha: 27/04/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión del Reporte Libro de Reclamaciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteLibroReclamacionController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteLibroReclamacionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Listar los nombres de la personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="nombre">parte del nombre para buscar coincidencias</param>
        /// <returns>Lista de los nombres en un List<ItemComboAutocompleDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaNombreReclamo(string? nombre)
        {
            try
            {
                IReporteLibroReclamacionService reporteLibroReclamacionService = new ReporteLibroReclamacionService(unitOfWork);
                return Ok(reporteLibroReclamacionService.ObtenerListaNombreReclamo(nombre));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Listar los dni de las personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="Dni">parte del dni para buscar coincidencias</param>
        /// <returns>Lista de los dnis en un List<ItemComboAutocompleDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaDniReclamo(string? dni)
        {
            try
            {
                IReporteLibroReclamacionService reporteLibroReclamacionService = new ReporteLibroReclamacionService(unitOfWork);
                return Ok(reporteLibroReclamacionService.ObtenerListaDniReclamo(dni));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de libro de reclamaciones segun el filtro ingresado
        /// </summary>
        /// <param name="filtroReporte">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns>Lista del reporte libro de reclamaciones en un List<ReporteLibroReclamacionDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteLibroReclamacion([FromBody] ReporteLibroReclamacionFiltroDTO filtroReporte)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteLibroReclamacionService reporteLibroReclamacionService = new ReporteLibroReclamacionService(unitOfWork);
                return Ok(reporteLibroReclamacionService.GenerarReporteLibroReclamacion(filtroReporte));
            }
            catch
            {
                throw;
            }
        }
    }
}
