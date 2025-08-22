using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
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
    public class ProgramaGeneralMaterialEstudioAdicionalController : ControllerBase
    {
        private IProgramaGeneralMaterialEstudioAdicionalService _programaGeneralMaterialEstudioAdicional;
        public ProgramaGeneralMaterialEstudioAdicionalController(IProgramaGeneralMaterialEstudioAdicionalService programaGeneralMaterialEstudioAdicional)
        {
            _programaGeneralMaterialEstudioAdicional = programaGeneralMaterialEstudioAdicional;
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
                var resultado = _programaGeneralMaterialEstudioAdicional.ObtenerProgramaGeneralMaterialEstudio();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FeedbackTipo
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[Action]/{idPgeneral}")]
        public IActionResult ObtenerDetalle(int idPgeneral)
        {
            try
            {
                var resultado = _programaGeneralMaterialEstudioAdicional.ObtenerProgramaGeneralMaterialEstudioDetalle(idPgeneral);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los Id y Nombres
        /// </summary>
        /// <returns> Lista TipoDocumentoAlumnoCombosDTO </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] ProgramaGeneralMaterialEstudioAdicionalEntidadDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _programaGeneralMaterialEstudioAdicional.InsertarActualizarProgramaGeneralMaterialEstudio(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los Id y Nombres
        /// </summary>
        /// <returns> Lista TipoDocumentoAlumnoCombosDTO </returns>
        [Authorize]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _programaGeneralMaterialEstudioAdicional.EliminarProgramaGeneralMaterialEstudio(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
