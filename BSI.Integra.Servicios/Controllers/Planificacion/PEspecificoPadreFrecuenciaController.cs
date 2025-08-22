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
    /// Controlador: PEspecificoPadreFrecuenciaController
    /// Autor: Giancarlo Romero
    /// Fecha: 30/05/2023
    /// <summary>
    /// Gestión de PEspecificoädreFrecuencia
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[ServiceFilter(typeof(JwtActionFilter))]
    public class PEspecificoPadreFrecuenciaController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IPEspecificoPadreFrecuenciaService _pEspecificoPadreFrecuenciaService;
        public PEspecificoPadreFrecuenciaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pEspecificoPadreFrecuenciaService = new PEspecificoPadreFrecuenciaService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista con las frecuencias del programa padre
        /// </summary>
        /// <returns> Lista List<TPespecificoPadreFrecuenciaSesion> </returns>
        [HttpGet("[Action]/{idPEspecifico}")]
        public IActionResult Obtener(int idPEspecifico)
        {
            return Ok(_pEspecificoPadreFrecuenciaService.ObtenerPorIdPespecifico(idPEspecifico));
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta una lista con las frecuencias del programa padre
        /// </summary>
        /// <returns> bool </returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public ActionResult Insertar([FromBody] PEspecificoPadreFrecuenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoPadreFrecuenciaService.Insertar(dto, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una lista con las frecuencias del programa padre
        /// </summary>
        /// <param name="json"></param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [Authorize]
        [HttpPut]
        public ActionResult Actualizar([FromBody] PEspecificoPadreFrecuenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoPadreFrecuenciaService.Actualizar(dto, registroClaimToken.UserName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
