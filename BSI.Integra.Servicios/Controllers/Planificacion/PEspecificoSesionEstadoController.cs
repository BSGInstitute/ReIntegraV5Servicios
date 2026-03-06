using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
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
    public class PEspecificoSesionEstadoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPEspecificoSesionEstadoService _PEspecificoSesionEstadoService;
        public PEspecificoSesionEstadoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _PEspecificoSesionEstadoService = new PEspecificoSesionEstadoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 06/03/2026
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos de PEspecificoSesionEstado
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _PEspecificoSesionEstadoService.Obtener();
            return Ok(resultado);
        }

        /// Tipo Función: POST 
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 06/03/2026
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad PEspecificoSesionEstado</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PEspecificoSesionEstadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PEspecificoSesionEstadoService.Insertar(dto, _tokenManager.UserName);
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
        public IActionResult Actualizar([FromBody] PEspecificoSesionEstadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PEspecificoSesionEstadoService.Actualizar(dto, _tokenManager.UserName);
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
            var respuesta = _PEspecificoSesionEstadoService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
