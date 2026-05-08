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

        /// Autor: Humberto Oscata
        /// Fecha: 08/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de programas configurados
        /// </summary>
        /// <returns>Listado de programas configurados</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 07/05/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los programas de pla.T_PGeneral que no tienen argumentos activos configurados aún.
        /// Usado por el modal "Agregar Programa" para poblar el dropdown de selección.
        /// Al seleccionar, el frontend navega directo al detalle SIN hacer POST de creación.
        /// </summary>
        /// <returns>Listado de programas disponibles para configurar</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerProgramasDisponiblesConfigurar()
        {
            try
            {
                var listado = _categoriaArgumentosService.ObtenerProgramasDisponiblesConfigurar();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina un programa configurado y sus detalles configurados
        /// </summary>
        /// <param name="id">id del programa</param>
        /// <returns>Estado eliminacion</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene un el detalle con jerarquia de una programa (programa, categorias y argumentos)
        /// </summary>
        /// <param name="id">Id del programa</param>
        /// <returns>Objeto programa con detalles</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 08/01/2026
        /// Version: 1.0
        /// <summary>
        /// Crea un nuevo registro para argumento pro categoria especifica
        /// </summary>
        /// <param name="request">Cuerpo para crear nuevo argumento</param>
        /// <returns>Estado creacion</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Editar un registro para argumento por categoria especifica
        /// </summary>
        /// <param name="request">Cuerpo para editar argumento</param>
        /// <returns>Estado edicion</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult EditarArgumentoPorCategoria(EditarArgumentoPorCategoriaDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = _categoriaArgumentosService.EditarArgumentoPorCategoria(request, usuario);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro para argumento por categoria especifica
        /// </summary>
        /// <param name="request">Cuerpo para eliminar argumento</param>
        /// <returns>Estado eliminacion</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult EliminarArgumentoPorCategoria(EliminarArgumentoPorCategoriaDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = _categoriaArgumentosService.EliminarArgumentoPorCategoria(request, usuario);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 08/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado programas generales validos para crear registros programa
        /// </summary>
        /// <returns>Listado de programas generasles validos</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de categorias de argumento creadas
        /// </summary>
        /// <returns>Listado de categorias</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Crea una categoria argumento
        /// </summary>
        /// <param name="request">Cuerpo para crear nueva categoria</param>
        /// <returns>Estado Creacion</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Edita una categoria argumento
        /// </summary>
        /// <param name="request">Cuerpo para edicion de categoria</param>
        /// <returns>Estado edicion</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina una categoria argumento
        /// </summary>
        /// <param name="id">id de la categoria</param>
        /// <returns>Estado eliminacion</returns>
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
