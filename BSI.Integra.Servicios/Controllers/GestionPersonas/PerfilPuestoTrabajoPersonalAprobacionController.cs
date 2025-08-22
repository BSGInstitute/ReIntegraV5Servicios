using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PerfilPuestoTrabajoPersonalAprobacionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPerfilPuestoTrabajoPersonalAprobacionService _perfilPuestoTrabajoPersonalAprobacionService;
        public PerfilPuestoTrabajoPersonalAprobacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _perfilPuestoTrabajoPersonalAprobacionService = new PerfilPuestoTrabajoPersonalAprobacionService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 20/01/2025
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos de PerfilPuestoTrabajoPersonalAprobacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombos()
        {
            var resultado = _perfilPuestoTrabajoPersonalAprobacionService.ObtenerCombos();
            return Ok(resultado);
        }

        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 20/01/2025
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos de PerfilPuestoTrabajoPersonalAprobacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPerfilPuestoTrabajoPersonalAprobacion()
        {
            var resultado = _perfilPuestoTrabajoPersonalAprobacionService.ObtenerPerfilPuestoTrabajoPersonalAprobacion();
            return Ok(resultado);
        }
        /// Tipo Función: POST 
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 20/01/2025
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad PerfilPuestoTrabajoPersonalAprobacion</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PerfilPuestoTrabajoPersonalAprobacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _perfilPuestoTrabajoPersonalAprobacionService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 20/01/2025
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] PerfilPuestoTrabajoPersonalAprobacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _perfilPuestoTrabajoPersonalAprobacionService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una Eliminacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <returns>Retorna 200 y objeto eliminado o 400 y mensaje de error</returns>

        [HttpDelete("[action]")]
        public IActionResult Eliminar(EliminarPuestoTrabajoDTO dto)
        {
            var respuesta = _perfilPuestoTrabajoPersonalAprobacionService.Eliminar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
    
}
