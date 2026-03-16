using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// <summary>
    /// Controlador para el manejo de fases de gestión de contactos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FaseGestionContactoController : Controller
    {
        private IFaseGestionContactoService _faseGestionContactoService;

        public FaseGestionContactoController(IFaseGestionContactoService faseGestionContactoService)
        {
            _faseGestionContactoService = faseGestionContactoService;
        }

        /// <summary>
        /// Obtiene todas las fases de gestión de contacto activas
        /// </summary>
        /// <returns>Lista de fases de gestión de contacto</returns>
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _faseGestionContactoService.Obtener();
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene una fase de gestión de contacto por su ID
        /// </summary>
        /// <param name="id">ID de la fase</param>
        /// <returns>Datos de la fase de gestión de contacto</returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var resultado = _faseGestionContactoService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inserta una nueva fase de gestión de contacto
        /// </summary>
        /// <param name="dto">Datos de la fase a insertar</param>
        /// <returns>Fase de gestión de contacto creada</returns>
        //[Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] FaseGestionContactoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _faseGestionContactoService.Insertar(dto, "lzaafe");
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza una fase de gestión de contacto existente
        /// </summary>
        /// <param name="dto">Datos de la fase a actualizar</param>
        /// <returns>Fase de gestión de contacto actualizada</returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FaseGestionContactoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _faseGestionContactoService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina lógicamente una fase de gestión de contacto
        /// </summary>
        /// <param name="id">ID de la fase a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [Authorize]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _faseGestionContactoService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
    }
}
