using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ChatDetalleIntegraController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión de ChatDetalleIntegra
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ChatDetalleIntegraController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ChatDetalleIntegraController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ChatDetalleIntegra entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<ChatDetalleIntegra> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] ChatDetalleIntegra entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<ChatDetalleIntegra> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_ChatDetalleIntegra
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerChatDetalleIntegra")]
        public IActionResult ObtenerChatDetalleIntegra()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                return Ok(servicio.ObtenerChatDetalleIntegra());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_ChatDetalleIntegra para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de chat para pantalla2
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerHistorialChatPortal/{idPersonal}/{idAlumno}")]
        public IActionResult ObtenerHistorialChatPortal(int idPersonal, int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                return Ok(servicio.ObtenerHistorialChatRecibidos(idPersonal, idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion">Id del Personal</param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        [Route("[action]/{idInteraccion}")]
        [HttpGet]
        public ActionResult ObtenerDetalleChatPorIdInteraccion(int idInteraccion)
        {
            try
            {
                var servicioChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);
                return Ok(servicioChatDetalleIntegra.DetalleChatPorIdInteraccion(idInteraccion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion">Id del Personal</param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        [Route("[action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerCursoOportunidad (int idOportunidad)
        {
            try
            {
                var servicioChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);
                return Ok(servicioChatDetalleIntegra.ObtenerCursoOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion">Id del Personal</param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        [Route("[action]/{idAlumno}/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerDetalleChatPorIdAlumno(int idAlumno,int idPGeneral)
        {
            try
            {
                var servicioChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);
                return Ok(servicioChatDetalleIntegra.DetalleChatPorIdAlumno(idAlumno , idPGeneral));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene plantillas de mensajes para chat
        /// </summary>
        /// <param name="IdPlantilla">Id de Plantilla</param>
        /// <returns> Lista de Objeto de tipo (Int, String)</returns>
        [Route("[action]/{idPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerPlantillaChatIntegraSoporte(int idPlantilla)
        {
            try
            {
                PlantillaService servicioPlantilla = new PlantillaService(unitOfWork);
                var lista = servicioPlantilla.ObtenerPlantillaChatIntegraSoporte(idPlantilla).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de chat por IdAlumno para personal Soporte - Validando Mensaje Ofensivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{idAlumno}/{idInteraccion}")]
        [HttpGet]
        public ActionResult ObtenerDetalleChatPorIdInteraccionControlMensajeSoporte(int idAlumno, int idInteraccion)
        {
            try
            {
                var ChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);

                //ACTUALIZAMOS LEIDO A 1
                InteraccionChatIntegraService interaccionChatIntegraService = new InteraccionChatIntegraService(unitOfWork);
                var objetoInteraccion = interaccionChatIntegraService.ObtenerPorId(idInteraccion);
                if (objetoInteraccion != null)
                {
                    objetoInteraccion.Leido = true;
                    objetoInteraccion.NroMensajesSinLeer = 0;
                    objetoInteraccion.FechaModificacion = DateTime.Now;
                    interaccionChatIntegraService.Update(objetoInteraccion);
                }

                return Ok(ChatDetalleIntegra.ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
      


        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 15/07/2024
        /// Versión: 1.0
        /// <returns>Historial Chats soporte </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerHistoriaChatSoporteAtc([FromBody] HistorialChatEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerHistorialChatDetalleIntegra(dto.valor);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Preguntas de evaluación ordenadas por versión de formulario</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPreguntasPorVersionFormulario([FromBody] ObtenerPreguntasRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerPreguntasPorVersionFormulario(dto.IdVersionFormularioEvaluacionChatbot);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Respuestas de evaluación ordenadas por pregunta</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerRespuestasPorPregunta([FromBody] ObtenerRespuestasRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerRespuestasPorPregunta(dto.IdPregunta);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Respuestas de evaluación ordenadas por versión de formulario</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerRespuestasPorVersionFormulario([FromBody] ObtenerPreguntasRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerRespuestasPorVersionFormulario(dto.IdVersionFormularioEvaluacionChatbot);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Preguntas con respuestas incluidas ordenadas por versión de formulario</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPreguntasConRespuestas([FromBody] ObtenerPreguntasRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerPreguntasConRespuestas(dto.IdVersionFormularioEvaluacionChatbot);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 24/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las respuestas del cliente por formulario aplicado
        /// </summary>
        /// <param name="dto">Objeto con el Id del formulario aplicado</param>
        /// <returns>Lista de respuestas del cliente con detalles</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerRespuestasUsuarioPorFormularioAplicado([FromBody] ObtenerRespuestasClienteRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerRespuestasUsuarioPorFormularioAplicado(dto.IdChatbotPortalHiloChat);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Versiones de formulario activas</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerVersionesFormularioActivas()
        {
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerVersionesFormularioActivas();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Tipos de entrada activos</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTiposEntradaActivos()
        {
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerTiposEntradaActivos();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Chat entre chatbot y cliente por IdAlumno</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerChatBotPorAlumno([FromBody] ObtenerChatRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerChatPorAlumno(dto.IdAlumno);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Alexis Arroyo
        /// Fecha: 2026-04-27
        /// Versión: 1.0
        /// <returns>Historial de mensajes WhatsApp ATC por IdAlumno</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerChatBotWhatsAppAtcPorAlumno([FromBody] ObtenerChatRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerChatWhatsAppAtcPorAlumno(dto.IdAlumno);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Chat entre chatbot y cliente por IdAlumno</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerChatBotPorPortalSegmento([FromBody] ObtenerChatRequest2DTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerChatPorPortalSegmento(dto.IdContactoPortalSegmento);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 22/10/2025
        /// Versión: 1.0
        /// <returns>Datos de hilos de chat con información de alumnos y matrículas</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerHilosChatConAlumnos([FromBody] ObtenerHilosConAlumnosPaginadosRequestDTO dto)
        {
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerHilosChatConAlumnos(
                    dto.FechaInicio, dto.FechaFin, dto.PageNumber, dto.PageSize, dto.CodigoMatricula);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Alexis Arroyo
        /// Fecha: 05/05/2026
        /// Versión: 1.0
        /// <returns>Hilos de chat por segmento paginados con filtros opcionales por rango de fecha</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerHilosChatPorSegmentoPaginado([FromBody] ObtenerHilosPorSegmentoPaginadosRequestDTO dto)
        {
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerHilosChatPorSegmentoPaginado(
                    dto.FechaInicio, dto.FechaFin, dto.PageNumber, dto.PageSize);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 22/10/2025
        /// Versión: 1.0
        /// <returns>Hilos de chat por segmento</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerHilosChatPorSegmento()
        {
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerHilosChatPorSegmento();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <returns>Resultado de la inserción completa de la evaluación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarRespuestaEvaluacionCompleta([FromBody] InsertarRespuestaEvaluacionCompletaRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var usuario = User.Identity.Name ?? "Sistema";
                var resultado = servicio.InsertarRespuestaEvaluacionCompleta(dto, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Alexis Arroyo
        /// Fecha: 27/04/2026
        /// Versión: 1.0
        /// <returns>Lista de respuestas del cliente para un hilo de WhatsApp ATC</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerRespuestasUsuarioPorFormularioAplicadoWhatsapp([FromBody] ObtenerRespuestasClienteWhatsappRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerRespuestasUsuarioPorFormularioAplicadoWhatsapp(dto.IdChatbotWhatsAppHiloChat);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Alexis Arroyo
        /// Fecha: 27/04/2026
        /// Versión: 1.0
        /// <returns>Resultado de la inserción completa de la evaluación para WhatsApp ATC</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarRespuestaEvaluacionCompletaWhatsapp([FromBody] InsertarRespuestaEvaluacionCompletaWhatsappRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var usuario = User.Identity.Name ?? "Sistema";
                var resultado = servicio.InsertarRespuestaEvaluacionCompletaWhatsapp(dto, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de chat por IdAlumno para personal Soporte - Validando Mensaje Ofensivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{idMatriculaCabecera}/{userName}")]
        [HttpGet]
        public ActionResult FinalizarChatActividadAtc(int idMatriculaCabecera,string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);
                InteraccionChatIntegraService interaccionChatIntegraService = new InteraccionChatIntegraService(unitOfWork);
                return Ok(ChatDetalleIntegra.FinalizarChatAtc(idMatriculaCabecera, userName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de chat por IdAlumno para personal Soporte - Validando Mensaje Ofensivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{idOportunidad}/{userName}")]
        [HttpGet]
        public ActionResult FinalizarChatActividadComercial(int idOportunidad, string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);
                InteraccionChatIntegraService interaccionChatIntegraService = new InteraccionChatIntegraService(unitOfWork);
                return Ok(ChatDetalleIntegra.FinalizarChatComercial(idOportunidad, userName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de chat por IdAlumno para personal Soporte - Validando Mensaje Ofensivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GetIdUltimaInteraccion(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);
                InteraccionChatIntegraService interaccionChatIntegraService = new InteraccionChatIntegraService(unitOfWork);
                return Ok(ChatDetalleIntegra.GetIdUltimaInteraccion(idMatriculaCabecera));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de chat por IdAlumno para personal Soporte - Validando Mensaje Ofensivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{idAlumno}")]
        [HttpGet]
        public ActionResult GetIdUltimaInteraccionComercial(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ChatDetalleIntegra = new ChatDetalleIntegraService(unitOfWork);
                InteraccionChatIntegraService interaccionChatIntegraService = new InteraccionChatIntegraService(unitOfWork);
                return Ok(ChatDetalleIntegra.GetIdUltimaInteraccionComercial(idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Alexis Arroyo
        /// Fecha: 2026-04-29
        /// Versión: 1.0
        /// <returns>Hilos de chat paginados (Portal + WhatsApp) por alumno desde fecha de corte</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerHilosPaginadosPorAlumno([FromBody] ObtenerHilosPaginadosRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerHilosPaginadosPorAlumno(dto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Alexis Arroyo
        /// Fecha: 2026-05-05
        /// Versión: 1.0
        /// <returns>Hilos de chat paginados (Portal) por segmento (idContactoPortalSegmento)</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerHilosPaginadosPorSegmento([FromBody] ObtenerHilosPaginadosPorSegmentoRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerHilosPaginadosPorSegmento(dto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Alexis Arroyo
        /// Fecha: 2026-05-02
        /// Versión: 1.0
        /// <returns>Solicitudes vinculadas a un hilo de chat (Portal o WhatsApp)</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesPorHiloChat([FromBody] ObtenerSolicitudesPorHiloChatRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerSolicitudesPorHiloChat(dto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla
        /// Fecha: 2024-12-10
        /// Versión: 1.0
        /// <returns>Historial de Chats Academicos Online para persistencia </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerChatsAcademicosHabilitadosCoordinadora([FromBody] HistorialChatEntradaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerChatsAcademicosHabilitadosCoordinadora(dto.valor,dto.esOnline);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Funcion: POST
        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene actividades AONLINE/ONLINE para un alumno y programa
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerActividadesAtc([FromBody] ObtenerActividadesAtcRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerActividadesAtc(dto.IdPEspecifico, dto.IdAlumno);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Funcion: POST
        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Amplia la fecha de entrega de un cuestionario o tarea
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AmpliarFechaEntregaAtc([FromBody] AmpliarFechaEntregaRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.AmpliarFechaEntrega(dto.IdPEspecifico, dto.IdAlumno, dto.IdActividad, dto.Fecha, dto.TipoActividad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Funcion: POST
        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene sesiones con estado de asistencia
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAsistenciaAtc([FromBody] ObtenerAsistenciaAtcRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.ObtenerAsistenciaAtc(dto.IdPEspecifico, dto.IdAlumno);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Funcion: POST
        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Registra justificacion de inasistencia para una sesion
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistrarAsistenciaAtc([FromBody] RegistrarAsistenciaAtcRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ChatDetalleIntegraService(unitOfWork);
                var respuesta = servicio.RegistrarAsistenciaAtc(dto.SesionId, dto.IdAlumno);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
