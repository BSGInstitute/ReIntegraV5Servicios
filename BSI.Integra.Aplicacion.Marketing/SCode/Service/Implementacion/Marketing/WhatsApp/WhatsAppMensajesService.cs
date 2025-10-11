using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Client;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Twilio.TwiML.Voice;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class WhatsAppMensajesService : IWhatsAppMensajesService
    {
        private IUnitOfWork _unitOfWork;
        public WhatsAppMensajesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        public WhatsAppMensajeEnviadoRespuestaDTO WhatsAppMensajeAlumnoAccesos(PlantillaWhatsAppEnvioAccesoDTO plantillaEnviada)
        {
            WhatsAppEnviarAccesosAlumnoDTO whatsAppEnviarMensajeDTO = new WhatsAppEnviarAccesosAlumnoDTO
            {
                Id = 0,
                WaTo = plantillaEnviada.DatoAlumno.Celular,
                WaType = "hsm",
                WaTypeMensaje = 8,
                WaRecipientType = "hsm",
                WaBody = "datos_accesos_portal_web",
                WaCaption = plantillaEnviada.Plantilla,
                IdPais = plantillaEnviada.DatoAlumno.IdCodigoPais,
                EsMigracion = true,
                IdMigracion = 0,
                IdPersonal = 5447,
                IdAlumno = plantillaEnviada.DatoAlumno.Id,
                usuario = "usuarioEjemplo",
                DatosPlantillaWhatsApp = plantillaEnviada.ListaEtiquetas
            };

            string mensaje = "";
            bool estadoWhatsapp = true;
            WhatsAppMensajeEnviadoRespuestaDTO respuesta = new WhatsAppMensajeEnviadoRespuestaDTO();
            int idPaisParaUrl;
            IWhatsAppDesuscritoService whatsAppDesuscritoService = new WhatsAppDesuscritoService(_unitOfWork);
            var mensajeEnviadoResp = "";
            if (whatsAppEnviarMensajeDTO != null)
            {
                string Celular = "";
                if (whatsAppEnviarMensajeDTO.IdPais == 51)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 51;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 57)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 57;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 591)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 591;
                }
                else
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 0;
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

                        IWhatsAppConfiguracionService whatsAppConfiguracionService = new WhatsAppConfiguracionService(_unitOfWork);
                        IWhatsAppUsuarioCredencialService whatsAppUsuarioCredencialService = new WhatsAppUsuarioCredencialService(_unitOfWork);


                        var whatsAppHostDatosDTO = whatsAppConfiguracionService.ObtenerCredencialHost(idPaisParaUrl);
                        var credencialTokenExpiraDTO = whatsAppUsuarioCredencialService.ValidarCredencialesUsuario(whatsAppEnviarMensajeDTO.IdPersonal, idPaisParaUrl);

                        string urlToPost = whatsAppHostDatosDTO.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        WhatsAppMensajeEnviado mensajeEnviado = new WhatsAppMensajeEnviado();

                        if (credencialTokenExpiraDTO == null || DateTime.Now >= credencialTokenExpiraDTO.ExpiresAfter)
                        {
                            string urlToPostUsuario = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/users/login";

                            var userLogin = whatsAppUsuarioCredencialService.CredencialUsuarioLogin(whatsAppEnviarMensajeDTO.IdPersonal);

                            var client = new RestClient(urlToPostUsuario);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", whatsAppHostDatosDTO.IpHost);
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
                                    modelCredencial.IdWhatsAppConfiguracion = whatsAppHostDatosDTO.Id;
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
                            _tokenComunicacion = credencialTokenExpiraDTO.UserAuthToken;
                            banderaLogin = true;
                        }
                        if (banderaLogin)
                        {
                            switch (whatsAppEnviarMensajeDTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;


                                                            // return errorTitle;
                                                            // Hacer algo con los datos de error
                                                        }
                                                        break;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    break;
                                case "hsm":

                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(_mensajePlantilla);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        // resultado = Client.UploadString(urlToPost, MyParameters);
                                        try
                                        {
                                            resultado = Client.UploadString(urlToPost, MyParameters);
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;

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
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        // resultado = client.UploadString(urlToPost, myParameters);
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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
                                case "document":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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

                                case "button":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
                                    _waType = "button";

                                    MensajeBotonEnvio _mensajeBoton = new MensajeBotonEnvio();
                                    _mensajeBoton.to = whatsAppEnviarMensajeDTO.WaTo;
                                    _mensajeBoton.type = whatsAppEnviarMensajeDTO.WaType;
                                    _mensajeBoton.recipient_type = whatsAppEnviarMensajeDTO.WaRecipientType;
                                    _mensajeBoton.button = new Button();
                                    _mensajeBoton.button.payload = whatsAppEnviarMensajeDTO.WaCaption;
                                    _mensajeBoton.button.text = whatsAppEnviarMensajeDTO.WaLink;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeBoton);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeBoton);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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
                            }
                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            if (estadoWhatsapp != false)
                            {
                                foreach (var itemGuardar in datoRespuesta.messages)
                                {
                                    IWhatsAppMensajeEnviadoService whatsAppMensajeEnviadoService = new WhatsAppMensajeEnviadoService(_unitOfWork);

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

                                    whatsAppMensajeEnviadoService.Add(mensajeEnviado);
                                }


                                if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "hsm")
                                {
                                    mensajeEnviadoResp = whatsAppEnviarMensajeDTO.WaCaption;
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "text")
                                {
                                    mensajeEnviadoResp = whatsAppEnviarMensajeDTO.WaBody;
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "document")
                                {
                                    mensajeEnviadoResp = "<a href=" + whatsAppEnviarMensajeDTO.WaLink + " download target=_blank><span style=font-size:32px; class=fa fa-file aria-hidden=false></span><span style=display: block;>TA-ED-Regular.pdf</span><a>";
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "image")
                                {
                                    mensajeEnviadoResp = "<a href=" + whatsAppEnviarMensajeDTO.WaLink + " download target=_blank><img src=" + whatsAppEnviarMensajeDTO.WaLink + " height=200 alt=><span style=display: block;></span><a>";
                                }
                                mensaje = mensajeEnviadoResp;
                                estadoWhatsapp = true;
                            }

                            //return (mensajeEnviadoResp);
                        }
                        else
                        {
                            mensaje = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                            estadoWhatsapp = false;
                            //return ("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    mensaje = "El numero esta desuscrito";
                    estadoWhatsapp = false;
                    //return ("El numero esta desuscrito");
                }
            }
            else
            {
                mensaje = "Los datos enviados no pueden ser nulos o estar vacios.";
                estadoWhatsapp = false;
                //return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            respuesta.Mensaje = mensaje;
            respuesta.EstadoMensaje = estadoWhatsapp;
            return respuesta;
        }


        /// Autor: Gilmer Quispe
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        public WhatsAppMensajeEnviadoRespuestaDTO WhatsAppMensaje(WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO)
        {
            string mensaje = "";
            bool estadoWhatsapp = true;
            WhatsAppMensajeEnviadoRespuestaDTO respuesta = new WhatsAppMensajeEnviadoRespuestaDTO();
            int idPaisParaUrl;
            IWhatsAppDesuscritoService whatsAppDesuscritoService = new WhatsAppDesuscritoService(_unitOfWork);
            var mensajeEnviadoResp = "";
            if (whatsAppEnviarMensajeDTO != null)
            {
                string Celular = "";
                if (whatsAppEnviarMensajeDTO.IdPais == 51)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 51;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 57)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 57;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 591)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 591;
                }
                else
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 0;
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

                        IWhatsAppConfiguracionService whatsAppConfiguracionService = new WhatsAppConfiguracionService(_unitOfWork);
                        IWhatsAppUsuarioCredencialService whatsAppUsuarioCredencialService = new WhatsAppUsuarioCredencialService(_unitOfWork);


                        var whatsAppHostDatosDTO = whatsAppConfiguracionService.ObtenerCredencialHost(idPaisParaUrl);
                        var credencialTokenExpiraDTO = whatsAppUsuarioCredencialService.ValidarCredencialesUsuario(whatsAppEnviarMensajeDTO.IdPersonal, idPaisParaUrl);

                        string urlToPost = whatsAppHostDatosDTO.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        WhatsAppMensajeEnviado mensajeEnviado = new WhatsAppMensajeEnviado();

                        if (credencialTokenExpiraDTO == null || DateTime.Now >= credencialTokenExpiraDTO.ExpiresAfter)
                        {
                            string urlToPostUsuario = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/users/login";

                            var userLogin = whatsAppUsuarioCredencialService.CredencialUsuarioLogin(whatsAppEnviarMensajeDTO.IdPersonal);

                            var client = new RestClient(urlToPostUsuario);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", whatsAppHostDatosDTO.IpHost);
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
                                    modelCredencial.IdWhatsAppConfiguracion = whatsAppHostDatosDTO.Id;
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
                            _tokenComunicacion = credencialTokenExpiraDTO.UserAuthToken;
                            banderaLogin = true;
                        }
                        if (banderaLogin)
                        {
                            switch (whatsAppEnviarMensajeDTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;


                                                            // return errorTitle;
                                                            // Hacer algo con los datos de error
                                                        }
                                                        break;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    break;
                                case "hsm":

                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                            if (listaDatos.texto.Count() < 1)
                                            {
                                                Dato.text = ".";
                                            }
                                            else
                                            {
                                                Dato.text = listaDatos.texto;
                                            }
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
                                        Client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        // resultado = Client.UploadString(urlToPost, MyParameters);
                                        try
                                        {
                                            resultado = Client.UploadString(urlToPost, MyParameters);
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;

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
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        // resultado = client.UploadString(urlToPost, myParameters);
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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
                                case "document":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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

                                case "button":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
                                    _waType = "button";

                                    MensajeBotonEnvio _mensajeBoton = new MensajeBotonEnvio();
                                    _mensajeBoton.to = whatsAppEnviarMensajeDTO.WaTo;
                                    _mensajeBoton.type = whatsAppEnviarMensajeDTO.WaType;
                                    _mensajeBoton.recipient_type = whatsAppEnviarMensajeDTO.WaRecipientType;
                                    _mensajeBoton.button = new Button();
                                    _mensajeBoton.button.payload = whatsAppEnviarMensajeDTO.WaCaption;
                                    _mensajeBoton.button.text = whatsAppEnviarMensajeDTO.WaLink;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeBoton);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeBoton);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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
                            }
                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            if (estadoWhatsapp != false)
                            {
                                foreach (var itemGuardar in datoRespuesta.messages)
                                {
                                    IWhatsAppMensajeEnviadoService whatsAppMensajeEnviadoService = new WhatsAppMensajeEnviadoService(_unitOfWork);

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

                                    whatsAppMensajeEnviadoService.Add(mensajeEnviado);
                                }


                                if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "hsm")
                                {
                                    mensajeEnviadoResp = whatsAppEnviarMensajeDTO.WaCaption;
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "text")
                                {
                                    mensajeEnviadoResp = whatsAppEnviarMensajeDTO.WaBody;
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "document")
                                {
                                    mensajeEnviadoResp = "<a href=" + whatsAppEnviarMensajeDTO.WaLink + " download target=_blank><span style=font-size:32px; class=fa fa-file aria-hidden=false></span><span style=display: block;>TA-ED-Regular.pdf</span><a>";
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "image")
                                {
                                    mensajeEnviadoResp = "<a href=" + whatsAppEnviarMensajeDTO.WaLink + " download target=_blank><img src=" + whatsAppEnviarMensajeDTO.WaLink + " height=200 alt=><span style=display: block;></span><a>";
                                }
                                mensaje = mensajeEnviadoResp;
                                estadoWhatsapp = true;
                            }

                            //return (mensajeEnviadoResp);
                        }
                        else
                        {
                            mensaje = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                            estadoWhatsapp = false;
                            //return ("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    mensaje = "El numero esta desuscrito";
                    estadoWhatsapp = false;
                    //return ("El numero esta desuscrito");
                }
            }
            else
            {
                mensaje = "Los datos enviados no pueden ser nulos o estar vacios.";
                estadoWhatsapp = false;
                //return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            respuesta.Mensaje = mensaje;
            respuesta.EstadoMensaje = estadoWhatsapp;
            return respuesta;
        }




        /// Autor: Christian Quispe
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        public WhatsAppMensajeEnviadoRespuestaDTO EnviarMensajeWhatsapp(WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO, string usuario)
        {
            string mensaje = "";
            bool estadoWhatsapp = true;
            WhatsAppMensajeEnviadoRespuestaDTO respuesta = new WhatsAppMensajeEnviadoRespuestaDTO();
            int idPaisParaUrl;
            IWhatsAppDesuscritoService whatsAppDesuscritoService = new WhatsAppDesuscritoService(_unitOfWork);
            var mensajeEnviadoResp = "";
            if (whatsAppEnviarMensajeDTO != null)
            {
                string Celular = "";
                if (whatsAppEnviarMensajeDTO.IdPais == 51)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 51;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 57)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 57;
                }
                else if (whatsAppEnviarMensajeDTO.IdPais == 591)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 591;
                }
                else
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo;
                    idPaisParaUrl = 0;
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

                        IWhatsAppConfiguracionService whatsAppConfiguracionService = new WhatsAppConfiguracionService(_unitOfWork);
                        IWhatsAppUsuarioCredencialService whatsAppUsuarioCredencialService = new WhatsAppUsuarioCredencialService(_unitOfWork);


                        var whatsAppHostDatosDTO = whatsAppConfiguracionService.ObtenerCredencialHost(idPaisParaUrl);
                        var credencialTokenExpiraDTO = whatsAppUsuarioCredencialService.ValidarCredencialesUsuario(whatsAppEnviarMensajeDTO.IdPersonal, idPaisParaUrl);

                        string urlToPost = whatsAppHostDatosDTO.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        WhatsAppMensajeEnviado mensajeEnviado = new WhatsAppMensajeEnviado();

                        if (credencialTokenExpiraDTO == null || DateTime.Now >= credencialTokenExpiraDTO.ExpiresAfter)
                        {
                            string urlToPostUsuario = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/users/login";

                            var userLogin = whatsAppUsuarioCredencialService.CredencialUsuarioLogin(whatsAppEnviarMensajeDTO.IdPersonal);

                            var client = new RestClient(urlToPostUsuario);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", whatsAppHostDatosDTO.IpHost);
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
                                    modelCredencial.IdWhatsAppConfiguracion = whatsAppHostDatosDTO.Id;
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
                            _tokenComunicacion = credencialTokenExpiraDTO.UserAuthToken;
                            banderaLogin = true;
                        }
                        if (banderaLogin)
                        {
                            switch (whatsAppEnviarMensajeDTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;


                                                            // return errorTitle;
                                                            // Hacer algo con los datos de error
                                                        }
                                                        break;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    break;
                                case "hsm":

                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                            if (listaDatos.texto.Count() < 1)
                                            {
                                                Dato.text = ".";
                                            }
                                            else
                                            {
                                                Dato.text = listaDatos.texto;
                                            }
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
                                        Client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        // resultado = Client.UploadString(urlToPost, MyParameters);
                                        try
                                        {
                                            resultado = Client.UploadString(urlToPost, MyParameters);
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;

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
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        // resultado = client.UploadString(urlToPost, myParameters);
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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
                                case "document":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
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
                                                            mensaje = errorTitle;
                                                            estadoWhatsapp = false;
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
                            }
                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            if (estadoWhatsapp != false)
                            {
                                foreach (var itemGuardar in datoRespuesta.messages)
                                {
                                    IWhatsAppMensajeEnviadoService whatsAppMensajeEnviadoService = new WhatsAppMensajeEnviadoService(_unitOfWork);

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
                                    mensajeEnviado.UsuarioCreacion = usuario;
                                    mensajeEnviado.UsuarioModificacion = usuario;

                                    whatsAppMensajeEnviadoService.Add(mensajeEnviado);
                                }


                                if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "hsm")
                                {
                                    mensajeEnviadoResp = whatsAppEnviarMensajeDTO.WaCaption;
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "text")
                                {
                                    mensajeEnviadoResp = whatsAppEnviarMensajeDTO.WaBody;
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "document")
                                {
                                    mensajeEnviadoResp = "<a href=" + whatsAppEnviarMensajeDTO.WaLink + " download target=_blank><span style=font-size:32px; class=fa fa-file aria-hidden=false></span><span style=display: block;>TA-ED-Regular.pdf</span><a>";
                                }
                                else if (whatsAppEnviarMensajeDTO.WaType.ToLower() == "image")
                                {
                                    mensajeEnviadoResp = "<a href=" + whatsAppEnviarMensajeDTO.WaLink + " download target=_blank><img src=" + whatsAppEnviarMensajeDTO.WaLink + " height=200 alt=><span style=display: block;></span><a>";
                                }
                                mensaje = mensajeEnviadoResp;
                                estadoWhatsapp = true;
                            }

                            //return (mensajeEnviadoResp);
                        }
                        else
                        {
                            mensaje = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                            estadoWhatsapp = false;
                            //return ("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    mensaje = "El numero esta desuscrito";
                    estadoWhatsapp = false;
                    //return ("El numero esta desuscrito");
                }
            }
            else
            {
                mensaje = "Los datos enviados no pueden ser nulos o estar vacios.";
                estadoWhatsapp = false;
                //return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            respuesta.Mensaje = mensaje;
            respuesta.EstadoMensaje = estadoWhatsapp;
            return respuesta;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes de WhatssApp
        /// </summary>
        /// <returns> object  </returns>
        public object WhatsAppMensajeVersionTemplate(WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO)
        {
            IWhatsAppDesuscritoService _repwhatsAppDesuscrito = new WhatsAppDesuscritoService(_unitOfWork);
            if (whatsAppEnviarMensajeDTO != null)
            {
                string Celular = "";
                if (whatsAppEnviarMensajeDTO.IdPais == 51)
                {
                    Celular = whatsAppEnviarMensajeDTO.WaTo.Substring(2, 9);
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
                if (!_repwhatsAppDesuscrito.ExistePorNumeroTelefono(Celular))
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
                        IWhatsAppConfiguracionService whatsAppConfiguracionService = new WhatsAppConfiguracionService(_unitOfWork);
                        IWhatsAppUsuarioCredencialService whatsAppUsuarioCredencialService = new WhatsAppUsuarioCredencialService(_unitOfWork);

                        var whatsAppHostDatosDTO = whatsAppConfiguracionService.ObtenerCredencialHost(whatsAppEnviarMensajeDTO.IdPais);
                        var credencialTokenExpiraDTO = whatsAppUsuarioCredencialService.ValidarCredencialesUsuario(whatsAppEnviarMensajeDTO.IdPersonal, whatsAppEnviarMensajeDTO.IdPais);

                        string urlToPost = whatsAppHostDatosDTO.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        WhatsAppMensajeEnviado mensajeEnviado = new WhatsAppMensajeEnviado();

                        if (credencialTokenExpiraDTO == null || DateTime.Now >= credencialTokenExpiraDTO.ExpiresAfter)
                        {
                            string urlToPostUsuario = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/users/login";

                            var userLogin = whatsAppUsuarioCredencialService.CredencialUsuarioLogin(whatsAppEnviarMensajeDTO.IdPersonal);

                            var client = new RestClient(urlToPostUsuario);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", whatsAppHostDatosDTO.IpHost);
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
                                    modelCredencial.IdWhatsAppConfiguracion = whatsAppHostDatosDTO.Id;
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
                            _tokenComunicacion = credencialTokenExpiraDTO.UserAuthToken;
                            banderaLogin = true;
                        }

                        if (banderaLogin)
                        {
                            switch (whatsAppEnviarMensajeDTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "hsm":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
                                    _waType = "template";

                                    MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

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

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajePlantilla);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "image":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "document":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages/";
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
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "button":
                                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/messages";
                                    _waType = "button";

                                    MensajeTextoEnvio _mensajeBoton = new MensajeTextoEnvio();

                                    _mensajeBoton.to = whatsAppEnviarMensajeDTO.WaTo;
                                    _mensajeBoton.type = whatsAppEnviarMensajeDTO.WaType;
                                    _mensajeBoton.recipient_type = whatsAppEnviarMensajeDTO.WaRecipientType;
                                    _mensajeBoton.text = new text();

                                    _mensajeBoton.text.body = whatsAppEnviarMensajeDTO.WaBody;

                                    using (WebClient client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeBoton);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeBoton);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = whatsAppHostDatosDTO.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);
                            foreach (var itemGuardar in datoRespuesta.messages)
                            {
                                IWhatsAppMensajeEnviadoService _mensajeEnviadoRepositorio = new WhatsAppMensajeEnviadoService(_unitOfWork);

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



                            return (mensajeEnviado.WaId);
                        }
                        else
                        {
                            return ("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                        }


                    }
                    catch (Exception ex)
                    {
                        return (ex);
                    }
                }
                else
                {
                    return ("El numero esta desuscrito");
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene mensaje de WhatssApp de tipo multimedia
        /// </summary>
        /// <returns> String </returns>
        public object WhatsAppMensajeMultimedia(string waId)
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
                IWhatsAppMensajeRecibidoService whatsAppMensajeRecibidoService = new WhatsAppMensajeRecibidoService(_unitOfWork);
                var whatsAppMensajeRecibido = whatsAppMensajeRecibidoService.ObtenerPorWaId(waId).FirstOrDefault();
                IWhatsAppConfiguracionService whatsAppConfiguracionService = new WhatsAppConfiguracionService(_unitOfWork);
                IWhatsAppUsuarioCredencialService whatsAppUsuarioCredencialService = new WhatsAppUsuarioCredencialService(_unitOfWork);


                var whatsAppHostDatosDTO = whatsAppConfiguracionService.ObtenerCredencialHost(whatsAppMensajeRecibido.IdPais);
                var credencialTokenExpiraDTO = whatsAppUsuarioCredencialService.ValidarCredencialesUsuario(4589, whatsAppMensajeRecibido.IdPais);

                if (credencialTokenExpiraDTO == null || DateTime.Now >= credencialTokenExpiraDTO.ExpiresAfter)
                {
                    string urlToPostUsuario = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/users/login";

                    var userLogin = whatsAppUsuarioCredencialService.CredencialUsuarioLogin(4589);

                    var client1 = new RestClient(urlToPostUsuario);
                    var request1 = new RestSharp.RestRequest(Method.POST);
                    request1.AddHeader("cache-control", "no-cache");
                    request1.AddHeader("Content-Length", "");
                    request1.AddHeader("Accept-Encoding", "gzip, deflate");
                    request1.AddHeader("Host", whatsAppHostDatosDTO.IpHost);
                    request1.AddHeader("Cache-Control", "no-cache");
                    request1.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                    request1.AddHeader("Content-Type", "application/json");
                    IRestResponse response1 = client1.Execute(request1);

                    if (response1.StatusCode == HttpStatusCode.OK)
                    {
                        var datos = JsonConvert.DeserializeObject<userLogeo>(response1.Content);

                        foreach (var item in datos.users)
                        {
                            WhatsAppUsuarioCredencial modelCredencial = new WhatsAppUsuarioCredencial();

                            modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                            modelCredencial.IdWhatsAppConfiguracion = whatsAppHostDatosDTO.Id;
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
                    _tokenComunicacion = credencialTokenExpiraDTO.UserAuthToken;
                    banderaLogin = true;
                }

                if (banderaLogin)
                {
                    string urlToPost = whatsAppHostDatosDTO.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    urlToPost = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/media/" + whatsAppMensajeRecibido.WaIdTypeMensaje;

                    var client = new RestClient(urlToPost);
                    var request = new RestSharp.RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Connection", "keep-alive");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", whatsAppHostDatosDTO.IpHost);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("Authorization", "Bearer " + _tokenComunicacion);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    IRestResponse response = client.Execute(request);
                    string respuesta = string.Empty;

                    if (whatsAppMensajeRecibido.WaType.Contains("image"))
                    {
                        respuesta = whatsAppMensajeRecibidoService.guardarArchivos(response.RawBytes, whatsAppMensajeRecibido.WaType, response.ContentType, whatsAppMensajeRecibido.WaIdTypeMensaje + ".jpeg", whatsAppMensajeRecibido.IdPais);
                    }
                    else if (whatsAppMensajeRecibido.WaType.Contains("voice"))
                    {
                        respuesta = whatsAppMensajeRecibidoService.guardarArchivos(response.RawBytes, whatsAppMensajeRecibido.WaType, response.ContentType, whatsAppMensajeRecibido.WaIdTypeMensaje + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + ".ogg", whatsAppMensajeRecibido.IdPais);
                    }
                    else if (whatsAppMensajeRecibido.WaType.Contains("video"))
                    {
                        respuesta = whatsAppMensajeRecibidoService.guardarArchivos(response.RawBytes, whatsAppMensajeRecibido.WaType, response.ContentType, whatsAppMensajeRecibido.WaIdTypeMensaje + ".mp4", whatsAppMensajeRecibido.IdPais);
                    }
                    else if (whatsAppMensajeRecibido.WaType.Contains("audio"))
                    {
                        respuesta = whatsAppMensajeRecibidoService.guardarArchivos(response.RawBytes, whatsAppMensajeRecibido.WaType, response.ContentType, whatsAppMensajeRecibido.WaIdTypeMensaje + ".mp4", whatsAppMensajeRecibido.IdPais);
                    }
                    else
                    {
                        respuesta = whatsAppMensajeRecibidoService.guardarArchivos(response.RawBytes, whatsAppMensajeRecibido.WaType, response.ContentType, whatsAppMensajeRecibido.WaFileName, whatsAppMensajeRecibido.IdPais);
                    }

                    whatsAppMensajeRecibido.WaFile = respuesta;
                    whatsAppMensajeRecibidoService.Update(whatsAppMensajeRecibido);


                    return (respuesta);
                }
                else
                {
                    return ("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                }
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/03/2023
        /// Version: 1.0
        /// <summary>
        /// Adjunta archivo para mensaje de WhatsApp
        /// </summary>
        /// <returns> String </returns>
        public object AdjuntarArchivoWhatsApp(IFormFile file)
        {
            string respuesta = string.Empty;

            try
            {
                IWhatsAppMensajeRecibidoService whatsAppMensajeRecibidoService = new WhatsAppMensajeRecibidoService(_unitOfWork);
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = whatsAppMensajeRecibidoService.GuardarArchivos(fileBytes, file.ContentType, file.FileName);
                }

                if (string.IsNullOrEmpty(respuesta))
                {
                    return (new { Resultado = "Error" });
                }
                else
                {
                    return (new { Resultado = "Ok", UrlArchivo = respuesta, NombreArchivo = file.FileName });
                }
            }
            catch (Exception Ex)
            {
                return (new { Resultado = "Error" });
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ultimo mensjae de chat de WhatsApp por Id de personal.
        /// </summary>
        /// <returns> object </returns>
        public object WhatsAppUltimoMensajeChat(int idPersonal)
        {
            if (idPersonal != null)
            {
                try
                {
                    IWhatsAppMensajeEnviadoService whatsAppMensajeEnviadoService = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var _restultado = whatsAppMensajeEnviadoService.ListaUltimoMensajeChats(idPersonal);

                    if (_restultado != null)
                    {
                        return (_restultado);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesDTO> </returns>
        public object WhatsAppUltimoMensajeRecibidosChat(int idPersonal)
        {
            if (idPersonal != null)
            {
                try
                {
                    IWhatsAppMensajeEnviadoService whatsAppMensajeEnviadoService = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var whatsAppMensajesDTOs = whatsAppMensajeEnviadoService.ListaUltimoMensajeChatsRecibido(idPersonal);
                    if (whatsAppMensajesDTOs != null)
                    {
                        return (whatsAppMensajesDTOs);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de mensajes recibidos a chat de asesor
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> object </returns>
        public object HistorialMensajeRecibidosChat(int idPersonal, string numero, string area)
        {
            if (idPersonal == null)
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {
                IWhatsAppMensajeEnviadoService servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var resultado = servicio.HistorialChatsRecibido(idPersonal, numero, area);
                if (resultado == null)
                {
                    return ("Error: Sin Datos");
                }
                return (resultado);
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene historial de chat de asesor
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> object </returns> 
        public object HistorialMensajeRecibidosChat(int idPersonal, string numero, string area, int idTipoAgenda)
        {
            if (idPersonal != null)
            {
                try
                {
                    IWhatsAppMensajeEnviadoService _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var _restultado = _repWhatsAppMensajeEnviado.HistorialChatsRecibido(idPersonal, numero, area, idTipoAgenda);
                    if (_restultado != null)
                    {
                        return (_restultado);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo mensaje recibido de WhatsApp por asesor.
        /// </summary>
        /// <returns> Lista de objetoDTO: List<WhatsAppMensajesDTO> </returns>
        public object WhatsAppUltimoMensajeEnviadosChat(int idPersonal)
        {
            if (idPersonal != null)
            {
                try
                {
                    IWhatsAppMensajeEnviadoService servicioWhatsappMsjEnv = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var whatsAppMensajesDTOs = servicioWhatsappMsjEnv.ListaUltimoMensajeChatsEnviado(idPersonal);

                    if (whatsAppMensajesDTOs != null)
                    {
                        return (whatsAppMensajesDTOs);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
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
        public object WhatsAppHistorialMensajeChat(int idPersonal, string numero, string area)
        {
            if (idPersonal == null)
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
            try
            {
                IWhatsAppMensajeEnviadoService servicio = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var whatsAppHistorialMensajesDTOs = servicio.ListaHistorialMensajeChat(idPersonal, numero, area);
                if (whatsAppHistorialMensajesDTOs == null)
                {
                    return ("Error: Sin Datos");
                }
                return (whatsAppHistorialMensajesDTOs);
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene mensajes multimedia de WhatsApp por id de WhatsApp.
        /// </summary>
        /// <returns> String </returns>
        public object WhatsAppObtenerMensajeMultimedia(string waId)
        {
            if (waId != null)
            {
                try
                {
                    IWhatsAppMensajeEnviadoService _objetoMensaje = new WhatsAppMensajeEnviadoService(_unitOfWork);

                    var _restultado = _objetoMensaje.ObtenerMensajeMultimedia(waId);

                    if (_restultado != null)
                    {
                        return (_restultado);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene conversacion por numero.
        /// </summary>
        /// <returns> object </returns>
        public object ObtenerConversacionPorNumero(string numero, int idPais)
        {
            try
            {
                IWhatsAppMensajeEnviadoService whatsAppMensajeEnviadoService = new WhatsAppMensajeEnviadoService(_unitOfWork);
                string Celular = "";
                if (idPais == 51)
                {
                    Celular = numero.Substring(2, 9);
                }
                else if (idPais == 57)
                {
                    Celular = "00" + numero;
                }
                else if (idPais == 591)
                {
                    Celular = "00" + numero;
                }
                else
                {
                    Celular = "00" + numero;
                }
                var personalAlumnoDTO = _unitOfWork.OportunidadRepository.ObtenerOportunidadPorNumero(Celular);

                if (personalAlumnoDTO == null)
                {
                    var DatosConversacion = whatsAppMensajeEnviadoService.ObtenerConversacionNumero(numero);
                    if (DatosConversacion == null) // si no tine conversaciones buscar si existe alumno
                    {
                        return (DatosConversacion);
                    }
                    return (DatosConversacion);
                }
                return (personalAlumnoDTO);
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene conversacion por oportunidad.
        /// </summary>
        /// <returns> object </returns>
        public PersonalAlumnoDTO ObtenerConversacionPorOportunidad(string numero, int idPais)
        {
            try
            {
                string Celular = "";
                if (idPais == 51)
                {
                    Celular = numero.Substring(2, 9);
                }
                else if (idPais == 57)
                {
                    Celular = "00" + numero;
                }
                else if (idPais == 591)
                {
                    Celular = "00" + numero;
                }
                else
                {
                    Celular = "00" + numero;
                }
                var personalAlumnoDTO = _unitOfWork.OportunidadRepository.ObtenerOportunidadPorNumero(Celular);

                return (personalAlumnoDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de personal.
        /// </summary>
        /// <returns> objeto PersonalAlumdnoDTO </returns>
        public PersonalAlumnoDTO ObtenerPersonalConfiguracion(string numero, int idCentroCosto, int idPais)
        {
            try
            {
                string celular = "";
                if (idPais == 51)
                {
                    celular = numero.Substring(2, 9);
                }
                else if (idPais == 57)
                {
                    celular = "00" + numero;
                }
                else if (idPais == 591)
                {
                    celular = "00" + numero;
                }
                else
                {
                    celular = "00" + numero;
                }
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerOportunidadPorNumero(idCentroCosto, celular);
                return oportunidad;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0 
        /// <summary>
        /// Validar numero libre.
        /// </summary>
        /// <returns> Boolean </returns>
        public bool ValidarNumeroLibre(string numero, int idPais, int idCentroCosto, int idPersonal)
        {
            try
            {
                IWhatsAppMensajeEnviadoService servicioMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioMensajeEnviado.ObtenerRespuestaValidarNumeroLibreCompleto(numero, idPais, idCentroCosto, idPersonal);
                return (respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el asesor con menos chats offline.
        /// </summary>
        /// <returns> Objeto PersonalNumeroMinimoChatDTO </returns>
        public PersonalNumeroMinimoChatDTO ObtenerAsesorConMenorChatsOffLine()
        {
            try
            {
                IWhatsAppMensajeEnviadoService _repWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = _repWhatsAppMensajeEnviado.ObtenerAsesorConMenorChat();
                return (respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        public bool ValidarPlantillasEnviadas(string plantilla, string numero)
        {
            try
            {
                IWhatsAppMensajeEnviadoService servicioWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioWhatsAppMensajeEnviado.ValidarPlantillasEnviadas(plantilla, numero);
                return (respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Christian Quispe
        /// Fecha: 23/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        public bool ValidarPlantillasEnviadasComercial(string plantilla, string numero, int IdPersonal, int idCodigoPais, int idPersonalAsignado)
        {
            try
            {
                //calculo el ultimo mensaje enviado por el cliente
                IWhatsAppMensajeEnviadoService servicioWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                bool respuestaFinal = false;
                var respuesta = servicioWhatsAppMensajeEnviado.ValidarUltimoMensajeRecibido(numero, IdPersonal, idCodigoPais, idPersonalAsignado);
                if (respuesta != null)//trae el ultimo mensaje enviado por el cliente al asesor 
                {
                    respuestaFinal = _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarPlantillasEnviadasApiComercial(plantilla, numero, respuesta.FechaMensaje);
                }
                else//el cliente nunca ha enviado un mensaje al asesor
                {
                    respuestaFinal = _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarPlantillasEnviadasApiComercialPersonal(plantilla, numero, IdPersonal);
                }

                return (respuestaFinal);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Christian Quispe
        /// Fecha: 23/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        public bool ValidarMesajeRecibidosApiComercial(string numero)
        {
            try
            {
                var respuesta = _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarMesajeRecibidosApiComercial(numero);
                return (respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida las plantillas para mensajes enviados.
        /// </summary>
        /// <returns> Boolean </returns>
        public bool ValidarPlantillasEnviadasNuevoWebHook(string plantilla, string numero)
        {
            try
            {
                IWhatsAppMensajeEnviadoService servicioWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioWhatsAppMensajeEnviado.ValidarPlantillasEnviadasNuevoWebHook(plantilla, numero);
                return (respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool ValidarMesajesEnviadosEn24Horas(string numero)
        {
            try
            {
                IWhatsAppMensajeEnviadoService servicioWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioWhatsAppMensajeEnviado.ValidarMesajesEnviadosEn24Horas(numero);
                return (respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool ValidarMesajesEnviadosEn24HorasComercial(string numero, int IdPersonal, int idCodigoPais, int idPersonalAsignado)
        {
            try
            {
                IWhatsAppMensajeEnviadoService servicioWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioWhatsAppMensajeEnviado.ValidarMesajesEnviadosEn24HorasComercial(numero, IdPersonal, idCodigoPais, idPersonalAsignado);
                return (respuesta);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool ValidarMesajesEnviadosEn24HorasNuevoWebHook(string numero)
        {
            try
            {
                IWhatsAppMensajeEnviadoService servicioWhatsAppMensajeEnviado = new WhatsAppMensajeEnviadoService(_unitOfWork);
                var respuesta = servicioWhatsAppMensajeEnviado.ValidarMesajesEnviadosEn24HorasNuevoWebHook(numero);
                return (respuesta);
            }
            catch (Exception ex)
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
        public object ObtenerOportunidadPorAsesorYAlumno(int idPersonal, int idAlumno, string numero)
        {
            try
            {
                var oportunidadBandejaCorreoDTO = _unitOfWork.OportunidadRepository.ObtenerOportunidadPorAsesorYAlumno(idAlumno, idPersonal, numero);

                return (new { respuesta = oportunidadBandejaCorreoDTO != null ? true : false, oportunidadBandejaCorreoDTO });

            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el último mensaje recibido por oportunidad.
        /// </summary>
        /// <returns> object </returns>
        public object WhatsAppUltimoMensajeRecibidosPorOportunidad(Dictionary<string, string> filtro)
        {
            if (filtro != null)
            {
                try
                {
                    IWhatsAppMensajeRecibidoService servicioMensajeEnviado = new WhatsAppMensajeRecibidoService(_unitOfWork);
                    var idPersonal = filtro.Where(w => w.Key.ToLower() == "idasesor").FirstOrDefault();
                    var restultado = servicioMensajeEnviado.ObtenerMensajesRecibidosOperaciones(Convert.ToInt32(idPersonal.Value));

                    if (restultado != null)
                    {
                        return (restultado);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.1
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> object </returns>
        public object WhatsAppUltimoMensajeRecibidosChatControlMensajes(int idPersonal)
        {
            if (idPersonal != null)
            {
                try
                {
                    IWhatsAppMensajeEnviadoService objetoMensaje = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var resultado = objetoMensaje.ListaUltimoMensajeChatsRecibidoControlMensaje(idPersonal);
                    if (resultado != null)
                    {
                        return (resultado);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesControlOfensivoDTO>  </returns>
        public object WhatsAppHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area)
        {
            if (idPersonal != null)
            {
                try
                {
                    IWhatsAppMensajeEnviadoService objetoMensaje = new WhatsAppMensajeEnviadoService(_unitOfWork);
                    var resultado = objetoMensaje.ListaHistorialMensajeChatControlMensaje(idPersonal, numero, area);

                    if (resultado != null)
                    {
                        return (resultado);
                    }
                    else
                    {
                        return ("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return (ex);
                }
            }
            else
            {
                return ("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        public async void WhatsAppNotificacionesMensaje(WhatsappEnvioDTO envioString)
        {
            string respuesta = "OK";
            try
            {

                var url2 = "https://integrav4-signalrcore.bsginstitute.com/";
                //var url2 = "https://localhost:7120/";

                var connection = new HubConnectionBuilder()
                .WithUrl(url2 + "hubChatWhatsapp_Peru?idUsuario=WebHook&&usuarioNombre=WebHook&&rooms=''")
                .Build();

                await connection.StartAsync();

                await connection.InvokeAsync("ActualizarIntegraWhatsApp", envioString.Asesor, envioString.obj);

                //await connection.StopAsync();

            }
            catch (Exception ex)
            {


            }
        }

        public async void WavixNotificacionesMensaje(int idPersonal, EstadoLlamadaDTO envioString)
        {
            string respuesta = "OK";
            try
            {

                //var url2 = "https://integrav4-signalrcore.bsginstitute.com/";
                var url2 = "https://localhost:7120/";

                var connection = new HubConnectionBuilder()
                .WithUrl(url2 + "hubChatWhatsapp_Peru?idUsuario=WebHook&&usuarioNombre=WebHook&&rooms=''")
                .Build();

                await connection.StartAsync();

                await connection.InvokeAsync("NotificarWavixEstadoLlamada", idPersonal, envioString);

                //await connection.StopAsync();

            }
            catch (Exception ex)
            {


            }
        }

        public async void ActualizarEstadoMarcador(MarcadorAsesorDTO item)
        {
            try
            {

                var url2 = "https://integrav4-signalrcore.bsginstitute.com/";
                string marcadorActivo = item.MarcadorActivo == true ? "1" : "0";
                //var url2 = "https://localhost:7120/";

                var connection = new HubConnectionBuilder()
                .WithUrl(url2 + "hubChatWhatsapp_Peru?idUsuario=WebHook&&usuarioNombre=WebHook&&rooms=''")
                .Build();

                await connection.StartAsync();

                await connection.InvokeAsync("ActualizarMarcadorAgenda", item.IdPersonal, marcadorActivo);

                //await connection.StopAsync();

            }
            catch (Exception ex)
            {
            }
        }

        public KeyValuePair<string, AsesorSignalDTO> VerificarAsesorOnline(int IdPersonal)
        {
            KeyValuePair<string, AsesorSignalDTO> data = new();
            try
            {

                var url2 = "https://integrav4-signalrcore.bsginstitute.com/";
                //var url2 = "https://localhost:7120/";
                var connection = new HubConnectionBuilder()
                .WithUrl(url2 + "hubChatWhatsapp_Peru?idUsuario=WebHook&&usuarioNombre=WebHook&&rooms=''")
                .Build();

                connection.StartAsync();

                return connection.InvokeAsync<KeyValuePair<string, AsesorSignalDTO>>("VerificarAsesorOnline", IdPersonal).Result;

                //await connection.StopAsync();

            }
            catch (Exception ex)
            {

                return data;

            }
        }



    }
}

