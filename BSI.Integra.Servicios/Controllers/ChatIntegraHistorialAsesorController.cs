using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ChatIntegraHistorialAsesorController
    /// Autor: Jonathan Caipo
    /// Fecha: 18/10/2022
    /// <summary>
    /// Gestión del Historial de Chat Integra de Asesores
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ChatIntegraHistorialAsesorController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ChatIntegraHistorialAsesorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obiene el Hitorial total del chat de un asesor según su idPersonal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerTodoHistorialChatPorAsesor(int idPersonal)
        {
            try
            {
                ChatIntegraHistorialAsesorService chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorService(unitOfWork);
                return Ok(chatIntegraHistorialAsesor.ObtenerTodoHistorialChatPorAsesor(idPersonal));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial chats Por Alumno
        /// </summary>
        /// <returns> List<ChatHistorialAsesor> </returns>
        [Route("[action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerTodoHistorialChatsPorAlumno(int idAlumno)
        {
            try
            {
                ChatIntegraHistorialAsesorService chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorService(unitOfWork);
                return Ok(chatIntegraHistorialAsesor.ObtenerTodoHistorialChatsPorAlumno(idAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
