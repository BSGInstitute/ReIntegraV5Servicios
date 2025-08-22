using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppUsuarioController
    /// Autor: Jorge Gamero.
    /// Fecha: 14/08/2024
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppUsuarioController : ControllerBase
    {
        private IWhatsAppUsuarioService _whatsAppUsuarioService;

        public WhatsAppUsuarioController(IUnitOfWork unitOfWork)
        {
            _whatsAppUsuarioService = new WhatsAppUsuarioService(unitOfWork);
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero.
        /// Fecha: 17/08/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla
        /// </summary>
        /// <param name="WhatsAppDatoUsuarioDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: WhatsAppUsuario </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] WhatsAppDatoUsuarioDTO whatsAppDatoUsuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var whatsappUsuario = new WhatsAppUsuario();
                whatsappUsuario.Id = whatsAppDatoUsuarioDTO.Id;
                whatsappUsuario.IdPersonal = whatsAppDatoUsuarioDTO.IdPersonal;
                whatsappUsuario.RolUser = whatsAppDatoUsuarioDTO.RolUser;
                whatsappUsuario.UserUsername = whatsAppDatoUsuarioDTO.UserUsername;
                whatsappUsuario.UserPassword = whatsAppDatoUsuarioDTO.UserPassword;
                whatsappUsuario.EsMigracion = true;
                whatsappUsuario.Estado = true;
                whatsappUsuario.UsuarioCreacion = whatsAppDatoUsuarioDTO.UsuarioSistema;
                whatsappUsuario.UsuarioModificacion = whatsAppDatoUsuarioDTO.UsuarioSistema;
                whatsappUsuario.FechaCreacion = DateTime.Now;
                whatsappUsuario.FechaModificacion = DateTime.Now;
                whatsappUsuario.IdMigracion = null;
                var resultado = _whatsAppUsuarioService.Add(whatsappUsuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Jorge Gamero.
        /// Fecha: 17/08/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla
        /// </summary>
        /// <param name="WhatsAppUsuarioEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: WhatsAppUsuario </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] WhatsAppUsuarioEntradaDTO whatsAppUsuarioEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var whatsappUsuario = new WhatsAppUsuario();
                whatsappUsuario = _whatsAppUsuarioService.ObtenerPorId(whatsAppUsuarioEntradaDTO.Id.Value);
                whatsappUsuario.IdPersonal = whatsAppUsuarioEntradaDTO.IdPersonal;
                whatsappUsuario.RolUser = whatsAppUsuarioEntradaDTO.RolUser;
                whatsappUsuario.UserUsername = whatsAppUsuarioEntradaDTO.UserUsername;
                whatsappUsuario.UserPassword = whatsAppUsuarioEntradaDTO.UserPassword;
                whatsappUsuario.UsuarioModificacion = whatsAppUsuarioEntradaDTO.UsuarioSistema;
                whatsappUsuario.FechaModificacion = DateTime.Now;
                var resultado = _whatsAppUsuarioService.Update(whatsappUsuario);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
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
                var resultado = _whatsAppUsuarioService.Delete(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaPersonal()
        {
            try
            {
                var rpta = _whatsAppUsuarioService.ObtenerListaPersonal();
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCredencialesUsuarios()
        {
            try
            {
                var rpta = _whatsAppUsuarioService.ObtenerCredencialesUsuario();
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}