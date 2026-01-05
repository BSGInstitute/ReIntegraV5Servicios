using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

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
        private ITokenManager _tokenManager;
        private IUnitOfWork unitOfWork;
        private readonly IAsistenciaWebinarService _AsistenciaWebinarService;
        public AsistenciaWebinarController(IUnitOfWork unitOfWork)
        {
            _AsistenciaWebinarService = new AsistenciaWebinarService(unitOfWork);
            this.unitOfWork = unitOfWork;
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

                NotificarAsistenciaWebinarWS(filtro.EstadoAsistencia, "resultado");

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

        public async void NotificarAsistenciaWebinarWS(bool EstadoAsistencia, string participante)
        {
            try
            {
                //var url2 = "https://integrav4-signalrcore.bsginstitute.com/";
                var url2 = "https://localhost:7120/";

                var connection = new HubConnectionBuilder()
                .WithUrl(url2 + "hubIntegraHub?idUsuario=WebHook&&usuarioNombre=WebHook&&rooms=''")
                .Build();
                await connection.StartAsync();
                await connection.InvokeAsync("AsistenciaRegistradaWebinar", EstadoAsistencia, participante);
            }
            catch (Exception ex)
            {
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
                var rpta = _AsistenciaWebinarService.CancelarWebinar(body, _tokenManager.UserName);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
