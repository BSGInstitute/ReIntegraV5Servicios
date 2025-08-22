using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: MaterialCriterio de VerificacionController
    /// Autor: Gretel Canasa
    /// Fecha: 18/04/2023
    /// <summary>
    /// Gestion de Materiales Criterios de Verificación
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[ServiceFilter(typeof(JwtActionFilter))]
    public class MaterialCriterioVerificacionController : Controller
    {
        private IMaterialCriterioVerificacionService _materialCriterioVerificacionService;
        public MaterialCriterioVerificacionController(IMaterialCriterioVerificacionService materialCriterioVerificacionService)
        {
            _materialCriterioVerificacionService = materialCriterioVerificacionService;
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los materiales de Criterios de Verificacion
        /// </summary>
        /// <returns> Lista MaterialCriterioVerificacionDTO </returns>
        //[AllowAnonymous]
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _materialCriterioVerificacionService.Obtener();
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
        /// Obtiene Material de Criterios de Verificacion por Id
        /// </summary>
        /// <param name="id">Id del Material de Criterios de Verificacion</param>
        /// <returns> MaterialCriterioVerificacionDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var resultado = _materialCriterioVerificacionService.ObtenerPorId(id);
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
        /// Registra un nuevo Material de Criterio de Verificacion
        /// </summary>
        /// <param name="dto">Material de Criterio de Verificacion</param>
        /// <returns>MaterialCriterioVerificacionDTO</returns>
        /// 
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] MaterialCriterioVerificacionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialCriterioVerificacionService.Insertar(dto, registroClaimToken.UserName);
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
        /// Registra una lista de Materiales Criterios de Verificación
        /// </summary>
        /// <param name="dtos">Lista de Materiales Criterios de Verificación</param>
        /// <returns>Lista MaterialCriterioVerificacionDTO</returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<MaterialCriterioVerificacionDTO> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialCriterioVerificacionService.InsertarLista(dtos, registroClaimToken.UserName);
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
        /// Modifica un Material de Criterios de Verificacion
        /// </summary>
        /// <param name="dto">Material de Criterio de Verificacion</param>
        /// <returns> MaterialCriterioVerificacionDTO </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] MaterialCriterioVerificacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialCriterioVerificacionService.Actualizar(dto, registroClaimToken.UserName);
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
        /// Modifica una lista de Materiales de Criterios de Verificacion
        /// </summary>
        /// <param name="dto">Materiales Criterios de Verificación</param>
        /// <returns> Lista MaterialCriterioVerificacionDTO </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<MaterialCriterioVerificacionDTO> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialCriterioVerificacionService.ActualizarLista(dtos, registroClaimToken.UserName);
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
        /// Elimina un registro de Material de Criterio de Verificacion
        /// </summary>
        /// <param name="id">Id Material de Criterio de Verificacion</param>
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
                var resultado = _materialCriterioVerificacionService.Eliminar(id, registroClaimToken.UserName);
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
        /// Elimina registros de materiales de Criterios de Verificacion por ids
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
                var resultado = _materialCriterioVerificacionService.EliminarLista(ids, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

    }
}
