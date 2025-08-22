
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: ReporteSeguimientoOportunidadesController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 28/11/2023
    /// <summary>
    /// Gestión Reporte Seguimiento de Oportunidades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteSeguimientoOportunidadesTresCxController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        private IReporteSeguimientoOportunidadService _reporteSeguimientoOportunidadService;
        public ReporteSeguimientoOportunidadesTresCxController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _reporteSeguimientoOportunidadService = new ReporteSeguimientoOportunidadService(_unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 28/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de seguimiento de oportunidades de comercial
        /// </summary>
        /// <returns>Lista ReporteSeguimientoOportunidadDTO</returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public ActionResult<List<ReporteSeguimientoOportunidadDTO>> GenerarReporte([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _reporteSeguimientoOportunidadService.ReporteSeguimientoOportunidadTresCx(filtros);
            if (_tokenManager.AreaTrabajo == "VE" && (_tokenManager.TipoPersonal == "Asesor" || _tokenManager.TipoPersonal == "Coordinador") && _tokenManager.UserName != "juancarlos")
            {
                resultado = resultado.Where(w => w.Asesor != "Asesor Asignacion Automatica").ToList();
            }
            return Ok(resultado);

        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 28/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de oportunidades logs por id oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerListaOportunidadLog(int idOportunidad)
        {
            var resultado = _reporteSeguimientoOportunidadService.ObtenerListaOportunidadLog3cx(idOportunidad);
            return Ok(new { log = resultado.Item1, bloques = resultado.Item2 });
        }
    }
}
