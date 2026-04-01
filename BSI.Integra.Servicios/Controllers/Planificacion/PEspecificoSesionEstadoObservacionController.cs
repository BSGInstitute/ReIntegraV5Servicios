using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PEspecificoSesionEstadoObservacionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPEspecificoSesionEstadoObservacionService _PEspecificoSesionEstadoObservacionService;
        public PEspecificoSesionEstadoObservacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _PEspecificoSesionEstadoObservacionService = new PEspecificoSesionEstadoObservacionService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 06/03/2026
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos de PEspecificoSesionEstadoObservacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _PEspecificoSesionEstadoObservacionService.Obtener();
            return Ok(resultado);
        }

        /// Tipo Función: POST 
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 06/03/2026
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad PEspecificoSesionEstadoObservacion</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PEspecificoSesionEstadoObservacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PEspecificoSesionEstadoObservacionService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 06/03/2026
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] PEspecificoSesionEstadoObservacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PEspecificoSesionEstadoObservacionService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: DELETE
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 06/03/2026
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una Eliminacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <returns>Retorna 200 y objeto eliminado o 400 y mensaje de error</returns>

        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _PEspecificoSesionEstadoObservacionService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
