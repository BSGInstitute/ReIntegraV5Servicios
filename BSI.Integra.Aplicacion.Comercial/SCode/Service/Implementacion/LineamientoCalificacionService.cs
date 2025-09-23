

using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;
using TransicionFase = BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial.TransicionFase;
using Microsoft.Extensions.Logging;


namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    /// Service: LineamientoCalificacionService
    /// Autor: Joseph Llanque.
    /// Fecha: 03/07/2025
    /// <summary>
    /// Gestión general de TLineamientoCalificacion
    /// </summary>
    public class LineamientoCalificacionService : ILineamientoCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public LineamientoCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TLineamientoCalificacion, LineamientoCalificacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public LineamientoCalificacion Add(LineamientoCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LineamientoCalificacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LineamientoCalificacion Update(LineamientoCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LineamientoCalificacion>(modelo);
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
                _unitOfWork.LineamientoCalificacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LineamientoCalificacion> Add(List<LineamientoCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LineamientoCalificacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LineamientoCalificacion> Update(List<LineamientoCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LineamientoCalificacion>>(modelo);
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
                _unitOfWork.LineamientoCalificacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> LineamientoCalificacion </returns>
        public LineamientoCalificacion ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<LineamientoCalificacion> ObtenerLineamiento()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerLineamiento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionDTO> EsquemaCalificacionConfigurado()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.EsquemaCalificacionConfigurado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamada()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.HistorialVersionCalificacionLlamada();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionMasivaActiva()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionMasivaActiva();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionMasivaActiva()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionCalificacionMasivaActiva();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionAuto()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionCalificacionAuto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionTranscripcionAuto()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionTranscripcionAuto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamiento(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionLineamiento(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionLineamientoHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionLineamientoHistorico(IdOportunidad, IdLlamadaWebphoneCruceCentral3Cx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionPuntoGeneralHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionPuntoGeneralHistorico(IdOportunidad, IdLlamadaWebphoneCruceCentral3Cx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamientoGeneral(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionLineamientoGeneral(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionAutomaticaLineamiento(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionAutomaticaLineamiento(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool GuardarCalificacionLlamada(CalificacionLlamadaManualDTO calificacionLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.GuardarCalificacionLlamada(calificacionLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Califica una llamada automáticamente.
        /// </summary>
        /// <param name="calificacionLlamada">Objeto de calificación automática.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public bool CalificarLlamadaAutomaticamente(CalificacionLlamadaAutomaticaDTO calificacionLlamada)
        {
            try
            {
                //var listaIdCriterio = calificacionLlamada.Calificaciones
                //    .Select(x => x.IdLineamientoCalificacion)
                //    .Where(id => id != null)
                //    .Distinct()
                //    .ToList();

                return _unitOfWork.LineamientoCalificacionRepository.CalificarLlamadaAutomaticamente(calificacionLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActualizarEstadoCalificacionLlamada(EstadoLlamadaCalificadaDTO estadoLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActualizarEstadoCalificacionLlamada(estadoLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool CongelarConfiguracion(CongelamientoConfiguracionDTO congelamientoConfiguracionDTO)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.CongelarConfiguracion(congelamientoConfiguracionDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActivarConfiguracion(CongelamientoConfiguracionActivaDTO activarConfiguracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActivarConfiguracion(activarConfiguracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelAutomatico(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelAutomatico(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelAutomaticoCalificacion(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelAutomaticoCalificacion(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelCalificacionAuto(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelCalificacionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelTranscripcionAuto(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelTranscripcionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActivarConfiguracionTranscripcionAuto(ConfiguracionActivoProcesoDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActivarConfiguracionTranscripcionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActivarConfiguracionCalificacionAuto(ConfiguracionActivoProcesoDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActivarConfiguracionCalificacionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public async Task<List<bool>> TranscripcionAuto(int tipoTranscripcion)
        {
            IEnumerable<LlamadaProcesoAutoDTO> items = null;
            switch (tipoTranscripcion)
            {
                case 1:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionTranscripcionAuto();
                    break;
                case 2:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionTranscripcionMasiva();
                    break;
                default:
                    return new List<bool>();
            }
            var resultados = new List<bool>();
            var itemsOrdenados = items
                .GroupBy(x => x.IdOportunidad)
                .SelectMany(g => g
                    .OrderBy(x => x.IdActividadDetalle)
                    .ThenBy(x => x.IdLlamada)
                )
                .ToList();

            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/");

            // Configurar headers como en Postman
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            var semaphore = new SemaphoreSlim(6); // 6 llamadas concuerrentes


            var tasks = itemsOrdenados.Select(async item =>
            {
                var historialReprogramaciones = _unitOfWork.LineamientoCalificacionRepository.ObtenerOcurrenciaRegistrada(item.IdOportunidad).Select(oc => new
                {
                    IdLlamada = oc.IdLlamada,
                    EstadoOcurrencia = oc.EstadoOcurrenciaAlterno,
                    ocurrencia = oc.OcurrenciaAlterno,
                    Fecha = oc.FechaReal
                })
                .ToList();


                    var datos = _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionCambioFaseOportunidad(item.IdFaseOportunidad_Ant, item.IdFaseOportunidad);

                    var transicionesAgrupadas = datos
                        .GroupBy(t => new {
                            t.IdTransicionFaseOportunidad,
                            t.IdFaseOrigen,
                            t.NombreFaseOrigen,
                            t.CodigoFaseOrigen,
                            t.IdFaseDestino,
                            t.NombreFaseDestino,
                            t.CodigoFaseDestino
                        })
                        .Select(g => new TransicionFase
                        {
                            IdTransicionFaseOportunidad = g.Key.IdTransicionFaseOportunidad,
                            FaseOrigen = new Fase
                            {
                                IdFaseOrigen = g.Key.IdFaseOrigen,
                                NombreFaseOrigen = g.Key.NombreFaseOrigen,
                                CodigoFaseOrigen = g.Key.CodigoFaseOrigen
                            },
                            FaseDestino = new Fase
                            {
                                IdFaseDestino = g.Key.IdFaseDestino,
                                NombreFaseDestino = g.Key.NombreFaseDestino,
                                CodigoFaseDestino = g.Key.CodigoFaseDestino
                            },
                            Criterios = g.GroupBy(c => new {
                                c.IdCriterio,
                                c.OrdenCriterio,
                                c.NombreCriterio
                            })
                            .Select(cg => new Criterio
                            {
                                IdCriterio = cg.Key.IdCriterio,
                                OrdenCriterio = cg.Key.OrdenCriterio,
                                NombreCriterio = cg.Key.NombreCriterio,
                                Lineamientos = cg.Select(l => new Lineamiento
                                {
                                    IdLineamientoCalificacionFase = l.IdLineamientoCalificacionFase,
                                    OrdenLineamiento = l.OrdenLineamiento,
                                    NombreLineamientoCalificacionFase = l.NombreLineamientoCalificacionFase,
                                    Criticidad = new Criticidad
                                    {
                                        IdCriticidadCalificacion = l.IdCriticidadCalificacion,
                                        NombreCriticidad = l.NombreCriticidad
                                    }
                                }).OrderBy(l => l.OrdenLineamiento).ToList()
                            }).OrderBy(c => c.OrdenCriterio).ToList()
                        })
                        .OrderBy(t => t.IdTransicionFaseOportunidad)
                        .ToList();

                    

                var payload = new
                {
                    idLlamada = item.IdLlamada.ToString(),
                    idActividadDetalle = item.IdActividadDetalle.ToString(),
                    idPersonal = item.IdPersonal_Asignado,
                    username = "System-Auto",
                    contacto = "Generico",/*Lo recibe para enviar por signal cuando es manual el proceso*/
                    audio_url = item.UrlAudioProcesado,
                    locale = "es-ES",
                    ocurrencia = item.Ocurrencia,
                    historialReprogramaciones= historialReprogramaciones,
                    informacionFases= transicionesAgrupadas,
                    faseOrigen =item.IdFaseOportunidad_Ant,
                    faseDestino=item.IdFaseOportunidad
                };

                await semaphore.WaitAsync();
                try
                {
                    var response = await httpClient.PostAsJsonAsync("transcriptions/transcribe", payload);
                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, $"Error al transcribir llamada {item.IdLlamadaWebphoneCruceCentralTresCx}");
                    return false;
                }
                finally
                {
                    semaphore.Release();
                }
            });

            resultados = (await Task.WhenAll(tasks)).ToList();
            return resultados;
        }



        public async Task<List<bool>> CalificacionAuto(int tipoCalificacion)
        {

            var serviceInformacionOportunidad = new OportunidadInformacionService(_unitOfWork);
            var serviceMotivacionesPrograma = new ProgramaGeneralMotivacionService(_unitOfWork);
            var serviceObjeciones = new ProgramaGeneralProblemaService(_unitOfWork);
            var serviceInformacionPrograma = new InformacionProgramaService(_unitOfWork);
            var servicePGeneral = new PGeneralService(_unitOfWork);
            IEnumerable<LlamadaProcesoAutoDTO> items = null;
            switch (tipoCalificacion)
            {
                case 1:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionCalificacionAuto();
                    break;
                case 2:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionCalificacionMasiva();
                    break;
                default:
                    return new List<bool>();
            }


            /*AQui obtengo la version vigente de lineamientos para calificar*/
            var lineamientoVigente = _unitOfWork.LineamientoCalificacionRepository
                .HistorialVersionCalificacionLlamada()
                .FirstOrDefault(x => x.EsVigente);


            if (lineamientoVigente == null || items == null || !items.Any())
                return new List<bool>();

            var lineamientoVigenteProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                lineamientoVigente.ConfiguracionJSON,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;
            var resultados = new List<bool>();
            var itemsAgrupadosPorOportunidad = items.GroupBy(x => x.IdOportunidad)
                                                    .Select(g => new
                                                    {
                                                        IdOportunidad = g.Key,
                                                        Llamadas = g.OrderBy(x => x.IdActividadDetalle).ToList()
                                                    })
                                                    .ToList();

            var lineamientos = BuildLineamientosFormateados(lineamientoVigenteProcesados);

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/")
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            var semaphore = new SemaphoreSlim(10);
            foreach (var oportunidad in itemsAgrupadosPorOportunidad)
            {
                var llamadasParaCalificar = ObtenerSiguienteLlamadaParaCalificar(oportunidad.IdOportunidad);

                if (!llamadasParaCalificar.Any())
                {
                    // No hay llamadas listas para calificar
                    continue;
                }

                // Procesar la llamada lista
                var tasksOportunidad = llamadasParaCalificar.Select(async item =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        // Filtrams historico
                        var historicos = oportunidad.Llamadas
                            .Where(x => x.IdActividadDetalle < item.IdActividadDetalle)
                            .ToList();

                        // Validar histórico: todos deben estar transcritos y calificados
                        bool historicoOk = historicos.All(h => h.EsLlamadaTranscrita == true && h.EsLlamadaCalificada == true);

                        // Validar actual: solo transcrita, no calificada
                        bool actualOk = item.EsLlamadaTranscrita == true && item.EsLlamadaCalificada != true;

                        if (!historicoOk || !actualOk)
                            return false; // No califica cuando no cumple el histórico previo transcrito y calificado
                        var todasLasLlamadas = _unitOfWork.LineamientoCalificacionRepository
                                                        .ObtenerHistoricoLlamadaCompletoPorIdOportunidad(oportunidad.IdOportunidad);

                       var llamadasHistoricas = todasLasLlamadas
                            .Where(x => x.IdActividadDetalle <= item.IdActividadDetalle)
                            .OrderByDescending(x => x.IdActividadDetalle)
                            .ThenByDescending(x => x.IdLlamada)
                            .Select(x => x.IdLlamada)
                            .ToList();

                        var transcripcionesHistoricas = new List<TranscripcionCompletaResponseDTO>();
                        foreach (var idLlamadaHistorica in llamadasHistoricas)
                        {
                            var transcripcionHistorica = await ObtenerTranscripcion(idLlamadaHistorica);
                            if (transcripcionHistorica != null)
                                transcripcionesHistoricas.Add(transcripcionHistorica);
                        }

                        /*Se debe limpiar la data , actualmte se obtiene en html*/
                        /*Se obteine data  acerta de la informacion del programa*/
                        CargarInformacionProgramaAutomaticoRespuestaDTO InformacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomatico(item.IdCentroCosto, item.IdCodigoPais, 0, 0);
                        /*Se obteine data  acerta del apartado presentacion programa de la ficha de la agenda*/
                        CargarInformacionProgramaAutomaticoRespuestaDTO PresentacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomaticoSpeech(item.IdCentroCosto, item.IdCodigoPais, 0, 0);
                        /*Se obteine data  acerta de Motivaciones del Programa*/
                        IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> MotivacionPrograma = serviceMotivacionesPrograma.ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(item.IdOportunidad);
                        /*Informacion de solicitudes de informacion previas a la actual*/
                        OportunidadInformacionDTO HistoricoSolicitudInformacion = serviceInformacionOportunidad.ObtenerOportunidadInformacion(item.IdAlumno, item.IdClasificacionPersona);
                        /*Se obteine data  acerta de Objetivo programa*/
                        IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> PublicoObjetivoPrograma = servicePGeneral.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(item.IdCentroCosto, item.IdOportunidad);
                        /*Se obteine data  acerta de lso problemas reportados para el cliente*/
                        IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObjecionesCliente = serviceObjeciones.ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(item.IdOportunidad);

                        // Crear brochure
                        var brochure = new
                        {
                            InformacionPrograma = InformacionPrograma,
                            PresentacionPrograma = PresentacionPrograma,
                            MotivacionPrograma = MotivacionPrograma,
                            HistoricoSolicitudInformacion = HistoricoSolicitudInformacion,
                            PublicoObjetivoPrograma = PublicoObjetivoPrograma,
                            ObjecionesCliente = ObjecionesCliente
                        };

                        var payload = new
                        {
                            idPersonal = item.IdPersonal_Asignado,
                            contacto = "Generico",
                            userName = "System-auto",
                            idCodigoPais = item.IdCodigoPais.ToString(),
                            transcription = transcripcionesHistoricas,
                            lineamientos,
                            brochure
                        };

                        //var response = await httpClient.PostAsJsonAsync("grading/queue/batch", payload);
                        var response = await httpClient.PostAsJsonAsync("grading/queue/batch", payload);

                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();
                        var evaluacionData = JsonSerializer.Deserialize<ResultadoEvaluacion>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return true;
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogError(ex, $"Error al calificar llamada {item.IdLlamada}");
                        return false;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                var resultadosOportunidad = await Task.WhenAll(tasksOportunidad);
                resultados.AddRange(resultadosOportunidad);
            }
            return resultados.ToList();
        }

        private List<LlamadaProcesoAutoDTO> ObtenerSiguienteLlamadaParaCalificar(int idOportunidad)
        {
            // Obtener TODAS las llamadas de la oportunidad (transcritas y no transcritas)
            var todasLasLlamadas = _unitOfWork.LineamientoCalificacionRepository
                .ObtenerHistoricoLlamadaCompletoPorIdOportunidad(idOportunidad);

            if (!todasLasLlamadas.Any())
                return new List<LlamadaProcesoAutoDTO>();

            // Ordenar por fechaLlamada (orden cronológico real)
            var llamadasOrdenadas = todasLasLlamadas
                .OrderBy(x => x.IdActividadDetalle) // Primero por ID (más antiguo)
                .ThenBy(x => x.FechaLlamada).ToList();        // Luego por fecha (más antigua)

            // Buscar la primera llamada que esté transcrita pero no calificada
            var primeraLlamadaParaCalificar = llamadasOrdenadas
                .FirstOrDefault(x => x.EsLlamadaTranscrita == true && x.EsLlamadaCalificada != true);

            if (primeraLlamadaParaCalificar == null)
            {
                return new List<LlamadaProcesoAutoDTO>();
            }

            // Encontrar el índice de la primera llamada para calificar
            var indicePrimeraParaCalificar = llamadasOrdenadas.FindIndex(x => x.IdLlamada == primeraLlamadaParaCalificar.IdLlamada);

            // Verificar que todas las llamadas anteriores estén transcritas y calificadas
            for (int i = 0; i < indicePrimeraParaCalificar; i++)
            {
                var llamadaAnterior = llamadasOrdenadas[i];
                if (llamadaAnterior.EsLlamadaTranscrita != true || llamadaAnterior.EsLlamadaCalificada != true)
                {
                    return new List<LlamadaProcesoAutoDTO>();
                }
            }

            return new List<LlamadaProcesoAutoDTO> { primeraLlamadaParaCalificar };
        }
        public object BuildLineamientosFormateados(ConfiguracionLineamientoDTO lineamientoVigenteProcesados)
        {
            var fasesDict = new Dictionary<string, object>();

            foreach (var fase in lineamientoVigenteProcesados.FasesCalificacion.OrderBy(f => f.Orden))
            {
                var criteriosDict = new Dictionary<string, object>();

                var criterios = lineamientoVigenteProcesados.CriteriosCalificacion
                    .Where(c => c.IdFaseCalificacion == fase.Id)
                    .OrderBy(c => c.Orden)
                    .ToList();

                foreach (var criterio in criterios)
                {
                    var lineamientos = lineamientoVigenteProcesados.LineamientosCalificacion
                        .Where(l => l.IdCriterioCalificacionLlamada == criterio.Id)
                        .OrderBy(l => l.Orden)
                        .Select(l => new
                        {
                            id = l.Id,
                            orden = l.Orden,
                            lineamiento = l.NombreLineamiento,
                            importancia = GetCriticidadNombre(l.IdCriticidadCalificacion, lineamientoVigenteProcesados)
                        })
                        .ToList();

                    criteriosDict[criterio.NombreCriterio] = new
                    {
                        id = criterio.Id,
                        orden = criterio.Orden,
                        lineamientos
                    };
                }

                fasesDict[fase.Nombre] = new
                {
                    id = fase.Id,
                    orden = fase.Orden,
                    criterios = criteriosDict
                };
            }

            var puntosGenerales = (lineamientoVigenteProcesados.PuntosGeneralesCalificacion != null
                ? lineamientoVigenteProcesados.PuntosGeneralesCalificacion
                    .OrderBy(pg => pg.Orden)
                    .Select(pg => new
                    {
                        id = pg.Id,
                        orden = pg.Orden,
                        nombre = pg.Nombre,
                        descripcion = pg.Descripcion
                    }).Cast<object>().ToList()
                : new List<object>());
            return new
            {
                fases = fasesDict,
                puntosgenerales = puntosGenerales
            };
        }

        private string GetCriticidadNombre(int criticidadId, ConfiguracionLineamientoDTO dto)
        {
            return dto.CriticidadCalificacion
                .FirstOrDefault(c => c.Id == criticidadId)?.NombreCriticidad ?? string.Empty;
        }


        public async Task<CalificacionLlamadaAutomaticaDTO> ProcesarYConstruirPayloadCalificacionAsync(ResultadoEvaluacion evaluacionData, int idLlamada, int idVersionConfiguracion, string usuario, ConfiguracionLineamientoDTO configuracionActiva)
        {
            var calificaciones = new List<DetalleCalificacionMualDTO>();
            var calificacionesPuntosGenerales = new List<DetalleCalificacionPuntoGeneralDTO>();
            var calificacionesPuntosCriticos = new List<DetallePuntosCriticosDTO>();
            var calificacionesFase = new List<DetalleCalificacionFaseDTO>();


            var evaluacion = evaluacionData?.GradedResult;

            if (evaluacion == null)
                return null;

            // Procesar fases y criterios
            foreach (var fase in evaluacion.Fases)
            {
                // Obtener el IdFase por el primer criterio de la fase
                int? idFase = null;
                if (fase.Criterios != null && fase.Criterios.Count > 0)
                {
                    var criterioId = fase.Criterios[0].Id;
                    idFase = configuracionActiva.CriteriosCalificacion
                        .Where(c => c.Id == criterioId)
                        .Select(c => c.IdFaseCalificacion)
                        .FirstOrDefault();
                }

                // Agregar calificación de fase si corresponde
                if (idFase.HasValue && (!string.IsNullOrWhiteSpace(fase.JustificacionGeneral) || !string.IsNullOrWhiteSpace(fase.BrechaGeneral)))
                {
                    calificacionesFase.Add(new DetalleCalificacionFaseDTO
                    {
                        IdFase = idFase.Value,
                        JustificacionGeneral = fase.JustificacionGeneral,
                        BrechaGeneral = fase.BrechaGeneral
                    });
                }


                foreach (var criterio in fase.Criterios)
                {
                    if (criterio.Lineamientos != null && criterio.Lineamientos.Any())
                    {
                        calificaciones.Add(new DetalleCalificacionMualDTO
                        {
                            IdCriterioCalificacionLlamada = criterio.Id,
                            Nota = criterio.Calificacion,
                            Comentario = criterio.Justificacion,
                            Brecha = criterio.Brecha
                        });
                    }
                }
            }

            // Procesar puntos generales
            if (evaluacion.Puntosgenerales != null)
            {
                foreach (var punto in evaluacion.Puntosgenerales)
                {
                    calificacionesPuntosGenerales.Add(new DetalleCalificacionPuntoGeneralDTO
                    {
                        idCalificacionPuntoGeneral = punto.Id,
                        Nota = punto.Calificacion,
                        Comentario = punto.Justificacion
                    });
                }
            }

            // Procesar puntos críticos
            if (evaluacion.Puntoscriticos != null)
            {
                foreach (var kvp in evaluacion.Puntoscriticos)
                {
                    calificacionesPuntosCriticos.Add(new DetallePuntosCriticosDTO
                    {
                        Criterio = kvp?.Nombre,
                        Nota = kvp?.Calificacion,
                        Comentario = kvp?.Feedback
                    });
                }
            }

            // Construcción final del payload
            return new CalificacionLlamadaAutomaticaDTO
            {
                IdLlamada = idLlamada,
                IdVersion = idVersionConfiguracion,
                Calificaciones = calificaciones,
                CalificacionesPuntosGenerales = calificacionesPuntosGenerales,
                CalificacionesPuntosCriticos = calificacionesPuntosCriticos,
                Usuario = usuario
            };
        }


        public async Task<CalificacionLlamadaAutomaticaDTO> ProcesarYConstruirPayloadCalificacionBatchAsync(GradedResult evaluacionData, int idLlamada, int idVersionConfiguracion, string usuario, ConfiguracionLineamientoDTO configuracionActiva)
        {
            var calificaciones = new List<DetalleCalificacionMualDTO>();
            var calificacionesPuntosGenerales = new List<DetalleCalificacionPuntoGeneralDTO>();
            var calificacionesPuntosCriticos = new List<DetallePuntosCriticosDTO>();
            var calificacionesFase = new List<DetalleCalificacionFaseDTO>();
            


            var evaluacion = evaluacionData;

            if (evaluacion == null)
                return null;

            // Procesar fases y criterios
            foreach (var fase in evaluacion.Fases)
            {
                // Obtener el IdFase por el primer criterio de la fase
                int? idFase = null;
                if (fase.Criterios != null && fase.Criterios.Count > 0)
                {
                    var criterioId = fase.Criterios[0].Id;
                    idFase = configuracionActiva.CriteriosCalificacion
                        .Where(c => c.Id == criterioId)
                        .Select(c => c.IdFaseCalificacion)
                        .FirstOrDefault();
                }

                // Agregar calificación de fase si corresponde
                if (idFase.HasValue && (!string.IsNullOrWhiteSpace(fase.JustificacionGeneral) || !string.IsNullOrWhiteSpace(fase.BrechaGeneral)))
                {
                    calificacionesFase.Add(new DetalleCalificacionFaseDTO
                    {
                        IdFase = idFase.Value,
                        JustificacionGeneral = fase.JustificacionGeneral,
                        BrechaGeneral = fase.BrechaGeneral
                    });
                }


                foreach (var criterio in fase.Criterios)
                {
                    if (criterio.Lineamientos != null && criterio.Lineamientos.Any())
                    {
                        calificaciones.Add(new DetalleCalificacionMualDTO
                        {
                            IdCriterioCalificacionLlamada = criterio.Id,
                            Nota = criterio.Calificacion,
                            Comentario = criterio.Justificacion,
                            Brecha = criterio.Brecha
                        });
                    }
                }
            }

            // Procesar puntos generales
            if (evaluacion.Puntosgenerales != null)
            {
                foreach (var punto in evaluacion.Puntosgenerales)
                {
                    calificacionesPuntosGenerales.Add(new DetalleCalificacionPuntoGeneralDTO
                    {
                        idCalificacionPuntoGeneral = punto.Id,
                        Nota = punto.Calificacion,
                        Comentario = punto.Justificacion
                    });
                }
            }

            // Procesar puntos críticos
            if (evaluacion.Puntoscriticos != null)
            {
                foreach (var kvp in evaluacion.Puntoscriticos)
                {
                    calificacionesPuntosCriticos.Add(new DetallePuntosCriticosDTO
                    {
                        Criterio = kvp.Nombre,
                        Nota = kvp.Calificacion,
                        Comentario = kvp.Feedback
                    });
                }
            }
            // Si todas las notas de criterios y puntos generales son -1 y existen todos los puntos generales,
            // actualizar el comentario de cada punto general con el valor de evaluacionData.IterrupcionLlamada
            bool todosCriteriosMenosUno = calificaciones.All(x => x.Nota == -1);
            bool todosPuntosGeneralesMenosUno = calificacionesPuntosGenerales.All(x => x.Nota == -1);
            bool tieneTodosPuntosGenerales = evaluacion.Puntosgenerales != null &&
                calificacionesPuntosGenerales.Count == evaluacion.Puntosgenerales.Count;


            if (todosCriteriosMenosUno && todosPuntosGeneralesMenosUno && tieneTodosPuntosGenerales)
            {
                foreach (var punto in calificacionesPuntosGenerales)
                {
                    punto.Comentario = evaluacionData.InterrupcionLlamada;
                }
                foreach (var criterio in calificaciones)
                {
                    criterio.Comentario = evaluacionData.InterrupcionLlamada;
                }
            }

            // Construcción final del payload
            return new CalificacionLlamadaAutomaticaDTO
            {
                IdLlamada = idLlamada,
                IdVersion = idVersionConfiguracion,
                Calificaciones = calificaciones,
                CalificacionesPuntosGenerales = calificacionesPuntosGenerales,
                CalificacionesPuntosCriticos = calificacionesPuntosCriticos,
                CalificacionesFase = calificacionesFase,
                InterrupcionLlamada= evaluacionData.InterrupcionLlamada,
                Usuario = usuario
            };
        }

        /// Autor:Joseph Llanque
        /// Fecha: 25/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<TranscripcionCompletaResponseDto> </returns>
        public async Task<TranscripcionCompletaResponseDTO> ObtenerTranscripcion(int idLlamada)
        {
            try
            {
                var data = (await _unitOfWork.TranscripcionLlamadaRepository.ObtenerTranscripcion(idLlamada)).ToList();
                if (!data.Any()) return null;

                var transcripcion = data.First();

                var dto = new TranscripcionCompletaResponseDTO
                {
                    IdLlamada = transcripcion.IdLlamadaWebphoneCruceCentralTresCx.ToString(),
                    IdActividadDetalle = null,
                    Status = "success",
                    Transcription = new TranscriptionDto
                    {
                        Source = transcripcion.Source,
                        Timestamp = transcripcion.Timestamp,
                        DurationInTicks = transcripcion.DurationInTicks ?? 0,
                        DurationMilliseconds = transcripcion.DurationMilliseconds ?? 0,
                        Duration = transcripcion.Duration,
                        Summary = transcripcion.Summary,
                        Ocurrencia_Consistente = transcripcion.OcurrenciaConsistente.HasValue && transcripcion.OcurrenciaConsistente.Value ? "si" : "no",

                        CombinedRecognizedPhrases = data
                            .Where(x => x.FraseCombinadaId != null)
                            .GroupBy(x => x.FraseCombinadaId)
                            .Select(g => new CombinedRecognizedPhraseDto
                            {
                                Channel = g.First().FC_Channel,
                                Lexical = g.First().FC_Lexical,
                                Itn = g.First().FC_ITN,
                                MaskedITN = g.First().FC_MaskedITN,
                                Display = g.First().FC_Display
                            }).ToList(),

                        RecognizedPhrases = data
                            .Where(x => x.FraseReconocidaId != null)
                            .GroupBy(x => x.FraseReconocidaId)
                            .Select(g => new RecognizedPhraseDto
                            {
                                RecognitionStatus = g.First().RecognitionStatus,
                                Channel = g.First().FR_Channel,
                                Speaker = g.First().Speaker,
                                Offset = g.First().Offset,
                                Duration = g.First().FR_Duration,
                                OffsetInTicks = g.First().OffsetInTicks,
                                DurationInTicks = g.First().DurationInTicksFraseReconocida,
                                DurationMilliseconds = g.First().DurationMillisecondsFraseReconocida,
                                OffsetMilliseconds = g.First().OffsetMilliseconds,
                                NBest = g.Select(x => new NBestDto
                                {
                                    Confidence = x.Confidence,
                                    Lexical = x.DFR_Lexical,
                                    Itn = x.DFR_ITN,
                                    MaskedITN = x.DFR_MaskedITN,
                                    Display = x.DFR_Display,
                                    Sentiment = x.DFR_Sentiment
                                }).ToList()
                            }).ToList(),

                        Recomendaciones = data
                            .Where(x => x.RecomendacionId != null)
                            .Select(x => x.Recomendacion.ToString())
                            .Distinct()
                            .ToList()
                    }
                };

                return dto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteCalificacionResponse ObtenerReporte(ReporteCalificacionRequest req)
        {
            var (filas, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReporte(req);
            var agrupado = filas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g
                        .Where(x => x.PuntajePromedio >= 0) 
                        .Select(x => x.PuntajePromedio)
                        .ToList();

                    decimal? promedio = notasValidas.Count > 0 ? Math.Round(notasValidas.Average(), 2) : (decimal?)null;


                    var puntosCriticos = g
                                        .Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                                        .Select(x => x.PuntoCritico!.Trim())
                                        .Distinct()
                                        .ToList();


                    return new LlamadaCalificadaDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        IdOcurrenciaPadreAlterno = first.IdOcurrenciaPadreAlterno,
                        IdOcurrenciaActividadAlterno = first.IdOcurrenciaActividadAlterno,
                        IdOcurrenciaAlterno = first.IdOcurrenciaAlterno,
                        OcurrenciaPadreAlterno = first.OcurrenciaPadreAlterno,
                        OcurrenciaAlterno = first.OcurrenciaAlterno,
                        EstadoOcurrenciaAlterno = first.EstadoOcurrenciaAlterno,
                        PuntosCriticos = puntosCriticos,
                        ComentarioLlamadaNoCalificada = null,
                        OcurrenciaConsistente=first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia=first.ComentarioConsistenciaOcurrencia,
                        CambioFaseConsistente=first.CambioFaseConsistente,
                        ComentarioConsistenciaCambioFase=first.ComentarioConsistenciaCambioFase,
                        InterrupcionLlamada=first.InterrupcionLlamada

                    };
                })
                .ToList();
            return new ReporteCalificacionResponse
            {
                TotalRegistros = total,
                Data = agrupado
            };
        }
        public ReporteCalificacionResponse ObtenerReporteFase(ReporteCalificacionRequest req)
        {
            var (filas, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReporteFase(req);
            var agrupado = filas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g
                        .Where(x => x.PuntajePromedio >= 0)
                        .Select(x => x.PuntajePromedio);

                    decimal? promedio = notasValidas.Any()
                        ? Math.Round(notasValidas.Average(), 2)
                        : (decimal?)null;

                    var puntosCriticos = g
                                        .Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                                        .Select(x => x.PuntoCritico!.Trim())
                                        .Distinct()
                                        .ToList();

                    return new LlamadaCalificadaDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        IdOcurrenciaPadreAlterno = first.IdOcurrenciaPadreAlterno,
                        IdOcurrenciaActividadAlterno = first.IdOcurrenciaActividadAlterno,
                        IdOcurrenciaAlterno = first.IdOcurrenciaAlterno,
                        IdVersionConfiguracionCalificacionLlamada = first.IdVersionConfiguracionCalificacionLlamada,
                        OcurrenciaPadreAlterno = first.OcurrenciaPadreAlterno,
                        OcurrenciaAlterno = first.OcurrenciaAlterno,
                        EstadoOcurrenciaAlterno = first.EstadoOcurrenciaAlterno,
                        OcurrenciaConsistente = first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia = first.ComentarioConsistenciaOcurrencia,
                        InterrupcionLlamada = first.InterrupcionLlamada
                    };
                })
                .ToList();

            return new ReporteCalificacionResponse
            {
                TotalRegistros = total,
                Data = agrupado
            };
        }


        public ReporteCalificacionGlobalResponse ObtenerPromedioGlobal(ReporteCalificacionGlobalRequest request)
        {
            var filas = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosParaPromedioGlobal(request);

            // Agrupa todas las llamadas (sin filtrar criterios)
            var agrupado = filas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var notasValidas = g
                        .Where(x => x.PuntajePromedio >= 0)
                        .Select(x => x.PuntajePromedio);

                    decimal? promedio = notasValidas.Any()
                        ? Math.Round(notasValidas.Average(), 2)
                        : (decimal?)null;

                    return new
                    {
                        IdLlamada = g.Key,
                        Promedio = promedio,
                        TotalCalificaciones = notasValidas.Count()
                    };
                })
                .Where(x => x.Promedio.HasValue)
                .ToList();

            var totalLlamadas = agrupado.Count;
            var totalCalificaciones = agrupado.Sum(x => x.TotalCalificaciones);

            // Calcular promedio global excluyendo calificacion
            var promediosValidos = filas
                .Where(x => x.PuntajePromedio >= 0 && x.IdCriterioCalificacion != 36 && x.IdCriterioCalificacion != 37)
                .Select(x => x.PuntajePromedio);

            var promedioGlobal = promediosValidos.Any()
                ? Math.Round(promediosValidos.Average(), 2)
                : 0;

            return new ReporteCalificacionGlobalResponse
            {
                TotalLlamadas = totalLlamadas,
                PromedioGlobal = promedioGlobal,
                TotalCalificaciones = totalCalificaciones,
                FechaCalculo = DateTime.Now
            };
        }


        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public IEnumerable<CalificacionFaseDTO> ObtenerCalificacionFase(int idLlamada, bool tipoCalificacion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerCalificacionFase(idLlamada, tipoCalificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public async Task ProcesarCalificacionBatch(ResultadoEvaluacionBatch payload)
        {

            if (payload == null)
                throw new ArgumentNullException(nameof(payload), "El cuerpo de calificación es nulo.");

            /*AQui obtengo la version vigente de lineamientos para calificar*/
            var lineamientoVigente = _unitOfWork.LineamientoCalificacionRepository
                    .HistorialVersionCalificacionLlamada()
                    .FirstOrDefault(x => x.EsVigente);
            if (lineamientoVigente == null)
                throw new InvalidOperationException("No existe una versión vigente de lineamientos para calificar.");
            if (string.IsNullOrWhiteSpace(lineamientoVigente.ConfiguracionJSON))
                throw new InvalidOperationException("La configuración de lineamientos vigente está vacía o nula.");

            ConfiguracionLineamientoDTO lineamientoVigenteProcesados;
            try
            {
                lineamientoVigenteProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                    lineamientoVigente.ConfiguracionJSON,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? throw new InvalidOperationException("No se pudo deserializar la configuración de lineamientos vigente.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al deserializar la configuración de lineamientos vigente.", ex);
            }
            var payloadCalificacion = await ProcesarYConstruirPayloadCalificacionBatchAsync(
                payload.GradedResult,
                payload.IdLlamada,
                lineamientoVigente.Id,
                "system-auto",
                lineamientoVigenteProcesados
            );

            if (payloadCalificacion == null)
                throw new InvalidOperationException("No se pudo construir el payload de calificación automática.");

            var resultado = _unitOfWork.LineamientoCalificacionRepository.CalificarLlamadaAutomaticamente(payloadCalificacion);
            if (!resultado)
                throw new InvalidOperationException("No se pudo guardar la calificación automática en la base de datos.");

        }


        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public IEnumerable<InformacionLlamadaTresCxDTO> ObtenerInformacionLlamada(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerInformacionLlamada(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public async Task<InsertRecomendacionResultDTO> ProcesarRecomendacionesBatch(RecomendacionLlamadaDTO payload)
        {

            try
            {
                return await _unitOfWork.LineamientoCalificacionRepository.ProcesarRecomendacionesBatch(payload);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        



        public async Task<bool> ProcesarPuntoCriticoDiario()
        {
            var informacionDelDia = _unitOfWork.LineamientoCalificacionRepository.ObtenerPuntosCriticosPorDia();
            if (informacionDelDia == null || !informacionDelDia.Any())
                return true; 

            var asesores = informacionDelDia.GroupBy(f => f.IdPersonal);

            // 3) Configurar HttpClient
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/")
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BSG/PCDiario");
            httpClient.DefaultRequestHeaders.ConnectionClose = false;

            var procesadosOk = 0;
            var totalAsesores = asesores.Count();

            foreach (var asesorGroup in asesores)
            {
                var idPersonal = asesorGroup.Key;

                // 4) Agrupar por llamada del asesor
                var llamadas = asesorGroup
                    .GroupBy(x => x.IdLlamadaWebphoneCruceCentralTresCx)
                    .Select(g =>
                    {
                        var resumen = g.Select(x => x.ResumenLlamada)
                                       .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));

                        var puntosCriticos = g.Select(x => x.PuntoCritico?.Trim())
                                              .Where(s => !string.IsNullOrWhiteSpace(s))
                                              .Distinct(StringComparer.OrdinalIgnoreCase)
                                              // .Take(3) // si quieres topear por llamada, descomenta
                                              .ToList()!;

                        return new LlamadaPuntoCriticoDTO
                        {
                            idLlamada = g.Key.ToString(),
                            summary = resumen,
                            puntoscriticos = puntosCriticos
                        };
                    })
                    .OrderByDescending(l => l.idLlamada)
                    .ToList();

                if (llamadas.Count == 0)
                {
                    // no hay nada que enviar de este asesor; lo consideramos OK
                    procesadosOk++;
                    continue;
                }

                var fechaGeneracion = asesorGroup.First().FechaReal.Date;

                var payload = new RecomendacionPuntoCriticoLlamadaDTO
                {
                    items = llamadas
                };


                try
                {
                    // Imprimir el JSON generado del payload antes de enviarlo
                    Console.WriteLine(JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true }));
                    using var response = await httpClient.PostAsJsonAsync("consolidadocritico/comercial", payload);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var resultadoPuntoCritico = JsonSerializer.Deserialize<ResultadoPuntoCriticoConsolidaddoDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                    // Guardar "congelamiento": JSON crudo tal como vino
                    var resultado = _unitOfWork.LineamientoCalificacionRepository.InsertarCongelamientoPuntoCritico(
                        idPersonal: idPersonal,
                        fechaGeneracion: fechaGeneracion,
                        resultadoPuntoCritico: json
                    );

                    if (resultado) procesadosOk++;
                }
                catch (Exception ex)
                {
                        // log y continuar con el siguiente asesor
                        Console.WriteLine("Error procesando IdPersonal {IdPersonal}", idPersonal);


                }
            }

            // 8) Devuelve OK solo si TODOS los asesores salieron bien
            return procesadosOk == totalAsesores;
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos el resumen de punto critico diario por asesor
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<PuntoCriticoResumenDiarioDTO> ObtenerPuntoCriticoDiario(PuntoCriticoResumenEntradaDTO payload)
        {
            try
            {
                var idPersonal = payload.IdPersonal;
                var FechaGeneracion = payload.FechaGeneracion;
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerPuntoCriticoDiario(idPersonal,FechaGeneracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}




    



