using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: SubNivelCcController
    /// Autor Creacion: Gilmer Qm.
    /// Fecha: 09/05/2023
    /// <summary>
    /// Gestión de SubNivelCc
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SubNivelCcController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ISubNivelCcService _subNivelCcService;

        public SubNivelCcController(IUnitOfWork unitOfWork, ISubNivelCcService SubNivelCcService)
        {
            _unitOfWork = unitOfWork;
            _subNivelCcService = SubNivelCcService;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="subNivelCcDTO"> SubNivelCc Jaon</param> 
        /// <returns> SubNivelCcDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        [Authorize]
        public ActionResult Insertar([FromBody] SubNivelCcDTO subNivelCcDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _subNivelCcService.Insertar(subNivelCcDTO, registroClaimToken.UserName);
                return Ok(resultado);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Gilmer Qm
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="subNivelCcDTO"> SubNivelCc Jaon</param> 
        /// <returns> SubNivelCcDTO </returns>
        [Authorize]
        [Route("[Action]/")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] SubNivelCcDTO subNivelCcDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _subNivelCcService.Actualizar(subNivelCcDTO, registroClaimToken.UserName);
                return Ok(resultado);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de Material de Accion
        /// </summary>
        /// <param name="id">Id Material de Accion</param>
        /// <returns> true </returns>
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
                var resultado = _subNivelCcService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros por el filtro
        /// </summary>
        /// <param name="filtroCompuestroGrillaDTO"> filtro para obtener datos </param> 
        /// <returns> FiltroCompuestoGrillaDTO </returns>}
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPorFiltro([FromBody] FiltroKendoGridDTO filtroCompuestroGrillaDTO)
        {
            try
            {
                var subNivelCcListaDTOs = _subNivelCcService.ObtenerPorFiltro(filtroCompuestroGrillaDTO);
                return Ok(new { data = subNivelCcListaDTOs, Total = subNivelCcListaDTOs.FirstOrDefault().Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
