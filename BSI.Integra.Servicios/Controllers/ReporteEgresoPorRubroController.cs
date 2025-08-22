using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteEgresoPorRubroController
    /// Autor: Rodrigo Montesinos.
    /// Fecha: 12/01/2023
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class ReporteEgresoPorRubroController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public ReporteEgresoPorRubroController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// retorna una lista de sedes
        /// </summary>
        /// <returns>List<SedeFiltroCiudadDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSedes()
        {
            try
            {
                SedeService _repoSede = new SedeService(unitOfWork);
                var Lista = _repoSede.ObtenerListaSedesSegunFur();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// retorna una lista reportes por rubro
        /// </summary>
        /// <returns>List<ReporteEgresoPorRubroDTO></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarReporteEgresoPorRubro([FromBody] ReporteEgresoPorRubroSedesAnioDTO Filtro)
        {
            try
            {
                return Ok(new ReporteEgresoPorRubroService(unitOfWork).VizualizarReporteEgresoPorRubro(Filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// retorna una lista reportes de egresos por rubro
        /// </summary>
        /// <returns>List<DesgloseReporteEgresoPorRubroDTO></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarDesgloseReporteEgresoPorRubro([FromBody] FiltroDesgloseReporteEgresoPorRubroDTO Filtro)
        {
            try
            {
                return Ok(new ReporteEgresoPorRubroService(unitOfWork).VizualizarDesgloseReporteEgresoPorRubro(Filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
