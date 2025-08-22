using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
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
    public class UsuarioController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IUsuarioService _usuarioService; 
        public UsuarioController(IUnitOfWork _unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _usuarioService = new UsuarioService(_unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 25/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo roles
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCombo()
        {
            try
            {
                return Ok(_usuarioService.ObtenerCombo());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 25/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo roles
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerTodo()
        {
            try
            {
                return Ok(_usuarioService.ObtenerTodo());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 25/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo roles
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [JwtExpirationValidation]
        [Route("[Action]")]
        [HttpPost]
        public IActionResult InsertarUsuario(IntegraUsuarioDTO dto)
        {
            try
            {
                var res = _usuarioService.InsertarUsuario(dto, _tokenManager.UserName);
                return Ok(res);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 25/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo roles
        /// </summary>
        /// <returns>List<ComboDTO></returns>
        [JwtExpirationValidation]
        [Route("[Action]")]
        [HttpPut]
        public IActionResult ActualizarUsuario(IntegraUsuarioDTO dto)
        {
            try
            {
                var res = _usuarioService.ActualizarUsuario(dto, _tokenManager.UserName);
                return Ok(res);
            }
            catch
            {
                throw;
            }
        }
    }
}
