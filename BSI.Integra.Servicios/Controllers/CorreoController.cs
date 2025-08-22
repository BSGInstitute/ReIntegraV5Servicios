using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AgendaTabController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 20/07/2022
    /// <summary>
    /// Gestión de Correos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CorreoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CorreoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los correos enviados por el asesor segun ciertos filtros
        /// </summary>
        /// <param name="filtroBandejaCorreo">Filtros para Bandeja Correo</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerCorreosEnviadosPorAsesor")]
        public IActionResult ObtenerCorreosEnviadosPorAsesor([FromBody] FiltroBandejaCorreoDTO filtroBandejaCorreo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioGmailCorreo = new GmailCorreoService(unitOfWork);
                return Ok(servicioGmailCorreo.ObtenerCorreosEnviadosPorFiltroBandeja(filtroBandejaCorreo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene correos recibidos.
        /// </summary>
        /// <param name="filtroBandejaCorreo">Filtros para Bandeja Correo</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerCorreoRecibido([FromBody] FiltroBandejaCorreoDTO filtroBandejaCorreo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IBandejaCorreoService bandejaCorreoService = new BandejaCorreoService(unitOfWork);
            var resultado = bandejaCorreoService.ObtenerCorreoRecibido(filtroBandejaCorreo);
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion de Gmail.
        /// </summary>
        /// <returns> objetoDTO: CorreoBodyDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerInformacionGmail(int IdCorreo, int IdAsesor, string Folder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (IdAsesor <= 0)
            {
                return BadRequest("El Id Asesor no es valido");
            }
            try
            {
                var servicioGmailCliente = new GmailClienteService(unitOfWork);
                var resultado = servicioGmailCliente.ObtenerCorreoBody(IdAsesor, IdCorreo, Folder);
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("No se encontró correo");
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 24/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Descargar
        /// </summary>
        /// <returns> File </returns>
        [Route("[action]/{idCorreo}/{nombreArchivo}/{idAsesor}/{folder}")]
        [HttpGet]
        public ActionResult Descargar(int idCorreo, string nombreArchivo, int idAsesor, string folder)
        {
            try
            {
                ICorreoService _correoService = new CorreoService(unitOfWork);
                var resultado = _correoService.Descargar(idCorreo, nombreArchivo, idAsesor, folder);
                return File(resultado, "application/pdf", nombreArchivo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 24/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Enviar mensaje.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> EnviarMensaje([FromForm] ParametrosEnviarMensajeDTO informacionCorreo)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var registroToken = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (registroToken.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    IList<IFormFile> Files = new List<IFormFile>();
                    if (informacionCorreo.Files != null && informacionCorreo.Files.Count() > 0)
                        Files = Request.Form.Files.ToList();

                    IGmailCorreoService gmailCorreoService = new GmailCorreoService(unitOfWork);
                    var resultado = await gmailCorreoService.EnviarMensajeCorreo(informacionCorreo, Files, registroToken.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new UnauthorizedAccessException("No tiene acceso al Envio de Correos");
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene correos enviados por ID.
        /// </summary>
        /// <returns> objetoDTO: CorreoBodyDTO </returns>
        [Route("[Action]/{idGmailCorreo}/{usuario}")]
        [HttpGet]
        public ActionResult ObtenerCorreoEnviadoPorId(int idGmailCorreo, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idGmailCorreo <= 0)
            {

                return BadRequest("Correo invalido");
            }
            try
            {
                var serviceGmailCorreo = new GmailCorreoService(unitOfWork);
                var resultado = serviceGmailCorreo.ObtenerCorreoEnviadoPorId(idGmailCorreo, usuario);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 04/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Enviar mensaje Gmail.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EnviarMensajeGmail([FromForm] EnviarMensajeGmailDTO parametrosEntrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var personalService = new PersonalService(unitOfWork);
                var gmailCorreoService = new GmailCorreoService(unitOfWork);
                var interaccionService = new InteraccionService(unitOfWork);
                var mandrilEnvioCorreoService = new MandrilEnvioCorreoService(unitOfWork);
                var asesor = personalService.ObtenerNombreApellido(parametrosEntrada.Remitente);
                var Mailservice = new TMK_MailService();

                parametrosEntrada.DestinatarioCc = parametrosEntrada.DestinatarioCc ?? "";
                parametrosEntrada.DestinatarioBcc = parametrosEntrada.DestinatarioBcc ?? "";

                byte[] dataMensaje = Convert.FromBase64String(parametrosEntrada.Mensaje);
                var mensajeCorreo = Encoding.UTF8.GetString(dataMensaje);

                if (!mensajeCorreo.Contains("https://repositorioweb.blob.core.windows.net/firmas/"))
                {
                    string firma = string.Empty;
                    string[] separacionEmail = asesor.Email.Split('@');
                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + separacionEmail[0] + ".png' />";
                    mensajeCorreo += "<br/>--<br/>" + firma;
                }
                parametrosEntrada.Destinatario = parametrosEntrada.Destinatario.Replace("<", "").Replace(">", "");

                var gmailCorreoBO = new GmailCorreo
                {
                    IdEtiqueta = 1,//sent:1 , inbox:2
                    Asunto = parametrosEntrada.Asunto,
                    Fecha = DateTime.Now,
                    EmailBody = mensajeCorreo,
                    Seen = false,
                    Remitente = parametrosEntrada.Remitente,
                    Cc = parametrosEntrada.DestinatarioCc,
                    Bcc = parametrosEntrada.DestinatarioBcc,
                    Destinatarios = parametrosEntrada.Destinatario,
                    IdPersonal = asesor.Id,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = parametrosEntrada.Usuario,
                    UsuarioModificacion = parametrosEntrada.Usuario
                };
                var interacion = new Interaccion();
                if (parametrosEntrada.IdActividadDetalle != null)
                {
                    interacion.IdActividadDetalle = parametrosEntrada.IdActividadDetalle;
                    interacion.IdTipoInteraccionGeneral = 1;
                    interacion.Fecha = DateTime.Now;
                    interacion.Estado = true;
                    interacion.FechaCreacion = DateTime.Now;
                    interacion.FechaModificacion = DateTime.Now;
                    interacion.UsuarioCreacion = parametrosEntrada.Usuario;
                    interacion.UsuarioModificacion = parametrosEntrada.Usuario;
                }
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = parametrosEntrada.Remitente,
                    Recipient = parametrosEntrada.Destinatario,
                    Subject = parametrosEntrada.Asunto,
                    Message = mensajeCorreo,
                    Cc = parametrosEntrada.DestinatarioCc,
                    Bcc = parametrosEntrada.DestinatarioBcc,
                    AttachedFiles = null,
                    RemitenteC = asesor.Nombres + ' ' + asesor.Apellidos
                };
                Mailservice.SetData(mailData);
                if (parametrosEntrada.Files != null)
                {
                    foreach (var file in parametrosEntrada.Files)
                    {
                        Mailservice.SetFiles(file);
                    }
                }
                var filtroBandejaCorreo = new FiltroBandejaCorreoService(unitOfWork);
                var gmailClienteService = new GmailClienteService(unitOfWork);
                CorreoClienteCredencialDTO correoClienteCredencialDTO = gmailClienteService.ObtenerClienteCredencial(parametrosEntrada.IdAsesor);
                IList<IFormFile> Files = Request.Form.Files.ToList();
                EstadoCorreoSmtpDTO rptEnvio = new();
                if (correoClienteCredencialDTO != null)
                {
                    if (parametrosEntrada.envioGrupo == true)
                    {
                        var listaEmails = parametrosEntrada.GrupoEmail.Split(',');
                        {
                            foreach (var item in listaEmails)
                                mailData.Recipient = item;
                            rptEnvio = filtroBandejaCorreo.envioEmailAdjuntoOperaciones(correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, mailData, Files);
                        }
                    }
                    else
                    {
                        rptEnvio = filtroBandejaCorreo.envioEmailAdjuntoOperaciones(correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, mailData, Files);
                    }
                    if (rptEnvio.codigo != "200")
                        return BadRequest(rptEnvio.respuesta);
                }
                using (
                    var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    gmailCorreoService.Add(gmailCorreoBO);
                    if (parametrosEntrada.IdActividadDetalle != null)
                    {
                        interaccionService.Add(interacion);
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtener Speech de correos.
        /// </summar
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCorreoSpeech([FromBody] FiltroBandejaCorreoDTO filtroBandejaCorreoDTOEntrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var filtroBandejaCorreo = new FiltroBandejaCorreoDTO();
                var filtroBandejaCorreoService = new FiltroBandejaCorreoService(unitOfWork);
                filtroBandejaCorreo.Page = filtroBandejaCorreoDTOEntrada.Page;
                filtroBandejaCorreo.PageSize = filtroBandejaCorreoDTOEntrada.PageSize;
                filtroBandejaCorreo.Skip = filtroBandejaCorreoDTOEntrada.Skip;
                filtroBandejaCorreo.Take = filtroBandejaCorreoDTOEntrada.Take;
                filtroBandejaCorreo.IdAsesor = filtroBandejaCorreoDTOEntrada.IdAsesor;
                filtroBandejaCorreo.Folder = filtroBandejaCorreoDTOEntrada.Folder;
                filtroBandejaCorreo.TipoCorreos = filtroBandejaCorreoDTOEntrada.TipoCorreos;
                filtroBandejaCorreo.FiltroKendo = filtroBandejaCorreoDTOEntrada.FiltroKendo;

                var lista = new BandejaCorreoDTO();
                lista = filtroBandejaCorreoService.ObtenerBandejaEntradaMailInboxSpeech(filtroBandejaCorreo);
                if (filtroBandejaCorreo.TipoCorreos == "Masivos")
                {
                    var emailPersona = filtroBandejaCorreo.FiltroKendo.Filters.Where(x => x.Field == "Destinatario").FirstOrDefault().Value;
                    var _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingService(unitOfWork);
                    if (emailPersona != null)
                    {
                        var listaMasivos = _repConfiguracionEnvioMailing.ObtenerEnviosMasivos(emailPersona);
                        lista.ListaCorreos.AddRange(listaMasivos);
                        lista.TotalEnviados += listaMasivos.Count;
                        var objGmail = new CorreoGmailService(unitOfWork);
                        var queryFiltroGmailCorreo = " AND Destinatarios like '%" + emailPersona + "%' and (ConCopia='modpru@bsginstitute.com' or ConCopia='modpru@bsgrupo.com') AND UsuarioCreacion = 'SYSTEM'";
                        var MensajesGmailCorreo = objGmail.FiltroCorreosPorPersonaGmailCorreo(queryFiltroGmailCorreo);
                        if (MensajesGmailCorreo != null)
                        {
                            foreach (var item in MensajesGmailCorreo)
                            {
                                item.EnvioMasivoMandrill = true;
                            }
                            lista.ListaCorreos.AddRange(MensajesGmailCorreo);
                            lista.TotalEnviados += MensajesGmailCorreo.Count;
                        }
                    }
                    lista.ListaCorreos = lista.ListaCorreos.OrderByDescending(x => x.Fecha).ToList();
                }
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtener cantidad de correos recibidos por personal.
        /// </summary>
        /// <returns> objetoDTO: BandejaCorreoDTO </returns>
        [Route("[action]/{folderCorreo}/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCorreosRecibidosPorPersonal(string folderCorreo, int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int tipoFolder = 0;
                if (folderCorreo.ToLower().Contains("inbox"))
                {
                    tipoFolder = 1;
                }
                else if (folderCorreo.ToLower().Contains("enviados"))
                {
                    tipoFolder = 3;
                }

                CorreoGmailService servicioCorreoGmail = new CorreoGmailService(unitOfWork);
                var listaCorreos = servicioCorreoGmail.ContadorCorreosPorPersona(idPersonal, tipoFolder);
                return Ok(listaCorreos);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de envio masivo.
        /// </summary>
        /// <returns> objetoDTO: CorreoBodyDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerInformacionEnvioMasivo(int idCorreo, int idAsesor, string? folder, string destinatario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAsesor <= 0)
            {
                return BadRequest(ErrorSistema.Instance.MensajeError(201));
            }
            try
            {
                CorreoService servicioCorreo = new CorreoService(unitOfWork);
                var correoBodyDTO = servicioCorreo.ObtenerInformacionEnvioMasivo(idCorreo, idAsesor, folder, destinatario);
                return Ok(correoBodyDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los correos enviados al alumno exclusivamente de los de ventas
        /// </summary>
        /// <returns> string: correoAlumno</returns>
        [Route("[Action]/{emailAlumno}")]
        [HttpGet]
        public ActionResult ObtenerCorreosEnviadosAlumnoSoloVentas(string emailAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var correo = new List<CorreoAlumnoVentasDTO>();
                GmailCorreoService servicioGmailCorreo = new GmailCorreoService(unitOfWork);
                correo = servicioGmailCorreo.ObtenerCorreosAlumnosSoloVentas(emailAlumno);
                return Ok(correo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener correos por grupos
        /// </summary>
        /// <returns> objetoBO: ListaCorreosGrupoBO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCorreosGrupos(CorreosGrupoDTO obtenerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroBandejaCorreoService filtroBandejaCorreoService = new FiltroBandejaCorreoService(unitOfWork);
                ListaCorreosGrupoDTO correosDTO = new ListaCorreosGrupoDTO();
                correosDTO.ListaCorreos = "";
                correosDTO.TotalCorreos = 0;
                correosDTO.Errores = false;
                int cantidadError = 0;
                if ((obtenerDTO.paquete.Count > 0 && obtenerDTO.estado.Count > 0 && obtenerDTO.subEstado.Count > 0) //si las 3 listas tienen items
                    || (obtenerDTO.paquete.Count > 0 && obtenerDTO.estado.Count > 0 && obtenerDTO.subEstado.Count == 0)//o si paquetes y estado tienen items
                    || (obtenerDTO.paquete.Count > 0 && obtenerDTO.estado.Count == 0 && obtenerDTO.subEstado.Count == 0))//o si solo Paquetes tiene items
                {
                    ListaCorreosGrupoDTO correos = new ListaCorreosGrupoDTO();
                    if (obtenerDTO.estado.Count > 0 && obtenerDTO.subEstado.Count > 0)//Si estados y subestado tienen items
                    {
                        foreach (var item in obtenerDTO.paquete)
                        {
                            correos = filtroBandejaCorreoService.ObtenerCorreosGrupos(obtenerDTO.idCentroCosto, item, obtenerDTO.estado, obtenerDTO.subEstado);
                            if (correos.Errores != true)
                            {
                                if (correosDTO.ListaCorreos != "")
                                {
                                    correosDTO.ListaCorreos = correosDTO.ListaCorreos + ',' + correos.ListaCorreos;
                                }
                                else
                                {
                                    correosDTO.ListaCorreos = correosDTO.ListaCorreos + correos.ListaCorreos;
                                }
                                correosDTO.TotalCorreos = correosDTO.TotalCorreos + correos.TotalCorreos;
                                correosDTO.Errores = correos.Errores;
                            }
                            else
                            {
                                cantidadError++;
                            }
                        }
                    }
                    if (obtenerDTO.estado.Count > 0 && obtenerDTO.subEstado.Count == 0)//si estado tiene items
                    {
                        foreach (var item in obtenerDTO.paquete)
                        {
                            correos = filtroBandejaCorreoService.ObtenerCorreosGrupos(obtenerDTO.idCentroCosto, item, obtenerDTO.estado, obtenerDTO.subEstado);
                            if (correos.Errores != true)
                            {
                                if (correosDTO.ListaCorreos != "")
                                {
                                    correosDTO.ListaCorreos = correosDTO.ListaCorreos + ',' + correos.ListaCorreos;
                                }
                                else
                                {
                                    correosDTO.ListaCorreos = correosDTO.ListaCorreos + correos.ListaCorreos;
                                }

                                correosDTO.TotalCorreos = correosDTO.TotalCorreos + correos.TotalCorreos;
                                correosDTO.Errores = correos.Errores;
                            }
                            else
                            {
                                cantidadError++;
                            }
                        }
                    }
                    if (obtenerDTO.estado.Count == 0 && obtenerDTO.subEstado.Count == 0)//si ninguno tiene items solo Paquetes 
                    {
                        foreach (var item in obtenerDTO.paquete)
                        {
                            correos = filtroBandejaCorreoService.ObtenerCorreosGrupos(obtenerDTO.idCentroCosto, item, obtenerDTO.estado, obtenerDTO.subEstado);
                            if (correos.Errores != true)
                            {
                                if (correosDTO.ListaCorreos != "")
                                {
                                    correosDTO.ListaCorreos = correosDTO.ListaCorreos + ',' + correos.ListaCorreos;
                                }
                                else
                                {
                                    correosDTO.ListaCorreos = correosDTO.ListaCorreos + correos.ListaCorreos;
                                }

                                correosDTO.TotalCorreos = correosDTO.TotalCorreos + correos.TotalCorreos;
                                correosDTO.Errores = correos.Errores;
                            }
                            else
                            {
                                cantidadError++;
                            }
                        }
                    }
                }
                else
                {
                    ListaCorreosGrupoDTO correos = new ListaCorreosGrupoDTO();
                    if (obtenerDTO.estado.Count > 0 && obtenerDTO.subEstado.Count > 0)//Si estados y subestado tienen items
                    {
                        correos = filtroBandejaCorreoService.ObtenerCorreosGrupos(obtenerDTO.idCentroCosto, 0, obtenerDTO.estado, obtenerDTO.subEstado);
                        correosDTO.ListaCorreos = correos.ListaCorreos;
                        correosDTO.TotalCorreos = correos.TotalCorreos;
                        correosDTO.Errores = correos.Errores;

                    }
                    if (obtenerDTO.estado.Count > 0 && obtenerDTO.subEstado.Count == 0)//si estado tiene items
                    {
                        correos = filtroBandejaCorreoService.ObtenerCorreosGrupos(obtenerDTO.idCentroCosto, 0, obtenerDTO.estado, obtenerDTO.subEstado);
                        correosDTO.ListaCorreos = correos.ListaCorreos;
                        correosDTO.TotalCorreos = correos.TotalCorreos;
                        correosDTO.Errores = correos.Errores;

                    }
                    correos = filtroBandejaCorreoService.ObtenerCorreosGrupos(obtenerDTO.idCentroCosto, 0, obtenerDTO.estado, obtenerDTO.subEstado);

                    if (obtenerDTO.estado.Count == 0 && obtenerDTO.subEstado.Count == 0)
                    {
                        correos = filtroBandejaCorreoService.ObtenerCorreosGrupos(obtenerDTO.idCentroCosto, 0, obtenerDTO.estado, obtenerDTO.subEstado);
                        correosDTO.ListaCorreos = correos.ListaCorreos;
                        correosDTO.TotalCorreos = correos.TotalCorreos;
                        correosDTO.Errores = correos.Errores;
                    }
                }
                if (cantidadError == obtenerDTO.paquete.Count() && obtenerDTO.paquete.Count != 0)
                {
                    correosDTO.Errores = true;
                }
                return Ok(correosDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Miguel Quiñones.
        /// Fecha: 09/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso portal web alumno
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EnviarAccesoPortalWebAlumno()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CorreoGmailService correoService = new CorreoGmailService(unitOfWork);
                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                var plantilla = correoService.obtenerPlantilla();



                // .FirstBy(x => x.Nombre.Contains("Datos de Acceso Portal Web") && x.IdPlantillaBase == 2);

                return Ok(plantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: -- , Miguel quiñones.
        /// Fecha: 10/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso moodle alumno
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAccesoMoodleAlumno([FromBody] DatosOportunidadAccesosPortalDTO datosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CorreoGmailService correoService = new CorreoGmailService(unitOfWork);
                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                var plantilla = correoService.enviarAccesoMoodleAlumno(datosOportunidad);



                // .FirstBy(x => x.Nombre.Contains("Datos de Acceso Portal Web") && x.IdPlantillaBase == 2);

                return Ok(plantilla);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: -- , Miguel Quiñones.
        /// Fecha: 13/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso de portal web alumno por WhatssApp
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAccesoPortalWebAlumnoWhatsApp([FromBody] DatosOportunidadAccesosPortalDTO datosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CorreoGmailService correoService = new CorreoGmailService(unitOfWork);
                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                var plantilla = correoService.enviarAccesoPortalWebAlumnoWhatsApp(datosOportunidad);

                return Ok(plantilla);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: -- Miguel Quiñones
        /// Fecha: 13/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Enviar Acceso moodle alumno por WhatssApp
        /// </summary>
        /// <returns> objetoBO: PlantillaBO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAccesoMoodleAlumnoWhatsApp([FromBody] DatosOportunidadAccesosPortalDTO datosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CorreoGmailService enviarAccesoMoodleAlumnoWhatsApp = new CorreoGmailService(unitOfWork);
                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                var plantilla = enviarAccesoMoodleAlumnoWhatsApp.enviarAccesoMoodleAlumnoWhatsApp(datosOportunidad);

                return Ok(plantilla);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        /// TipoFuncion: POST
        /// Autor: -- , Miguel Quiñones.
        /// Fecha: 13/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Enviar condiciones y caracteristicas
        /// </summary>
        /// <returns> Integer </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarCondicionesCaracteristicas([FromBody] DatosOportunidadAccesosPortalDTO datosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CorreoGmailService enviarAccesoMoodleAlumnoWhatsApp = new CorreoGmailService(unitOfWork);
                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                var plantilla = enviarAccesoMoodleAlumnoWhatsApp.enviarCondicionesCaracteristicas(datosOportunidad);

                return Ok(plantilla);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Miguel Quiñones
        /// Fecha: 13/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta Registro de Envio
        /// </summary>
        /// <returns> Confirmación de Inserción </returns>
        /// <returns> Bool </returns>
        [Route("[action]/{IdOportunidad}/{NombreUsuario}")]
        [HttpGet]
        public IActionResult InsertarEnvio(int IdOportunidad, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CorreoGmailService enviarAccesoMoodleAlumnoWhatsApp = new CorreoGmailService(unitOfWork);
                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                var documentoEnviadoWebPw = enviarAccesoMoodleAlumnoWhatsApp.InsertarEnvio(IdOportunidad, NombreUsuario);

                return Ok(documentoEnviadoWebPw);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }


}

