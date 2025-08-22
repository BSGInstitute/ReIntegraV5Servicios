using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class FurTipoSolicitudController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public FurTipoSolicitudController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [Route("agregar")]
        [HttpPost]
        public IActionResult Add([FromBody] FurTipoSolicitudDTO entidad)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                return Ok(new FurTipoSolicitudService(unitOfWork).Add(entidad,usuario));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("actualizar")]
        [HttpPut]
        public IActionResult Update([FromBody] TFurTipoSolicitudDTOV2 entidad)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                return Ok(new FurTipoSolicitudService(unitOfWork).Update(entidad,usuario));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("eliminar/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                return Ok(new FurTipoSolicitudService(unitOfWork).Delete(id, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("agregar/lista")]
        [HttpPost]
        public IActionResult Add([FromBody] IEnumerable<FurTipoSolicitudDTO> listadoEntidad)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                return Ok(new FurTipoSolicitudService(unitOfWork).Add(listadoEntidad,usuario));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("actualizar/lista")]
        [HttpPut]
        public IActionResult Update([FromBody] IEnumerable<TFurTipoSolicitudDTO> listadoEntidad)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                return Ok(new FurTipoSolicitudService(unitOfWork).Update(listadoEntidad, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("eliminar/Lista")]
        [HttpDelete]
        public IActionResult Delete([FromBody]IEnumerable<int> listadoIds)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                return Ok(new FurTipoSolicitudService(unitOfWork).Delete(listadoIds, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("Obtener/porId/{id}")]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                return Ok(new FurTipoSolicitudService(unitOfWork).ObtenerPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("Obtener/todos")]
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            try
            {
                return Ok(new FurTipoSolicitudService(unitOfWork).ObtenerTodos());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("Obtener/PorTexto/{texto}")]
        [HttpGet]
        public IActionResult ObtenerPorTexto(string texto)
        {
            try
            {
                return Ok(new FurTipoSolicitudService(unitOfWork).ObtenerPorTexto(texto));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
