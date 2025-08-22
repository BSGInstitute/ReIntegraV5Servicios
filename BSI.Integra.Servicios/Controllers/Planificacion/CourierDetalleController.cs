using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: CourierController
    /// Autor: Gretel Canasa
    /// Fecha: 18/04/2023
    /// <summary>
    /// Gestion de Materiales de Accion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[ServiceFilter(typeof(JwtActionFilter))]
    public class CourierDetalleController : Controller
    {
        private ICourierDetalleService _courierDetalleService;
        public CourierDetalleController(ICourierDetalleService courierDetalleService)
        {
            _courierDetalleService = courierDetalleService;
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista CourierDetalleDTO </returns>
        //[AllowAnonymous]
        [HttpGet("[Action]/{idCourier}")]
        public IActionResult ObtenerPorIdCourier(int idCourier)
        {
            try
            {
                var resultado = _courierDetalleService.ObtenerPorIdCourier(idCourier);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Material de accion por Id
        /// </summary>
        /// <param name="id">Id del Material de accion</param>
        /// <returns> CourierDetalleDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var resultado = _courierDetalleService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <returns>CourierDetalleDTO</returns>
        /// 
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] CourierDetalleDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _courierDetalleService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
      
        /// Tipo Función: PUT
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica un Material de accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <returns> CourierDetalleDTO </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CourierDetalleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _courierDetalleService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
      
        /// Tipo Función: DELETE
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de Material de Accion
        /// </summary>
        /// <param name="id">Id Material de Accion</param>
        /// <returns> true </returns>
        [Authorize]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _courierDetalleService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina registros de materiales de accion por ids
        /// </summary>
        /// <param name="ids">Lista de ids</param>
        /// <returns> true </returns>
        [Authorize]
        [HttpDelete("[Action]")]
        public IActionResult EliminarLista([FromBody] List<int> ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _courierDetalleService.EliminarLista(ids, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

    }
}
