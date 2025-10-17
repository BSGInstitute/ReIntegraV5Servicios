using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProgramaGeneralProblemaFactorSolucionController
    /// Autor: Marco Jose Villanueva
    /// Fecha: 17|0/2025
    /// <summary>
    /// Gestión de Problemas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralProblemaFactorSolucionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IProgramaGeneralProblemaFactorSolucionService _programaGeneralProblemaFactorolucionService;
        public ProgramaGeneralProblemaFactorSolucionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralProblemaFactorolucionService = new ProgramaGeneralProblemaFactorSolucionService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17|0/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad ProgramaGeneralProblemaFactorSolucionPwDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] ProgramaGeneralProblemaFactorSolucionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaFactorolucionService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17|0/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] ProgramaGeneralProblemaFactorSolucionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaFactorolucionService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17|0/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idProgramaGeneralProblemaFactorSolucion}")]
        public IActionResult Eliminar(int idProgramaGeneralProblemaFactorSolucion)
        {
            var respuesta = _programaGeneralProblemaFactorolucionService.Eliminar(idProgramaGeneralProblemaFactorSolucion, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17|0/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id ProgramaGeneralProblemaFactorSolucion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _programaGeneralProblemaFactorolucionService.Obtener();
            return Ok(resultado);
        }
    }
}
