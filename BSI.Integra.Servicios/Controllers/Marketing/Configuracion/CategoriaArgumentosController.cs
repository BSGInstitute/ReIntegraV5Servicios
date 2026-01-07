using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Configuracion;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Marketing.Configuracion
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class CategoriaArgumentosController : ControllerBase
    {
        private readonly ICategoriaArgumentosService _categoriaArgumentosService;

        public CategoriaArgumentosController(ICategoriaArgumentosService categoriaArgumentosService)
        {
            _categoriaArgumentosService = categoriaArgumentosService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoCategoriaArgumento()
        {
            try
            {
                var listado = _categoriaArgumentosService.ObtenerListadoCategoriaArgumento();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CrearCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = _categoriaArgumentosService.CrearCategoriaArgumento(request, usuario);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EditarCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = _categoriaArgumentosService.EditarCategoriaArgumento(request, usuario);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EliminarCategoriaArgumento([FromBody] int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var resultado = _categoriaArgumentosService.EliminarCategoriaArgumento(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
