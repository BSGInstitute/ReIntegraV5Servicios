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
        /// Tipo Función: GET
        /// Autor: Christopher Sandy D'Paris
        /// Fecha: 05/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener Cantidad de alumnos que confirmaron su particapacion en el webinar
        /// </summary>
        /// <returns></returns>
        [Route("[action]/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerCantidadAlumnosConfirmadosWebinar(int IdPEspecifico)
        {
            try
            {
                return Ok(true);
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
        /// Confirma Asistencia de alumno a el webinar seleccionado
        /// </summary>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult AsistenciaOriginal([FromBody] WebinarAlumnoAsistenciaDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _AsistenciaWebinarService.AsistenciaWebinar(filtro);
                var response = new
                {
                    Mensaje = resultado,
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
                    IdMatriculaCabecera = filtro.IdMatriculaCabecera,
                    IdPEspecificoSesion = filtro.IdPEspecificoSesion,
                    Estado = filtro.EstadoAsistencia,
                    Mensaje = resultado
                });

                var response = new
                {
                    Mensaje = resultado,
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
    }
}
