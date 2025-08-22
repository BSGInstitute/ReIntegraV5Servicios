using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: InteraccionChatIntegraController
    /// Autor: Margiory  Ramirez Neyra.
    /// Fecha: 27/09/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class InteraccionChatIntegraController : Controller
    {
        private IUnitOfWork unitOfWork;
        public InteraccionChatIntegraController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] InteraccionChatIntegra entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new InteraccionChatIntegraService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<InteraccionChatIntegra> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new InteraccionChatIntegraService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] InteraccionChatIntegra entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new InteraccionChatIntegraService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Margiory  Ramirez Neyra.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<InteraccionChatIntegra> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new InteraccionChatIntegraService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 27/09/2022
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
                var servicio = new InteraccionChatIntegraService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 27/09/2022
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
                var servicio = new InteraccionChatIntegraService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: POST
        /// Autor:Margiory Ramirez Neyra.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera registro para Reporte Chact Log 
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ReporteChatLog([FromBody] ChatReporteDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servico = new InteraccionChatIntegraService(unitOfWork);
                var chatLog = servico.GenerarReporteChatLog(data);

                return Ok(chatLog);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// <param name="data"></param>
        /// Obtiene todos los registros guardados e de los filtros Area ,Pais, Centro Costo y Personal
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteChat()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicioAreaCapacitacion = new AreaCapacitacionService(unitOfWork);
                var servicioCentroCosto = new CentroCostoService(unitOfWork);
                var servicioPersonal = new PersonalService(unitOfWork);
                var servicioPais = new PaisService(unitOfWork);


                var areaCapacitacion = servicioAreaCapacitacion.ObtenerCombo();
                var centroCosto = servicioCentroCosto.ObtenerCombo();
                var personal = servicioPersonal.CargarPersonalParaFiltro();
                var pais = servicioPais.ObtenerComboConMoneda();

                return Ok(new
                {
                    areaCapacitacion,
                    centroCosto,
                    personal,
                    pais,

                });


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genrea el registro de ChatAgrupado
        /// </summary>
        /// <param name="data"></param>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ReporteChat([FromBody] ChatReporteDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servico = new InteraccionChatIntegraService(unitOfWork);
                var chat = servico.GenerarReporteChat(data);


                return Ok(chat);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Id del Alumno
        /// </summary>
        /// <param name="idInteraccionChat"></param>
        /// <returns></returns>
        [Route("[action]/{idInteraccionChat}")]
        [HttpGet]
        public ActionResult ActualizarIdAlumno(int idInteraccionChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int? idalumno = null;
                FaseOportunidadService faseOportunidadService = new FaseOportunidadService(unitOfWork);
                InteraccionChatIntegraService interaccionChatIntegraService = new InteraccionChatIntegraService(unitOfWork);
                OportunidadDatosChatDTO oportunidadValores = null;

                var idfaseoportunidadPortal = faseOportunidadService.ObtenerFaseOportunidadPorInteraccionId(idInteraccionChat).IdFaseOportunidadPortal;
                var alumno = unitOfWork.ChatDetalleIntegraRepository.ObtenerPorIntegraChatYRemintente(idInteraccionChat, "visitante");
                var interaccionChatIntegra = interaccionChatIntegraService.ObtenerPorId(idInteraccionChat);
                if (idfaseoportunidadPortal != "00000000-0000-0000-0000-000000000000")
                {
                    oportunidadValores = faseOportunidadService.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(idfaseoportunidadPortal);
                    if (oportunidadValores == null)
                    {
                        oportunidadValores = faseOportunidadService.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(idfaseoportunidadPortal);
                    }

                    interaccionChatIntegra.IdAlumno = oportunidadValores.IdContacto;
                    interaccionChatIntegra.UsuarioModificacion = "SYSTEM";
                    interaccionChatIntegra.FechaModificacion = DateTime.Now;
                    interaccionChatIntegraService.Update(interaccionChatIntegra);
                    return Ok(new { NombreAlumno = string.Concat(oportunidadValores.Nombre1, " ", oportunidadValores.ApellidoPaterno, (string.IsNullOrEmpty(oportunidadValores.ApellidoMaterno) == true ? "" : " " + oportunidadValores.ApellidoMaterno)), IdAlumno = oportunidadValores.IdContacto });
                }
                else
                {
                    var nombreAlumno = "";
                    interaccionChatIntegra.IdAlumno = idalumno;
                    interaccionChatIntegra.UsuarioModificacion = "SYSTEM";
                    interaccionChatIntegra.FechaModificacion = DateTime.Now;
                    interaccionChatIntegraService.Update(interaccionChatIntegra);
                    if (alumno != null)
                    {
                        nombreAlumno = alumno.IdRemitente;
                    }
                    return Ok(new { NombreAlumno = nombreAlumno, IdAlumno = idalumno });
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}












