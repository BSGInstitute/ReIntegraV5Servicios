using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionDePersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionDePersona
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PuestoTrabajoNivelController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private INivelPuestoTrabajoService _nivelPuestoTrabajoService;
        public PuestoTrabajoNivelController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _nivelPuestoTrabajoService = new PuestoTrabajoNivelService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Tipo Función: GET
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 21/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PuestoTrabajoNivel
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _nivelPuestoTrabajoService.Obtener();
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 21/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PuestoTrabajoNivelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _nivelPuestoTrabajoService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: PUT
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>

        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] PuestoTrabajoNivelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _nivelPuestoTrabajoService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: DELETE
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>

        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _nivelPuestoTrabajoService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
