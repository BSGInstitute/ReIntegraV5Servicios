using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Twilio.Jwt.AccessToken;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoDatoService
    /// Autor: Margiory Ramirez Neyra
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class FacebookService : IFacebookService
    {
        private IUnitOfWork _unitOfWork;
        private string token = string.Empty;
        private Mapper _mapper;
        public FacebookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var objToken = _unitOfWork.AutenticacionServicioExternoRepository.ObtenerTokenpoId(1);//ValorEstatico.IdAutenticacionFacebookLeadsReportes
            token = objToken != null ? objToken.Valor : string.Empty;


        }

        private string urlBaseV12 = "https://graph.facebook.com/v20.0/";



        #region Metodos Base

        #endregion


        public List<AnuncioFacebookMetricaDTO> DescargarMetricaFacebookAnuncio(string fechaInicio, string fechaFin)
        {
            try
            {


                var _repFacebookCuentaPublicitaria = _unitOfWork.FacebookCuentaPublicitariaRepository;
                List<string> listaFacebookIdCuenta = _repFacebookCuentaPublicitaria.GetBy(x => x.Estado == true).Select(s => s.FacebookIdCuentaPublicitaria).Distinct().ToList();

                List<AnuncioFacebookMetricaDTO> resultado = new List<AnuncioFacebookMetricaDTO>();

                foreach (var facebookIdCuenta in listaFacebookIdCuenta)
                {
                    AnuncioFacebookMetricaSinProcesarDTO resultadoSinProcesar = new AnuncioFacebookMetricaSinProcesarDTO();
                    string respuestaFacebook = string.Empty;
                    string urlPaginacion = null;

                    do
                    {
                        try
                        {
                            using (WebClient client = new WebClient())
                            {
                                //this.NoINteresaElSslEstoHaraQuePuedasEjecutarLoQueNoPodias();
                                string urlGet = urlPaginacion ?? $"{urlBaseV12}{facebookIdCuenta}/insights?fields=ad_id,adset_id,campaign_id,account_id,spend,impressions,unique_clicks,clicks,inline_link_clicks,reach&level=ad&time_range={{\"since\":\"{fechaInicio}\",\"until\":\"{fechaFin}\"}}";
                                client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                respuestaFacebook = client.DownloadString(urlGet);
                            }

                            resultadoSinProcesar = JsonConvert.DeserializeObject<AnuncioFacebookMetricaSinProcesarDTO>(respuestaFacebook);
                            urlPaginacion = resultadoSinProcesar.paging == null ? null : resultadoSinProcesar.paging.next;

                            resultado.AddRange(resultadoSinProcesar.data.ToList());
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    } while (urlPaginacion != null);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Descarga las campanias Facebook y su padre desde la plataforma de Facebook
        /// </summary>
        /// <param name="listaCampania">Lista de string con el id de la plataforma de Facebook</param>
        /// <returns>string</returns>
        public List<CampaniaFacebookDTO> DescargarCampaniayPadre(List<string> listaCampania)
        {
            try
            {
                List<CampaniaFacebookDTO> listaJerarquiaFacebook = new List<CampaniaFacebookDTO>();
                List<FacebookRespuestaBatchDTO> resultadoSinProcesar = new List<FacebookRespuestaBatchDTO>();
                string respuestaFacebook = string.Empty;

                List<List<string>> bloqueListaCampania =
                                listaCampania.Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 25)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                foreach (var segmentoListaCampania in bloqueListaCampania)
                {
                    List<FacebookFormatoPostDTO> campaniaFacebookFormatoPost = new List<FacebookFormatoPostDTO>();

                    foreach (var urlRelativa in segmentoListaCampania)
                    {
                        campaniaFacebookFormatoPost.Add(new FacebookFormatoPostDTO()
                        {
                            method = "GET",
                            relative_url = $"{urlRelativa}?fields=id,name,account_id"
                        });
                    }

                    using (WebClient client = new WebClient())
                    {
                        string cuerpoPost = JsonConvert.SerializeObject(new BatchFacebookFormatoPostDTO { batch = campaniaFacebookFormatoPost.ToArray() });

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        client.Headers[HttpRequestHeader.ContentLength] = cuerpoPost.Length.ToString();
                        respuestaFacebook = client.UploadString(urlBaseV12, cuerpoPost);
                    }

                    resultadoSinProcesar = JsonConvert.DeserializeObject<List<FacebookRespuestaBatchDTO>>(respuestaFacebook);

                    foreach (var datoSinProcesar in resultadoSinProcesar)
                        listaJerarquiaFacebook.Add(JsonConvert.DeserializeObject<CampaniaFacebookDTO>(datoSinProcesar.body));
                }

                return listaJerarquiaFacebook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descarga los conjuntos anuncios Facebook y su padre desde la plataforma de Facebook
        /// </summary>
        /// <param name="listaConjuntoAnuncio">Lista de string con el id de la plataforma de Facebook</param>
        /// <returns>string</returns>
        public List<ConjuntoAnuncioFacebookJerarquiaDTO> DescargarConjuntoAnuncioyPadre(List<string> listaConjuntoAnuncio)
        {
            try
            {
                List<ConjuntoAnuncioFacebookJerarquiaDTO> listaJerarquiaFacebook = new List<ConjuntoAnuncioFacebookJerarquiaDTO>();
                List<FacebookRespuestaBatchDTO> resultadoSinProcesar = new List<FacebookRespuestaBatchDTO>();
                string respuestaFacebook = string.Empty;

                List<List<string>> bloqueListaConjuntoAnuncio =
                                listaConjuntoAnuncio.Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 25)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                foreach (var segmentoListaConjuntoAnuncio in bloqueListaConjuntoAnuncio)
                {
                    List<FacebookFormatoPostDTO> conjuntoAnuncioFacebookFormatoPost = new List<FacebookFormatoPostDTO>();

                    foreach (var urlRelativa in segmentoListaConjuntoAnuncio)
                    {
                        conjuntoAnuncioFacebookFormatoPost.Add(new FacebookFormatoPostDTO()
                        {
                            method = "GET",
                            relative_url = $"{urlRelativa}?fields=id,name,optimization_goal,billing_event,daily_budget,campaign_id,start_time,budget_remaining,created_time,effective_status,configured_status,status,updated_time"
                        });
                    }

                    using (WebClient client = new WebClient())
                    {
                        string cuerpoPost = JsonConvert.SerializeObject(new BatchFacebookFormatoPostDTO { batch = conjuntoAnuncioFacebookFormatoPost.ToArray() });

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        client.Headers[HttpRequestHeader.ContentLength] = cuerpoPost.Length.ToString();
                        respuestaFacebook = client.UploadString(urlBaseV12, cuerpoPost);
                    }

                    resultadoSinProcesar = JsonConvert.DeserializeObject<List<FacebookRespuestaBatchDTO>>(respuestaFacebook);

                    foreach (var datoSinProcesar in resultadoSinProcesar)
                        listaJerarquiaFacebook.Add(JsonConvert.DeserializeObject<ConjuntoAnuncioFacebookJerarquiaDTO>(datoSinProcesar.body));
                }

                return listaJerarquiaFacebook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Descarga los anuncios Facebook y su padre desde la plataforma de Facebook
        /// </summary>
        /// <param name="listaAnuncio">Lista de string con el id de la plataforma de Facebook</param>
        /// <returns>string</returns>
        public List<AnuncioFacebookDTO> DescargarAnuncioyPadre(List<string> listaAnuncio)
        {
            try
            {
                List<AnuncioFacebookDTO> listaJerarquiaFacebook = new List<AnuncioFacebookDTO>();
                List<FacebookRespuestaBatchDTO> resultadoSinProcesar = new List<FacebookRespuestaBatchDTO>();
                string respuestaFacebook = string.Empty;

                List<List<string>> bloqueListaAnuncio =
                                listaAnuncio.Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 25)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                foreach (var segmentoListaAnuncio in bloqueListaAnuncio)
                {
                    List<FacebookFormatoPostDTO> anuncioFacebookFormatoPost = new List<FacebookFormatoPostDTO>();

                    foreach (var urlRelativa in segmentoListaAnuncio)
                    {
                        anuncioFacebookFormatoPost.Add(new FacebookFormatoPostDTO()
                        {
                            method = "GET",
                            relative_url = $"{urlRelativa}?fields=id,name,adset_id"
                        });
                    }

                    using (WebClient client = new WebClient())
                    {
                        string cuerpoPost = JsonConvert.SerializeObject(new BatchFacebookFormatoPostDTO { batch = anuncioFacebookFormatoPost.ToArray() });

                        client.Encoding = Encoding.UTF8;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        client.Headers[HttpRequestHeader.ContentLength] = cuerpoPost.Length.ToString();
                        respuestaFacebook = client.UploadString(urlBaseV12, cuerpoPost);
                    }

                    resultadoSinProcesar = JsonConvert.DeserializeObject<List<FacebookRespuestaBatchDTO>>(respuestaFacebook);

                    foreach (var datoSinProcesar in resultadoSinProcesar)
                        listaJerarquiaFacebook.Add(JsonConvert.DeserializeObject<AnuncioFacebookDTO>(datoSinProcesar.body));
                }

                return listaJerarquiaFacebook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<DetalleMensajeDTO> DescargarConversacionPorIdPagina(string idPagina, string token)
        {
            try
            {

                List<string> ultimosDosIds = new List<string>();

                using (WebClient client = new WebClient())
                {
                    string endpoint = $"{urlBaseV12}{idPagina}/conversations?platform=MESSENGER&access_token={token}";

                    string respuestaFacebook = client.DownloadString(endpoint);

                    ConversacionFacebookDTO conversacion = JsonConvert.DeserializeObject<ConversacionFacebookDTO>(respuestaFacebook);

                    List<ConversacionDataDTO> data = conversacion.data;

                    if (data.Count >= 1)
                    {
                        ultimosDosIds.Add(data[data.Count - 1].id);
                    }

                    var respuesta = ObtenerMensajes(ultimosDosIds, token);

                    return respuesta;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DetalleMensajeDTO> DescargarConversacionPorIdUsuario(string idPagina, string idUsuario, string token)
        {
            try
            {

                List<string> ultimosDosIds = new List<string>();

                using (WebClient client = new WebClient())
                {
                    string endpoint = $"{urlBaseV12}{idPagina}/conversations?platform=MESSENGER&user_id={idUsuario}&access_token={token}";

                    string respuestaFacebook = client.DownloadString(endpoint);

                    ConversacionFacebookDTO conversacion = JsonConvert.DeserializeObject<ConversacionFacebookDTO>(respuestaFacebook);

                    List<ConversacionDataDTO> data = conversacion.data;

                    if (data.Count >= 1)
                    {
                        ultimosDosIds.Add(data[data.Count - 1].id);
                    }

                    var respuesta = ObtenerMensajes(ultimosDosIds, token);

                    return respuesta;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DetalleMensajeDTO> ObtenerMensajes(List<string> listaIdsConversaciones, string token)
        {
            try
            {
                List<string> listMensajes = new List<string>();

                foreach (var idConversacion in listaIdsConversaciones)
                {
                    using (WebClient client = new WebClient())
                    {
                        string endpoint = $"{urlBaseV12}/{idConversacion}?fields=messages&access_token={token}";

                        string respuestaFacebook = client.DownloadString(endpoint);

                        MensajesFacebookDTO conversacion = JsonConvert.DeserializeObject<MensajesFacebookDTO>(respuestaFacebook);

                        if (conversacion != null && conversacion.messages != null && conversacion.messages.data.Count > 0)
                        {
                            int cantidadMensajes = conversacion.messages.data.Count;

                            listMensajes.AddRange(conversacion.messages.data.Select(mensaje => mensaje.id));

                            if(conversacion.messages.paging.next != null)
                            {
                                string endpoint2 = conversacion.messages.paging.next;

                                string respuestaPaginacion = client.DownloadString(endpoint2);

                                MensajesDataDTO conversacionPaginacion = JsonConvert.DeserializeObject<MensajesDataDTO>(respuestaPaginacion);

                                listMensajes.AddRange(conversacionPaginacion.data.Select(mensaje => mensaje.id));
                            }
                        }
                    }
                }

                var respuesta = ObtenerDetalleMensajes(listMensajes, token);

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DetalleMensajeDTO> ObtenerDetalleMensajes(List<string> listaUltimosDosMensajes, string token)
        {
            try
            {
                List<DetalleMensajeDTO> respuesta = new List<DetalleMensajeDTO>();

                foreach (var idMensaje in listaUltimosDosMensajes)
                {
                    using (WebClient client = new WebClient())
                    {
                        string endpoint = $"{urlBaseV12}{idMensaje}?fields=id,created_time,from,to,message&access_token={token}";

                        string respuestaFacebook = client.DownloadString(endpoint);

                        DetalleMensajeDTO mensaje = JsonConvert.DeserializeObject<DetalleMensajeDTO>(respuestaFacebook);

                        respuesta.Add(mensaje);
                    }
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}

