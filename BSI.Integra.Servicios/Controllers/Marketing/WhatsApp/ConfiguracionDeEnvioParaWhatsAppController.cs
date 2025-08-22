using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class ConfiguracionDeEnvioParaWhatsAppController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ConfiguracionDeEnvioParaWhatsAppController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ConfiguracionDeEnvioParaWhatsAppCreate entidad)
        {

            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork);
                var respuesta = servicio.Add(entidad, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<ConfiguracionDeEnvioParaWhatsAppDTO> listado)
        {

            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork);
                var respuesta = servicio.Add(listado,usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] ConfiguracionDeEnvioParaWhatsAppCreate entidad)
        {
            
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork);
                var respuesta = servicio.Update(entidad,usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new ConfiguracionDeEnvioParaWhatsAppService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
