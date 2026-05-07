using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FeriadoController
    /// Autor: aarroyoh
    /// Fecha: 06/05/2026
    /// <summary>
    /// CRUD de feriados (pla.T_Feriado) y consulta combinada por país (pla.V_FeriadoConPais).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FeriadoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IFeriadoService _feriadoService;

        public FeriadoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _feriadoService = new FeriadoService(unitOfWork);
        }

        /// Tipo Función: GET
        /// <summary>Lista todos los feriados activos.</summary>
        /// <returns>IEnumerable<FeriadoDTO></returns>
        [HttpGet("Listar")]
        [Authorize]
        public IActionResult Listar()
        {
            return Ok(_feriadoService.Listar());
        }

        /// Tipo Función: GET
        /// <summary>Obtiene un feriado por id.</summary>
        /// <returns>FeriadoDTO</returns>
        [HttpGet("ObtenerPorId/{id}")]
        [Authorize]
        public IActionResult ObtenerPorId(int id)
        {
            return Ok(_feriadoService.ObtenerPorId(id));
        }

        /// Tipo Función: GET
        /// <summary>Lista feriados activos cruzados con país, filtrando por uno o varios IdTroncalPais.</summary>
        /// <returns>IEnumerable<FeriadoConPaisDTO></returns>
        /// <example>GET api/Feriado/ListarPorPaises?idsTroncalPais=1&idsTroncalPais=2</example>
        [HttpGet("ListarPorPaises")]
        [Authorize]
        public IActionResult ListarPorPaises([FromQuery] int[] idsTroncalPais)
        {
            return Ok(_feriadoService.ListarPorPaises(idsTroncalPais));
        }

        /// Tipo Función: POST
        /// <summary>Crea un nuevo feriado.</summary>
        /// <returns>FeriadoDTO creado (con Id).</returns>
        [HttpPost("Insertar")]
        [Authorize]
        public IActionResult Insertar([FromBody] FeriadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_feriadoService.Insertar(dto, registroClaimToken.UserName));
        }

        /// Tipo Función: PUT
        /// <summary>Actualiza un feriado existente.</summary>
        /// <returns>FeriadoDTO actualizado.</returns>
        [HttpPut("Actualizar")]
        [Authorize]
        public IActionResult Actualizar([FromBody] FeriadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_feriadoService.Actualizar(dto, registroClaimToken.UserName));
        }

        /// Tipo Función: DELETE
        /// <summary>Elimina (soft delete) un feriado por id.</summary>
        /// <returns>bool</returns>
        [HttpDelete("Eliminar/{id}")]
        [Authorize]
        public IActionResult Eliminar(int id)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_feriadoService.Eliminar(id, registroClaimToken.UserName));
        }

        /// Tipo Función: GET
        /// <summary>Combo de TroncalCiudad activos para alimentar dropdowns del CRUD de feriados.</summary>
        /// <returns>IEnumerable<ComboDTO></returns>
        [HttpGet("ComboTroncalCiudad")]
        [Authorize]
        public IActionResult ComboTroncalCiudad()
        {
            return Ok(_feriadoService.ComboTroncalCiudad());
        }

        /// Tipo Función: GET
        /// <summary>Combo de paises activos (conf.T_Pais). El IdTroncalPais en otras tablas apunta a este maestro.</summary>
        /// <returns>IEnumerable<ComboDTO></returns>
        [HttpGet("ComboTroncalPais")]
        [Authorize]
        public IActionResult ComboTroncalPais()
        {
            return Ok(_feriadoService.ComboTroncalPais());
        }
    }
}
