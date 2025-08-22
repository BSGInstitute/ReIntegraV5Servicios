using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteTasaConversionHistoricaController
    /// Autor: Jonathan Caipo
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión Reporte: Reporte de la Tasa de Conversión Histórica
    /// </summary>
    [Route("api/ReporteTasaConversionHistorica")]
    [ApiController]
    public class ReporteTasaConversionHistoricaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteTasaConversionHistoricaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información para combos de interfaz
        /// </summary>
        /// <returns> objeto Agrupado <returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteTasaConversionHistorica()
        {
            try
            {
                PersonalService servicioPersonal = new PersonalService(unitOfWork);
                ReporteTasaConversionConsolidadaDTO resultado = new ReporteTasaConversionConsolidadaDTO();

                resultado.Asesores = servicioPersonal.ObtenerPersonalAsesoresFiltro();
                resultado.Coordinadores = servicioPersonal.ObtenerPersonalCoordinadoresFiltro();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de las Áreas por combo
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboArea()
        {
            try
            {
                AreaCapacitacionService servicioAreaCapacitacion = new AreaCapacitacionService(unitOfWork);
                var resultado = servicioAreaCapacitacion.ObtenerCombo().Select(o => new { id = o.Id, name = o.Nombre }).ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de las Subáreas por combo
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboSubArea()
        {
            try
            {
                SubAreaCapacitacionService servicioSubAreaCapacitacion = new SubAreaCapacitacionService(unitOfWork);
                var resultado = servicioSubAreaCapacitacion.ObtenerCombo().Select(o => new { id = o.Id, name = o.Nombre, area = o.IdAreaCapacitacion }).ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros en Combo de PGeneral
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboPGeneral()
        {
            try
            {
                PGeneralService servicioPGeneral = new PGeneralService(unitOfWork);
                var resultado = servicioPGeneral.ObtenerTodoGrid().Select(o => new { id = o.IdPgeneral, name = o.Nombre, subarea = o.IdSubArea }).ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
