using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppMensajeEnviadoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión de WhatsAppMensajeEnviado
    /// </summary>
    [Route("api/WhatsAppMensajes")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppMensajesController : ControllerBase
    {
        private ITokenManager _tokenManager;

        private IUnitOfWork unitOfWork;
        public WhatsAppMensajesController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        [Route("[action]")]
        [HttpPost]
        public WhatsAppMensajeEnviadoRespuestaDTO WhatsAppMensaje([FromBody] WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO)
        {

            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                var Waid = whatsAppMensajesService.WhatsAppMensaje(whatsAppEnviarMensajeDTO);
                return Waid;

            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 28/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{idAlumno}/{idPlantilla}")]
        [HttpGet]
        public WhatsAppMensajeEnviadoRespuestaDTO GenerarPlantillaWhatsappAlumno(int idAlumno, int idPlantilla)
        {
            try
            {
                IAgendaService agendaService = new AgendaService(unitOfWork);
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                var resultado = agendaService.GenerarPlantillaWhatsappAlumno(idAlumno, idPlantilla);
                var Waid = whatsAppMensajesService.WhatsAppMensajeAlumnoAccesos(resultado);
                return Waid;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        [Route("[action]")]
        [HttpPost]
        //[Authorize]
        [JwtExpirationValidation]
        public IActionResult EnviarMensajeWhatsappComercial([FromBody] WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO)
        {
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                var Waid = whatsAppMensajesService.EnviarMensajeWhatsapp(whatsAppEnviarMensajeDTO, _tokenManager.UserName);
                return Ok(Waid);
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes de WhatssApp
        /// </summary>
        /// <returns> Objeto TWhatsAppMensajeEnviado  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppMensajeVersionTemplate([FromBody] WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppMensajeVersionTemplate(whatsAppEnviarMensajeDTO));
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene mensaje de WhatssApp de tipo multimedia
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]/{waId}")]
        [HttpGet]
        public ActionResult WhatsAppMensajeMultimedia(string waId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppMensajeMultimedia(waId));
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Adjunta archivo para mensaje de WhatsApp
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AdjuntarArchivoWhatsApp([FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.AdjuntarArchivoWhatsApp(file));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo mensjae de chat de WhatsApp por Id de personal.
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeChat(int idPersonal)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppUltimoMensajeChat(idPersonal));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeRecibidosChat(int idPersonal)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppUltimoMensajeRecibidosChat(idPersonal));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
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
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.HistorialMensajeRecibidosChat(idPersonal, numero, area));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de mensajes recibidos a chat de asesor
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{idPersonal}/{numero}/{area}/{idTipoAgenda}")]
        [HttpGet]
        public ActionResult HistorialMensajeRecibidosChat(int idPersonal, string numero, string area, int idTipoAgenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.HistorialMensajeRecibidosChat(idPersonal, numero, area, idTipoAgenda));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo mensaje recibido de WhatsApp por asesor.
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeEnviadosChat(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppUltimoMensajeEnviadosChat(idPersonal));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
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
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppHistorialMensajeChat(idPersonal, numero, area));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene mensajes multimedia de WhatsApp por id de WhatsApp.
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]/{waId}")]
        [HttpGet]
        public ActionResult WhatsAppObtenerMensajeMultimedia(string waId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppObtenerMensajeMultimedia(waId));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 11/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene mensajes multimedia de WhatsApp (Planificación) por id de WhatsApp.
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]/{waId}")]
        [HttpGet]
        public ActionResult WhatsAppObtenerMensajeMultimediaPla(string waId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppObtenerMensajeMultimediaPla(waId));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene conversacion por numero.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{numero}/{idPais}")]
        [HttpGet]
        public ActionResult ObtenerConversacionPorNumero(string numero, int idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ObtenerConversacionPorNumero(numero, idPais));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene conversacion por oportunidad.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{numero}/{idPais}")]
        [HttpGet]
        public ActionResult ObtenerConversacionPorOportunidad(string numero, int idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ObtenerConversacionPorOportunidad(numero, idPais));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de personal.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        [Route("[action]/{numero}/{idCentroCosto}/{idPais}")]
        [HttpGet]
        public ActionResult ObtenerPersonalConfiguracion(string numero, int idCentroCosto, int idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ObtenerPersonalConfiguracion(numero, idCentroCosto, idPais));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0 
        /// <summary>
        /// Validar numero libre.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{numero}/{idPais}/{idCentroCosto}/{idPersonal}")]
        [HttpGet]
        public ActionResult ValidarNumeroLibre(string numero, int idPais, int idCentroCosto, int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ValidarNumeroLibre(numero, idPais, idCentroCosto, idPersonal));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el asesor con menos chats offline.
        /// </summary>
        /// <returns> Objeto PersonalNumeroMinimoChatDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAsesorConMenorChatsOffLine()
        {
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ObtenerAsesorConMenorChatsOffLine());
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{plantilla}/{numero}")]
        [HttpGet]
        public ActionResult ValidarPlantillasEnviadas(string plantilla, string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ValidarPlantillasEnviadas(plantilla, numero));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 23/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{plantilla}/{numero}/{IdPersonal}/{IdCodigoPais}/{idPersonalAsignado}")]
        [HttpGet]
        public ActionResult ValidarPlantillasEnviadasComercial(string plantilla, string numero, int IdPersonal, int IdCodigoPais, int idPersonalAsignado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ValidarPlantillasEnviadasComercial(plantilla, numero, IdPersonal, IdCodigoPais, idPersonalAsignado));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 23/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{numero}")]
        [HttpGet]
        public ActionResult ValidarMesajeRecibidosApiComercial(string numero)
        {

            IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
            return Ok(whatsAppMensajesService.ValidarMesajeRecibidosApiComercial(numero));
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        [Route("[action]/{plantilla}/{numero}")]
        [HttpGet]
        public ActionResult ValidarPlantillasEnviadasNuevoWebHook(string plantilla, string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ValidarPlantillasEnviadasNuevoWebHook(plantilla, numero));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [Route("[action]/{numero}")]
        [HttpGet]
        public ActionResult ValidarMesajesEnviadosEn24Horas(string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ValidarMesajesEnviadosEn24Horas(numero));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [Route("[action]/{numero}/{IdPersonal}/{IdCodigoPais}/{idPersonalAsignado}")]
        [HttpGet]
        public ActionResult ValidarMesajesEnviadosEn24HorasComercial(string numero, int IdPersonal, int IdCodigoPais, int idPersonalAsignado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ValidarMesajesEnviadosEn24HorasComercial(numero, IdPersonal, IdCodigoPais, idPersonalAsignado));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [Route("[action]/{numero}")]
        [HttpGet]
        public ActionResult ValidarMesajesEnviadosEn24HorasNuevoWebHook(string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ValidarMesajesEnviadosEn24HorasNuevoWebHook(numero));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Optiene oportunidad por asesor y alumno.
        /// </summary>
        /// <returns> Objeto OportunidadDatosChatWhatsAppDTO </returns>
        [Route("[action]/{idPersonal}/{idAlumno}/{numero}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadPorAsesorYAlumno(int idPersonal, int idAlumno, string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.ObtenerOportunidadPorAsesorYAlumno(idPersonal, idAlumno, numero));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el último mensaje recibido por oportunidad.
        /// </summary>
        /// <returns> Objeto WhatsAppMensajesRecibidosOperacionesDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppUltimoMensajeRecibidosPorOportunidad([FromBody] Dictionary<string, string> filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppUltimoMensajeRecibidosPorOportunidad(filtro));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesControlOfensivoDTO>  </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeRecibidosChatControlMensajes(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppUltimoMensajeRecibidosChatControlMensajes(idPersonal));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesControlOfensivoDTO>  </returns>
        [Route("[action]/{idPersonal}/{numero}/{area}")]
        [HttpGet]
        public ActionResult WhatsAppHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                return Ok(whatsAppMensajesService.WhatsAppHistorialMensajeChatControlMensaje(idPersonal, numero, area));
            }
            catch (Exception Ex)
            {
                throw;
            }
        }



        // POST api/<Pruebas Miguel Q>
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsaappWebHookPeru(WhatsappEnvioDTO envioString)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                whatsAppMensajesService.WhatsAppNotificacionesMensaje(envioString);
                return Ok();
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public IActionResult VerificarAsesorOnline(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IWhatsAppMensajesService whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                var datos = whatsAppMensajesService.VerificarAsesorOnline(IdPersonal);
                return Ok(datos);
            }
            catch (Exception Ex)
            {
                throw;
            }

        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult EnvioWhatsAsignacion(EnvioWhatsappAsignacionDTO envio)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAsignacionManualService whatsAppMensajesService = new AsignacionManualService(unitOfWork);
                whatsAppMensajesService.EnvioWhats(envio.idOportunidad, envio.idPais, envio.idPersonal, envio.IdCategoriaOrigen);
                return Ok();
            }
            catch (Exception Ex)
            {
                throw;
            }


        }


    }
}
