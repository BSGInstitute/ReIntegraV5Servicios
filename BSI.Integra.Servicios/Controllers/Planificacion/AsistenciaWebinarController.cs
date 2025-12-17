using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Hubs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: AsistenciaWebinarController
    /// Autor: Giancarlo Romero
    /// Fecha: 22/05/2023
    /// <summary>
    /// Gestion de endpoints para la asistencia de webinar
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AsistenciaWebinarController : Controller
    {
        private IUnitOfWork unitOfWork;
        private readonly IHubContext<WebinarHub> _hubContext;
        private readonly IAsistenciaWebinarService _AsistenciaWebinarService;
        public AsistenciaWebinarController(IUnitOfWork unitOfWork, IHubContext<WebinarHub> hubContext)
        {
            _AsistenciaWebinarService = new AsistenciaWebinarService(unitOfWork);
            this.unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }
        
        /// Tipo Función: POST
        /// Autor: Christopher Sandy D'Paris
        /// Fecha: 05/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Confirma Asistencia de alumno a el webinar seleccionado
        /// </summary>
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> Asistencia([FromBody] WebinarAlumnoAsistenciaDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = _AsistenciaWebinarService.AsistenciaWebinar(filtro);

                // Emitir evento SignalR
                await _hubContext.Clients.All.SendAsync("AsistenciaRegistrada", new
                {
                    Estado = filtro.EstadoAsistencia,
                    Response = resultado
                });

                var response = new
                {
                    Response = resultado,
                    EsCorrecto = true
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new
                {
                    Mensaje = e.Message,
                    EsCorrecto = false
                };

                return BadRequest(response);
            }
        }

        /// Tipo Función: POST
        /// Autor: Christopher Sandy D'Paris
        /// Fecha: 05/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Simulacion Confirmacion Webinar Automatica
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ConfirmacionWebinarAutomatica([FromBody] ConfirmacionWebinarAutomaticaDTO body)
        {
            try
            {
                var rpta = _AsistenciaWebinarService.ConfirmacionWebinarAutomatica(body.IdPEspecificoSesion);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Christopher Sandy D'Paris
        /// Fecha: 05/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Simulacion Envio Correo al Cancelar Webinar
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult CancelarWebinarNotificacion([FromBody] CancelarWebinarDTO body)
        {
            try
            {
                var rpta = _AsistenciaWebinarService.CancelarWebinar(body,"ctumir");
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
