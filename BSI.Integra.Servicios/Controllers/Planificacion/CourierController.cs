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
    public class CourierController : Controller
    {
        private ICourierService _courierService;
        public CourierController(ICourierService courierService)
        {
            _courierService = courierService;
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista CourierDTO </returns>
        //[AllowAnonymous]
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _courierService.Obtener();
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
        /// <returns> CourierDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var resultado = _courierService.ObtenerPorId(id);
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
        /// <returns>CourierDTO</returns>
        /// 
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] CourierDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _courierService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        ///// Tipo Función: POST
        ///// Autor: Gretel Canasa
        ///// Fecha: 18/04/2023
        ///// Versión: 1.0
        ///// <summary>
        ///// Registra una lista de Materiales de Accion
        ///// </summary>
        ///// <param name="dtos">Lista de Materiales de Accion</param>
        ///// <returns>Lista CourierDTO</returns>
        //[Authorize]
        //[HttpPost("[Action]")]
        //public IActionResult InsertarLista([FromBody] List<CourierDTO> dtos)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
        //        var resultado = _courierService.InsertarLista(dtos, registroClaimToken.UserName);
        //        return Ok(resultado);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        /// Tipo Función: PUT
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica un Material de accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <returns> CourierDTO </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CourierDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _courierService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        ///// Tipo Función: PUT
        ///// Autor: Gretel Canasa
        ///// Fecha: 18/04/2023
        ///// Versión: 1.0
        ///// <summary>
        ///// Modifica una lista de Materiales de accion
        ///// </summary>
        ///// <param name="dto">Materiales de Accion</param>
        ///// <returns> Lista CourierDTO </returns>
        //[Authorize]
        //[HttpPut("[Action]")]
        //public IActionResult ActualizarLista([FromBody] List<CourierDTO> dtos)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
        //        var resultado = _courierService.ActualizarLista(dtos, registroClaimToken.UserName);
        //        return Ok(resultado);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
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
                var resultado = _courierService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        ///// Tipo Función: DELETE
        ///// Autor: Gretel Canasa
        ///// Fecha: 18/04/2023
        ///// Versión: 1.0
        ///// <summary>
        ///// Elimina registros de materiales de accion por ids
        ///// </summary>
        ///// <param name="ids">Lista de ids</param>
        ///// <returns> true </returns>
        //[Authorize]
        //[HttpDelete("[Action]")]
        //public IActionResult EliminarLista([FromBody] List<int> ids)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
        //        var resultado = _courierService.EliminarLista(ids, registroClaimToken.UserName);
        //        return Ok(resultado);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

    }
}
