using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: CriterioEvaluacionCategoriumController
    /// Autor: Klebert Layme.
    /// Fecha: 08/05/2023
    /// <summary>
    /// Gestión de CriterioEvaluacionCategorium
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class CriterioEvaluacionCategoriumController : Controller
    {

        private ICriterioEvaluacionCategoriumService _criterioEvaluacionCategoriumService;
        public CriterioEvaluacionCategoriumController(ICriterioEvaluacionCategoriumService criterioEvaluacionCategoriumService)
        {

            _criterioEvaluacionCategoriumService = criterioEvaluacionCategoriumService;
        }


        /// Tipo Función: GET
        /// Autor: Klebert Layme.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene CriterioEvaluacionCategorium por Id
        /// </summary>
        /// <param name="id">Id del CriterioEvaluacionCategorium</param>
        /// <returns> CriterioEvaluacionCategoriumDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var resultado = _criterioEvaluacionCategoriumService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch
            {
                throw;

            }
        }
        /// Tipo Función: GET
        /// Autor: Klebert Layme.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CriterioEvaluacionCategorium
        /// </summary>
        /// <returns> lista de CriteiroEvaluacionCategoriumDTO </returns>
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _criterioEvaluacionCategoriumService.Obtener();
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Klebert Layme.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de T_CriterioEvaluacionCategorium
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] CriterioEvaluacionCategoriumDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioEvaluacionCategoriumService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: PUT
        /// Autor: Klebert Layme.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">CriterioEvaluacionCategorium </param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CriterioEvaluacionCategoriumDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioEvaluacionCategoriumService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Klebert Layme.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de Criterio Evaluacion Categorium
        /// </summary>
        /// <param name="id">Id de Criterio Evaluacion Categorium</param>
        /// <returns>True</returns>
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
                var resultado = _criterioEvaluacionCategoriumService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        } 
        /// Tipo Función: GET 
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ComboTipoPersona (Alumno y Expositor)
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombo()
        {

            try
            {
                var resultado = new { Categoria = _criterioEvaluacionCategoriumService.ObtenerCombo() };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}