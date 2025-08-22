using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: MaterialAccionController
    /// Autor: Giancarlo A. Romero Monroy
    /// Fecha: 25/05/2023
    /// <summary>
    /// Gestion de Materiales de Accion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AsociarTagProgramaController : Controller
    {
        private IAsociarTagProgramaService _asociarProgramaTagService;
        private ITokenManager _tokenManager;
        public AsociarTagProgramaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _asociarProgramaTagService = new AsociarTagProgramaService(unitOfWork);
        }

        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 22/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los combos para el modulo Asociar Tags a Programas
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCombosModulo()
        {
            var resultado = await _asociarProgramaTagService.ObtenerCombosModulo();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Giancarlo Romero Monroy
        /// Fecha: 26/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los programas tag
        /// </summary>
        /// <returns> Lista DTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerProgramas()
        {
            var resultado = _asociarProgramaTagService.ObtenerProgramas();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 23/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Todos los ParametroSeo por el idTag
        /// </summary>
        /// <param name="idTag"></param>
        /// <returns> Lista DTO </returns>
        [HttpGet("[Action]/{idTag}")]
        public IActionResult ObtenerTodoParametroContenido(int idTag)
        {
            var resultado = _asociarProgramaTagService.ObtenerTodoParametroPorIdTag(idTag);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Giancarlo Romero Monroy
        /// Fecha: 26/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los programas tag
        /// </summary>
        /// <returns> Lista List<ComboDTO> </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult ObtenerTagSinAsociar(int idPgeneral)
        {
            try
            {
                var resultado = _asociarProgramaTagService.ObtenerTagSinAsociarPw(idPgeneral);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
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
        [HttpPost("[Action]/{idPgeneral}")]
        public IActionResult AsociarTag([FromBody] List<int> idsTag, int idPgeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _asociarProgramaTagService.AsociarTag(idsTag, idPgeneral, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Desasocia Tag según idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="idTag"></param>
        /// <returns> bool </returns>
        [Authorize]
        [HttpDelete("[Action]/{idPGeneral}/{idTag}")]
        public IActionResult DesasociarTag(int idPGeneral, int idTag)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _asociarProgramaTagService.DesasociarTag(idPGeneral, idTag, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los Tags Asociados por PGeneral
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <returns> Lista DTO </returns>
        [HttpGet("[Action]/{idPGeneral}")]
        public IActionResult ObtenerTodoTagPorPrograma(int idPGeneral)
        {
            try
            {
                var resultado = _asociarProgramaTagService.ObtenerTodoTagPorPrograma(idPGeneral);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta Tag y Asocia Tag
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Entidad </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult InsertarTagAsociar([FromBody] CompuestoTagDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var resultado = _asociarProgramaTagService.InsertarTagAsociar(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Tag
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Entidad </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult ActualizarTag([FromBody] CompuestoTagDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var resultado = _asociarProgramaTagService.ActualizarTag(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina Tag
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Bool </returns>
        [Authorize]
        [HttpDelete("[Action]/{idTag}")]
        public IActionResult EliminarTag(int idTag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var resultado = _asociarProgramaTagService.EliminarTag(idTag, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
    }
}
