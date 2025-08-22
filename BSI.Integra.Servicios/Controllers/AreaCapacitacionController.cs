using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AreaCapacitacionController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión de AreaCapacitacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AreaCapacitacionController : ControllerBase
    {
        private IAreaCapacitacionService _areaCapacitacionService;
        public AreaCapacitacionController(IUnitOfWork unitOfWork)
        {
            _areaCapacitacionService = new AreaCapacitacionService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CompuestoAreaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _areaCapacitacionService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CompuestoAreaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _areaCapacitacionService.Actualizar(dto, registroClaimToken.UserName);
                
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _areaCapacitacionService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_AreaCapacitacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener()
        {
            try
            {
                return Ok(_areaCapacitacionService.Obtener());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_AreaCapacitacion para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            try
            {
                return Ok(_areaCapacitacionService.ObtenerCombo());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
