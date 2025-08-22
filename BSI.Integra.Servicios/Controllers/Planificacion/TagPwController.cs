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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TagPwController : ControllerBase
    {
        private ITagPwService _tagPw;
        private IUnitOfWork _unitOfWork;
        public TagPwController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tagPw = new TagPwService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _tagPw.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]/{id}")]
        public IActionResult ObtenerParametroSeo(int id)
        {
            try
            {
                var resultado = _tagPw.ObtenerParametroPorIdTag(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] TagEntidadPwDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tagPw.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult Actualizar([FromBody] TagEntidadPwDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tagPw.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tagPw.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
