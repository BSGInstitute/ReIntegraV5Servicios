using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

using Nancy.Json;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class WhatsAppMensajeEnviadoApiComercialService : IWhatsAppMensajeEnviadoApiComercialService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppMensajeEnviadoApiComercialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public List<ChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingPorCelular(string Celular)
        {
            try
            {
                List<ObtenerChatWhatsAppMarketingPorCelularDTO> ChatWhatsAppMarketing = new List<ObtenerChatWhatsAppMarketingPorCelularDTO>();
                ChatWhatsAppMarketing = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppMarketingPorCelular(Celular);
                List<ChatWhatsAppMarketingPorCelularDTO> resultadoAgrupado = new List<ChatWhatsAppMarketingPorCelularDTO>();
                resultadoAgrupado = ChatWhatsAppMarketing.GroupBy(x => new { x.CelularUM, x.IdAlumnoUM, x.EmailUM, x.IdPaisEmpresa }).Select(x => new ChatWhatsAppMarketingPorCelularDTO
                {
                    CelularUM = x.Key.CelularUM,
                    IdAlumnoUM = x.Key.IdAlumnoUM,
                    EmailUM = x.Key.EmailUM,
                    IdPaisEmpresa = x.Key.IdPaisEmpresa,
                    ListaAlumnosPorCelular = x.GroupBy(y => new { y.IdAlumno, y.Email, y.FechaCreacion }).Select(y => new ObtenerChatWhatsAppMarketingAlumnoDTO
                    {
                        IdAlumno = y.Key.IdAlumno,
                        Email = y.Key.Email,
                        FechaCreacion = y.Key.FechaCreacion,
                    }).ToList(),
                    MensajePorCelular = x.GroupBy(y => new { y.Estatus, y.Tipo, y.IdAlumnoCelular, y.Celular, y.Alumno, y.Mensaje, y.Personal, y.FechaMensaje }).Select(y => new ObtenerChatWhatsAppMarketingMensajeDTO
                    {
                        Estatus = y.Key.Estatus,
                        Tipo = y.Key.Tipo,
                        IdAlumnoCelular = y.Key.IdAlumnoCelular,
                        Celular = y.Key.Celular,
                        Alumno = y.Key.Alumno,
                        Mensaje = y.Key.Mensaje,
                        Personal = y.Key.Personal,
                        FechaMensaje = y.Key.FechaMensaje
                    }).ToList(),
                }).ToList();
                if (resultadoAgrupado.Count() == 0) throw new Exception("No se encontraron Datos");
                return resultadoAgrupado;
            }
            catch (Exception e)
            {
                throw new Exception("No se encontraron Datos");
            }
        }

        public RespuestaMensajeWhatsappDTO EnvioMensajePorPlantilla(WhatsAppMensajePlantillaComDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                WhatsAppEnviarMensajeDTO objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO();

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
                objetoWhatsAppHook.IdAlumno = json.IdAlumno;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = json.DatosPlantillaWhatsApp;
                //limpia el \t
                foreach (var campo in json.DatosPlantillaWhatsApp)
                {
                    campo.texto = campo.texto.Replace("\t", " ");
                    campo.texto = campo.texto.Replace("\n", " ");
                }
                //fin limpia el \t
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                //string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphComercial";
                string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphComercial";

                RespuestaMensajeWhatsappDTO respuesta = new RespuestaMensajeWhatsappDTO();
                try
                {
                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;
                    //datoRespuesta = UrlPost(url, serializedResult);
                    try
                    {
                        //guardo en la base de datos
                        var resultado = _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarMensajesLogJsonEnvios(json.IdAlumno, json.WaTo, datoRespuesta.Mensaje);
                        if (datoRespuesta.Mensaje.Contains("131026"))
                        {
                            respuesta.Mensaje = "El cliente no tiene whatsapp activo o esta inhabilitado temporalmente!!!";
                            datoRespuesta.EstadoMensaje = true;
                            //Actualizo el estado de whatsapp
                        }
                        else if (datoRespuesta.Mensaje.Contains("000001"))
                        {
                            respuesta.Mensaje = "El asesor no tiene chip asignado para el pais del alumno!!!";
                            datoRespuesta.EstadoMensaje = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        var resultado = _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarMensajesLogJsonEnvios(json.IdAlumno, json.WaTo, ex.Message);
                    }

                    respuesta.Estado = (datoRespuesta.EstadoMensaje) ? true : false;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    var resultado = _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarMensajesLogJsonEnvios(json.IdAlumno, json.WaTo, ex.Message);
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
        public bool EnvioMensajePorTexto(WhatsAppMensajeTextoComDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                WhatsAppEnviarMensajeDTO objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO();
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
                objetoWhatsAppHook.IdAlumno = json.IdAlumno;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = null;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphComercial";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphComercial";
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
        public async Task<ResultadoChatAsistente> EnvioMensajeAsistenteComercialPorTexto(AsistenteComercialMensajeTextoComDTO json, string usuario)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                List<TextResponse> datoRespuesta = new List<TextResponse>();
                AsistenteComercialMensajeTextoComDTO nuevoChatAsistente = json;

                if (json.ChatId == 0)
                {
                    var resultado = _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarAsistenteComercialHiloChat(json.IdAlumno, json.IdOportunidad);
                    nuevoChatAsistente.ChatId = resultado.Resultado;
                    nuevoChatAsistente.TiempoActual = DateTime.Now;
                }
                else
                {
                    nuevoChatAsistente.ChatId = json.ChatId;
                    nuevoChatAsistente.TiempoActual = DateTime.Now;
                }

                //insertar mensaje en el historial del usuario
                var resultadomensaje = _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarAsistenteComercialHiloChatMensaje(nuevoChatAsistente.ChatId, json.EntradaUsuario, true);



                //var serializedResult = Serializer.Serialize(nuevoChatAsistente);
                //string url = $"http://ia-asistente-interno-api.bsginstitute.com/testing/api/comercial/chat/stream/postgres";//develop
                string url = $"http://ia-asistente-interno-api.bsginstitute.com/api/comercial/chat/stream";//develop

                using (var client = new HttpClient())
                {
                    try
                    {


                        string jsonContent = JsonConvert.SerializeObject(nuevoChatAsistente);
                        //Console.WriteLine("Enviando: " + jsonContent);

                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(url, content);

                        // Leer la respuesta incluso si hay error
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine($"Status Code: {response.StatusCode}");
                        //Console.WriteLine($"Respuesta: {responseBody}");

                        if (response.IsSuccessStatusCode)
                        {
                            //Console.WriteLine("Éxito!");
                            datoRespuesta = DeserializeMultipleJsonLines(responseBody);

                        }
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine($"Error: {e.Message}");
                    }
                }

                string result = string.Join("", datoRespuesta.Select(w=>w.Text));

                //insertar mensaje en el historial del modelo
                var resultadomensaje2 = _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarAsistenteComercialHiloChatMensaje(nuevoChatAsistente.ChatId, result, false);


                ResultadoChatAsistente  resultadoChat = new ResultadoChatAsistente();
                resultadoChat.result = result;
                resultadoChat.ChatId = nuevoChatAsistente.ChatId;

                return resultadoChat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<TextResponse> DeserializeMultipleJsonLines(string responseBody)
        {
            var responses = new List<TextResponse>();

            // Dividir por líneas
            var lines = responseBody.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    try
                    {
                        var textResponse = JsonConvert.DeserializeObject<TextResponse>(line);
                        responses.Add(textResponse);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error al deserializar línea: {line}");
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return responses;
        }
        public bool EnvioMensajePorArchivo(WhatsAppMensajeArchivoComDTO json, string usuario, int idPersonal)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                WhatsAppEnviarMensajeDTO objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO();

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
                objetoWhatsAppHook.IdAlumno = json.IdAlumno;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = null;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphComercial";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphComercial";
                try
                {
                    datoRespuesta = UrlPost(url, serializedResult);
                    return (datoRespuesta.EstadoMensaje) ? true : false;
                }
                catch { }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// método que funciona el de asignación, manda los datos a V4 para que el dato se asigne
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
        /// Autor: Flavio R.M.F.
        /// Fecha: 12/07/2024
        /// Version: 1.0
        /// <summary>
        /// método que funciona el de asignación, manda los datos a V4 para que el dato se asigne
        /// </summary>
        /// <returns>bool</returns>
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
        /// Autor: Flavio R.M.F.
        /// Fecha: 12/07/2024
        /// Version: 1.0
        /// <summary>
        /// método que funciona el de asignación, manda los datos a V4 para que el dato se asigne
        /// </summary>
        /// <returns>bool</returns>
        private async Task<List<TextResponse>> UrlPostAsistenteAsync(string UrlBase, string jsonStringResult)
        {
            List<TextResponse> respuestaMensajeHook = new List<TextResponse>();
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
                    respuestaMensajeHook = JsonConvert.DeserializeObject<List<TextResponse>>(responseBody)!;
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
