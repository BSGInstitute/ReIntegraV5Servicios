using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppContactoController
    /// Autor: Gilmer Quispe
    /// Fecha: 06/12/2022
    /// <summary>
    /// Gestión de WhatsApp de Contactos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppContactoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public WhatsAppContactoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer  Quispe.
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Valida el numero de whatsapp del contacto
        /// </summary>
        /// <param name="idPersonal"> id del personal </param>
        /// <param name="idPais"> id de País </param>
        /// <param name="validarNumerosWhatsAppDTO"> objeto dto que contiene numeros para su validacion </param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Route("[action]/{idPersonal}/{idPais}")]
        [HttpPost]
        public ActionResult WhatsAppValidarNumeros(int idPersonal, int idPais, [FromBody] ValidarNumerosWhatsAppDTO validarNumerosWhatsAppDTO)
        {
            if (validarNumerosWhatsAppDTO != null)
            {
                string urlToPost;
                bool banderaLogin = false;
                string tokenComunicacion = string.Empty;
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    var whatsAppConfiguracionService = new WhatsAppConfiguracionService(unitOfWork);
                    var whatsAppUsuarioCredencialService = new WhatsAppUsuarioCredencialService(unitOfWork);
                    var credencialesHost = whatsAppConfiguracionService.ObtenerCredencialHost(idPais);
                    var tokenValida = whatsAppUsuarioCredencialService.ValidarCredencialesUsuario(idPersonal, idPais);
                    var mensajeJSON = JsonConvert.SerializeObject(validarNumerosWhatsAppDTO);
                    string resultado = string.Empty;
                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = whatsAppUsuarioCredencialService.CredencialUsuarioLogin(idPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                WhatsAppUsuarioCredencial whatsAppUsuarioCredencial = new WhatsAppUsuarioCredencial();

                                whatsAppUsuarioCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                whatsAppUsuarioCredencial.IdWhatsAppConfiguracion = credencialesHost.Id;
                                whatsAppUsuarioCredencial.UserAuthToken = item.token;
                                whatsAppUsuarioCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                whatsAppUsuarioCredencial.EsMigracion = true;
                                whatsAppUsuarioCredencial.Estado = true;
                                whatsAppUsuarioCredencial.FechaCreacion = DateTime.Now;
                                whatsAppUsuarioCredencial.FechaModificacion = DateTime.Now;
                                whatsAppUsuarioCredencial.UsuarioCreacion = "whatsapp";
                                whatsAppUsuarioCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = whatsAppUsuarioCredencialService.Add(whatsAppUsuarioCredencial);

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

                    urlToPost = credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;
                            var serializer = new JavaScriptSerializer();
                            var serializedResult = serializer.Serialize(validarNumerosWhatsAppDTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        return Ok(datoRespuesta.contacts);
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
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }

        }
    }
}
