using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Repositorio.UnitOfWork;
using RestSharp;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Web.Helpers;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    public class PostulanteWhatsAppService : IPostulanteWhatsAppService
    {
        private IUnitOfWork _unitOfWork;
        private IPostulanteService _postulanteService;
        private IPlantillaService _plantillaService;


        public PostulanteWhatsAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _postulanteService = new PostulanteService(unitOfWork);
            _plantillaService = new PlantillaService(unitOfWork);
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        public WhatsAppMensajeEnviadoRespuestaDTO WhatsAppMensaje(WhatsAppMensajeEnviadoPostulanteDTO whatsAppEnviarMensajeDTO)
        {
            string mensaje = "";
            bool estadoWhatsapp = true;
            WhatsAppMensajeEnviadoRespuestaDTO respuesta = new WhatsAppMensajeEnviadoRespuestaDTO();
            int idPaisParaUrl;
            var whatsAppDesuscrito = _unitOfWork.WhatsAppDesuscritoRepository;
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
                if (!whatsAppDesuscrito.ExistePorNumeroTelefono(Celular))
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

                        var whatsAppConfiguracionRepository = _unitOfWork.WhatsAppConfiguracionRepository;
                        var whatsAppUsuarioCredencialRepository = _unitOfWork.WhatsAppUsuarioCredencialRepository;


                        var whatsAppHostDatosDTO = whatsAppConfiguracionRepository.ObtenerCredencialHost(idPaisParaUrl);
                        var credencialTokenExpiraDTO = whatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(whatsAppEnviarMensajeDTO.IdPersonal, idPaisParaUrl);

                        string urlToPost = whatsAppHostDatosDTO.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        WhatsAppMensajeEnviadoPostulante mensajeEnviado = new WhatsAppMensajeEnviadoPostulante();

                        if (credencialTokenExpiraDTO == null || DateTime.Now >= credencialTokenExpiraDTO.ExpiresAfter)
                        {
                            string urlToPostUsuario = whatsAppHostDatosDTO.UrlWhatsApp + "/v1/users/login";

                            var userLogin = whatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(whatsAppEnviarMensajeDTO.IdPersonal);

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
                                    var rpta = whatsAppUsuarioCredencialRepository.Add(modelCredencial);
                                    _unitOfWork.Commit();
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
                                    _waType = "hsm";

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

                                    if (whatsAppEnviarMensajeDTO.datosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        //_mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                        foreach (var listaDatos in whatsAppEnviarMensajeDTO.datosPlantillaWhatsApp)
                                        {
                                            parameters _dato = new parameters();
                                            _dato.type = "text";
                                            _dato.text = listaDatos.texto;
                                            Componente.parameters.Add(_dato);
                                        }
                                        _mensajePlantilla.template.components.Add(Componente);
                                    }


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

                            //string xd = @"{'messages':[{'id':'gBGGUZB0hxQfAgloSseVBoAyKMU'}],'meta':{'api_status':'stable','version':'2.57.2'}}";
                            //string xd = @"{""contacts"":[{""input"":""51907487141"",""wa_id"":""51907487141""}],""messages"":[{""id"":""gBGGUZB0hxQfAgnY4eOv656F5II""}],""meta"":{""api_status"":""stable"",""version"":""2.57.2""}}";
                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado.ToString());

                            if (estadoWhatsapp != false)
                            {
                                foreach (var itemGuardar in datoRespuesta.messages)
                                {
                                    var whatsAppMensajeEnviadoService = _unitOfWork.WhatsAppMensajeEnviadoPostulanteRepository;

                                    mensajeEnviado.WaId = itemGuardar.id;
                                    mensajeEnviado.WaTo = whatsAppEnviarMensajeDTO.WaTo;
                                    mensajeEnviado.WaType = _waType;
                                    mensajeEnviado.WaRecipientType = whatsAppEnviarMensajeDTO.WaRecipientType;
                                    mensajeEnviado.WaBody = whatsAppEnviarMensajeDTO.WaBody;
                                    mensajeEnviado.WaCaption = whatsAppEnviarMensajeDTO.WaCaption;
                                    mensajeEnviado.WaLink = whatsAppEnviarMensajeDTO.WaLink;
                                    mensajeEnviado.WaFileName = whatsAppEnviarMensajeDTO.WaFileName;
                                    mensajeEnviado.IdPais = whatsAppEnviarMensajeDTO.IdPais;
                                    if (whatsAppEnviarMensajeDTO.IdPostulante != 0)
                                    {
                                        mensajeEnviado.IdPostulante = whatsAppEnviarMensajeDTO.IdPostulante;
                                    }
                                    else
                                    {
                                        mensajeEnviado.IdPostulante = null;
                                    }

                                    mensajeEnviado.IdPersonal = whatsAppEnviarMensajeDTO.IdPersonal;
                                    mensajeEnviado.Estado = true;
                                    mensajeEnviado.FechaCreacion = DateTime.Now;
                                    mensajeEnviado.FechaModificacion = DateTime.Now;
                                    mensajeEnviado.UsuarioCreacion = whatsAppEnviarMensajeDTO.usuario;
                                    mensajeEnviado.UsuarioModificacion = whatsAppEnviarMensajeDTO.usuario;

                                    whatsAppMensajeEnviadoService.Add(mensajeEnviado);
                                    _unitOfWork.Commit();
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


        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene la lista de chats con postulantes asociados al idPersonal
        /// </summary>
        /// <returns>Lista de WhatsAppMensajeEnviadoPostulante</returns>
        public IEnumerable<WhatsAppMensajesPostulanteDTO> WhatsAppUltimoMensajeRecibidosChat(int IdPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeRecibidoPostulanteRepository.WhatsAppUltimoMensajeRecibidosChat(IdPersonal);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        /// Autor: Eliot Arias F.   
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Envia un mensaje de texto al contacto
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        public PlantillaWhatsAppPostulante GenerarPlantillaGPWhatsapp(GestionPersonasPlantillaWhatsAppDTO Plantilla)
        {
            try
            {
                var reemplazoEtiquetaPlantilla = new PlantillaWhatsAppPostulante();
                //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                //IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                //PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                //PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
                //TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
                List<string> Incorrectos = new List<string>();
                List<string> Incorrectos2 = new List<string>();
                var personalUsuario = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(Plantilla.Usuario);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(personalUsuario.Id);
                var postulanteProcesoSeleccion = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPorIdPostulante(Plantilla.IdPostulante);
                var accesoPostulante = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPostulanteProcesoSeleccion(postulanteProcesoSeleccion.Id);

                if (Plantilla.IdPlantilla == 1300)
                {
                    if (accesoPostulante.Token == null)
                    {
                        TTokenPostulanteProcesoSeleccion tokenPostulanteProcesoSeleccion = new TTokenPostulanteProcesoSeleccion();
                        var token = _postulanteService.GenerarClave(8);
                        var tokenHash = Crypto.HashPassword(token);
                        var tokenPostulante = _unitOfWork.TokenPostulanteProcesoSeleccionRepository.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(postulanteProcesoSeleccion.Id);

                        TTokenPostulanteProcesoSeleccion tokenNueva = new TTokenPostulanteProcesoSeleccion()
                        {
                            IdPostulanteProcesoSeleccion = postulanteProcesoSeleccion.Id,
                            Token = token,
                            TokenHash = tokenHash,
                            GuidAccess = Guid.NewGuid(),
                            Activo = true,
                            Estado = true,
                            UsuarioCreacion = Plantilla.Usuario,
                            UsuarioModificacion = Plantilla.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            FechaEnvioAccesos = DateTime.Now
                        };

                        _unitOfWork.TokenPostulanteProcesoSeleccionRepository.Insert(tokenNueva);
                        _unitOfWork.Commit();
                        accesoPostulante.Token = token;
                        accesoPostulante.GuidAccess = tokenNueva.GuidAccess;
                    }
                }

                var postulante = _unitOfWork.PostulanteRepository.FirstById(postulanteProcesoSeleccion.IdPostulante);
                if (accesoPostulante.Id > 0 && accesoPostulante.Id != null)
                {
                    DateTime? fecha = null;
                    if (Plantilla.Fecha.HasValue)
                    {
                        fecha = Plantilla.Fecha.Value.AddHours(-5);
                    }
                    //var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    //{
                    //    IdPlantilla = Postulantes.IdPlantilla,
                    //    IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
                    //    Personal = personal,
                    //    FechaGP = fecha
                    //};
                    reemplazoEtiquetaPlantilla = _plantillaService.ReemplazarEtiquetasProcesoSeleccionWhatsApp(Plantilla.IdPlantilla, postulanteProcesoSeleccion.Id, personal, Plantilla.Fecha);
                }
                return reemplazoEtiquetaPlantilla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en GenerarPlantillaGPWhatsapp " + ex.Message);
            }
        }

        /// Autor: Eliot Arias F.   
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Valida si el postulante envio un mensaje en las ultimas 24 horas
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        public bool ValidarMensajeRecibido24Horas(string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeRecibidoPostulanteRepository.ValidarMensajeRecibido24Horas(numero);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.   
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Valida si se le envio al postulante la etiqueta en las ultimas 24 horas
        /// </summary>
        /// <returns> Id del mensaje enviado </returns>
        public bool ValidarUltimaPlantillaEnviada(string plantilla, string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoPostulanteRepository.ValidarUltimaPlantillaEnviada(plantilla, numero);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

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
        public List<WhatsAppHistorialPostulanteMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area)
        {

            try
            {
                return _unitOfWork.WhatsAppMensajeRecibidoPostulanteRepository.ListaHistorialMensajeChat(idPersonal, numero, area);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        public RespuestaMensajeWhatsappDTO EnvioMensajePorPlantilla(WhatsAppMensajePostulantePlantillaComDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                WhatsAppMensajeEnviadoPostulanteDTO objetoWhatsAppHook = new WhatsAppMensajeEnviadoPostulanteDTO();

                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = json.WaTo;
                objetoWhatsAppHook.WaId = null;
                objetoWhatsAppHook.WaType = "hsm";
                objetoWhatsAppHook.WaTypeMensaje = json.WaTypeMensaje;
                objetoWhatsAppHook.WaRecipientType = "hsm";
                objetoWhatsAppHook.WaBody = json.WaBody;
                objetoWhatsAppHook.WaFile = null;
                objetoWhatsAppHook.WaFileName = null;
                objetoWhatsAppHook.WaMimeType = null;
                objetoWhatsAppHook.WaSha256 = null;
                objetoWhatsAppHook.WaLink = null;
                objetoWhatsAppHook.WaCaption = json.WaCaption;
                objetoWhatsAppHook.IdPais = json.IdPais;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = idPersonal;
                objetoWhatsAppHook.IdPostulante = json.IdPostulante;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.datosPlantillaWhatsApp = json.DatosPlantillaWhatsApp;
                //limpia el \t
                foreach (var campo in json.DatosPlantillaWhatsApp)
                {
                    campo.texto = campo.texto.Replace("\t", " ");
                    campo.texto = campo.texto.Replace("\n", " ");
                }
                //fin limpia el \t
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                // string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGp";
                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGp";

                RespuestaMensajeWhatsappDTO respuesta = new RespuestaMensajeWhatsappDTO();
                try
                {
                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;
                    //datoRespuesta = UrlPost(url, serializedResult);
                    try
                    {
                        //guardo en la base de datos
                        var resultado = _unitOfWork.WhatsAppMensajeEnviadoPostulanteRepository.InsertarMensajesLogJsonEnvios(json.IdPostulante, json.WaTo, datoRespuesta.Mensaje);
                        if (datoRespuesta.Mensaje.Contains("131026"))
                        {
                            respuesta.Mensaje = "El cliente no tiene whatsapp activo o esta inhabilitado temporalmente!!!";
                            datoRespuesta.EstadoMensaje = true;
                            //Actualizo el estado de whatsapp
                        }
                        else if (datoRespuesta.Mensaje.Contains("000001"))
                        {
                            respuesta.Mensaje = "El operador no tiene chip asignado para el pais del alumno!!!";
                            datoRespuesta.EstadoMensaje = true;
                        }

                    }
                    catch (Exception e) { }

                    respuesta.Estado = (datoRespuesta.EstadoMensaje) ? true : false;
                    return respuesta;
                }
                catch { }
                respuesta.Estado = true;
                respuesta.Mensaje = "Fallo algo al momento de enviar el whatsapp, volver a intentar!!!";


                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EnvioMensajePorTexto(WhatsAppMensajeTextoPostulanteDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                WhatsAppMensajeEnviadoPostulanteDTO objetoWhatsAppHook = new WhatsAppMensajeEnviadoPostulanteDTO();
                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = json.WaTo;
                objetoWhatsAppHook.WaId = null;
                objetoWhatsAppHook.WaType = "text";
                objetoWhatsAppHook.WaTypeMensaje = 8;
                objetoWhatsAppHook.WaRecipientType = "text";
                objetoWhatsAppHook.WaBody = json.WaBody;
                objetoWhatsAppHook.WaFile = null;
                objetoWhatsAppHook.WaFileName = null;
                objetoWhatsAppHook.WaMimeType = null;
                objetoWhatsAppHook.WaSha256 = null;
                objetoWhatsAppHook.WaLink = null;
                objetoWhatsAppHook.WaCaption = null;
                objetoWhatsAppHook.IdPais = json.IdPais;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = idPersonal;
                objetoWhatsAppHook.IdPostulante = json.IdPostulante;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.datosPlantillaWhatsApp = null;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGestionPersonas";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGestionPersonas";
                try
                {
                    //datoRespuesta = UrlPost(url, serializedResult);
                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;
                    return (datoRespuesta.EstadoMensaje == true) ? true : false;
                }
                catch { }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Eliot Arias F
        /// Fecha: 19/12/2024
        /// Version: 1.0
        /// <summary>
        /// Envio de mensajes al web hook
        /// </summary>
        /// <returns>bool</returns>
        private RespuestaMensajeHookDTO UrlPost(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                string parsedContent = jsonStringResult;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(content);

                return respuestaMensajeHook;
            }
            catch (Exception ex)
            {
                return respuestaMensajeHook;
            }
        }
        /// Autor: Eliot R Arias F.
        /// Fecha: 24/12/2024
        /// Version: 1.0
        /// <summary>
        /// metodo async para mandar los mensajes al servicio web hook de whatsapp
        /// </summary>
        /// <returns>RespuestaWebHookDTO</returns>
        private async Task<RespuestaMensajeHookDTO> UrlPostAsync(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
            try
            {
                //var handler = new HttpClientHandler
                //{
                //    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                //};

                using (HttpClient client = new HttpClient(/*handler*/))
                {
                    var content = new StringContent(jsonStringResult, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(UrlBase, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(responseBody)!;
                    }
                    else
                    {
                        respuestaMensajeHook.EstadoMensaje = false;
                        respuestaMensajeHook.Mensaje = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                respuestaMensajeHook.EstadoMensaje = false;
                respuestaMensajeHook.Mensaje = $"Excepción: {ex.Message}";
            }
            return respuestaMensajeHook;
        }

        public List<RespuestaMensajeWhatsappDTO> EnvioMensajesPorPlantillaMasivo(
            EnvioPlantillaPostulanteDTO DatosEnvio)
        {
            var respuestas = new List<RespuestaMensajeWhatsappDTO>();

            var listaMensajes = prepararListaDTO(DatosEnvio);

            foreach (var mensaje in listaMensajes)
            {
                var respuesta = new RespuestaMensajeWhatsappDTO();

                try
                {
                    var Serializer = new JavaScriptSerializer();
                    var datoRespuesta = new RespuestaMensajeHookDTO();
                    var objetoWhatsAppHook = new WhatsAppMensajeEnviadoPostulanteDTO
                    {
                        Id = 0,
                        WaTo = mensaje.WaTo,
                        WaId = null,
                        WaType = "hsm",
                        WaTypeMensaje = mensaje.WaTypeMensaje,
                        WaRecipientType = "hsm",
                        WaBody = mensaje.WaBody,
                        WaFile = null,
                        WaFileName = null,
                        WaMimeType = null,
                        WaSha256 = null,
                        WaLink = null,
                        WaCaption = mensaje.WaCaption,
                        IdPais = mensaje.IdPais,
                        EsMigracion = true,
                        IdMigracion = 0,
                        IdPersonal = mensaje.IdPersonal.Value,
                        IdPostulante = mensaje.IdPostulante,
                        usuario = mensaje.Usuario,
                        datosPlantillaWhatsApp = mensaje.DatosPlantillaWhatsApp
                    };

                    // Limpia caracteres no deseados en los datos de la plantilla
                    foreach (var campo in mensaje.DatosPlantillaWhatsApp)
                    {
                        campo.texto = campo.texto.Replace("\t", " ").Replace("\n", " ");
                    }

                    var serializedResult = Serializer.Serialize(objetoWhatsAppHook);

                    
                    //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGestionPersonas";
                    string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGestionPersonas";

                    try
                    {
                        // Realiza el envío y obtiene la respuesta
                        datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;

                        // Registra el envío en la base de datos
                        try
                        {
                            _unitOfWork.WhatsAppMensajeEnviadoPostulanteRepository.InsertarMensajesLogJsonEnvios(
                                mensaje.IdPostulante, mensaje.WaTo, datoRespuesta.Mensaje);

                            if (datoRespuesta.Mensaje.Contains("131026"))
                            {
                                respuesta.Mensaje = "El cliente no tiene WhatsApp activo o está inhabilitado temporalmente.";
                                datoRespuesta.EstadoMensaje = true;
                            }
                            else if (datoRespuesta.Mensaje.Contains("000001"))
                            {
                                respuesta.Mensaje = "El operador no tiene chip asignado para el país del alumno.";
                                datoRespuesta.EstadoMensaje = true;
                            }
                        }
                        catch (Exception e)
                        {
                            // Manejo de errores al registrar en la base de datos
                        }

                        // Asigna el estado de la respuesta
                        respuesta.Estado = datoRespuesta.EstadoMensaje;
                        respuesta.Mensaje = datoRespuesta.EstadoMensaje
                            ? "Mensaje enviado exitosamente."
                            : "Fallo al enviar el mensaje.";
                    }
                    catch
                    {
                        respuesta.Estado = false;
                        respuesta.Mensaje = "Error al enviar el mensaje. Por favor, intente nuevamente.";
                    }
                }
                catch (Exception ex)
                {
                    respuesta.Estado = false;
                    respuesta.Mensaje = $"Error inesperado: {ex.Message}";
                }

                respuestas.Add(respuesta);
            }

            return respuestas;
        }

        public List<WhatsAppMensajePostulantePlantillaComDTO> prepararListaDTO(EnvioPlantillaPostulanteDTO datosEnvio)
        {
            try
            {
                var listaPreparadaWhatsApp = new List<WhatsAppMensajePostulantePlantillaComDTO>();
                foreach (var datoPlantilla in datosEnvio.ListaIdPostulanteProcesoSeleccion)
                {
                    var personalUsuario = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(datosEnvio.Usuario);
                    var personal = _unitOfWork.PersonalRepository.ObtenerPorId(personalUsuario.Id);

                    var postulanteProcesoSeleccion = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPostulanteProcesoSeleccion(datoPlantilla);
                    var postulante = _unitOfWork.PostulanteRepository.FirstById(postulanteProcesoSeleccion.IdPostulante);

                    var celularLimpio = ObtenerNumeroWhatsApp(postulante.IdPais.Value, postulante.Celular);

                    var PlantillaConstruida = new GestionPersonasPlantillaWhatsAppDTO
                    {
                        IdPersonal = personal.Id,
                        IdPlantilla = datosEnvio.IdPlantilla,
                        Fecha = datosEnvio.Fecha,
                        IdPostulante = postulanteProcesoSeleccion.IdPostulante,
                        Usuario = datosEnvio.Usuario
                    };

                    var plantillaWhatsAppDTO = GenerarPlantillaGPWhatsapp(PlantillaConstruida);
                    var whatsAppMensajePostulantePlantilla = new WhatsAppMensajePostulantePlantillaComDTO
                    {
                        WaTo = celularLimpio,
                        WaCaption = plantillaWhatsAppDTO.Plantilla,
                        WaBody = plantillaWhatsAppDTO.Descripcion,
                        DatosPlantillaWhatsApp = plantillaWhatsAppDTO.ListaEtiquetas,
                        IdPersonal = personal.Id,
                        IdPostulante = postulanteProcesoSeleccion.IdPostulante,
                        Usuario = datosEnvio.Usuario,
                        WaTypeMensaje = 8,
                        IdPais = postulante.IdPais.Value
                    };
                    listaPreparadaWhatsApp.Add(whatsAppMensajePostulantePlantilla);

                }
                return listaPreparadaWhatsApp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ObtenerNumeroWhatsApp(int codigoPais, string celular)
        {
            try
            {
                if (celular.Contains("-"))
                {
                    var index = celular.IndexOf("-");
                    celular = celular.Substring(index + 1);
                }
                if (codigoPais == 51)
                {
                    if (celular.Length == 9)
                    {
                        celular = "51" + celular;
                    }
                }
                else if (codigoPais == 57)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 12)
                    {
                        celular = "57" + celular;
                    }
                }
                else if (codigoPais == 591)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 11)
                    {
                        celular = "591" + celular;
                    }
                }
                return celular;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool EnvioMensajePorArchivo(WhatsAppMensajeArchivoPostulanteDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                WhatsAppMensajeEnviadoPostulanteDTO objetoWhatsAppHook = new WhatsAppMensajeEnviadoPostulanteDTO();

                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = json.WaTo;
                objetoWhatsAppHook.WaId = null;
                objetoWhatsAppHook.WaType = json.WaType;
                objetoWhatsAppHook.WaTypeMensaje = 2;
                objetoWhatsAppHook.WaRecipientType = "individual";
                objetoWhatsAppHook.WaBody = null;
                objetoWhatsAppHook.WaFile = null;
                objetoWhatsAppHook.WaFileName = json.WaFileName;
                objetoWhatsAppHook.WaMimeType = null;
                objetoWhatsAppHook.WaSha256 = null;
                objetoWhatsAppHook.WaLink = json.WaLink;
                objetoWhatsAppHook.WaCaption = null;
                objetoWhatsAppHook.IdPais = json.IdPais;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = idPersonal;
                objetoWhatsAppHook.IdPostulante = json.IdPostulante;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.datosPlantillaWhatsApp = null;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGestionPersonas";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGestionPersonas";
                //try
                //{
                //    datoRespuesta = UrlPost(url, serializedResult);
                //    return (datoRespuesta.EstadoMensaje == true) ? true : false;
                //}
                try
                {
                    //datoRespuesta = UrlPost(url, serializedResult);
                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;
                    return (datoRespuesta.EstadoMensaje == true) ? true : false;
                }
                catch { }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}