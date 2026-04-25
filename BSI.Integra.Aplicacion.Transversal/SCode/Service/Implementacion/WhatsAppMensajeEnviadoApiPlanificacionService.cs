using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

using Nancy.Json;

using Newtonsoft.Json;

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion.WhatsAppMensajeEnviadoApiPlanificacionDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Autor: Lolo Zaa
    /// Fecha: 06/03/2026
    /// <summary>
    /// Servicio de envio de mensajes WhatsApp para Planificacion.
    /// Patron basado en WhatsAppMensajeEnviadoApiComercialService
    /// pero usando IdProveedor y endpoint de Planificacion.
    /// </summary>
    public class WhatsAppMensajeEnviadoApiPlanificacionService : IWhatsAppMensajeEnviadoApiPlanificacionService
    {
        private IUnitOfWork _unitOfWork;

        public WhatsAppMensajeEnviadoApiPlanificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public RespuestaMensajeWhatsappPlaDTO EnvioMensajePorPlantilla(WhatsAppMensajePlantillaPlaDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                WhatsAppEnviarMensajePlaDTO objetoWhatsAppHook = new WhatsAppEnviarMensajePlaDTO();

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
                objetoWhatsAppHook.IdProveedor = json.IdProveedor;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = json.DatosPlantillaWhatsApp;

                foreach (var campo in json.DatosPlantillaWhatsApp)
                {
                    campo.texto = campo.texto.Replace("\t", " ");
                    campo.texto = campo.texto.Replace("\n", " ");
                }

                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = "https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphPlanificacion";
                //string url = "https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphPlanificacion";//local
                RespuestaMensajeWhatsappPlaDTO respuesta = new RespuestaMensajeWhatsappPlaDTO();
                try
                {
                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;
                    try
                    {
                        _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarMensajesLogJsonEnvios(null, json.WaTo, datoRespuesta.Mensaje);
                        if (datoRespuesta.Mensaje.Contains("131026"))
                        {
                            respuesta.Mensaje = "El docente no tiene whatsapp activo o esta inhabilitado temporalmente!!!";
                            datoRespuesta.EstadoMensaje = true;
                        }
                        else if (datoRespuesta.Mensaje.Contains("000001"))
                        {
                            respuesta.Mensaje = "El coordinador no tiene chip asignado para el pais del docente!!!";
                            datoRespuesta.EstadoMensaje = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarMensajesLogJsonEnvios(null, json.WaTo, ex.Message);
                    }

                    respuesta.Estado = datoRespuesta.EstadoMensaje;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarMensajesLogJsonEnvios(null, json.WaTo, ex.Message);
                }
                respuesta.Estado = true;
                respuesta.Mensaje = "Fallo algo al momento de enviar el whatsapp, volver a intentar!!!";

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EnvioMensajePorTexto(WhatsAppMensajeTextoPlaDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                WhatsAppEnviarMensajePlaDTO objetoWhatsAppHook = new WhatsAppEnviarMensajePlaDTO();
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
                objetoWhatsAppHook.IdProveedor = json.IdProveedor;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = null;

                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = "https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphPlanificacion";
                //string url = "https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphPlanificacion";
                try
                {
                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;

                    if (!datoRespuesta.EstadoMensaje)
                    {
                        Console.WriteLine($"[WhatsApp PLA] Hook respondio EstadoMensaje=false — Mensaje: {datoRespuesta.Mensaje} | WaId: {datoRespuesta.WaId}");
                    }

                    return datoRespuesta.EstadoMensaje;
                }
                catch (Exception hookEx)
                {
                    Console.WriteLine($"[WhatsApp PLA] Error llamando al hook: {hookEx.Message}");
                    if (hookEx.InnerException != null)
                        Console.WriteLine($"[WhatsApp PLA] Inner: {hookEx.InnerException.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EnvioMensajePorArchivo(WhatsAppMensajeArchivoPlaDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                WhatsAppEnviarMensajePlaDTO objetoWhatsAppHook = new WhatsAppEnviarMensajePlaDTO();

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
                objetoWhatsAppHook.IdProveedor = json.IdProveedor;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = null;

                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = "https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphPlanificacion";//LOCAL
                //string url = "https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphPlanificacion";

                try
                {
                    datoRespuesta = UrlPost(url, serializedResult);
                    return datoRespuesta.EstadoMensaje;
                }
                catch { }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        private async Task<RespuestaMensajeHookDTO> UrlPostAsync(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    var content = new StringContent(jsonStringResult, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(UrlBase, content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(responseBody)!;
                }
                return respuestaMensajeHook;
            }
            catch (Exception ex)
            {
                return respuestaMensajeHook;
            }
        }
    }
}
