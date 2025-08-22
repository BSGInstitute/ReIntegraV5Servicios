using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaOperacionesService
    /// Autor: Jonathan Caipo
    /// Fecha: 30/12/2022
    /// <summary>
    /// Gestión general de Plantilla Operaciones
    /// </summary>
    public class PlantillaOperacionesService : IPlantillaOperacionesService
    {
        private IUnitOfWork _unitOfWork;

        public PlantillaOperacionesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Valida los número del conjunto lista
        /// </summary>
        /// <param name="numerosValidados"></param>
        public void ValidarNumeroConjuntoLista(ref List<WhatsAppResultadoConjuntoListaDTO> numerosValidados)
        {
            string urlToPost;
            bool banderaLogin = false;
            string tokenComunicacion = string.Empty;

            foreach (var alumno in numerosValidados)
            {
                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + alumno.Celular);

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(alumno.IdCodigoPais);
                    //calcula el valor que tiene
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(alumno.IdPersonal.Value, alumno.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.ObtenerCredencialUsuarioLogin(alumno.IdPersonal.Value);

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

                                var respuesta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Add(modelCredencial);
                                _unitOfWork.Commit();
                                tokenComunicacion = item.token;
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
                        tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";
                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();
                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                alumno.Validado = false;
                            }
                            else
                            {
                                alumno.Validado = true;
                            }
                        }
                    }
                    else
                    {
                        alumno.Validado = false;
                    }
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("ccrispin@bsginstitute.com");
                    correos.Add("jvillena@bsginstitute.com");
                    correos.Add("jquinones@bsginstitute.com");

                    TMK_MailService mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Validacion Numero WhatsApp";
                    mailData.Message = "Alumno: " + alumno.IdAlumno.ToString() + ", Numero: " + alumno.Celular.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    mailservice.SetData(mailData);
                    mailservice.SendMessageTask();
                }
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza el envío automático de la plantilla
        /// </summary>
        /// <param name="mensajeAlumno"></param>
        public void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> mensajeAlumno)
        {
            PlantillaService _repPlantilla = new PlantillaService(_unitOfWork);

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var idPlantilla = mensajeAlumno.FirstOrDefault().IdPlantilla.Value;
            var plantilla = _repPlantilla.ObtenerPlantillaPorId(idPlantilla);
            foreach (var alumnoMensaje in mensajeAlumno)
            {
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = alumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = plantilla.Descripcion,
                    WaCaption = alumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = alumnoMensaje.objetoplantilla
                };

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(alumnoMensaje.IdCodigoPais);
                    //personal debe tener accesos
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(alumnoMensaje.IdPersonal.Value, alumnoMensaje.IdCodigoPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.ObtenerCredencialUsuarioLogin(alumnoMensaje.IdPersonal.Value);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    if (banderaLogin)
                    {
                        switch (DTO.WaType.ToLower())
                        {
                            case "text":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                _waType = "text";

                                MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                _mensajeTexto.to = DTO.WaTo;
                                _mensajeTexto.type = DTO.WaType;
                                _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                _mensajeTexto.text = new text();

                                _mensajeTexto.text.body = DTO.WaBody;

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


                                _mensajePlantilla.to = DTO.WaTo;
                                _mensajePlantilla.type = "template";
                                _mensajePlantilla.template = new template();
                                _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                _mensajePlantilla.template.name = DTO.WaBody;
                                _mensajePlantilla.template.language = new language();
                                _mensajePlantilla.template.language.policy = "deterministic";
                                _mensajePlantilla.template.language.code = "es";
                                _mensajePlantilla.template.components = new List<components>();
                                components Componente = new components();
                                Componente.type = "body";

                                if (DTO.datosPlantillaWhatsApp != null)
                                {
                                    Componente.parameters = new List<parameters>();
                                    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    {
                                        //parameters Dato = new parameters();
                                        //Dato.type = "text";
                                        //Dato.text = listaDatos.Texto;

                                        //Componente.parameters.Add(Dato);

                                        parameters Dato = new parameters();
                                        Dato.type = "text";
                                        if (listaDatos.Texto.Count() < 1)
                                        {
                                            Dato.text = ".";
                                        }
                                        else
                                        {
                                            Dato.text = listaDatos.Texto;
                                        }
                                        Componente.parameters.Add(Dato);
                                    }
                                }
                                _mensajePlantilla.template.components.Add(Componente);
                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    //resultado = client.UploadString(urlToPost, myParameters);
                                    try
                                    {
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }
                                    catch (WebException ex)
                                    {
                                        // Ocurrió un error al enviar la solicitud HTTP
                                        var responseStream = ex.Response?.GetResponseStream();
                                        if (responseStream != null)
                                        {
                                            using (var reader = new StreamReader(responseStream))
                                            {
                                                // Leer el cuerpo de la respuesta
                                                var responseText = reader.ReadToEnd();

                                                // Deserializar la respuesta a una clase que contenga la propiedad "errors"
                                                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseText);

                                                // Verificar si la respuesta contiene errores
                                                if (responseObject.errors != null && responseObject.errors.HasValues)
                                                {
                                                    // Manejar los errores como sea necesario
                                                    foreach (var error in responseObject.errors)
                                                    {
                                                        int errorCode = (int)error.code;
                                                        string errorTitle = (string)error.title;
                                                        string errorDetails = (string)error.details;
                                                        //mensaje = errorTitle/*;*/
                                                        throw ex;
                                                        //return errorTitle;
                                                        // Hacer algo con los datos de error
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case "image":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "image";

                                MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                _mensajeImagen.to = DTO.WaTo;
                                _mensajeImagen.type = DTO.WaType;
                                _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                _mensajeImagen.image = new image();

                                _mensajeImagen.image.caption = DTO.WaCaption;
                                _mensajeImagen.image.link = DTO.WaLink;

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
                                _mensajeDocumento.to = DTO.WaTo;
                                _mensajeDocumento.type = DTO.WaType;
                                _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                _mensajeDocumento.document = new document();

                                _mensajeDocumento.document.caption = DTO.WaCaption;
                                _mensajeDocumento.document.link = DTO.WaLink;
                                _mensajeDocumento.document.filename = DTO.WaFileName;

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
                                   
                                
                                    break;
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                System.Threading.Thread.Sleep(5000);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza el envío de correos.
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="codigoAlumno"></param>
        /// <param name="destinatarios"></param>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public bool Envio(string remitente, string codigoAlumno, string destinatarios, int idPlantilla)
        {
            try
            {
                AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                PersonalService personalService = new PersonalService(_unitOfWork);
                PlantillaService plantillaService = new PlantillaService(_unitOfWork);
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                PlantillaBaseService plantillaBaseService = new PlantillaBaseService(_unitOfWork);
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);

                var matriculaCabecera = matriculaCabeceraService.ObtenerPorCodigoMatricula(codigoAlumno);
                if (matriculaCabecera.Id == null || matriculaCabecera.Id <= 0)
                {
                    throw new Exception("Codigo de alumno no valido!");
                }

                var detalleMatriculaCabecera = matriculaCabeceraService.ObtenerDetalleMatricula(matriculaCabecera.Id);

                if (!alumnoService.ExistePorId(matriculaCabecera.IdAlumno))
                {
                    throw new Exception("No existe el Alumno");
                }

                if (!plantillaService.ExistePorId(idPlantilla))
                {
                    throw new Exception("No existe el Id de la Plantilla");
                }

                var plantilla = plantillaService.ObtenerPorId(idPlantilla);
                if (!plantillaBaseService.ExistePorId(plantilla.IdPlantillaBase))
                {
                    throw new Exception("No existe el Id de la Plantilla Base");
                }

                var plantillaBase = plantillaService.ObtenerPlantillaCorreo(idPlantilla);
                var alumno = alumnoService.ObtenerPorId(matriculaCabecera.IdAlumno);

                var oportunidad = oportunidadService.ObtenerPorId(detalleMatriculaCabecera.IdOportunidad);
                var personal = personalService.ObtenerPorId(oportunidad.IdPersonalAsignado.Value);

                IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO
                {
                    IdOportunidad = oportunidad.Id,
                    IdPlantilla = idPlantilla
                });

                var destinatario = destinatarios.Split(";");

                if (plantilla.IdPlantillaBase == 2) //ValorEstatico.IdPlantillaBaseEmail
                {
                    var emailCalculado = resultadoReemplazo.EmailReemplazado;
                    List<string> correosPersonalizadosCopia = new List<string>();
                    //cuando la plantilla es condiciones y caracteristicas
                    //1227	Condiciones y Características - PERÚ OPERACIONES
                    //1245	Condiciones y Características - COLOMBIA OPERACIONES
                    if (remitente == "matriculas@bsginstitute.com" && (idPlantilla == 1227 || idPlantilla == 1245))
                    {
                        correosPersonalizadosCopia.Add("grabaciones@bsginstitute.com");
                    }
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "lhuallpa@bsginstitute.com",
                    };

                    List<string> correosPersonalizados = new List<string>
                    {
                    };
                    correosPersonalizados.AddRange(destinatario.ToList());

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = remitente,
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailService();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica guardar envio
                    var gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = remitente,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);
                    _unitOfWork.Commit();
                }
                else if (plantilla.IdPlantillaBase == 8) //ValorEstatico.IdPlantillaBaseWhatsAppFacebook
                {   //logica whatsapp
                    var whatsAppCalculado = resultadoReemplazo.WhatsAppReemplazado;

                    var listaWhatsappConjuntoListaResultado = new List<WhatsAppResultadoConjuntoListaDTO>();

                    foreach (var Destinatario in destinatario)
                    {
                        listaWhatsappConjuntoListaResultado.Add(new WhatsAppResultadoConjuntoListaDTO()
                        {
                            IdAlumno = alumno.Id,
                            Celular = Destinatario,
                            IdPersonal = personal.Id,
                            IdCodigoPais = alumno.IdCodigoPais ?? default,
                            IdConjuntoListaResultado = 0,
                            IdPgeneral = null,
                            IdPlantilla = idPlantilla,
                            NroEjecucion = 1,
                            objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                            Plantilla = whatsAppCalculado.Plantilla,
                            Validado = false
                        });
                    }
                    PlantillaOperacionesService plantillaOperacionesService = new PlantillaOperacionesService(_unitOfWork);
                    plantillaOperacionesService.ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado == true).ToList();
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                    plantillaOperacionesService.EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Envia un numero individual de la oportunidad una plantilla y segun el flag
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idPlantilla"></param>
        /// <param name="flagSeccion"></param>
        /// <returns></returns>
        public bool EnvioWhatsappNumeroIndividual(int idOportunidad, int idPlantilla, int flagSeccion)
        {
            try
            {
                PlantillaService plantillaService = new PlantillaService(_unitOfWork);
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                AlumnoService alumnoService = new AlumnoService(_unitOfWork);

                var plantilla = plantillaService.ObtenerPorId(idPlantilla);
                var oportunidad = oportunidadService.ObtenerPorId(idOportunidad);

                if (oportunidad.IdPersonalAsignado == 125 || _unitOfWork.WhatsAppUsuarioCredencialRepository.ObtenerCredencialUsuarioLogin(oportunidad.IdPersonalAsignado.Value) == null)
                    throw new Exception("No se enviara mensajes desde el asesor asignacion automatica");

                var alumno = alumnoService.ObtenerPorId(oportunidad.IdAlumno.Value);
                alumno.Celular = alumno.Celular.Replace("+", string.Empty);

                switch (alumno.IdPais)
                {
                    case 51:
                        alumno.Celular = "51" + alumno.Celular;
                        break;
                    case 57 when alumno.Celular.Length == 12 && alumno.Celular.StartsWith("57"):
                        alumno.Celular = alumno.Celular;
                        break;
                    case 57:
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                        break;
                    case 591 when alumno.Celular.Length == 11 && alumno.Celular.StartsWith("591"):
                        alumno.Celular = alumno.Celular;
                        break;
                    case 591:
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                        break;
                    default:
                        alumno.Celular = "1";
                        break;
                }

                var destinatarios = alumno.Celular.Split(";");

                if (plantilla.IdPlantillaBase == 8)
                {
                    IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                    ReemplazoEtiquetaPlantillaDTO reemplazoEtiqueta = new()
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPlantilla = idPlantilla
                    };
                    PlantillaWhatsAppCalculadoDTO whatsAppCalculado;
                    if (flagSeccion == 1)
                        whatsAppCalculado = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(reemplazoEtiqueta).WhatsAppReemplazado;
                    else
                        whatsAppCalculado = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetasNuevasOportunidades(reemplazoEtiqueta).WhatsAppReemplazado;

                    var listaWhatsappConjuntoListaResultado = destinatarios.Select(destinatario => new WhatsAppResultadoConjuntoListaDTO
                    {
                        IdAlumno = alumno.Id,
                        Celular = destinatario,
                        IdPersonal = oportunidad.IdPersonalAsignado,
                        IdCodigoPais = alumno.IdCodigoPais ?? default,
                        IdConjuntoListaResultado = 0,
                        IdPgeneral = null,
                        IdPlantilla = idPlantilla,
                        NroEjecucion = 1,
                        objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                        Plantilla = whatsAppCalculado.Plantilla,
                        Validado = false
                    }).ToList();
                    PlantillaOperacionesService plantillaOperacionesService = new PlantillaOperacionesService(_unitOfWork);
                    plantillaOperacionesService.ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado).ToList();
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                    plantillaOperacionesService.EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FiltroPlantillaTipadaDTO ObtenerFiltros()
        {
            try
            {
                FiltroPlantillaTipadaDTO data = new();
                data = _unitOfWork.PlantillaRepository.ObtenerFiltros();
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
