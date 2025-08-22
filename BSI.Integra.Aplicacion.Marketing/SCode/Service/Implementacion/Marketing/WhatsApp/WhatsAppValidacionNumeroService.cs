using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNet.SignalR.Client;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class WhatsAppValidacionNumeroService : IWhatsAppValidacionNumeroService
    {
        private Mapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        public string UrlHostWhatsApp = "https://167.71.101.242:9090";
        public string IpHostWhatsApp = "167.71.101.242:9090";
        public string UserAuthToken = "eyJhbGciOiAiSFMyNTYiLCAidHlwIjogIkpXVCJ9.eyJ1c2VyIjoianJpdmVyYSIsImlhdCI6MTU2NTk4MzM2OCwiZXhwIjoxNTY2NTg4MTY4LCJ3YTpyYW5kIjoyNDIwNjg1ODM0MjM0NDQ4Njg2fQ.OD5A53Pd8FPLr6dd4Es6vxPS3hlSle1VLYZOY7TPd0k";
        string _tokenComunicacion = "";
        private readonly WhatsAppMensajePublicidadRepository _repWhatsAppMensajePublicidad;

        public WhatsAppValidacionNumeroService(IUnitOfWork unitOfWork)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFiltradoDeDatosPorPrioridadWhatsApp, FiltradoDeDatosPorPrioridadWhatsAppDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
            this.unitOfWork = unitOfWork;
        }
        public void NoINteresaElSslEstoHaraQuePuedasEjecutarLoQueNoPodias()
        { 
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }
        public bool ValidarNumeroParaWhatsApp(int idCampaniaGeneral, int prioridad)
        {
            try
            {
                var credencialeshost = unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHostGeneral();
                var DataObtenida = unitOfWork.FiltradoDeDatosPorPrioridadWhatsAppRepository.ObtenerFiltradoPorCampaniaGeneralAndPrioridad(idCampaniaGeneral,prioridad);
                var dataSegmentada = DataObtenida.GroupBy(x => new
                {
                    x.IdPais
                }).Select(g => new HelperToSendWhatsAppApiValidatorDTO {
                    idpais = g.Key.IdPais != null ? Convert.ToInt32(g.Key.IdPais) : 0,
                    ip = credencialeshost.Where(x => x.IdPais == g.Key.IdPais).Select(x => x.IpHost).FirstOrDefault(),
                    url = credencialeshost.Where(x => x.IdPais == g.Key.IdPais).Select(x => x.UrlWhatsApp).FirstOrDefault(),
                    DataParaValidar = DataObtenida.Where(x => x.IdPais == g.Key.IdPais && x.EsValidoParaWhatsApp==false).Select(x => new FiltradoDeDatosPorPrioridadWhatsAppDTO
                    {
                        IdPais = x.IdPais,
                        Id = x.Id,
                        Estado = x.Estado,
                        EsValidoParaWhatsApp = x.EsValidoParaWhatsApp,
                        FechaCreacion = x.FechaCreacion,
                        FechaModificacion = x.FechaModificacion,
                        IdAlumno = x.IdAlumno,
                        IdAreaCapacitacion = x.IdAreaCapacitacion,
                        IdAreaFormacion = x.IdAreaFormacion,
                        IdAreaTrabajo = x.IdAreaTrabajo,
                        IdcampaniaGeneral = x.IdCampaniaGeneral,
                        IdCampaniaGeneralDetalle = x.IdCampaniaGeneralDetalle,
                        IdCargo = x.IdCargo,
                        IdIndustria = x.IdIndustria,
                        IdProgramaGeneral = x.IdProgramaGeneral,
                        Movil = x.Movil,
                        Prioridad = x.Prioridad,
                        UsuarioCreacion = x.UsuarioCreacion,
                        UsuarioModificacion = x.UsuarioModificacion
                    }).ToList()
                }).ToList();
                var IdPersonal = 0;
                var credencialesDeUsuario = unitOfWork.WhatsAppUsuarioCredencialRepository.ObtenerCredencialUsuarioLogin(5099);
                NoINteresaElSslEstoHaraQuePuedasEjecutarLoQueNoPodias();

                IEnumerable<TWhatsAppMensajePublicidad> ListwhatsAppMensajePublicidad = new List<TWhatsAppMensajePublicidad>();
              

                foreach (var data in dataSegmentada)
                {
                    var credentials = new WhatsAppHostDatosDTO { IpHost = data.ip ,UrlWhatsApp=data.url};
                    var resultadoLogin = loginWpp(credencialesDeUsuario, credentials);
                    ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                    DTO.contacts = new List<string>();
                    DTO.blocking = "wait";
                    DTO.contacts.AddRange(data.DataParaValidar.Select(x => x.Movil= "+"+x.Movil).ToList()) ;
                    var respuesta =HeartBeatDeValidacionWhatsApp(credentials, resultadoLogin, DTO);
                    foreach (var item in respuesta.contacts)
                    {
                        WhatsAppMensajePublicidadDTO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadDTO();
                        if (item.status == "invalid")
                        {
                            whatsAppMensajePublicidad.EsValido = false;
                        }
                        else
                        {
                            whatsAppMensajePublicidad.EsValido = true;
                        }

                        //Alumno.Validado = true;
                        whatsAppMensajePublicidad.IdAlumno = data.DataParaValidar.Where(y=>y.Movil==item.input).Select(x=>x.IdAlumno).FirstOrDefault();
                        whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = null;
                      //  whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = data.DataParaValidar.Where(y => y.Movil == item.input).Select(x => Convert.ToInt32(x.IdPais)).FirstOrDefault();
                        whatsAppMensajePublicidad.Celular = data.DataParaValidar.Where(y => y.Movil == item.input).Select(x => x.Movil).FirstOrDefault();
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                        whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                    }
                    
                }
                _repWhatsAppMensajePublicidad.Insert(ListwhatsAppMensajePublicidad);
                //else
                //{
                //    Alumno.Validado = false;

                //    whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                //    whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                //    whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                //    whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                //    whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                //    whatsAppMensajePublicidad.Celular = Alumno.Celular;
                //    whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                //    whatsAppMensajePublicidad.Estado = true;
                //    whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                //    whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                //    whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                //    whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                //    _repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                //    return BadRequest("ErrorGenerico en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                //}
                //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                return true;

                //}





            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidarNumeroParaWhatsAppPorBloque(int idCampaniaGeneral)
        {
            try
            {
                var DataObtenida = unitOfWork.FiltradoDeDatosPorPrioridadWhatsAppRepository.ObtenerFiltradoPorCampaniaGeneral(idCampaniaGeneral);
                return true;
            }catch(Exception e)
            {
                throw e;
            }
        }
        public numerosValidos HeartBeatDeValidacionWhatsApp(WhatsAppHostDatosDTO _credencialesHost,bool banderaLogin, ValidarNumerosWhatsAppDTO DTO)
        {
            try
            {
                var urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";
                var resultado = "";

                if (banderaLogin)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;

                        var serializer = new JavaScriptSerializer();

                        var serializedResult = serializer.Serialize(DTO);
                        var mensajeJSON = JsonConvert.SerializeObject(DTO);
                        string myParameters = serializedResult;
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, myParameters);

                    }

                    var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                    return datoRespuesta;
                }
                return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool loginWpp(CredencialUsuarioLoginDTO UserData, WhatsAppHostDatosDTO _credencialesHost)
        {
            string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";


            var client = new RestClient(urlToPostUsuario);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Length", "");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", _credencialesHost.IpHost);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(UserData.UserUsername + ":" + UserData.UserPassword)));
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                foreach (var item in datos.users)
                {
                    TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                    modelCredencial.IdWhatsAppUsuario = UserData.IdWhatsAppUsuario;
                    modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                    modelCredencial.UserAuthToken = item.token;
                    modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                    modelCredencial.EsMigracion = true;
                    modelCredencial.Estado = true;
                    modelCredencial.FechaCreacion = DateTime.Now;
                    modelCredencial.FechaModificacion = DateTime.Now;
                    modelCredencial.UsuarioCreacion = "whatsapp";
                    modelCredencial.UsuarioModificacion = "whatsapp";

                    var rpta = unitOfWork.WhatsAppUsuarioCredencialRepository.Insert(modelCredencial);

                    _tokenComunicacion = item.token;
                }
                return true;
            }
            return false;
        }
    }
}
