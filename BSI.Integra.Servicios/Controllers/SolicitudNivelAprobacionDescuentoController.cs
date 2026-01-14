using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudNivelAprobacionDescuentoController
    /// Autor: Lolo Zaa
    /// Fecha: 12/01/2026
    /// <summary>
    /// Gestión de solicitudes de aprobación para tipos de descuento
    /// Flujo: Solicitante -> Coordinador -> Gerencia
    /// </summary>
    [Route("api/SolicitudNivelAprobacionDescuento")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class SolicitudNivelAprobacionDescuentoController : ControllerBase
    {
        private readonly ITipoDescuentoSolicitudService _tipoDescuentoSolicitudService;

        public SolicitudNivelAprobacionDescuentoController(ITipoDescuentoSolicitudService tipoDescuentoSolicitudService)
        {
            _tipoDescuentoSolicitudService = tipoDescuentoSolicitudService;
        }

        /// TipoFuncion: POST
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Versión: 1.1
        /// <summary>
        /// Inserta una nueva solicitud de aprobación para tipo de descuento
        /// </summary>
        /// <param name="solicitud">Datos de la solicitud con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarSolicitud([FromForm] TipoDescuentoSolicitudEntradaDTO solicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                solicitud.Usuario = registroClaimToken.UserName;

                _tipoDescuentoSolicitudService.InsertarSolicitud(solicitud);

                return Ok(new { mensaje = "Solicitud creada exitosamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: GET
        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las solicitudes de aprobación de tipos de descuento
        /// </summary>
        /// <returns>List TipoDescuentoSolicitudListadoDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodasSolicitudes()
        {
            try
            {
                var resultado = _tipoDescuentoSolicitudService.ObtenerTodasSolicitudes();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// TipoFuncion: PUT
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Coordinador
        /// Estado: Pendiente -> Aceptado Coordinador
        /// </summary>
        /// <param name="dto">Datos de aprobación con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult AprobarSolicitudCoordinador([FromForm] TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                dto.Usuario = registroClaimToken.UserName;

                _tipoDescuentoSolicitudService.AprobarSolicitudCoordinador(dto);

                return Ok(new { mensaje = "Solicitud aprobada por coordinador correctamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: PUT
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Coordinador
        /// Estado: Pendiente -> Rechazado Coordinador
        /// </summary>
        /// <param name="dto">Datos de rechazo con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult RechazarSolicitudCoordinador([FromForm] TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                dto.Usuario = registroClaimToken.UserName;

                _tipoDescuentoSolicitudService.RechazarSolicitudCoordinador(dto);

                return Ok(new { mensaje = "Solicitud rechazada por coordinador correctamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: PUT
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Gerencia
        /// Estado: Aceptado Coordinador -> Aceptado Gerencia
        /// </summary>
        /// <param name="dto">Datos de aprobación con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult AprobarSolicitudGerencia([FromForm] TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                dto.Usuario = registroClaimToken.UserName;

                _tipoDescuentoSolicitudService.AprobarSolicitudGerencia(dto);

                return Ok(new { mensaje = "Solicitud aprobada por gerencia correctamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: PUT
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Gerencia
        /// Estado: Aceptado Coordinador -> Rechazado Gerencia
        /// </summary>
        /// <param name="dto">Datos de rechazo con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult RechazarSolicitudGerencia([FromForm] TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                dto.Usuario = registroClaimToken.UserName;

                _tipoDescuentoSolicitudService.RechazarSolicitudGerencia(dto);

                return Ok(new { mensaje = "Solicitud rechazada por gerencia correctamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: POST
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Lista solicitudes de descuento con filtros y paginación
        /// </summary>
        /// <param name="filtro">Filtros de búsqueda y paginación</param>
        /// <returns>TipoDescuentoSolicitudPaginadoDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ListarSolicitudes([FromBody] TipoDescuentoSolicitudFiltroDTO filtro)
        {
            try
            {
                var resultado = _tipoDescuentoSolicitudService.ListarSolicitudes(filtro);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }
    }
}
