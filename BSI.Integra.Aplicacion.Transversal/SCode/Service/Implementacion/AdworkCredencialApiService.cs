using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Ads.GoogleAds;
using Google.Ads.GoogleAds.Config;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.Util;
using Google.Ads.GoogleAds.V13.Common;
using Google.Ads.GoogleAds.V13.Errors;
using Google.Ads.GoogleAds.V13.Services;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Gax.Grpc;
using Google.LongRunning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Google.Ads.GoogleAds.V13.Enums.KeywordPlanNetworkEnum.Types;
using Grpc.Core;
using MailBee;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Google.Apis.Auth.OAuth2.Responses;
using Newtonsoft.Json;
using CommandLine.Text;
using CommandLine;
using Google.Ads.Gax.Examples;
using BSI.Integra.Repositorio.Repository.Implementation;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AdworkCredencialApiService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AdworkCredencialApi
    /// </summary>
    public class AdworkCredencialApiService : IAdworkCredencialApiService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private GoogleAdsConfig _config;

        /// Autor: Rodrigo Montesinos
        /// Fecha: 21-03-2023
        /// <summary>
        /// Constructor que ingresa credenciales de googleadsapi
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="configRoot"></param>
        public AdworkCredencialApiService(IUnitOfWork unitOfWork, IConfiguration configRoot)
        {

            IConfigurationSection section = configRoot.GetSection("GoogleAdsApi");
            _config = new GoogleAdsConfig(section);

            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAdworkCredencialApi, AdworkCredencialApi>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        public AdworkCredencialApiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAdworkCredencialApi, AdworkCredencialApi>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AdworkCredencialApi Add(AdworkCredencialApi entidad)
        {
            try
            {
                var modelo = _unitOfWork.AdworkCredencialApiRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AdworkCredencialApi>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdworkCredencialApi Update(AdworkCredencialApi entidad)
        {
            try
            {
                var modelo = _unitOfWork.AdworkCredencialApiRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AdworkCredencialApi>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.AdworkCredencialApiRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdworkCredencialApi> Add(List<AdworkCredencialApi> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AdworkCredencialApiRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AdworkCredencialApi>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdworkCredencialApi> Update(List<AdworkCredencialApi> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AdworkCredencialApiRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AdworkCredencialApi>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.AdworkCredencialApiRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// Autor: Rodrigo montesinos
        /// Fecha creacion: 03/11/2023
        /// Descripcion: Esta funncion se encarga de realziar la modificion de los datos para generar el reporte de google ads, esta funcion contiene codigo legado 
        /// Comentario para cambios o modificaciones: --!!!!!! NO DEBE SER MODIFICADA SIN TENER ENCUENTA EL DANO EN V4 !!!!!!!--
        /// </summary>
        /// <param name="FiltroReporteAdwordsApiVolumenBusquedaDTO"></param>
        /// <param name="actualziacion"></param>
        /// <returns>IEnumerable<ReporteAdwordsApiPalabrasClaveRespuestaDTO></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<ReporteAdwordsApiPalabrasClaveRespuestaDTO> GenerarReporte(FiltroReporteAdwordsApiVolumenBusquedaDTO FiltroReporteAdwordsApiVolumenBusquedaDTO,bool actualziacion,string correo)
        {
            try
            {
                List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestas = new List<ReporteAdwordsApiPalabrasClaveRespuestaDTO>();
                List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestasGuardar = new List<ReporteAdwordsApiPalabrasClaveRespuestaDTO>();
                List<PalabraClaveVolumenDTO> palabraClaveVolumenDTOsHistorico = new List<PalabraClaveVolumenDTO>();
                List<PalabraClaveVolumenDTO> palabraClaveVolumenDTOs = new List<PalabraClaveVolumenDTO>();

                List<FiltroReporteGrupoPalabrasTipoPalabra> ReporteAdwordsTemporal = new List<FiltroReporteGrupoPalabrasTipoPalabra>();
                foreach (var InterarListaPalabras in FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras)
                {
                    char[] charsToTrim = { '*', ' ', '\t' };
                    string ListaPalabras = InterarListaPalabras.CadenaTexto;
                    string[] separado = ListaPalabras.Split(new string[] { "\n" }, StringSplitOptions.None);
                    foreach (string palabra in separado)
                    {
                        string almacenar = palabra;
                        almacenar = almacenar.Replace("\t", "");
                        string result = almacenar.Trim(charsToTrim);
                        FiltroReporteGrupoPalabrasTipoPalabra VarReporteAdwordsTemporal = new FiltroReporteGrupoPalabrasTipoPalabra()
                        {
                            TipoTexto = InterarListaPalabras.TipoTexto,
                            CadenaTexto = result,
                        };
                        ReporteAdwordsTemporal.Add(VarReporteAdwordsTemporal);
                    }
                }
                FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.Clear();
                FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.AddRange(ReporteAdwordsTemporal);

                var listaFrases = FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.Where(x => x.TipoTexto == 1).ToList();
                var listaPalabras = FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.Where(x => x.TipoTexto == 2).ToList();

                List<string> listaPalabrasAProcesar = new List<string>();
                string[] palabrasABuscar = Array.Empty<string>();



                listaPalabrasAProcesar.AddRange(listaPalabras.Select(s => s.CadenaTexto.Trim().Split(" ")[0]).Where(x => x.Length > 0).ToList());
                listaPalabrasAProcesar.AddRange(listaFrases.Select(s => s.CadenaTexto).Where(x => x.Length > 0).ToList());


                listaPalabrasAProcesar = listaPalabrasAProcesar.Distinct().ToList();

                List<long> Paises = new List<long>();

                foreach (int codigoPais in FiltroReporteAdwordsApiVolumenBusquedaDTO.Paises)
                {
                    Paises.Add(Convert.ToInt64(codigoPais));
                }
                long languageParameter = Convert.ToInt64(FiltroReporteAdwordsApiVolumenBusquedaDTO.IdIdioma);
                List<string> ListadoDePalabras = new List<string>();
                ListadoDePalabras = FiltroReporteAdwordsApiVolumenBusquedaDTO.ListaPalabras.Select(x => x.CadenaTexto).ToList();

                if (!actualziacion)
                {
                    List<AdwordsApiVolumenBusquedaHistoricoRespuestaDTO> listadehistoricos = new List<AdwordsApiVolumenBusquedaHistoricoRespuestaDTO>();
                    foreach (int codigoPais in FiltroReporteAdwordsApiVolumenBusquedaDTO.Paises)
                    {
                        var datospais = _unitOfWork.PaisRepository.FirstBy(x => x.CodigoGoogleId == codigoPais);

                        List<AdwordsApiVolumenBusquedaHistoricoRespuestaDTO> historico = _unitOfWork.AdwordsApiVolumenBusquedumRepository.ObtenerHistorico(FiltroReporteAdwordsApiVolumenBusquedaDTO.FechaInicio, FiltroReporteAdwordsApiVolumenBusquedaDTO.FechaFin, string.Join(",", ListadoDePalabras), datospais.Id)
                            .Select(x=> new AdwordsApiVolumenBusquedaHistoricoRespuestaDTO{
                                Pais=datospais.NombrePais,
                                Anho=x.Anho,
                                IdPais=x.IdPais,
                                Mes=x.Mes,
                                PalabraClave=x.PalabraClave,
                                PromedioBusqueda=x.PromedioBusqueda
                            }).ToList();

                        listadehistoricos.AddRange(historico);
                    }
                    reporteAdwordsApiPalabrasClaveRespuestas = listadehistoricos.GroupBy(x => new
                    {
                        x.IdPais,
                        x.Pais
                    }).Select(c => new ReporteAdwordsApiPalabrasClaveRespuestaDTO
                    {
                        IdPais = c.Key.IdPais,
                        Pais = c.Key.Pais,
                        Detalle = c.GroupBy(x=>new { x.PalabraClave }).Select(g => new PalabraClaveVolumenDTO
                        {
                            PalabraClave = g.Key.PalabraClave,
                            PromedioPorMes = listadehistoricos.Where(x => x.PalabraClave == g.Key.PalabraClave && x.Pais == c.Key.Pais && x.IdPais == c.Key.IdPais).Select(d => new Google.Api.Ads.AdWords.v201809.MonthlySearchVolume
                            {
                                count = d.PromedioBusqueda,
                                month = d.Mes,
                                year = d.Anho
                            }).ToArray(),
                        }).ToList(),
                    }).ToList();
                    if (reporteAdwordsApiPalabrasClaveRespuestas.Count > 0)
                    {
                        return reporteAdwordsApiPalabrasClaveRespuestas;
                    }
                }
                if (actualziacion)
                {
                    foreach (int codigoPais in FiltroReporteAdwordsApiVolumenBusquedaDTO.Paises)
                    {
                        var datospais = _unitOfWork.PaisRepository.FirstBy(x => x.CodigoGoogleId == codigoPais);
                        _unitOfWork.AdwordsApiVolumenBusquedumRepository.eliminarhistorico(FiltroReporteAdwordsApiVolumenBusquedaDTO.FechaInicio, FiltroReporteAdwordsApiVolumenBusquedaDTO.FechaFin, string.Join(",", ListadoDePalabras), datospais.Id);
                    }
                }

                _config.DeveloperToken = "yj96WeNiguTTxbTNJSvTHw";
                _config.OAuth2Mode = Google.Ads.Gax.Config.OAuth2Flow.APPLICATION;
                _config.OAuth2ClientId = "908308661940-5dqqc42726ubesati7t21ie9geshbf6s.apps.googleusercontent.com";
                _config.OAuth2ClientSecret = "AOPy5MD5YMZFb8HRpq8YdZW_";
                _config.OAuth2RefreshToken = "1//0hRHzyEfGwvC9CgYIARAAGBESNwF-L9IrKRAFRhh_lHAvrMcGqAXUf1nXwivx8KLGCW8FMzR96QTmOrJ-H0tSGsxAy0rgDYIAAog";
                _config.LoginCustomerId = "6799915323";

                GoogleAdsClient Gclient = new GoogleAdsClient(_config);

                return RunGooglekeywords(
                    Gclient,
                     5743207825,
                     FiltroReporteAdwordsApiVolumenBusquedaDTO.TipoPalabra,
                    Paises.ToArray(),
                    languageParameter,
                    ListadoDePalabras.ToArray(),
                    "",
                    FiltroReporteAdwordsApiVolumenBusquedaDTO.Usuario,
                    string.Join(",", ListadoDePalabras),
                    FiltroReporteAdwordsApiVolumenBusquedaDTO.FechaInicio,
                    FiltroReporteAdwordsApiVolumenBusquedaDTO.FechaFin,
                    correo
                    ); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Autor: Rodrigo montesinos
        /// Fecha creacion: 03/11/2023
        /// Descripcion: Esta funncion se encarga de generar el reporte de google adas y almacenar la informacion en la base de datos, funcion solo accessible por funcion dentro de servicio no desde controller
        /// </summary>
        /// <param name="client"></param>
        /// <param name="customerId"></param>
        /// <param name="tipodepago"></param>
        /// <param name="locationIds"></param>
        /// <param name="languageId"></param>
        /// <param name="keywordTexts"></param>
        /// <param name="pageUrl"></param>
        /// <param name="usuario"></param>
        /// <param name="Palabras"></param>
        /// <param name="inicio"></param>
        /// <param name="fin"></param>
        /// <returns>List<ReporteAdwordsApiPalabrasClaveRespuestaDTO></returns>
        /// <exception cref="ArgumentException"></exception>
        public List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> RunGooglekeywords(GoogleAdsClient client, long customerId,int tipodepago, long[] locationIds, long languageId, string[] keywordTexts, string pageUrl, string usuario,string Palabras,DateTime inicio, DateTime fin, string correo)
        {
            List<ReporteAdwordsApiPalabrasClaveRespuestaDTO> reporteAdwordsApiPalabrasClaveRespuestas = new List<ReporteAdwordsApiPalabrasClaveRespuestaDTO>();

            KeywordPlanIdeaServiceClient keywordPlanIdeaService =
                client.GetService(Services.V13.KeywordPlanIdeaService);
            List<AdwordsApiVolumenBusquedaHistoricoRespuestaDTO> listadehistoricos = new List<AdwordsApiVolumenBusquedaHistoricoRespuestaDTO>();

            foreach (long locationId in locationIds)
            {
                var datospais = _unitOfWork.PaisRepository.FirstBy(x => x.CodigoGoogleId == locationId);
                int idPais = datospais.Id;
                string pais = datospais.NombrePais;
                //Verifica si hay palabras que buscar si la longitud es 0  retorna un error
                if (keywordTexts.Length == 0 && string.IsNullOrEmpty(pageUrl))
                {
                    throw new ArgumentException("al menos una plabra o url es necesaria ");
                }
                //Se genera la peticion
                GenerateKeywordIdeasRequest request = new GenerateKeywordIdeasRequest()
                {
                    CustomerId = customerId.ToString(),
                };
                
                if (keywordTexts.Length == 0)
                {
                    request.UrlSeed = new UrlSeed()
                    {
                        Url = pageUrl
                    };
                }
                else if (string.IsNullOrEmpty(pageUrl))
                {
                    request.KeywordSeed = new KeywordSeed();
                    request.KeywordSeed.Keywords.AddRange(keywordTexts);
                }
                else
                {
                    request.KeywordAndUrlSeed = new KeywordAndUrlSeed();
                    request.KeywordAndUrlSeed.Url = pageUrl;
                    request.KeywordAndUrlSeed.Keywords.AddRange(keywordTexts);
                }
                //se incializa la limitacion de resultados obtenidos (inicio de busqueda)
                request.HistoricalMetricsOptions = new HistoricalMetricsOptions
                {
                    YearMonthRange= new YearMonthRange
                    {
                        Start=new YearMonth()
                        {
                            Month = ObtenerMesDeBusqueda(inicio.Month),
                            Year = inicio.Year
                        },
                        End= new YearMonth
                        {
                            Month = ObtenerMesDeBusqueda(fin.Month),
                            Year = fin.Year
                        }
                    }
                };

                // Crea una lista de paises por los cuales se realziara la busqueda
                request.GeoTargetConstants.Add(ResourceNames.GeoTargetConstant(locationId));

                //Indica el tipo de lenguaje que sera usado para la busqueda
                request.Language = ResourceNames.LanguageConstant(languageId);
                request.KeywordPlanNetwork = KeywordPlanNetwork.GoogleSearchAndPartners;

                try
                {
                    var response =keywordPlanIdeaService.GenerateKeywordIdeas(request);
                    List<AdwordsApiPalabraClave> adwordsApiPalabraClaveLista = new List<AdwordsApiPalabraClave>();
                    List<AdwordsApiVolumenBusquedum> adwordsApiVolumenBusquedaLista = new List<AdwordsApiVolumenBusquedum>();
                   
                    foreach (GenerateKeywordIdeaResult result in response)
                    {
                        
                        if (tipodepago == 1)
                        {
                            foreach (var busqueda in keywordTexts)
                            {
                                if (result != null)
                                {
                                    if (result.Text.ToLower()==busqueda.ToLower())
                                    {
                                        AdwordsApiPalabraClave adwordsApiPalabraClave = new AdwordsApiPalabraClave()
                                        {
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            PalabraClave = result.Text,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario
                                        };
                                        KeywordPlanHistoricalMetrics metrics = result.KeywordIdeaMetrics;
                                        int idAdwords = _unitOfWork.AdwordsApiPalabraClaveRepository.InsertarPalabraClaveYretornarId(adwordsApiPalabraClave);
                                        if (result.KeywordIdeaMetrics != null)
                                        {
                                            foreach (var volumenes in result.KeywordIdeaMetrics.MonthlySearchVolumes)
                                            {
                                                AdwordsApiVolumenBusquedum adwordsApiVolumenBusqueda = new AdwordsApiVolumenBusquedum()
                                                {
                                                    IdAdwordsApiPalabraClave = idAdwords,
                                                    Anho = Convert.ToInt32(volumenes.Year),
                                                    Mes = Convert.ToInt32(volumenes.Month)-1,
                                                    Estado = true,
                                                    FechaCreacion = DateTime.Now,
                                                    FechaModificacion = DateTime.Now,
                                                    IdPais = idPais,
                                                    PromedioBusqueda = Convert.ToInt32(volumenes.MonthlySearches),
                                                    UsuarioCreacion = usuario,
                                                    UsuarioModificacion = usuario,
                                                };
                                                adwordsApiVolumenBusquedaLista.Add(adwordsApiVolumenBusqueda);
                                            }
                                        }
                                        _unitOfWork.AdwordsApiVolumenBusquedumRepository.Add(adwordsApiVolumenBusquedaLista);
                                        _unitOfWork.Commit();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (result != null)
                            {
                                AdwordsApiPalabraClave adwordsApiPalabraClave = new AdwordsApiPalabraClave()
                                {
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    PalabraClave = result.Text,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario
                                };
                                KeywordPlanHistoricalMetrics metrics = result.KeywordIdeaMetrics;
                                int idAdwords = _unitOfWork.AdwordsApiPalabraClaveRepository.InsertarPalabraClaveYretornarId(adwordsApiPalabraClave);
                                if (result.KeywordIdeaMetrics != null)
                                {
                                    foreach (var volumenes in result.KeywordIdeaMetrics.MonthlySearchVolumes)
                                    {
                                        AdwordsApiVolumenBusquedum adwordsApiVolumenBusqueda = new AdwordsApiVolumenBusquedum()
                                        {
                                            IdAdwordsApiPalabraClave = idAdwords,
                                            Anho = Convert.ToInt32(volumenes.Year),
                                            Mes = Convert.ToInt32(volumenes.Month)-1,
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            IdPais = idPais,
                                            PromedioBusqueda = Convert.ToInt32(volumenes.MonthlySearches),
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                        };
                                        adwordsApiVolumenBusquedaLista.Add(adwordsApiVolumenBusqueda);
                                    }
                                }
                                _unitOfWork.AdwordsApiVolumenBusquedumRepository.Add(adwordsApiVolumenBusquedaLista);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                   //envio de correos, validar o modificar datos de envio aqui
                    List<string> correos = new List<string>();
                    correos.Add("ccrispin@bsginstitute.com");
                    correos.Add(correo);

                    TMK_MailService Mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "ccrispin@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Reporte Volumen Busqueda - Guardado EXITOSO";
                    mailData.Message = "Usuario: " + usuario + "<br> Palabras: " + Palabras;
                    mailData.Cc = "avasquezb@bsginstitute.com";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();


                    List<PalabraClaveVolumenDTO> palabraClaveVolumenDTOsHistorico = new List<PalabraClaveVolumenDTO>();
                    List<PalabraClaveVolumenDTO> palabraClaveVolumenDTOs = new List<PalabraClaveVolumenDTO>();
                    var historico = _unitOfWork.AdwordsApiVolumenBusquedumRepository.ObtenerHistorico(inicio, fin, string.Join(",", keywordTexts), datospais.Id)
                                        .Select(x => new AdwordsApiVolumenBusquedaHistoricoRespuestaDTO
                                        {
                                            Pais = datospais.NombrePais,
                                            Anho = x.Anho,
                                            IdPais = x.IdPais,
                                            Mes = x.Mes,
                                            PalabraClave = x.PalabraClave,
                                            PromedioBusqueda = x.PromedioBusqueda
                                        }).ToList();

                    listadehistoricos.AddRange(historico);
                }
                catch (GoogleAdsException ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("ccrispin@bsginstitute.com");
                    correos.Add(correo);

                    TMK_MailService Mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "ccrispin@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Reporte Volumen Busqueda - Guardado ERROR";
                    mailData.Message = "Usuario: " + usuario + "<br> Palabras: " + Palabras + "<br>Error: " + ex.Message + "<br> Especifico: " + ex.ToString();
                    mailData.Cc = "avasquezb@bsginstitute.com";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }

            reporteAdwordsApiPalabrasClaveRespuestas = listadehistoricos.GroupBy(x => new
            {
                x.IdPais,
                x.Pais
            }).Select(c => new ReporteAdwordsApiPalabrasClaveRespuestaDTO
            {
                IdPais = c.Key.IdPais,
                Pais = c.Key.Pais,
                Detalle = c.GroupBy(x => new { x.PalabraClave }).Select(g => new PalabraClaveVolumenDTO
                {
                    PalabraClave = g.Key.PalabraClave,
                    PromedioPorMes = listadehistoricos.Where(x => x.PalabraClave == g.Key.PalabraClave && x.Pais == c.Key.Pais && x.IdPais == c.Key.IdPais).Select(d => new Google.Api.Ads.AdWords.v201809.MonthlySearchVolume
                    {
                        count = d.PromedioBusqueda,
                        month = d.Mes,
                        year = d.Anho
                    }).ToArray(),
                }).ToList(),
            }).ToList();
            return reporteAdwordsApiPalabrasClaveRespuestas;
        }
        /// <summary>
        /// Autor: Rodrigo montesinos
        /// Fecha creacion: 03/11/2023
        /// Descripcion: Esta funcion se encarga de el objeto tipo de mes por numero de mes, el cual es encesario para la configuracion de metricas de busquea de google.ads
        /// </summary>
        /// <param name="mes"></param>
        /// <returns> Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear</returns>
        public Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear ObtenerMesDeBusqueda(int mes)
        {
            Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear dato = new Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear();
            switch (mes)
            {
                case 1:
                   dato= Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.January;
                    break;
                case 2:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.February;
                    break;
                case 3:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.March;
                    break;
                case 4:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.April;
                    break;
                case 5:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.May;
                    break;
                case 6:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.June;
                    break;
                case 7:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.July;
                    break;
                case 8:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.August;
                    break;
                case 9:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.September;
                    break;
                case 10:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.October;
                    break;
                case 11:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.November;
                    break;
                case 12:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.December;
                    break;
                default:
                    dato = Google.Ads.GoogleAds.V13.Enums.MonthOfYearEnum.Types.MonthOfYear.January;
                    break;
            }
            return dato;
        }
        /// <summary>
        /// Autor: Rodrigo montesinos
        /// Fecha creacion: 03/11/2023
        /// Descripcion: Esta funcion se encarga retornar todos los paises que tengan id de los ids reconocidos por google ads
        /// </summary>
        /// <returns>List<TPai></returns>
        public List<TPai> ObtenerPaises()
        {
            try
            {
                var repo = _unitOfWork.PaisRepository.GetBy(x => x.CodigoGoogleId != null).ToList();
                return repo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
