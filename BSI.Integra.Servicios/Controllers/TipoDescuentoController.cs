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
    /// Controlador: TipoDescuentoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión de TipoDescuento
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoDescuentoController : ControllerBase
    {
        private ITipoDescuentoService _tipoDescuentoService;
        public TipoDescuentoController(IUnitOfWork unitOfWork)
        {
            _tipoDescuentoService = new TipoDescuentoService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Klebert Layme
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CompuestoTipoDescuentoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tipoDescuentoService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: PUT
        /// Autor: Klebert Layme
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CompuestoTipoDescuentoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tipoDescuentoService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Klebert Layme.
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
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
                var resultado = _tipoDescuentoService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDescuento
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener()
        {
            return Ok(_tipoDescuentoService.Obtener());
        }

        /// Tipo Función: GET
        /// Autor: Klebert Layme
        /// Fecha: 27/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en ComboTipoDescuentoDTO para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombosModulo()
        {
            return Ok(_tipoDescuentoService.ObtenerCombosModulo());
        }
        /// Tipo Función: GET
        /// Autor: Klebert Layme
        /// Fecha: 27/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en ComboTipoDescuentoDTO para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>

        [HttpGet("[action]/{idDescuentoAsesor}")]
        public IActionResult ObtenerTiposPorIdTipoDescuento(int idDescuentoAsesor)
        {
            return Ok(_tipoDescuentoService.ObtenerTiposPorIdTipoDescuento(idDescuentoAsesor));
        }
    }
}
