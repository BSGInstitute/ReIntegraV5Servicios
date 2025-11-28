using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PaqueteTutorVirtualController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPaqueteTutorVirtualService _PaqueteTutorVirtualService;
        public PaqueteTutorVirtualController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _PaqueteTutorVirtualService = new PaqueteTutorVirtualService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha:  27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad PaqueteTutorVirtualDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PaqueteTutorVirtualDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PaqueteTutorVirtualService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] PaqueteTutorVirtualDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PaqueteTutorVirtualService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha:  27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _PaqueteTutorVirtualService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: POST
        /// Autor: Christopher Sandy D' Paris
        /// Fecha:  27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta un paquete completo con países y beneficios
        /// </summary>
        /// <param name="dto">Entidad PaqueteTutorVirtualGuardarDTO con estructura completa</param>
        /// <returns>Retorna 200 y objeto ingresado con IDs o 400 y mensaje de error </returns>
        //[Authorize]
        //[JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarCompleto([FromBody] PaqueteTutorVirtualGuardarDTO dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var respuesta = _PaqueteTutorVirtualService.InsertarCompleto(dto, "_tokenManager.UserName");
            return Ok(respuesta);
        }

        /// Tipo Función: PUT
        /// Autor: Christopher Sandy D' Paris
        /// Fecha:  27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un paquete completo con países y beneficios
        /// </summary>
        /// <param name="dto">Entidad PaqueteTutorVirtualGuardarDTO con estructura completa</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        //[Authorize]
        //[JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarCompleto([FromBody] PaqueteTutorVirtualGuardarDTO dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var respuesta = _PaqueteTutorVirtualService.ActualizarCompleto(dto, "_tokenManager.UserName");
            return Ok(respuesta);
        }

        /// Tipo Función: GET
        /// Autor: Christopher Sandy D' Paris
        /// Fecha: 27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle completo con países y beneficios
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos con detalle anidado o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerDetalle()
        {
            var resultado = _PaqueteTutorVirtualService.ObtenerDetalle();
            return Ok(resultado);
        }

    }

}
