using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: TipoFormacionController
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 15/04/2024
    /// <summary>
    /// Tipo Formacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class AnalisisProcesoSeleccionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IAnalisisProcesoSeleccionService _analisisProcesoSeleccionService;
        private IPuestoTrabajoService _puestoTrabajoService;
        private IProcesoSeleccionService _procesoSeleccionService;
        public AnalisisProcesoSeleccionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _analisisProcesoSeleccionService = new AnalisisProcesoSeleccionService(unitOfWork);
            _puestoTrabajoService = new PuestoTrabajoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 07/08/2025
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos de AnalisisProcesoSeleccion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombos()
        {
            var resultado = _analisisProcesoSeleccionService.ObtenerComboFiltro();
           
            return Ok(resultado);
        }

        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 07/08/2025
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos del Reporte
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult GenerarReporte([FromBody] FiltroAnalisisProcesoSeleccionDTO Filtro)
        {
            var resultado = _analisisProcesoSeleccionService.GenerarReporte(Filtro);
            return Ok(resultado);
        }

        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 07/08/2025
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos del Reporte_V2
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult GenerarReporte_V2([FromBody] FiltroAnalisisProcesoSeleccionDTO Filtro)
        {
            var resultado = _analisisProcesoSeleccionService.GenerarReporte_V2(Filtro);
            return Ok(resultado);
        }
    }
}
