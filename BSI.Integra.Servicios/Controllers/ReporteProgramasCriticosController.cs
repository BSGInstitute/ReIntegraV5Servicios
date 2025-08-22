using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PeriodoService = BSI.Integra.Aplicacion.Marketing.Service.Implementacion.PeriodoService;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteProgramasCriticosController
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 08/01/2024
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteProgramasCriticosController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ReporteProgramasCriticosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

      

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion basica para alimentar el modulo
        /// </summary>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporteAsignacion([FromBody] ReporteProgramasCriticosFiltroDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                return Ok(servicio.ObtenerReporteProgramasCriticosAsignacion( filtros));


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista para un combo y seleccionar los grupos de los grupos de filtro de programas criticos
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase FiltroIdNombreDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboGrupos()
        {
            try
            {
                var grupoFiltroProgramaCriticoRepositorio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                return Ok(grupoFiltroProgramaCriticoRepositorio.ObtenerFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 11/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el periodo actual para el reporte de asignacion diaria
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase FiltroIdNombreDTO, caso contrario response 400 con el mensaje de la excepcion</returns>

        [HttpGet("[action]")]
        public IActionResult ObtenerComboUltimoPeriodo()
        {
           
            try
            {
                var servicio = new PeriodoService(unitOfWork);
                return Ok(servicio.ObtenerUltimoPeriodo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 11/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el estado para el reporte 
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase FiltroIdNombreDTO, caso contrario response 400 con el mensaje de la excepcion</returns>

        [HttpGet("[action]")]
        public IActionResult ObtenerComboEstado()
        {

            try
            {
                var servicio = new EstadoProgramaEspecificoService(unitOfWork);
                return Ok(servicio.ObtenerComboEstado());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        /// Tipo Función: GET
        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion basica para alimentar el modulo
        /// </summary>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteProgramasCriticosFiltroDTO Filtros) { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GrupoFiltroProgramaCriticoService(unitOfWork);
                return Ok(servicio.ObtenerReporteProgramasCriticos(Filtros));


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}










