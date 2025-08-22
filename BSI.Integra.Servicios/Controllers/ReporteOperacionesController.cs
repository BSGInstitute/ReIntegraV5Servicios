using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteOperacionesController
    /// Autor: Jonathan Caipo
    /// Fecha: 17/01/2023
    /// <summary>
    /// Gestión de Alumno
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteOperacionesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteOperacionesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion para llenar los combos de las personas  y los paises
        /// </summary>
        /// <returns>ActionResult con estado 200, 400</returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosEstadoAlumno(int idPersonal)
        {
            try
            {
                PersonalService personalService = new PersonalService(unitOfWork);
                var asistentes = personalService.ObtenerPersonalAsignadoOperacionesTotalV2(idPersonal);
                var personalActivo = personalService.ObtenerPersonalAsesoresOperacionesActivos();
                var filtro = new
                {
                    listaPersonal = asistentes,
                    personalActivo
                };
                return Ok(filtro);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte completo del estado de pago de los alumnos.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteEstadoAlumnos([FromBody] ReporteTasaConversionConsolidadaFiltroDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteService reporteService = new ReporteService(unitOfWork);
                var reporte = reporteService.GenerarReporteEstadoAlumnos(filtro);
                return Ok(new { Records = reporte });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion para llenar los combos de las personas  y los paises
        /// </summary>
        /// <returns>ActionResult con estado 200, 400</returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int idPersonal)
        {
            try
            {
                PersonalService personalService = new PersonalService(unitOfWork);
                var asistentes = personalService.ObtenerPersonalAsignadoOperacionesTotalV2(idPersonal);
                var personalActivo = personalService.ObtenerPersonalAsesoresOperacionesActivos();
                var filtro = new
                {
                    listaPersonal = asistentes,
                    personalActivo
                };
                return Ok(filtro);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }



        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteIndicadoresOperativos([FromBody] ReporteTasaConversionConsolidadaFiltroDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                try
                {
                    ReporteService reporteService = new ReporteService(unitOfWork);
                    var reporte = reporteService.GenerarReporteIndicadoresOperativosService(filtro);
                    return Ok(new { Records = reporte });

                    /////////////////////////////////////////////////////////////////////////////////////////////////////
                    
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}