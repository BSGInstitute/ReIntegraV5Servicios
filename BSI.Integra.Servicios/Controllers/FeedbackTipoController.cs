using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FeedbackTipoController
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión de FeedbackTipo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FeedbackTipoController : ControllerBase
    {
        private IFeedbackTipoService _feedbackTipoService;
        public FeedbackTipoController(IFeedbackTipoService feedbackTipoService)
        {
            _feedbackTipoService = feedbackTipoService;
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FeedbackTipo
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _feedbackTipoService.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra un nuevo FeedbackTipo
        /// </summary>
        /// <param name="dto">FeedbackTipo</param>
        /// <returns>FeedbackTipoDTO</returns>
        /// 
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] FeedbackTipoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _feedbackTipoService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un registra FeedbackTipo
        /// </summary>
        /// <param name="dto">FeedbackTipo</param>
        /// <returns>FeedbackTipoDTO</returns>
        /// 
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FeedbackTipoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _feedbackTipoService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de Feedback Tipo
        /// </summary>
        /// <param name="id">Id Feedback Tipo</param>
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
                var resultado = _feedbackTipoService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
    }
}
