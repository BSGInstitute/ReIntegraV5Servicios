using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteCompromisoPagoAlumno
    /// Autor: Daniel Huaita
    /// Version: 2.0
    /// Fecha: 07/05/2021        
    /// <summary>
    /// Controlador de Reporte de Compromiso Pago Alumno
    /// </summary>
    [Route("api/ReporteCompromisoPagoAlumno")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteCompromisoPagoAlumnoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteCompromisoPagoAlumnoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 04/20/2023   
        /// Versión: 2.0
        /// <summary>
        /// Obtiene combos para el Reporte Compromiso Pago Alumno
        /// </summary>
        /// <returns> Lista de Objetos : comboPersonal,comboCentroCosto </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteCompromisoPagoAlumnoService(unitOfWork);
                ObtenerCombosDTO combos = servicio.obtenerCombos(IdPersonal);
                if (combos.comboPersonal == null || combos.comboCentroCosto == null)
                {
                    return BadRequest("Error al obtener los datos");
                }
                else
                {
                    return Ok(combos);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Daniel Huaita
        /// Fecha: 04/24/2023   
        /// Versión: 2.0
        /// <summary>
        /// Obtiene combos para el Reporte Compromiso Pago Alumno
        /// </summary>
        /// <param name="Obj"> Objetos Filtro Reporte </param>
        /// <returns> Objeto resultado filtro : ResultadoFiltroReporteCompromisoDTO </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerReporteCompromiso(GenerarReporteCompromisoPagoFiltroGrillaDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteCompromisoPagoAlumnoService(unitOfWork);
                return Ok(servicio.ObtenerReporteCompromiso(Obj));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
