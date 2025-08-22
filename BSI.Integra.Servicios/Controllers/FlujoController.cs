using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Servicios.Helpers;
using System.Security.Claims;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Servicios.Configurations;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PartnerPwController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión de PartnerPw
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FlujoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IFlujoService _flujoService;
        public FlujoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _flujoService = new FlujoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros Flujo
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _flujoService.Obtener();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos iniciales del modulo
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombos()
        {
            var resultado = _flujoService.ObtenerCombos();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los hijos en relacion al idFlujo
        /// </summary>
        [HttpGet("[action]/{idFlujo}")]
        public IActionResult ObtenerFlujoFase(int idFlujo)
        {
            var resultado = _flujoService.ObtenerFlujoFasePorIdFlujo(idFlujo);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los hijos en relacion al idFlujoFase
        /// </summary>
        [HttpGet("[action]/{idFlujoFase}")]
        public IActionResult ObtenerFlujoActividad(int idFlujoFase)
        {
            var resultado = _flujoService.ObtenerFlujoActividadPorIdFlujoFase(idFlujoFase);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los hijos en relacion al idFlujoFase
        /// </summary>
        [HttpGet("[action]/{idFlujoActividad}")]
        public IActionResult ObtenerFlujoOcurrencia(int idFlujoActividad)
        {
            var resultado = _flujoService.ObtenerFlujoOcurrenciaPorIdFlujoActividad(idFlujoActividad);
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Crea un nuevo Flujo
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] FlujoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un Flujo existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] FlujoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un flujo existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idFlujo}")]
        public IActionResult Eliminar(int idFlujo)
        {
            var resultado = _flujoService.Eliminar(idFlujo, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Crea un nuevo Flujo Fase
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarFase([FromBody] FlujoFaseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.InsertarFase(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un Flujo Fase existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarFase([FromBody] FlujoFaseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.ActualizarFase(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un flujo fase existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idFlujoFase}")]
        public IActionResult EliminarFase(int idFlujoFase)
        {
            var resultado = _flujoService.EliminarFase(idFlujoFase, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Crea un nuevo Flujo Actividad
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarFaseActividad([FromBody] FlujoActividadDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.InsertarActividad(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un Flujo Actividad existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarFaseActividad([FromBody] FlujoActividadDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.ActualizarActividad(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un flujo fase existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idFlujoActividad}")]
        public IActionResult EliminarFaseActividad(int idFlujoActividad)
        {
            var resultado = _flujoService.EliminarActividad(idFlujoActividad, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Crea un nuevo Flujo Ocurrencia
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarFaseActividadOcurrencia([FromBody] FlujoOcurrenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.InsertarOcurrencia(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un Flujo Ocurrencia existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarFaseActividadOcurrencia([FromBody] FlujoOcurrenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _flujoService.ActualizarOcurrencia(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un flujo Ocurrencia existente
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idFlujoOcurrencia}")]
        public IActionResult EliminarFaseActividadOcurrencia(int idFlujoOcurrencia)
        {
            var resultado = _flujoService.EliminarOcurrencia(idFlujoOcurrencia, _tokenManager.UserName);
            return Ok(resultado);
        }
    }
}
