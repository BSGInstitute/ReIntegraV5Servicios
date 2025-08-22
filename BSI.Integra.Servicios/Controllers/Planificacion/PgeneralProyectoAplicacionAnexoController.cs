using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: PgeneralProyectoAplicacionAnexoController
    /// Autor Creacion: Gilmer Qm.
    /// Fecha: 14/06/2023
    /// <summary>
    /// Gestión de PgeneralProyectoAplicacionAnexo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PgeneralProyectoAplicacionAnexoController : Controller
    {
        private IPgeneralProyectoAplicacionAnexoService _pgeneralProyectoAplicacionAnexoService;
        private ITokenManager _tokenManager;

        public PgeneralProyectoAplicacionAnexoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _pgeneralProyectoAplicacionAnexoService = new PgeneralProyectoAplicacionAnexoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// TipoFuncion: POST
        /// Autor Creacion: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <returns>Objeto<returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] PgeneralProyectoAplicacionAnexoDTO pgeneralProyectoAplicacionAnexoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _pgeneralProyectoAplicacionAnexoService.Insertar(pgeneralProyectoAplicacionAnexoDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor Creacion: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
        /// </summary>
        /// <returns>Objeto<returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Actualizar([FromBody] PgeneralProyectoAplicacionAnexoDTO pgeneralProyectoAplicacionAnexoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _pgeneralProyectoAplicacionAnexoService.Actualizar(pgeneralProyectoAplicacionAnexoDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
		/// Autor: Jonathan Caipo
		/// Fecha: 16/06/2023
		/// Versión: 1.0
		/// <summary>
		/// Trae los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
		/// </summary>
		/// <returns>Lista de objetos<returns>
		[Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerListaPgeneralProyectoAplicacionAnexo(int idPGeneral)
        {
            return Ok(_pgeneralProyectoAplicacionAnexoService.ObtenerListaPgeneralProyectoAplicacionAnexo(idPGeneral));
        }
        /// TipoFuncion: DELETE
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 01/08/2023
		/// Versión: 1.0
		/// <summary>
		/// Elimina un registro por id de la tabla fin.T_PgeneralProyectoAplicacionAnexo
		/// </summary>
		/// <returns>estado eliminacion<returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]/{id}")]
        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_pgeneralProyectoAplicacionAnexoService.Eliminar(id, _tokenManager.UserName));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/07/2021
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns>url archivo<returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult GuardarArchivo(IFormFile file)
        {
            return Ok(_pgeneralProyectoAplicacionAnexoService.GuardarArchivo(file));
        }
    }
}
