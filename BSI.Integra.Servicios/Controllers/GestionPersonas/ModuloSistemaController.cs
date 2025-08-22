using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: UsuarioController
    /// Autor: Christian AleX qUISPE Mamani
    /// Fecha: 25/10/2023
    /// <summary>
    /// Gestión de ActivarFur
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ModuloSistemaController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IUnitOfWork _unitOfWork;
        private IModuloSistemaV5Service _moduloSistemaService;
        public ModuloSistemaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _moduloSistemaService = new ModuloSistemaV5Service(unitOfWork);
            _tokenManager = tokenManager;
            _unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 27/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los modulos asociados a un usuario
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [Route("[Action]/{idUsuario}")]
        [HttpGet]
        public IActionResult ObtenerMisModulos(int idUsuario)
        {
            try
            {
                var res = _unitOfWork.ModuloSistemaV5Repository.ObtenerMisModulos(idUsuario);
                return Ok(res);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 27/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de modulos generales
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [Route("[Action]/{idUsuario}")]
        [HttpGet]
        public IActionResult ObtenerListaModulos(int idUsuario)
        {
            try
            {
                var res = _unitOfWork.ModuloSistemaV5Repository.ObtenerListaModulos(idUsuario);
                return Ok(res);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 27/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Asigna modulos
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public IActionResult AsignarModulos(AsignarModuloV5DTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var res = _moduloSistemaService.AsignarModulo(dto, registroClaimToken.UserName);
                return Ok(res);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 27/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Asigna modulos
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public IActionResult DesasignarModulos(AsignarModuloV5DTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var res = _moduloSistemaService.DesasignarModulo(dto, registroClaimToken.UserName);
                return Ok(res);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla
        /// Fecha: 06/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el nombre del módulo por el segmento de su url
        /// </summary>
        /// <returns>ModuloUrlDTO</returns>
        [Authorize]
        [Route("[Action]/{segmento}")]
        [HttpGet]
        public IActionResult ObtenerNombreUrlModulos(string segmento)
        {
            try
            {
                segmento = segmento.Replace("%2F", "/");
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var res = _moduloSistemaService.ObtenerNombreUrlModulos(segmento);
                return Ok(res);
            }
            catch
            {
                throw;
            }
        }
    }
}
