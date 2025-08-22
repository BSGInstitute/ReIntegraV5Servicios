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
    /// Controlador: WhatsAppConfiguracionApiController
    /// Autor: Jorge Gamero.
    /// Fecha: 19/08/2024
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppConfiguracionApiController : ControllerBase
    {
        private IWhatsAppConfiguracionApiService _whatsAppConfiguracionApiService;

        public WhatsAppConfiguracionApiController(IUnitOfWork unitOfWork)
        {
            _whatsAppConfiguracionApiService = new WhatsAppConfiguracionApiService(unitOfWork);
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero.
        /// Fecha: 17/08/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla
        /// </summary>
        /// <param name="WhatsAppConfiguracionApiEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: WhatsAppConfiguracionApi </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] WhatsAppConfiguracionApiInsertDTO whatsAppConfiguracionApiInsertDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var whatsAppConfiguracionApi = new WhatsAppConfiguracionApi();
                whatsAppConfiguracionApi.Id = whatsAppConfiguracionApiInsertDTO.Id;
                whatsAppConfiguracionApi.Numero = whatsAppConfiguracionApiInsertDTO.Numero;
                whatsAppConfiguracionApi.VName = whatsAppConfiguracionApiInsertDTO.VName;
                whatsAppConfiguracionApi.IdPais = whatsAppConfiguracionApiInsertDTO.IdPais;
                whatsAppConfiguracionApi.Bearer = whatsAppConfiguracionApiInsertDTO.Bearer;
                whatsAppConfiguracionApi.NumeroIndentificador = whatsAppConfiguracionApiInsertDTO.NumeroIndentificador;
                whatsAppConfiguracionApi.VersionApi = whatsAppConfiguracionApiInsertDTO.VersionApi;
                whatsAppConfiguracionApi.FechaExpiracion = whatsAppConfiguracionApiInsertDTO.FechaExpiracion;
                whatsAppConfiguracionApi.EsMigracion = false;
                whatsAppConfiguracionApi.Estado = true;
                whatsAppConfiguracionApi.UsuarioCreacion = whatsAppConfiguracionApiInsertDTO.UsuarioCreacion;
                whatsAppConfiguracionApi.UsuarioModificacion = whatsAppConfiguracionApiInsertDTO.UsuarioModificacion;
                whatsAppConfiguracionApi.FechaCreacion = DateTime.Now;
                whatsAppConfiguracionApi.FechaModificacion = DateTime.Now;
                whatsAppConfiguracionApi.IdMigracion = null;
                whatsAppConfiguracionApi.NumeroWhatsApp = whatsAppConfiguracionApiInsertDTO.NumeroWhatsApp;
                whatsAppConfiguracionApi.CuentaIdentificadorWhatsApp = whatsAppConfiguracionApiInsertDTO.CuentaIdentificadorWhatsApp;
                whatsAppConfiguracionApi.IdPersonalAreaTrabajo = whatsAppConfiguracionApiInsertDTO.IdPersonalAreaTrabajo;
                whatsAppConfiguracionApi.CodigoArea = whatsAppConfiguracionApiInsertDTO.CodigoArea;
                whatsAppConfiguracionApi.IdPersonalAsignado = whatsAppConfiguracionApiInsertDTO.IdPersonal_Asignado;
                var resultado = _whatsAppConfiguracionApiService.Add(whatsAppConfiguracionApi);
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
        /// <param name="WhatsAppConfiguracionApiEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: WhatsAppConfiguracionApi </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] WhatsAppConfiguracionApiEntradaDTO whatsAppConfiguracionApiEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var whatsAppConfiguracionApi = new WhatsAppConfiguracionApi();
                whatsAppConfiguracionApi = _whatsAppConfiguracionApiService.ObtenerPorId(whatsAppConfiguracionApiEntradaDTO.Id.Value);
                whatsAppConfiguracionApi.Numero = whatsAppConfiguracionApiEntradaDTO.Numero;
                whatsAppConfiguracionApi.VName = whatsAppConfiguracionApiEntradaDTO.VName;
                whatsAppConfiguracionApi.IdPais = whatsAppConfiguracionApiEntradaDTO.IdPais;
                whatsAppConfiguracionApi.Bearer = whatsAppConfiguracionApiEntradaDTO.Bearer;
                whatsAppConfiguracionApi.NumeroIndentificador = whatsAppConfiguracionApiEntradaDTO.NumeroIndentificador;
                whatsAppConfiguracionApi.VersionApi = whatsAppConfiguracionApiEntradaDTO.VersionApi;
                whatsAppConfiguracionApi.FechaExpiracion = whatsAppConfiguracionApiEntradaDTO.FechaExpiracion;
                whatsAppConfiguracionApi.FechaExpiracion = whatsAppConfiguracionApiEntradaDTO.FechaExpiracion;
                whatsAppConfiguracionApi.UsuarioModificacion = whatsAppConfiguracionApiEntradaDTO.UsuarioModificacion;
                whatsAppConfiguracionApi.FechaModificacion = DateTime.Now;
                whatsAppConfiguracionApi.NumeroWhatsApp = whatsAppConfiguracionApiEntradaDTO.NumeroWhatsApp;
                whatsAppConfiguracionApi.CuentaIdentificadorWhatsApp = whatsAppConfiguracionApiEntradaDTO.CuentaIdentificadorWhatsApp;
                whatsAppConfiguracionApi.IdPersonalAreaTrabajo = whatsAppConfiguracionApiEntradaDTO.IdPersonalAreaTrabajo;
                whatsAppConfiguracionApi.CodigoArea = whatsAppConfiguracionApiEntradaDTO.CodigoArea;
                whatsAppConfiguracionApi.IdPersonalAsignado = whatsAppConfiguracionApiEntradaDTO.IdPersonal_Asignado;
                var resultado = _whatsAppConfiguracionApiService.Update(whatsAppConfiguracionApi);
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
                var resultado = _whatsAppConfiguracionApiService.Delete(id, registroClaimToken.UserName);
                return Ok(resultado);
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
                var rpta = _whatsAppConfiguracionApiService.ObtenerCredencialesUsuarios();
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}