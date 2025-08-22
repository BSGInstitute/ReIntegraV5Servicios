using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Controlador: PEspecificoConsumoController
    /// Autor: Jonathan Caipo
    /// Fecha: 09/06/2023
    /// <summary>
    /// Gestión de PEspecificoConsumo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PEspecificoConsumoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IPEspecificoConsumoService _pEspecificoConsumoService;
        public PEspecificoConsumoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pEspecificoConsumoService = new PEspecificoConsumoService(unitOfWork);
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Inserta Sesiones FUR
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> bool </returns>
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public IActionResult InsertarFurSesiones([FromBody] List<PEspecificoConsumoDTO> dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoConsumoService.InsertarFurSesiones(dto, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Inserta Programas FUR
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> bool </returns>
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public IActionResult InsertarFurPrograma([FromBody] FurProgramaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoConsumoService.InsertarFurPrograma(dto, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
		/// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Elimina sesión FUR
        /// </summary>
        /// <param name="idFurSesion"></param>
        /// <returns></returns>
        [Route("[Action]/{idFurSesion}")]
        [Authorize]
        [HttpDelete]
        public IActionResult EliminarSesionFur(int idFurSesion)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoConsumoService.EliminarSesionFur(idFurSesion, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
		/// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Actualiza sesión FUR
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> bool </returns>
        [Route("[Action]")]
        [Authorize]
        [HttpPut]
        public IActionResult ActualizarSesionFur([FromBody] FurSesionFiltroDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoConsumoService.ActualizarSesionFur(dto, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
    }
}
