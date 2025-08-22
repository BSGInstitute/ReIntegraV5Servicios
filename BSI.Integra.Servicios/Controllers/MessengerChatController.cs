using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MessengerChatController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 19/07/2022
    /// <summary>
    /// Gestión de Chat de Messenger
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MessengerChatController : Controller
    {
        private IUnitOfWork unitOfWork;
        public MessengerChatController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] MessengerChat entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MessengerChatService(unitOfWork);
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
        /// Fecha: 21/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<MessengerChat> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MessengerChatService(unitOfWork);
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
        /// Fecha: 21/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] MessengerChat entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MessengerChatService(unitOfWork);
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
        /// Fecha: 21/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<MessengerChat> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MessengerChatService(unitOfWork);
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
        /// Fecha: 21/06/2022
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
                var servicio = new MessengerChatService(unitOfWork);
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
        /// Fecha: 21/06/2022
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
                var servicio = new MessengerChatService(unitOfWork);
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
        /// Fecha: 21/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MessengerChat
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerMessengerChat")]
        public IActionResult ObtenerMessengerChat()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MessengerChatService(unitOfWork);
                return Ok(servicio.ObtenerMessengerChat());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el valor de plantillas relacionadas a Messenger.
        /// </summary>
        /// <param name="nombrePlantillaBase">Nombre de Plantilla Base</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPlantillaMessengerParaAgenda")]
        public IActionResult ObtenerPlantillaMessengerParaAgenda()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaClaveValorService(unitOfWork);
                var PlantillaMessenger = servicio.ObtenerPlantillaPorNombrePlantillaBase("Facebook - Messenger").OrderBy(p => p.Nombre);
                var PlantillaComentarios = servicio.ObtenerPlantillaPorNombrePlantillaBase("Facebook - Comentarios").OrderBy(p => p.Nombre);

                return Ok(new { PlantillaMessenger, PlantillaComentarios });
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
        /// Obtiene el valor de plantillas relacionadas a Messenger.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="idAlumno"></param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerHistorialChatPorPersonal/{idPersonal}/{idAlumno}")]
        public ActionResult ObtenerHistorialChatPorPersonal(int idPersonal, int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MessengerChatService(unitOfWork);
                return Ok(servicio.ObtenerHistorialMessengerChatPorPersonal(idPersonal, idAlumno));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 11/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Valida si el Alumno existe
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ValidarAlumnoExiste([FromBody] AlumnoAutocompleteEmailDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService servicioAlumno = new AlumnoService(unitOfWork);
                var resultado = servicioAlumno.ValidarAlumnoExiste(filtro);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la plantilla de Messenger
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenterPlantillaMessenger()
        {
            try
            {
                PlantillaClaveValorService servicioPlantillaClaveValor = new PlantillaClaveValorService(unitOfWork);
                var plantillaMessenger = servicioPlantillaClaveValor.ObtenerPlantillaPorPlantillaBase("Facebook - Messenger").OrderBy(w => w.Nombre);
                var plantillaComentarios = servicioPlantillaClaveValor.ObtenerPlantillaPorPlantillaBase("Facebook - Comentarios").OrderBy(w => w.Nombre);

                return Ok(new { plantillaMessenger, plantillaComentarios });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Centro Costo
        /// </summary>
        /// <param name="Filtros"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoAutocomplete([FromBody] Dictionary<string, string>? filtro)
        {
            try
            {
                if (filtro != null)
                {
                    CentroCostoService servicioCentroCosto = new CentroCostoService(unitOfWork);
                    return Ok(servicioCentroCosto.ObtenerAutocompleteConPGeneral(filtro["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos por filtro de Contacto Oportunidad
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDatosFiltroContactoOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nombreOrigen = "LANPERFBK-INB-PV";
                var nombreCategoriaOrigen = "Facebook Inbox";
                PaisService servicioPais = new PaisService(unitOfWork);
                CiudadService servicioCiudad = new CiudadService(unitOfWork);
                TipoDatoService servicioTipoDato = new TipoDatoService(unitOfWork);
                FaseOportunidadService servicioFaseOportunidad = new FaseOportunidadService(unitOfWork);
                CategoriaOrigenService servicioCategoriaOrigen = new CategoriaOrigenService(unitOfWork);
                CargoService servicioCargo = new CargoService(unitOfWork);
                AreaFormacionService servicioAreaFormacion = new AreaFormacionService(unitOfWork);
                AreaTrabajoService servicioAreaTrabajo = new AreaTrabajoService(unitOfWork);
                IndustriaService servicioIndustria = new IndustriaService(unitOfWork);
                OrigenService servicioOrigen = new OrigenService(unitOfWork);

                var CategoriaOrigenFacebookInbox = servicioCategoriaOrigen.ObtenerCategoriaOrigenPorNombre("Facebook Inbox").FirstOrDefault();
                var CategoriaOrigenFacebookCorreo = servicioCategoriaOrigen.ObtenerCategoriaOrigenPorNombre("Facebook Correo").FirstOrDefault();
                var CategoriaOrigenFacebookComentarios = servicioCategoriaOrigen.ObtenerCategoriaOrigenPorNombre("Facebook Comentarios").FirstOrDefault();

                ContactoOportunidadDTO ContactoOportunidad = new ContactoOportunidadDTO
                {
                    Paises = servicioPais.ObtenerPaisZonaHoraria().ToList(),
                    Ciudades = servicioCiudad.ObtenerCombo().ToList(),
                    TipoDatoChats = servicioTipoDato.CargarTipoDatoChat().ToList(),
                    FaseOportunidades = servicioFaseOportunidad.ObtenerCombo().ToList(),
                    CategoriaOrigenes = servicioCategoriaOrigen.ObtenerCategoriaOrigenPorNombre(nombreCategoriaOrigen),
                    Cargos = servicioCargo.ObtenerCombo().ToList(),
                    AreasFormacion = servicioAreaFormacion.ObtenerCombo().ToList(),
                    AreasTrabajo = servicioAreaTrabajo.ObtenerCombo().ToList(),
                    Industrias = servicioIndustria.ObtenerCombo().ToList(),
                    Origenes = servicioOrigen.ObtenerOrigenPorCategoriaOrigen(CategoriaOrigenFacebookInbox != null ? CategoriaOrigenFacebookInbox.Id : 0, CategoriaOrigenFacebookCorreo != null ? CategoriaOrigenFacebookCorreo.Id : 0, CategoriaOrigenFacebookComentarios != null ? CategoriaOrigenFacebookComentarios.Id : 0)
                };

                return Ok(ContactoOportunidad);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el chat de Messenger
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        [Route("[Action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerMessengerChat(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MessengerChatService servicioMessengerChat = new MessengerChatService(unitOfWork);
                var ListaChatMessengerEnviado = servicioMessengerChat.ObtenerMessengerChatEnviadoPorPersonal(idPersonal);
                var ListaChatMessengerRecibido = servicioMessengerChat.ObtenerMessengerChatRecibidoPorPersonal(idPersonal);

                return Ok(new { ListaChatMessengerEnviado, ListaChatMessengerRecibido });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2022
        /// <summary>z
        /// Obtiene todo combo por alumno
        /// </summary>
        /// <param name="combo"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerTodoComboAlumno([FromBody] Dictionary<string, string> combo)
        {
            try
            {
                if (combo != null)
                {
                    AlumnoService servicioAlumno = new AlumnoService(unitOfWork);
                    var alumno = servicioAlumno.ObtenerTodoComboAutoComplete(combo["valor"]);
                    return Ok(alumno);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2022
        /// <summary>
        /// Obtiene datos de los alumnos por Email
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDatosAlumnoPorEmail([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro != null)
                {

                    AlumnoService servicioAlumno = new AlumnoService(unitOfWork);
                    var Alumno = servicioAlumno.AlumnnosTodoComboAutoCompletePorEmail(filtro["valor"]);
                    return Ok(Alumno);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el chat de Messenger por el Id del alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns></returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerAlumnosMessengerPorId(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var alumnoService = new AlumnoService(unitOfWork);
                var alumno = alumnoService.ObtenerAlumnoInformacionMessengerChatPorId(idAlumno);
                return Ok(alumno);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo combo de referido
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerTodoFiltroIdReferido([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro != null)
                {
                    AlumnoService servicioAlumno = new AlumnoService(unitOfWork);
                    var alumno = servicioAlumno.ObtenerTodoFiltroAutoCompleteReferido(int.Parse(filtro["valor"]));
                    return Ok(alumno);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

        }

        //Chat messenger


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarMensajeEnviado(MessengerMensajeEnviadoDTO datos)
        {
            try
            {

                MessengerChatService servicio = new MessengerChatService(unitOfWork);
                var messenger = servicio.InsertarMensajeEnviado(datos);
                return Ok(messenger);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarMensajeRecibido(MessengerMensajeRecibidoDTO datos)
        {
            try
            {
                MessengerChatService servicio = new MessengerChatService(unitOfWork);
                var messenger = servicio.InsertarMensajeRecibido(datos);
                return Ok(messenger);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarEventMensaje(EventoMensajeMessengerDTO datos)
        {
            try
            {
                MessengerChatService servicio = new MessengerChatService(unitOfWork);
                var messenger = servicio.InsertarEventMensaje(datos);
                return Ok(messenger);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
