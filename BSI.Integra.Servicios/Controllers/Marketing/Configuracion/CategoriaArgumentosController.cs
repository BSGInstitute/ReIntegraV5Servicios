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
        public IActionResult ObtenerListadoProgramaConfigurado()
        {
            try
            {
                var listado = _categoriaArgumentosService.ObtenerListadoProgramaConfigurado();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CrearProgramaConfigurado(CrearProgramaGeneralConfiguradoDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var resultado = _categoriaArgumentosService.CrearProgramaConfigurado(request, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EliminarProgramaConfigurado([FromBody] int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var resultado = _categoriaArgumentosService.EliminarProgramaConfigurado(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult EditarProgramaConfigurado(EditarProgramaConfiguradoDTO request)
        //{
        //    try
        //    {
        //        var claimsIdentity = User.Identity as ClaimsIdentity;
        //        var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
        //        var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

        //        var listado = _categoriaArgumentosService.EditarProgramaConfigurado(request, usuario);
        //        return Ok(listado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult ObtenerProgramaConfiguradoDetalle(int id)
        {
            try
            {
                var listado = _categoriaArgumentosService.ObtenerProgramaConfiguradoDetalle(id);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AgregarArgumentoPorCategoria(CrearArgumentoPorCategoriaDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = _categoriaArgumentosService.AgregarArgumentoPorCategoria(request, usuario);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult EditarArgumentoPorCategoria(EditarProgramaConfiguradoDTO request)
        //{
        //    try
        //    {
        //        var claimsIdentity = User.Identity as ClaimsIdentity;
        //        var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
        //        var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

        //        var listado = _categoriaArgumentosService.EditarArgumentoPorCategoria(request, usuario);
        //        return Ok(listado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult EliminarArgumentoPorCategoria(EditarProgramaConfiguradoDTO request)
        //{
        //    try
        //    {
        //        var claimsIdentity = User.Identity as ClaimsIdentity;
        //        var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
        //        var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

        //        var listado = _categoriaArgumentosService.EliminarArgumentoPorCategoria(request, usuario);
        //        return Ok(listado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoProgramaGeneralValido()
        {
            try
            {
                var listado = _categoriaArgumentosService.ObtenerListadoProgramaGeneralValido();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("[action]")]
        //public IActionResult ObtenerListadoCategoriaArgumentoPorPrograma()
        //{
        //    try
        //    {
        //        var listado = _categoriaArgumentosService.ObtenerListadoCategoriaArgumentoPorPrograma();
        //        return Ok(listado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

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
