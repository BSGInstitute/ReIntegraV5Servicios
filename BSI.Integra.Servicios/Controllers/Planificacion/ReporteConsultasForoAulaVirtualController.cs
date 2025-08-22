using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ReporteConsultasForoAulaVirtualController
    /// Autor: Edmundo Llaza
    /// Fecha: 2023-07-31
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteConsultasForoAulaVirtualController : Controller
    {
        private IReporteConsultasForoAulaVirtualService _reporteConsultasForoAulaVirtualService;
        public ReporteConsultasForoAulaVirtualController(IUnitOfWork unitOfWork)
        {
            _reporteConsultasForoAulaVirtualService = new ReporteConsultasForoAulaVirtualService(unitOfWork);
        }
        /// Tipo: Put
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-07-31
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de atención de un foro
        /// </summary>
        /// <returns>bool </returns>
        [Authorize]
        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarEstadoAtencionForo([FromBody] EstadoAtencionForoDTO datos)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var actualizacion = _reporteConsultasForoAulaVirtualService.ActualizarEstadoAtencionForo(datos.IdForo, datos.EstadoAtendido, registroClaimToken.UserName);
                return Ok(actualizacion);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo: Post
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el estado para eliminación de un foro
        /// </summary>
        /// <param name="Datos">Datos para actualizar </param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]/{idForoEliminado}")]
        [HttpDelete]
        public IActionResult EliminarForo(int idForoEliminado)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var eliminacion = _reporteConsultasForoAulaVirtualService.EliminarForo(idForoEliminado, registroClaimToken.UserName);
                return Ok(eliminacion);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo: Post
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el estado de abierto o cerrado de un foro
        /// </summary>
        /// <param name="Datos">Datos para actualizar </param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarAperturaForo(AperturaForoDTO Datos)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var apertura = _reporteConsultasForoAulaVirtualService.ActualizarAperturaForo(Datos.IdForo, Datos.EstadoForo, registroClaimToken.UserName);
                return Ok(apertura);

            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Version: 1.0
        /// <summary>
        /// Genera combo programa, docente, curso
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCombosModulo()
        {
            try
            {
                var combo = _reporteConsultasForoAulaVirtualService.ObtenerCombosModulo();
                return Ok(combo);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-07-08
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de consultas foro
        /// </summary>
        /// <param name="filtroReporte">Filtro para el reporte de de consultas foro  </param>
        /// <returns>El reporte retorna una Lista List<ReporteConsultasForoAulaVirtualDTO></returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteConsultasForo([FromBody] ReporteConsultasForoFiltroDTO filtroReporte)
        {
            try
            {
                var reporte = _reporteConsultasForoAulaVirtualService.GenerarReporteConsultasForoAulaVirtual(filtroReporte);
                return Ok(reporte);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Versión: 1.0
		/// <summary>
		/// Actualiza el encargado de revisión del foro 
		/// </summary>
		/// <param name="datos">Datos para acutalizar </param>
		/// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarPersonaRevisionForo(AsignarDocenteDTO datos)
        {
            try
            {

                var idForo = datos.IdForo;
                var idProveedor = datos.IdProveedor;

                var actualiza = _reporteConsultasForoAulaVirtualService.ActualizarPersonaRevisionForo(idForo, idProveedor);
                return Ok(actualiza);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de consultas del deralle del Foro de AulaVirtual.
        /// </summary>
        /// <returns>Retorma una lista List<ReporteConsultasForoDetalleAulaVirtualDTO> </returns>
        [Route("[Action]/{idForoCurso}")]
        [HttpGet]
        public IActionResult ObtenerDetalleForo(int idForoCurso)
        {
            try
            {
                var lista = _reporteConsultasForoAulaVirtualService.ObtenerDetalleForo(idForoCurso);
                return Ok(lista);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        /// Autor: Edmundo llaza
        /// Fecha: 2023-08-08
        /// <summary>
        /// Actualiza el estado para la eliminación de la respuesta de un foro
        /// </summary>
        /// <param name="idForoResp">Datos para actualizar </param>
        /// <returns></returns>
        [Authorize]
        [Route("[Action]/{idForoResp}")]
        [HttpDelete]
        public IActionResult EliminarForoRespuesta(int idForoResp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_reporteConsultasForoAulaVirtualService.EliminarForoRespuesta(idForoResp, registroClaimToken.UserName));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-09
        /// <summary>
        /// Funcion que envia correos para la reasignación de docentes para revisión de foros
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>Bool</returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public IActionResult EnvioCorreoAsignacionForoDocente([FromBody] ForosCorreoDTO datos)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var envioCorreo = _reporteConsultasForoAulaVirtualService.EnvioCorreoAsignacionForoDocente(datos, registroClaimToken.IdPersonal);
                return Ok(envioCorreo);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
