using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Configuracion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.EsquemaRespuestas;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Marketing.EsquemaRespuestas
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class EsquemaRespuestasController : ControllerBase
    {
        private readonly IEsquemaRespuestasService _esquemaRespuestasService;
        private readonly IConfiguracionExternaService _configuracionExternaService;

        public EsquemaRespuestasController(
            IEsquemaRespuestasService esquemaRespuestasService,
            IConfiguracionExternaService configuracionExternaService)
        {
            _esquemaRespuestasService      = esquemaRespuestasService;
            _configuracionExternaService   = configuracionExternaService;
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna el detalle completo de un esquema por su Id: lecturas, mensajes exactos,
        /// interpretaciones, subcategorias, fases, perfiles y respuestas.
        /// </summary>
        /// <param name="id">Id del esquema</param>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> ObtenerEsquemaPorId(int id)
        {
            try
            {
                var result = await _esquemaRespuestasService.ObtenerEsquemaPorIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna todos los esquemas activos con sus datos principales y totales calculados
        /// de lecturas y bloques de interpretacion.
        /// </summary>
        /// <returns>Listado de esquemas activos</returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ObtenerListadoEsquemas()
        {
            try
            {
                var listado = await _esquemaRespuestasService.ObtenerListadoEsquemasAsync();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Borrado logico en cascada de un esquema y todos sus registros hijo.
        /// </summary>
        /// <param name="id">Id del esquema a eliminar</param>
        /// <returns>Id del esquema eliminado</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult EliminarEsquema([FromBody] int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var resultado = _esquemaRespuestasService.EliminarEsquema(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 18/03/2026
        /// Version: 1.0
        /// <summary>
        /// Upsert de numero WhatsApp en T_AsistenteMarketingWhatsAppAsignacion.
        /// Si el numero no existe en conf.T_WhatsAppConfiguracionApi retorna error.
        /// Si ya existe en ia → actualiza EsquemaRespuesta y Estado.
        /// Si no existe en ia → inserta el registro completo.
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public IActionResult UpsertAsistenteWhatsAppAsignacion([FromBody] UpsertAsistenteWhatsAppAsignacionDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var claimsIdentity     = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario            = _respuestaCorrecta.RegistroClaimToken.UserName;

                var id = _esquemaRespuestasService.UpsertAsistenteWhatsAppAsignacion(request, usuario);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 18/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna el catalogo activo de numeros WhatsApp para asignacion al esquema chatbot.
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoNumero()
        {
            try
            {
                var listado = _esquemaRespuestasService.ObtenerListadoNumero();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna el catalogo activo de fases para asignacion a subcategorias del esquema chatbot.
        /// </summary>
        /// <returns>Listado de fases activas</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoFase()
        {
            try
            {
                var listado = _esquemaRespuestasService.ObtenerListadoFase();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna el catalogo activo de mensajes exactos utilizados como criterio de filtrado del BOT.
        /// </summary>
        /// <returns>Listado de mensajes exactos activos</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoMensajeExacto()
        {
            try
            {
                var listado = _esquemaRespuestasService.ObtenerListadoMensajeExacto();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Retorna el catalogo activo de perfiles de contacto para asignacion a subcategorias del esquema chatbot.
        /// </summary>
        /// <returns>Listado de perfiles activos</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoPerfil()
        {
            try
            {
                var listado = _esquemaRespuestasService.ObtenerListadoPerfil();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta un esquema completo (lecturas, mensajes exactos, interpretaciones,
        /// subcategorias y respuestas) de forma atomica. El frontend envia el JSON
        /// y el backend orquesta los inserts con una unica transaccion SQL.
        /// </summary>
        /// <param name="request">Configuracion completa del esquema</param>
        /// <returns>Id del esquema creado</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> InsertarEsquema([FromBody] CrearEsquemaRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var idEsquema = _esquemaRespuestasService.InsertarEsquema(request, usuario);
                await _configuracionExternaService.SincronizarEsquemaInteraccionAsync(idEsquema);
                return Ok(idEsquema);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza un esquema existente de forma atomica.
        /// Limpia las tablas N:M, realiza borrado logico de entidades hijas
        /// y reinsertar la nueva configuracion completa en una sola transaccion.
        /// </summary>
        /// <param name="request">Nueva configuracion completa del esquema con su Id</param>
        /// <returns>Id del esquema actualizado</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ActualizarEsquema([FromBody] ActualizarEsquemaRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var idEsquema = _esquemaRespuestasService.ActualizarEsquema(request, usuario);
                await _configuracionExternaService.SincronizarEsquemaInteraccionAsync(idEsquema);
                return Ok(idEsquema);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
