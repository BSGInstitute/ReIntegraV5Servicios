using BSI.Integra.Aplicacion.DTO;
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
    public class MaterialTipoController : ControllerBase
    {
        private IMaterialTipoService _materialTipoService;
        public MaterialTipoController(IMaterialTipoService materialTipoService)
        {
            _materialTipoService = materialTipoService;
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MaterialAccion, T_MaterialVersion, T_MaterialCriterioVerificacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombosModulo()
        {
            try
            {
                var resultado = _materialTipoService.ObtenerCombosModulo();
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
        /// Obtiene todos los registros guardados en T_MaterialAccion, T_MaterialVersion, T_MaterialCriterioVerificacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _materialTipoService.Obtener();
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
        /// Obtiene todos los registros guardados en T_MaterialAccion, T_MaterialVersion, T_MaterialCriterioVerificacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] MaterialTipoAsociacionEntidadDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialTipoService.InsertarMaterialTipo(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MaterialAccion, T_MaterialVersion, T_MaterialCriterioVerificacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] MaterialTipoAsociacionEntidadDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialTipoService.ActualizarMaterialTipo(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MaterialAccion, T_MaterialVersion, T_MaterialCriterioVerificacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialTipoService.EliminarMaterialTipo(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-04
        /// <summary>
        /// Obtiene registros de T_MaterialTipo para mostrarse en combo
        /// </summary>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            try
            {
                var combo = _materialTipoService.ObtenerCombo();
                return Ok(combo); 
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }
    }
}
