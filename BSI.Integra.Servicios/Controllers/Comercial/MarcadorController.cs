using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: AgendaInformacionActividadController
    /// Autor: Gilmer Quispe.
    /// Fecha: 18/02/2023
    /// <summary>
    /// Gestión de Actividades para Agenda
    /// </summary>
    [Route("api/Comercial/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MarcadorController : Controller
    {
        private IMarcadorService _marcadorService;
        private ITokenManager _tokenManager;
        public MarcadorController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _marcadorService = new MarcadorService(unitOfWork);
        }

        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la actividad siguiente del marcador
        /// </summary>
        /// <returns> 0 nro de dias sin contacto </returns>
        [HttpGet("[action]/{idAsesor}")]
        public IActionResult ObtenerActividad(int idAsesor)
        {
            var resultado = _marcadorService.ObtenerActividad(idAsesor);
            return Ok(new
            {
                Actividad = resultado
            });
        }

        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda Actividad Marcador Log
        /// </summary>
        /// <returns> 0 nro de dias sin contacto </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult GuardarActividadMarcador([FromBody] ActividadMarcadorLogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _marcadorService.GuardarActividadMarcador(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda Actividad Marcador Log
        /// </summary>
        /// <returns> 0 nro de dias sin contacto </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult GuardarNoContestadoMarcador([FromBody] ActividadMarcadorLogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _marcadorService.GuardarNoContestadoMarcador(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda Actividad Marcador Log
        /// </summary>
        /// <returns> 0 nro de dias sin contacto </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult GuardarContestadoMarcador([FromBody] ActividadMarcadorLogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _marcadorService.GuardarContestadoMarcador(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda Actividad Marcador Log
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{idActividadDetalle}/{idOportunidad}")]
        public IActionResult ObtenerActividadMarcadorPorIdActividadDetalle(int idActividadDetalle, int idOportunidad)
        {
            var resultado = _marcadorService.ObtenerPorIdActividadDetalleIdOportunidad(idActividadDetalle, idOportunidad);
            return Ok(resultado);
        }
    }
}
