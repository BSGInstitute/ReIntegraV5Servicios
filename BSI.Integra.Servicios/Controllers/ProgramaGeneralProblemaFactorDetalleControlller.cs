using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Controlador: ProgramaGeneralProblemaFactorDetalleController
    /// Autor: Marco Jose Villanueva
    /// Fecha: 17/10/2025
    /// <summary>
    /// Gestión de Problemas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralProblemaFactorDetalleController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IProgramaGeneralProblemaFactorDetalleService _programaGeneralProblemaFactorDetalleService;
        public ProgramaGeneralProblemaFactorDetalleController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralProblemaFactorDetalleService = new ProgramaGeneralProblemaFactorDetalleService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad ProgramaGeneralProblemaFactorDetallePwDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] ProgramaGeneralProblemaFactorDetalleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaFactorDetalleService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] ProgramaGeneralProblemaFactorDetalleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaFactorDetalleService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idProgramaGeneralProblemaFactorDetalle}")]
        public IActionResult Eliminar(int idProgramaGeneralProblemaFactorDetalle)
        {
            var respuesta = _programaGeneralProblemaFactorDetalleService.Eliminar(idProgramaGeneralProblemaFactorDetalle, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id ProgramaGeneralProblemaFactorDetalle
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _programaGeneralProblemaFactorDetalleService.Obtener();
            return Ok(resultado);
        }

        [HttpPost("[action]")]
        public IActionResult Existe([FromBody] string nombre)
        {
            var resultado = _programaGeneralProblemaFactorDetalleService.ExistePorNombre(nombre);
            return Ok(resultado);
        }
    }
}
