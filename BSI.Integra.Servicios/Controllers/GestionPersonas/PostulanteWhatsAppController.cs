using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: PostulanteWhatsAppController
    /// Autor: Eliot Arias F.
    /// Fecha: 04/12/2024
    /// <summary>
    /// Gestion de Postulante
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PostulanteWhatsAppController : Controller
    {
        private ITokenManager _tokenManager;
        private IPostulanteService _postulanteService;
        private IUnitOfWork _unitOfWork;
        private IPostulanteWhatsAppService _postulanteWhatsAppMensajesService;


        public PostulanteWhatsAppController(ITokenManager tokenManager, IUnitOfWork unitOfWork)
        {
            _tokenManager = tokenManager;
            _postulanteService = new PostulanteService(unitOfWork);
            _unitOfWork = unitOfWork;
            _postulanteWhatsAppMensajesService = new PostulanteWhatsAppService(_unitOfWork);
        }

        /// TipoFuncion: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Lista los chats con postulantes asociados al personal
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeRecibidosChat(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdPersonal == null)
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {
                var _restultado = _postulanteWhatsAppMensajesService.WhatsAppUltimoMensajeRecibidosChat(IdPersonal);

                if (_restultado != null)
                {
                    return Ok(_restultado);
                }
                else
                {
                    return BadRequest("Error: Sin Datos");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros conversacion de whatsapp por numero
        /// </summary>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("[action]/{saludo}")]
        [HttpGet]
        public ActionResult ObtenerConversacionPorNumero(string Numero, int IdPais)
        {
            return null;
        }

        /// TipoFuncion: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppMensaje([FromBody] WhatsAppMensajeEnviadoPostulanteDTO whatsAppEnviarMensajeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Waid = _postulanteWhatsAppMensajesService.WhatsAppMensaje(whatsAppEnviarMensajeDTO);
                return Ok(Waid);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera la plantilla para el envio
        /// </summary>
        /// <returns> Plantilla reemplazada con los datos </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarPlantillaGPWhatsapp([FromBody] GestionPersonasPlantillaWhatsAppDTO Plantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Waid = _postulanteWhatsAppMensajesService.GenerarPlantillaGPWhatsapp(Plantilla);
                return Ok(Waid);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 17/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Valida si el postulante envio un mensaje en las ultimas 24 horas
        /// </summary>
        /// <returns> WhatsAppHistorialMensajesPostulanteDTO </returns>
        [Route("[Action]/{numero}")]
        [HttpGet]
        public ActionResult ValidarMensajeRecibido24Horas(string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var Waid = _postulanteWhatsAppMensajesService.ValidarMensajeRecibido24Horas(numero);
                return Ok(Waid);
                //return Ok(false);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 17/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial de mensajes de whatsapp
        /// </summary>
        /// <returns> WhatsAppHistorialMensajesPostulanteDTO </returns>
        [Route("[action]/{plantilla}/{numero}")]
        [HttpGet]
        public ActionResult ValidarUltimaPlantillaEnviada(string plantilla, string numero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var validacion = _postulanteWhatsAppMensajesService.ValidarUltimaPlantillaEnviada(plantilla, numero);
                return Ok(validacion);
                //return Ok(false);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 20/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        [Route("[Action]/{IdPersonal}/{Numero}/{Area}")]
        [HttpGet]
        public ActionResult WhatsAppHistorialMensajeChat(int IdPersonal, string Numero, string Area)
        {
            if (IdPersonal != null)
            {
                try
                {
                    var _restultado = _postulanteWhatsAppMensajesService.ListaHistorialMensajeChat(IdPersonal, Numero, Area);

                    if (_restultado != null)
                    {
                        return Ok(_restultado);
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



        /// TipoFuncion: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EnvioMensajePlantilla([FromBody] WhatsAppMensajePostulantePlantillaComDTO whatsAppEnviarMensajeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Waid = _postulanteWhatsAppMensajesService.EnvioMensajePorPlantilla(whatsAppEnviarMensajeDTO, whatsAppEnviarMensajeDTO.Usuario, whatsAppEnviarMensajeDTO.IdPersonal.Value);
                return Ok(Waid);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeTexto(WhatsAppMensajeTextoPostulanteDTO json)
        {
            try
            {
                //Elimina la  \t
                json.WaBody = json.WaBody.Replace("\t", "   ");
                json.WaBody = json.WaBody.Replace("\n", "   ");

                var rpta = _postulanteWhatsAppMensajesService.EnvioMensajePorTexto(json, json.Usuario, json.IdPersonal.Value);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 24/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Envio masivo de WhatsApp a postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult EnviarMensajeMasivoWhatsAppPostulante([FromBody] EnvioPlantillaPostulanteDTO Postulantes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_postulanteWhatsAppMensajesService.EnvioMensajesPorPlantillaMasivo(Postulantes));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 12/26/202
        /// Version: 1.0
        /// <summary>
        /// Envia un archivo al postulante img/pdf .etc
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeArchivo(WhatsAppMensajeArchivoPostulanteDTO json)
        {
            try
            {
                var token = _tokenManager;
                return Ok(_postulanteWhatsAppMensajesService.EnvioMensajePorArchivo(json, token.UserName, (json.IdPersonal == null ? token.IdPersonal : json.IdPersonal.Value)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Eliot Arias F.
        /// Fecha: 12/26/202
        /// Version: 1.0
        /// <summary>
        /// Adjunta archivo para mensaje de WhatsApp
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AdjuntarArchivoWhatsApp([FromForm] IFormFile file)
        {
            string respuesta = string.Empty;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WhatsAppMensajeRecibidoService whatsAppMensajeRecibidoService = new WhatsAppMensajeRecibidoService(_unitOfWork);
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = whatsAppMensajeRecibidoService.GuardarArchivos(fileBytes, file.ContentType, file.FileName);
                }

                if (string.IsNullOrEmpty(respuesta))
                {
                    return Ok(new { Resultado = "Error" });
                }
                else
                {
                    return Ok(new { Resultado = "Ok", UrlArchivo = respuesta, NombreArchivo = file.FileName });
                }
            }
            catch (Exception Ex)
            {
                return Ok(new { Resultado = "Error" });
            }
        }


    }
}
