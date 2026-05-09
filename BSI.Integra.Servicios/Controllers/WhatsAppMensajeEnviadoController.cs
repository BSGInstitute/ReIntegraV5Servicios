using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Claims;
using BSI.Integra.Servicios.Helpers;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using Nancy.Diagnostics;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppMensajeEnviadoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión de WhatsAppMensajeEnviado
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppMensajeEnviadoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        public WhatsAppMensajeEnviadoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this._unitOfWork = unitOfWork;
            _tokenManager=tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] WhatsAppMensajeEnviado entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
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
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<WhatsAppMensajeEnviado> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
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
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] WhatsAppMensajeEnviado entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
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
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<WhatsAppMensajeEnviado> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
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
        /// Fecha: 11/06/2022
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
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
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
        /// Fecha: 11/06/2022
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
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
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
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_WhatsAppMensajeEnviado
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerWhatsAppMensajeEnviado")]
        public IActionResult ObtenerWhatsAppMensajeEnviado()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                return Ok(servicio.ObtenerWhatsAppMensajeEnviado());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_WhatsAppMensajeEnviado para combo.
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
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
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
        /// Obtiene historial de mensajes recibidos a chat de asesor
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [HttpGet("HistorialMensajeRecibidosChat/{idPersonal}/{numero}/{area}")]
        public IActionResult HistorialMensajeRecibidosChat(int idPersonal, string numero, string area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idPersonal == null)
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.HistorialChatsRecibido(idPersonal, numero, area);
                if (resultado == null)
                {
                    return BadRequest("Error: Sin Datos");
                }
                return Ok(resultado);
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
        /// Obtiene historial de mensajes recibidos a chat de asesor
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [HttpGet("WhatsAppHistorialMensajeChat/{idPersonal}/{numero}/{area}")]
        public IActionResult WhatsAppHistorialMensajeChat(int idPersonal, string numero, string area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idPersonal == null)
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.ListaHistorialMensajeChat(idPersonal, numero, area);
                if (resultado == null)
                {
                    return BadRequest("Error: Sin Datos");
                }
                return Ok(resultado);
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
        /// Obtiene historial de mensajes recibidos a chat de asesor
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [HttpGet("WhatsAppHistorialMensajeChatOperaciones/{idPersonal}/{numero}/{area}")]
        public IActionResult WhatsAppHistorialMensajeChatOperaciones(int idPersonal, string numero, string area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idPersonal == null)
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.ListaHistorialMensajeChatOperaciones(idPersonal, numero, area);
                if (resultado == null)
                {
                    return BadRequest("Error: Sin Datos");
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 18/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de mensajes recibidos a chat de asesor desde el portal web
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <param name="idAlumno"> Area </param>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [HttpGet("PortalWebHistorialMensajeChatOperaciones/{idPersonal}/{numero}/{area}/{idAlumno}")]
        public IActionResult PortalWebHistorialMensajeChatOperaciones(int idPersonal, string numero, string area, int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idPersonal == null)
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {

                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.ListaHistorialPortalWebMensajeChatOperaciones(idPersonal, numero, area,idAlumno);
                if (resultado == null)
                {
                    return BadRequest("Error: Sin Datos");
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de mensajes recibidos a chat de asesor de atencion al cliente
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <param name="idPais"> Id de Pais </param>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [HttpGet("WhatsAppHistorialMensajeChatAtc/{idPersonal}/{numero}/{area}/{idPais}")]
        public IActionResult WhatsAppHistorialMensajeChatAtc(int idPersonal, string numero, string area, string idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idPersonal == null)
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.ListaHistorialMensajeChatAtc(idPersonal, numero, area, idPais);
                if (resultado == null)
                {
                    return BadRequest("Error: Sin Datos");
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeRecibidosChat(int IdPersonal)
        {
            if (IdPersonal != null)
            {
                try
                {
                    var servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var restultado = servicioWhatsappMsjEnv.ListaUltimoMensajeChatsRecibido(IdPersonal);
                    if (restultado != null)
                    {
                        return Ok(restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo mensaje recibido de WhatsApp por asesor.
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeEnviadosChat(int IdPersonal)
        {
            if (IdPersonal != null)
            {
                try
                {
                    var servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var restultado = servicioWhatsappMsjEnv.ListaUltimoMensajeChatsEnviado(IdPersonal);

                    if (restultado != null)
                    {
                        return Ok(restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesControlOfensivoDTO>  </returns>
        [Route("[action]/{idPersonal}/{numero}/{area}")]
        [HttpGet]
        public ActionResult WhatsAppHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area)
        {
            if (idPersonal != null)
            {
                try
                {
                    WhatsAppMensajeEnviadoService objetoMensaje = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var resultado = objetoMensaje.ListaHistorialMensajeChatControlMensaje(idPersonal, numero, area);

                    if (resultado != null)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Optiene oportunidad por asesor y alumno.
        /// </summary>
        /// <returns> Objeto OportunidadDatosChatWhatsAppDTO </returns>
        [Route("[action]/{idPersonal}/{idAlumno}/{numero}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadPorAsesorYAlumno(int idPersonal, int idAlumno, string numero)
        {
            try
            {
                OportunidadService servicioWhatsAppMensajeEnviado = new OportunidadService(_unitOfWork);
                var oportunidad = servicioWhatsAppMensajeEnviado.ObtenerOportunidadPorAsesorYAlumno(idAlumno, idPersonal, numero);

                return Ok(new { respuesta = oportunidad != null ? true : false, oportunidad });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{plantilla}/{numero}")]
        [HttpGet]
        public ActionResult ValidarPlantillasEnviadas(string plantilla, string numero)
        {
            //Marketing
            try
            {
                WhatsAppMensajeEnviadoService servicioWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioWhatsAppMensajeEnviado.ValidarPlantillasEnviadas(plantilla, numero);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 10/05/2021
        /// Versión: 1.0 
        /// <summary>
        /// Validar numero libre.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{numero}/{idPais}/{idCentroCosto}/{idPersonal}")]
        [HttpGet]
        public ActionResult ValidarNumeroLibre(string numero, int idPais, int idCentroCosto, int idPersonal)
        {
            try
            {
                WhatsAppMensajeEnviadoService servicioMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioMensajeEnviado.ObtenerRespuestaValidarNumeroLibreCompleto(numero, idPais, idCentroCosto, idPersonal);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de personal.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{numero}/{idCentroCosto}/{idPais}")]
        [HttpGet]
        public ActionResult ObtenerPersonalConfiguracion(string numero, int idCentroCosto, int idPais)
        {
            try
            {
                AsesorChatService servicioConfiguracion = new AsesorChatService(_unitOfWork);
                var oportunidad = servicioConfiguracion.ObtenerPersonalConfiguracion(numero, idCentroCosto, idPais);
                return Ok(oportunidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de personal.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{Objeto}")]
        [HttpGet]
        public ActionResult AlertaDeObtjeto(string Objeto)
        {
            try
            {
                AsesorChatService servicioConfiguracion = new AsesorChatService(_unitOfWork);
                var oportunidad = servicioConfiguracion.NotificarError(Objeto);
                return Ok(oportunidad);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de personal.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{Celular}")]
        [HttpGet]
        public ActionResult BuscarAlumnoPorWebHook(string Celular)
        {
            try
            {
                AsesorChatService servicioConfiguracion = new AsesorChatService(_unitOfWork);
                var PersonalAlumno = servicioConfiguracion.BuscarAlumnoPorWebHook(Celular);
                return Ok(PersonalAlumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppMensaje([FromBody] WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO)
        {
            var whatsAppDesuscritoService = new WhatsAppDesuscritoService(_unitOfWork);
            if (whatsAppEnviarMensajeDTO != null)
            {
                string Celular = "";
                if (whatsAppEnviarMensajeDTO.IdPais == 51)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 57)
                {
                    Celular = "00" + whatsAppEnviarMensajeDTO.WaTo;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 591)
                {
                    Celular = "00" + whatsAppEnviarMensajeDTO.WaTo;
                }
                else
                {
                    Celular = "00" + whatsAppEnviarMensajeDTO.WaTo;
                }
                if (!whatsAppDesuscritoService.ExistePorNumeroTelefono(Celular))
                {
                    bool banderaLogin = false;
                    string _tokenComunicacion = string.Empty;

                    try
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        var whatsAppConfiguracionService = new WhatsAppConfiguracionService(_unitOfWork);
                        var whatsAppUsuarioCredencialService = new WhatsAppUsuarioCredencialService(_unitOfWork);

                        var _credencialesHost = whatsAppConfiguracionService.ObtenerCredencialHost(whatsAppEnviarMensajeDTO.IdPais);
                        var tokenValida = whatsAppUsuarioCredencialService.ValidarCredencialesUsuario(whatsAppEnviarMensajeDTO.IdPersonal, whatsAppEnviarMensajeDTO.IdPais);

                        string urlToPost = _credencialesHost.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        WhatsAppMensajeEnviado mensajeEnviado = new WhatsAppMensajeEnviado();

                        if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                        {
                            string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                            var userLogin = whatsAppUsuarioCredencialService.CredencialUsuarioLogin(whatsAppEnviarMensajeDTO.IdPersonal);

                            var client = new RestClient(urlToPostUsuario);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", _credencialesHost.IpHost);
                            request.AddHeader("Cache-Control", "no-cache");
                            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                            request.AddHeader("Content-Type", "application/json");
                            IRestResponse response = client.Execute(request);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                foreach (var item in datos.users)
                                {
                                    WhatsAppUsuarioCredencial modelCredencial = new WhatsAppUsuarioCredencial();
                                    modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                    modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                    modelCredencial.UserAuthToken = item.token;
                                    modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                    modelCredencial.EsMigracion = true;
                                    modelCredencial.Estado = true;
                                    modelCredencial.FechaCreacion = DateTime.Now;
                                    modelCredencial.FechaModificacion = DateTime.Now;
                                    modelCredencial.UsuarioCreacion = "whatsapp";
                                    modelCredencial.UsuarioModificacion = "whatsapp";
                                    var rpta = whatsAppUsuarioCredencialService.Add(modelCredencial);
                                    _tokenComunicacion = item.token;
                                }
                                banderaLogin = true;
                            }
                            else
                            {
                                banderaLogin = false;
                            }
                        }
                        else
                        {
                            _tokenComunicacion = tokenValida.UserAuthToken;
                            banderaLogin = true;
                        }
                        if (banderaLogin)
                        {
                            switch (whatsAppEnviarMensajeDTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                    _waType = "text";
                                    MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();
                                    _mensajeTexto.to = whatsAppEnviarMensajeDTO.WaTo;
                                    _mensajeTexto.type = whatsAppEnviarMensajeDTO.WaType;
                                    _mensajeTexto.recipient_type = whatsAppEnviarMensajeDTO.WaRecipientType;
                                    _mensajeTexto.text = new text();
                                    _mensajeTexto.text.body = whatsAppEnviarMensajeDTO.WaBody;

                                    using (WebClient client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeTexto);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }
                                    break;
                                case "hsm":

                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "template";

                                    MensajePlantillaWhatsAppEnvioTemplate _mensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                    _mensajePlantilla.to = whatsAppEnviarMensajeDTO.WaTo;
                                    _mensajePlantilla.type = "template";
                                    _mensajePlantilla.template = new template();
                                    _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    _mensajePlantilla.template.name = whatsAppEnviarMensajeDTO.WaBody;
                                    _mensajePlantilla.template.language = new language();
                                    _mensajePlantilla.template.language.policy = "deterministic";
                                    _mensajePlantilla.template.language.code = "es";
                                    _mensajePlantilla.template.components = new List<components>();
                                    components Componente = new components();
                                    Componente.type = "body";

                                    if (whatsAppEnviarMensajeDTO.DatosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        foreach (var listaDatos in whatsAppEnviarMensajeDTO.DatosPlantillaWhatsApp)
                                        {
                                            parameters Dato = new parameters();
                                            Dato.type = "text";
                                            Dato.text = listaDatos.texto;
                                            Componente.parameters.Add(Dato);
                                        }
                                    }

                                    _mensajePlantilla.template.components.Add(Componente);

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(_mensajePlantilla);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = Client.UploadString(urlToPost, MyParameters);
                                    }
                                    break;
                                case "image":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "image";

                                    MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                    _mensajeImagen.to = whatsAppEnviarMensajeDTO.WaTo;
                                    _mensajeImagen.type = whatsAppEnviarMensajeDTO.WaType;
                                    _mensajeImagen.recipient_type = whatsAppEnviarMensajeDTO.WaRecipientType;
                                    _mensajeImagen.image = new image();
                                    _mensajeImagen.image.caption = whatsAppEnviarMensajeDTO.WaCaption;
                                    _mensajeImagen.image.link = whatsAppEnviarMensajeDTO.WaLink;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeImagen);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }
                                    break;
                                case "document":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "document";

                                    MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                    _mensajeDocumento.to = whatsAppEnviarMensajeDTO.WaTo;
                                    _mensajeDocumento.type = whatsAppEnviarMensajeDTO.WaType;
                                    _mensajeDocumento.recipient_type = whatsAppEnviarMensajeDTO.WaRecipientType;
                                    _mensajeDocumento.document = new document();
                                    _mensajeDocumento.document.caption = whatsAppEnviarMensajeDTO.WaCaption;
                                    _mensajeDocumento.document.link = whatsAppEnviarMensajeDTO.WaLink;
                                    _mensajeDocumento.document.filename = whatsAppEnviarMensajeDTO.WaFileName;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeDocumento);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }
                                    break;
                            }
                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            foreach (var itemGuardar in datoRespuesta.messages)
                            {
                                var _mensajeEnviadoRepositorio = new WhatsAppMensajeEnviadoService(_unitOfWork);

                                mensajeEnviado.WaId = itemGuardar.id;
                                mensajeEnviado.WaTo = whatsAppEnviarMensajeDTO.WaTo;
                                mensajeEnviado.WaType = _waType;
                                mensajeEnviado.WaRecipientType = whatsAppEnviarMensajeDTO.WaRecipientType;
                                mensajeEnviado.WaBody = whatsAppEnviarMensajeDTO.WaBody;
                                mensajeEnviado.WaCaption = whatsAppEnviarMensajeDTO.WaCaption;
                                mensajeEnviado.WaLink = whatsAppEnviarMensajeDTO.WaLink;
                                mensajeEnviado.WaFileName = whatsAppEnviarMensajeDTO.WaFileName;
                                mensajeEnviado.IdPais = whatsAppEnviarMensajeDTO.IdPais;
                                if (whatsAppEnviarMensajeDTO.IdAlumno != 0)
                                {
                                    mensajeEnviado.IdAlumno = whatsAppEnviarMensajeDTO.IdAlumno;
                                }
                                else
                                {
                                    mensajeEnviado.IdAlumno = null;
                                }

                                mensajeEnviado.IdPersonal = whatsAppEnviarMensajeDTO.IdPersonal;
                                mensajeEnviado.Estado = true;
                                mensajeEnviado.FechaCreacion = DateTime.Now;
                                mensajeEnviado.FechaModificacion = DateTime.Now;
                                mensajeEnviado.UsuarioCreacion = whatsAppEnviarMensajeDTO.usuario;
                                mensajeEnviado.UsuarioModificacion = whatsAppEnviarMensajeDTO.usuario;

                                _mensajeEnviadoRepositorio.Add(mensajeEnviado);
                            }
                            return Ok(mensajeEnviado.WaId);
                        }
                        else
                        {
                            return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                }
                else
                {
                    return BadRequest("El numero esta desuscrito");
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }

        [Route("[action]/{idPais}")]
        [HttpGet]
        public ActionResult ObtenerApiWhatsApp(int idPais)
        {
            //public List<InfoApiWhatsappDTO> ListaInformacionApiWhatsapp(int idPais)
            try
            {
                WhatsAppMensajeEnviadoService servicioConfiguracion = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var data = servicioConfiguracion.ListaInformacionApiWhatsapp(idPais);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// ObtenerChatsWhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpGet("ObtenerChatWhatsAppMarketing/{Tab}/{Dia}")]
        public IActionResult ObtenerChatWhatsAppMarketing(int Tab, int Dia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicio.ObtenerChatWhatsAppMarketing(Tab, Dia);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Desconocido
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de ultimos mensajes por cliente, para una rango de fecha especifico
        /// </summary>
        /// <param name="filtro">Objeto con detalles del filtro para chats</param>
        /// <returns>Lista de ultimos mensajes por cliente</returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerChatWhatsAppMarketingV2([FromBody] FiltroChatWhatsappDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
            var respuesta = servicio.ObtenerChatWhatsAppMarketingV2(filtro);
            return Ok(respuesta);
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 27/03/2024
        /// Versión: 1.0
        /// <summary>
        /// ObtenerChatsWhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpGet("ObtenerChatWhatsAppFacebookMarketing/{Tab}/{Dia}")]
        public IActionResult ObtenerChatWhatsAppFacebookMarketing(int Tab, int Dia, int? idAsesor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var idAsesorToken = _respuestaCorrecta.RegistroClaimToken.IdPersonal;

                bool esValido = servicio.EsAsesorVentasValido(idAsesorToken);

                object respuesta;

                if (esValido == true)
                {
                
                    respuesta = servicio.ObtenerChatWhatsAppFacebookMarketing(Tab, Dia, idAsesorToken);
                }
                else
                {
                
                    respuesta = servicio.ObtenerChatWhatsAppFacebookMarketing(Tab, Dia, 0);
                }

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpGet("ObtenerChatWhatsAppMarketingPorCelular/{Celular}")]
        public IActionResult ObtenerChatWhatsAppMarketingPorCelular(string Celular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicio.ObtenerChatWhatsAppMarketingPorCelular(Celular);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpGet("ObtenerChatWhatsAppMarketingMasivoPorCelular/{Celular}")]
        public IActionResult ObtenerChatWhatsAppMarketingMasivoPorCelular(string Celular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicio.ObtenerChatWhatsAppMarketingMasivoPorCelular(Celular);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpGet("ObtenerChatWhatsAppMarketingBusquedaPorCelular/{Celular}")]
        public IActionResult ObtenerChatWhatsAppMarketingBusquedaPorCelular(string Celular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicio.ObtenerChatWhatsAppMarketingBusquedaPorCelular(Celular);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpPost("ArchivarChat/{Celular}/{IdAlumno}")]
        public IActionResult ArchivarChat(string Celular, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var IdUsuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;

                var respuesta = servicio.ArchivarChat(Celular, IdAlumno, IdUsuario, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpPost("[action]")]
        public IActionResult ArchivarChatMasivo([FromBody] List<WhatsAppChatArchivadoDTO> lista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicio.ArchivarChatMasivo(lista, _tokenManager.IdPersonal, _tokenManager.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpPost("Desuscribir/{Celular}/{IdAlumno}")]
        public IActionResult Desuscribir(string Celular, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var respuesta = servicio.Desuscribir(Celular, IdAlumno, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpPost("[action]")]
        public IActionResult DesuscribirChatMasivo([FromBody] List<WhatsAppChatArchivadoDTO> lista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicio.DesuscribirChatMasivo(lista, _tokenManager.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpPost("SuscribirAlumno/{Celular}/{IdAlumno}")]
        public IActionResult SuscribirAlumno(string Celular, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var IdUsuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;

                var respuesta = servicio.SuscribirAlumno(Celular, IdAlumno, IdUsuario, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpPost("DesArchivarChat/{Celular}/{IdAlumno}")]
        public IActionResult DesArchivarChat(string Celular, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var IdUsuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;

                var respuesta = servicio.DesArchivarChat(Celular, IdAlumno, IdUsuario, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpPost("ActualizarDatosAlumno")]
        public IActionResult ActualizarDatosAlumno(ObtenerAtributosAlumnoDTO AlumnoActualizar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var respuesta = servicio.ActualizarDatosAlumno(AlumnoActualizar, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombosAtributosAlumno()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);

                var respuesta = servicio.ObtenerCombosAtributosAlumno();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 23/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener Chats WhatsApp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [HttpGet("ObtenerDatosAlumnoWhatsApp/{IdAlumno}")]
        public IActionResult ObtenerDatosAlumnoWhatsApp(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicio.ObtenerDatosAlumnoWhatsApp(IdAlumno);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: Post
        /// Autor: Margiory Ramirez
        /// Fecha: 13/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Creacion de oportunidades Whatsapp
        /// </summary>
        /// <returns>Retorna 200 y listado de chats o 400 y mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult CrearOportunidadWhatsapp(OportunidadWhatsappDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var respuesta = servicio.CrearOportunidadWhatsapp(dto, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Autor:Margiory  Ramirez 
        /// Fecha: 11/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene probabilidad por oportunidad
        /// <returns> Lista de objetoDTO: List<ProbabilidaWhatsAppDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProbabilidadWhatsapp(int idOportunidad)
        {
            if (idOportunidad != null)
            {
                try
                {
                    var servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var restultado = servicioWhatsappMsjEnv.ObtenerProbabilidadPorOportunidad(idOportunidad);

                    if (restultado != null)
                    {
                        return Ok(restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }


        /// Autor:Margiory  Ramirez 
        /// Fecha: 11/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el nombre del programa original por  oportunidad
        /// <returns> Lista de objetoDTO: List<ProgramaPorOportunidadDTO> </returns>
        /// [Route("[action]")]
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProgramaPorOportunidadWhatsapp([FromBody] int idOportunidad)

        {
            if (idOportunidad != null)
            {
                try
                {
                    var servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var restultado = servicioWhatsappMsjEnv.ObtenerProgramaPorOportunidadWhatsapp(idOportunidad);

                    if (restultado != null)
                    {
                        return Ok(restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 11/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los programas relaciondos a venta cruzada basado en probabilidades
        /// </summary>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ValidarProbabilidadOportunidades(VentaCruzadaProbabilidadDTO ventaCruzada)
        {
            try
            {
                var servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultadoProbabilidad = servicioWhatsappMsjEnv.ValidarProbabilidadOportunidades(ventaCruzada.IdOportunidad, ventaCruzada.IdAlumno, ventaCruzada.IdArea, ventaCruzada.IdPGeneral);

                if (resultadoProbabilidad != null)
                {
                    return Ok(new
                    {
                        probabilidad = resultadoProbabilidad.Probabilidad,
                        apto = resultadoProbabilidad.Apto,
                        mensaje = resultadoProbabilidad.Mensaje,
                        listaVentaCruzada = resultadoProbabilidad.ListaVentaCruzadaWhatsapp

                    });
                }
                else
                {
                    return BadRequest("Error: Sin Datos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("ObtenerPersonalOportunidad")]
        public IActionResult ObtenerPersonalOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);


                var respuesta = servicio.ObtenerPersonalOportunidad();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerIdAsesorActual([FromBody] int idOportunidad)
        {
            if (idOportunidad != null)
            {
                try
                {
                    var servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var restultado = servicioWhatsappMsjEnv.ObtenerIdAsesorActual(idOportunidad);

                    if (restultado != null)
                    {
                        return Ok(restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ValidarProbabilidadOportunidadesRecalculo(VentaCruzadaProbabilidadDTO ventaCruzada)
        {
            try
            {
                var servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultadoProbabilidad = servicioWhatsappMsjEnv.ValidarProbabilidadOportunidadesRecalculo(ventaCruzada.IdOportunidad, ventaCruzada.IdAlumno, ventaCruzada.IdArea, ventaCruzada.IdPGeneral);

                if (resultadoProbabilidad != null)
                {
                    return Ok(new
                    {
                        probabilidad = resultadoProbabilidad.Probabilidad,
                        apto = resultadoProbabilidad.Apto,
                        mensaje = resultadoProbabilidad.Mensaje,
                        listaVentaCruzada = resultadoProbabilidad.ListaVentaCruzadaWhatsapp

                    });
                }
                else
                {
                    return BadRequest("Error: Sin Datos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("ObtenerProbabilidadModeloPredictivoMarketing/{idOportunidad}/{tipo}")]
        public ActionResult ObtenerProbabilidadModeloPredictivoMarketing(int idOportunidad, int tipo)
            {
                if (idOportunidad != null)
                {
                    try
                    {
                        var servicioWhatsappMsjEnv = new OportunidadService(_unitOfWork);
                        var restultado = servicioWhatsappMsjEnv.ObtenerProbabilidadModeloPredictivoMarketing(idOportunidad,tipo);

                        if (restultado != null)
                        {
                            return Ok(restultado);
                        }
                        else
                        {
                            return BadRequest("Error: Sin Datos");
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex);
                    }
                }
                else
                {
                    return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
                }
            }

        [Route("[action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerProbabilidadTodosProgramasPorAlumno(int idAlumno)
        {
            if (idAlumno > 0)
            {
                try
                {
                    var servicioModeloPredictivo = new OportunidadService(_unitOfWork);

                    servicioModeloPredictivo.ObtenerProbabilidadTodosProgramasPorAlumno(idAlumno);

                    return Ok($"Probabilidad calculada exitosamente para el alumno con ID {idAlumno}.");
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Mensaje = "Error durante el procesamiento.", Detalle = ex.Message });
                }
            }
            else
            {
                return BadRequest("El ID del alumno no puede ser nulo o inválido.");
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 29/08/2025
        /// Version: 1.0
        /// <summary>
        /// Captura registros de alumnos en base a chats mediante un modelo IA
        /// </summary>
        /// <param name="datosExtraccionRegistros">Contiene celular del alumno y rango de antiguedad de chats</param>
        /// <returns>Datos capturados por el modelo IA</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> CapturarRegistrosModeloIA([FromBody] DatosExtraccionRegistrosDTO datosExtraccionRegistros)
        {
            try
            {
                var _servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await _servicioWhatsappMsjEnv.CapturarRegistrosModeloIA(datosExtraccionRegistros);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Desactiva la interacción automática del asistente WhatsApp para un cliente y campania específicos
        /// </summary>
        /// <param name="celularAlumno">número de WhatsApp del alumno</param>
        /// <param name="idCampania">ID de campaña a desactivar</param>
        /// <returns>Resultado del servicio externo</returns>
        [Route("[action]/{celularAlumno}/{idCampania}")]
        [HttpPost]
        public async Task<ActionResult> DesactivarInteraccionAutomaticaWhatsapp(string celularAlumno, string idCampania)
        {
            try
            {
                var _servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await _servicioWhatsappMsjEnv.DesactivarInteraccionAutomaticaWhatsapp(celularAlumno, idCampania);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 01/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener datos extraidos mediante la interaccion automatica de un telefono
        /// </summary>
        /// <param name="celularAlumno">número de WhatsApp del alumno</param>
        /// <returns>Resultado del servicio externo</returns>
        [Route("[action]/{celularAlumno}")]
        [HttpGet]
        public async Task<ActionResult> ObtenerDatosExtraidosInteraccionAutomatica(string celularAlumno)
        {
            try
            {
                var _servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await _servicioWhatsappMsjEnv.ObtenerDatosExtraidosInteraccionAutomatica(celularAlumno);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 01/10/2025
        /// Version: 1.0
        /// <summary>
        /// Valida el guardado de los datos extraidos por la interaccion automatica de un telefono
        /// </summary>
        /// <param name="celularAlumno">número de WhatsApp del alumno</param>
        /// <returns>Resultado del servicio externo</returns>
        [Route("[action]/{celularAlumno}")]
        [HttpPost]
        public async Task<ActionResult> ValidarGuardadoDatosInteraccionAutomatica(string celularAlumno)
        {
            try
            {
                var _servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await _servicioWhatsappMsjEnv.ValidarGuardadoDatosInteraccionAutomatica(celularAlumno);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // -------------------------------------------------------
        // Modal Masivo Oportunidades WhatsApp
        // Autor: Miguel Valdivia  | Fecha: 2026-04-24
        // -------------------------------------------------------

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-04-24
        /// Versión: 1.0
        /// <summary>
        /// Dado un IdAlumno, retorna el IdCentroCosto asignado en la última campaña activa.
        /// GET /api/WhatsAppMensajeEnviado/ObtenerCentroCostoPorAlumno/{idAlumno}
        /// </summary>
        [Route("[action]/{idAlumno}")]
        [HttpGet]
        public IActionResult ObtenerCentroCostoPorAlumno(int idAlumno)
        {
            if (idAlumno <= 0)
                return BadRequest("El IdAlumno debe ser mayor a 0.");
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var request = new CentroCostoPorAlumnoRequestDTO { IdAlumno = idAlumno };
                var resultado = servicio.ObtenerCentroCostoPorAlumno(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-04-24
        /// Versión: 1.0
        /// <summary>
        /// Dada una lista de celulares y rango de horas, retorna datos de pre-carga masiva
        /// (perfil del alumno, mensajes recientes, historial de oportunidades, IdCentroCosto).
        /// POST /api/WhatsAppMensajeEnviado/ObtenerDatosPreCargaMasiva
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerDatosPreCargaMasiva([FromBody] PreCargaMasivaRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.ObtenerDatosPreCargaMasiva(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-04-24
        /// Versión: 1.0
        /// <summary>
        /// Actualiza datos de perfil (cargo, industria, área de formación, área de trabajo)
        /// para una lista de alumnos. Se llama por cada lead que tenga perfilModificado = true.
        /// POST /api/WhatsAppMensajeEnviado/ActualizarDatosAlumnoMasivo
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarDatosAlumnoMasivo([FromBody] List<ActualizarAlumnoMasivoItemDTO> lista)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var usuario = "sistema";
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.ActualizarDatosAlumnoMasivo(lista, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Autor: Miguel Valdivia
        /// Fecha: 2026-05-04
        /// Versión: 1.0
        /// <summary>
        /// Inicia una extraccion batch enviando los chats de varios leads al servicio de IA externo.
        /// POST /api/WhatsAppMensajeEnviado/IniciarExtraccionBatch
        /// </summary>
        [HttpPost("IniciarExtraccionBatch")]
        public async Task<ActionResult> IniciarExtraccionBatch([FromBody] ExtraccionBatchRequestDTO request)
        {
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await servicio.IniciarExtraccionBatch(request);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-05-04
        /// Versión: 1.0
        /// <summary>
        /// Consulta el estado de una extraccion batch por su callId.
        /// GET /api/WhatsAppMensajeEnviado/ObtenerEstadoExtraccion/{callId}
        /// </summary>
        [HttpGet("ObtenerEstadoExtraccion/{callId}")]
        public async Task<ActionResult> ObtenerEstadoExtraccion(string callId)
        {
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await servicio.ObtenerEstadoExtraccion(callId);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-05-04
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los resultados de una extraccion batch por su callId.
        /// GET /api/WhatsAppMensajeEnviado/ObtenerResultadosExtraccion/{callId}
        /// </summary>
        [HttpGet("ObtenerResultadosExtraccion/{callId}")]
        public async Task<ActionResult> ObtenerResultadosExtraccion(string callId)
        {
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await servicio.ObtenerResultadosExtraccion(callId);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-05-04
        /// Versión: 1.0
        /// <summary>
        /// Inicia una calificacion batch de leads mediante el servicio de IA externo.
        /// POST /api/WhatsAppMensajeEnviado/IniciarCalificacionBatch
        /// </summary>
        [HttpPost("IniciarCalificacionBatch")]
        public async Task<ActionResult> IniciarCalificacionBatch([FromBody] CalificacionLlamadaRequestDTO request)
        {
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await servicio.IniciarCalificacionBatch(request);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-05-04
        /// Versión: 1.0
        /// <summary>
        /// Consulta el estado de una calificacion batch por su llamadaId.
        /// GET /api/WhatsAppMensajeEnviado/ObtenerEstadoCalificacion/{llamadaId}
        /// </summary>
        [HttpGet("ObtenerEstadoCalificacion/{llamadaId}")]
        public async Task<ActionResult> ObtenerEstadoCalificacion(string llamadaId)
        {
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await servicio.ObtenerEstadoCalificacion(llamadaId);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: Miguel Valdivia 
        /// Fecha: 2026-05-04
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los resultados de una calificacion batch por su llamadaId.
        /// GET /api/WhatsAppMensajeEnviado/ObtenerResultadosCalificacion/{llamadaId}
        /// </summary>
        [HttpGet("ObtenerResultadosCalificacion/{llamadaId}")]
        public async Task<ActionResult> ObtenerResultadosCalificacion(string llamadaId)
        {
            try
            {
                var servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = await servicio.ObtenerResultadosCalificacion(llamadaId);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
