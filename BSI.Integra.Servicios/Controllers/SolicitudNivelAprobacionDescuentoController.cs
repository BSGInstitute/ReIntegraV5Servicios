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
    /// Autor Modificacion: Jose Vega
    /// Fecha Modificacion: 24/04/2026
    /// <summary>
    /// Gestión de solicitudes de aprobación para tipos de descuento.
    /// Flujo jerárquico: Solicitante -> Coordinador -> Supervisor -> Gerencia.
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

        /// TipoFuncion: POST
        /// Autor: Jose Vega
        /// Fecha: 24/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Supervisor.
        /// Estado actual: 7 (Pendiente Supervisor) -> 8 (Aprobado Supervisor) o 6 (Pendiente Gerencia).
        /// </summary>
        /// <param name="dto">Datos de aprobación con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AprobarSolicitudSupervisor([FromForm] TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                dto.Usuario = registroClaimToken.UserName;

                _tipoDescuentoSolicitudService.AprobarSolicitudSupervisor(dto);

                return Ok(new { mensaje = "Solicitud aprobada por supervisor correctamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jose Vega
        /// Fecha: 24/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Supervisor.
        /// Estado actual: 7 (Pendiente Supervisor) -> 9 (Rechazado Supervisor).
        /// </summary>
        /// <param name="dto">Datos de rechazo con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult RechazarSolicitudSupervisor([FromForm] TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                dto.Usuario = registroClaimToken.UserName;

                _tipoDescuentoSolicitudService.RechazarSolicitudSupervisor(dto);

                return Ok(new { mensaje = "Solicitud rechazada por supervisor correctamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: POST
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Autor Modificacion: Jose Vega
        /// Fecha Modificacion: 24/04/2026
        /// Versión: 1.1
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Coordinador.
        /// Estado actual: 1 (Pendiente Coordinador) -> 2 (Aprobado Coordinador) o 7 (Pendiente Supervisor).
        /// </summary>
        /// <param name="dto">Datos de aprobación con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPost]
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

        /// TipoFuncion: POST
        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Coordinador.
        /// Estado actual: 1 (Pendiente Coordinador) -> 3 (Rechazado Coordinador).
        /// </summary>
        /// <param name="dto">Datos de rechazo con archivo opcional</param>
        /// <returns>Mensaje de éxito</returns>
        [Route("[action]")]
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                // IdsAsesoresFiltro lo resuelve el backend a partir del token; no se acepta del cliente.
                filtro.IdsAsesoresFiltro = null;
                var resultado = _tipoDescuentoSolicitudService.ListarSolicitudes(filtro, registroClaimToken.IdPersonal);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 15/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los estados de solicitud de descuento activos
        /// </summary>
        /// <returns>List TipoDescuentoSolicitudEstadoDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadosSolicitud()
        {
            try
            {
                var resultado = _tipoDescuentoSolicitudService.ObtenerEstadosSolicitud();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(new { mensaje = e.Message });
            }
        }
    }
}
