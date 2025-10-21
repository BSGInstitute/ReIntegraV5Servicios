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
    /// Controlador: ProgramaGeneralProblemaFactorSubSolucionController
    /// Autor: Marco Jose Villanueva
    /// Fecha: 17|0/2025
    /// <summary>
    /// Gestión de Problemas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralProblemaFactorSubSolucionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IProgramaGeneralProblemaFactorSubSolucionService _programaGeneralProblemaFactorDetalleService;
        public ProgramaGeneralProblemaFactorSubSolucionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralProblemaFactorDetalleService = new ProgramaGeneralProblemaFactorSubSolucionService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17|0/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad ProgramaGeneralProblemaFactorSubSolucionPwDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] List<ProgramaGeneralProblemaFactorSubSolucionDTO> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaFactorDetalleService.Insertar(dtos, _tokenManager.UserName);
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
        public IActionResult Actualizar([FromBody] List<ProgramaGeneralProblemaFactorSubSolucionDTO> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return BadRequest("La lista está vacía.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rpta = _programaGeneralProblemaFactorDetalleService.Actualizar(dtos, _tokenManager.UserName);
            return Ok(rpta); 
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
        [HttpDelete("[action]/{idProgramaGeneralProblemaFactorSubSolucion}")]
        public IActionResult Eliminar(int idProgramaGeneralProblemaFactorSubSolucion)
        {
            var ok = _programaGeneralProblemaFactorDetalleService.Eliminar(idProgramaGeneralProblemaFactorSubSolucion, _tokenManager.UserName);
            return Ok(ok);
        }

        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva
        /// Fecha: 17|0/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id ProgramaGeneralProblemaFactorSubSolucion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _programaGeneralProblemaFactorDetalleService.Obtener();
            return Ok(resultado);
        }


        [HttpGet("[action]/{idProgramaGeneralProblemaFactorSolucion}")]
        public IActionResult ObtenerPorIdProgramaGeneralProblemaFactorSolucion(int idProgramaGeneralProblemaFactorSolucion)
        {
            var resultado = _programaGeneralProblemaFactorDetalleService.ObtenerPorIdProgramaGeneralProblemaFactorSolucion(idProgramaGeneralProblemaFactorSolucion);
            return Ok(resultado);
        }
    }
}
