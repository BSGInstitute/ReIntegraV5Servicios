using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: SubAreaCapacitacionController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión de SubAreaCapacitacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SubAreaCapacitacionController : ControllerBase
    {
        private ISubAreaCapacitacionService _subAreaCapacitacionService;
        public SubAreaCapacitacionController(ISubAreaCapacitacionService subAreaCapacitacionService)
        {
            _subAreaCapacitacionService = subAreaCapacitacionService;
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_SubAreaCapacitacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                return Ok(_subAreaCapacitacionService.Obtener());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET 
        /// Autor: Gilmer Qm.
        /// Fecha: 09/05/2023
        /// Versión: 1.1
        /// Modificacion: Se agrega todos los campos de la tabla T_SubAreaCapacitacion para el combo
        /// <summary>
        /// Obtiene todos los registros guardados en T_SubAreaCapacitacion para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            try
            {
                return Ok(_subAreaCapacitacionService.ObtenerCombo());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion a la tabla T_SubAreaParametroSeo
        /// </summary>
        /// <param name="subAreaCapacitacionDTO"> datos para insercion capturados del Json </param> 
        /// <returns> bool </returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        [Authorize]
        public ActionResult Insertar([FromBody] SubAreaCapacitacionDTO subAreaCapacitacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_subAreaCapacitacionService.Insertar(subAreaCapacitacionDTO, registroClaimToken.UserName));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion a la tabla T_SubAreaParametroSeo
        /// </summary>
        /// <param name="subAreaCapacitacionDTO"> datos para insercion capturados del Json </param> 
        /// <returns> bool </returns>
        [Authorize]
        [Route("[action]")]
        [HttpPut]
        [Authorize]
        public ActionResult Actualizar([FromBody] SubAreaCapacitacionDTO subAreaCapacitacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_subAreaCapacitacionService.Actualizar(subAreaCapacitacionDTO, registroClaimToken.UserName));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion lógica en la tabla T_SubAreaParametroSeo
        /// </summary>
        /// <param name="id"> id de T_SubAreaCapacitacion </param> 
        /// <returns> bool </returns>
        [Authorize]
        [Route("[action]/{id}")]
        [HttpDelete]
        [Authorize]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_subAreaCapacitacionService.Eliminar(id, registroClaimToken.UserName));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el informacion contenido de ParametroSeoPw por el IdSubAreaCapacitacion
        /// </summary> 
        /// <returns> List<ParametroContenidoDTO> </returns>
        [Route("[action]/{idSubAreaCapacitacion}")]
        [HttpGet]
        public ActionResult ObtenerParametroContenidoPorIdSubAreaCapacitacion(int idSubAreaCapacitacion)
        {
            try
            {
                return Ok(_subAreaCapacitacionService.ObtenerParametroContenidoPorIdSubAreaCapacitacion(idSubAreaCapacitacion));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
