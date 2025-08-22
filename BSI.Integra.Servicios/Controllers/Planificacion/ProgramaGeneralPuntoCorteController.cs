using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ProgramaGeneralPuntoCorteController
    /// Autor: Flavio
    /// Fecha: 17/07/2024
    /// <summary>
    /// Gestion de Materiales de Accion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class ProgramaGeneralPuntoCorteController : Controller
    {
        private IProgramaGeneralPuntoCorteService _programaGeneralPuntoCorteService;
        private ITokenManager _tokenManager;
        public ProgramaGeneralPuntoCorteController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _programaGeneralPuntoCorteService = new ProgramaGeneralPuntoCorteService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpGet("[Action]")]
        public IActionResult ObtenerComboModulo()
        {
            var resultado = _programaGeneralPuntoCorteService.ObtenerComboModulo();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpGet("[Action]")]
        public IActionResult ObtenerConfiguracionPuntoCorte()
        {
            var resultado = _programaGeneralPuntoCorteService.ObtenerConfiguracionPuntoCorte();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpGet("[Action]/{idProgramaGeneral}")]
        public IActionResult ObtenerPuntoCortePorPrograma(int idProgramaGeneral)
        {
            var resultado = _programaGeneralPuntoCorteService.ObtenerPuntoCortePorPrograma(idProgramaGeneral);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpPost("[Action]")]
        public IActionResult ObtenerDetallePuntoCortePorIdPuntoCorte([FromBody] PuntoCorteDetalleFiltroDTO filtro)
        {
            var resultado = _programaGeneralPuntoCorteService.ObtenerDetallePuntoCortePorIdPuntoCorte(filtro);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpGet("[Action]/{idProgramaGeneral}/{idPais}")]
        public IActionResult ObtenerPuntoCortePorProgramaPais(int idProgramaGeneral, int idPais)
        {
            var resultado = _programaGeneralPuntoCorteService.ObtenerPuntoCortePorProgramaPais(idProgramaGeneral, idPais);
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpPost("[Action]")]
        public IActionResult ActualizarProgramaGeneralPuntoCorte([FromBody] ProgramaGeneralPuntoCorteDTO dto)
        {
            var resultado = _programaGeneralPuntoCorteService.ActualizarProgramaGeneralPuntoCorte(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpPost("[Action]")]
        public IActionResult ActualizarProgramaGeneralPuntoCortePaises([FromBody] List<ProgramaGeneralPuntoCorteDTO> listaDto)
        {
            var resultado = _programaGeneralPuntoCorteService.ActualizarProgramaGeneralPuntoCortePaises(listaDto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpPost("[Action]")]
        public IActionResult ObtenerListaProgramaGeneralPuntoCorte([FromBody] ProgramaGeneralPuntoCorteFiltroDTO filtroProgramaGeneralPuntoCorte)
        {
            var resultado = _programaGeneralPuntoCorteService.ObtenerListaProgramaGeneralPuntoCorte(filtroProgramaGeneralPuntoCorte);
            return Ok(resultado);
        }
        /// Tipo Función: PUT
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpPut("[Action]")]
        public IActionResult ActualizarProgramaGeneralPuntoCorteMasivo([FromBody] ProgramaGeneralPuntoCorteMasivoDTO dto)
        {
            var resultado = _programaGeneralPuntoCorteService.ActualizarProgramaGeneralPuntoCorteMasivo(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: PUT
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpPut("[Action]")]
        public IActionResult ActualizarProgramaGeneralPuntoCorteConfiguracion([FromBody] List<ProgramaGeneralPuntoCorteConfiguracionDTO> dtoConfiguracion)
        {
            var resultado = _programaGeneralPuntoCorteService.ActualizarProgramaGeneralPuntoCorteConfiguracion(dtoConfiguracion, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: PUT
        /// Autor: Flavio
        /// Fecha: 17/07/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns></returns>
        [JwtExpirationValidation]
        [HttpDelete("[Action]/{idProgramaGeneral}")]
        public IActionResult Eliminar(int idProgramaGeneral, [FromBody] List<int> idPaises)
        {
            var resultado = _programaGeneralPuntoCorteService.Eliminar(idPaises, idProgramaGeneral, _tokenManager.UserName);
            return Ok(resultado);
        }
    }
}
