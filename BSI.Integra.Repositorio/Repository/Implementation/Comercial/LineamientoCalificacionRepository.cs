using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{
    /// Repositorio: LineamientoCalificacionRepository
    /// Autor: Joseph LLanque
    /// Fecha: 07/03/2025
    /// <summary>
    /// Gestión general de TLineamientoCalificacion
    /// </summary>
    public class LineamientoCalificacionRepository : GenericRepository<TLineamientoCalificacion>, ILineamientoCalificacionRepository
    {
        private Mapper _mapper;

        public LineamientoCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLineamientoCalificacion, LineamientoCalificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TLineamientoCalificacion MapeoEntidad(LineamientoCalificacion entidad)
        {
            try
            {
                //crea la entidad padre
                TLineamientoCalificacion modelo = _mapper.Map<TLineamientoCalificacion>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TLineamientoCalificacion Add(LineamientoCalificacion entidad)
        {
            try
            {
                var LineamientoCalificacion = MapeoEntidad(entidad);
                base.Insert(LineamientoCalificacion);
                return LineamientoCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TLineamientoCalificacion Update(LineamientoCalificacion entidad)
        {
            try
            {
                var LineamientoCalificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                LineamientoCalificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(LineamientoCalificacion);
                return LineamientoCalificacion;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TLineamientoCalificacion> Add(IEnumerable<LineamientoCalificacion> listadoEntidad)
        {
            try
            {
                List<TLineamientoCalificacion> listado = new List<TLineamientoCalificacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TLineamientoCalificacion> Update(IEnumerable<LineamientoCalificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TLineamientoCalificacion> listado = new List<TLineamientoCalificacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de com.T_FaseCalificacion por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> LineamientoCalificacion </returns>
        public LineamientoCalificacion ObtenerPorId(int id)
        {
            try
            {
                var rpta = new LineamientoCalificacion();
                var query = @"SELECT Id,
                                       IdCriterioCalificacionLlamada,
                                       IdCriticidadCalificacion,
                                       NombreLineamiento,
                                       Orden,
                                       Descripcion,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion
                                FROM com.T_LineamientoCalificacion
                                WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<LineamientoCalificacion>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener motivaciones por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<MotivacionRawDTO></returns> 
        public async Task<List<MotivacionRawDTO>> ObtenerMotivacionesPorIdPGeneralAsync(int idPGeneral)
        {
            var query = @"SELECT 
                    IdMotivacion,
                    NombreMotivacion,
                    IdArgumento,
                    ContenidoArgumento
                FROM pla.V_TProgramaGeneralMotivacion_MotivacionCliente
                WHERE IdPGeneral = @idPGeneral";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return new List<MotivacionRawDTO>();

            try
            {
                return JsonConvert.DeserializeObject<List<MotivacionRawDTO>>(resultado) ?? new List<MotivacionRawDTO>();
            }
            catch
            {
                return new List<MotivacionRawDTO>();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener objeciones de clientes por id PGeneral
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<ObjeccionClienteRawDTO></returns> 
        public async Task<List<ObjeccionClienteRawDTO>> ObtenerObjecionesClientesPorIdPGeneralAsync(int idPGeneral)
        {
            var query = @"SELECT
                                Id,
                                NombreObjecion,
                                IdDetalleSolucion,
                                Detalle,
                                Solucion
                            FROM pla.V_TProgramaGeneralProblema_ObjecionCliente
                            WHERE IdPGeneral = @idPGeneral AND VisibleAgenda = 1 AND Estado = 1";

            var resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

            if (string.IsNullOrEmpty(resultado) || resultado == "[]")
                return new List<ObjeccionClienteRawDTO>();

            try
            {
                return JsonConvert.DeserializeObject<List<ObjeccionClienteRawDTO>>(resultado) ?? new List<ObjeccionClienteRawDTO>();
            }
            catch
            {
                return new List<ObjeccionClienteRawDTO>();
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id,NombreLineamiento, 
                                FROM com.T_LineamientoCalificacion
                                WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<LineamientoCalificacion> ObtenerLineamiento()
        {
            try
            {
                var comboDTOs = new List<LineamientoCalificacion>();
                var query = @"SELECT  Id,
                                    IdCriterioCalificacionLlamada,
                                    IdCriticidadCalificacion,
                                    NombreLineamiento,
				                    Descripcion,
                                    Orden,
                                    HerramientaAnalisis
                                FROM com.T_LineamientoCalificacion
                                WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<LineamientoCalificacion>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionDTO> EsquemaCalificacionConfigurado()
        {
            try
            {
                var comboDTOs = new List<ConfiguracionEsquemaCalificacionDTO>();
                var query = @"SELECT Criterio,
                                     Criticidad,
                                     DescripcionCriterio,
                                     DescripcionCriticidad,
                                     DescripcionLineamiento,
                                     EstadoCriterio,
                                     EstadoCriticidad,
                                     EstadoFase,
                                     EstadoLineamiento,
                                     EsVigente,
                                     Fase,
                                     FechaVigenciaFin,
                                     FechaVigenciaInicio,
                                     HerramientaAnalisis,
                                     IdCriterio,
                                     IdCriticidad,
                                     IdFase,
                                     IdLineamiento,
                                     NombreLineamiento,
                                     OrdenCriterio,
                                     OrdenFase,
                                     OrdenLineamiento,
                                     VERSION FROM [com].[V_ConfiguracionEsquemaCalificacionLlamadaVersion]";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ConfiguracionEsquemaCalificacionDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamada()
        {
            try
            {
                var comboDTOs = new List<ConfiguracionEsquemaCalificacionLlamdaDTO>();
                var query = @"SELECT Id,
                                     FechaVersion,
                                     DescripcionVersion,
                                     ConfiguracionJSON,
                                     EsVigente,
                                     Comentario,
                                     UsuarioCreacion,
                                     UsuarioModificacion,
                                     FechaCreacion,
                                     FechaModificacion
                              FROM com.T_VersionConfiguracionCalificacionLlamada
                              WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ConfiguracionEsquemaCalificacionLlamdaDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 20/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cabeceras del historial filtrando directamente por la columna IdPersonalAreaTrabajo.
        /// </summary> 
        public IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamadaV2(int idPersonalAreaTrabajo)
        {
            try
            {

                var query = @"
                SELECT 
                     Id
                    ,DescripcionVersion
                    ,EsVigente
                    ,Comentario
                    ,UsuarioCreacion
                    ,UsuarioModificacion
                    ,FechaCreacion
                    ,FechaModificacion
                FROM com.T_EvaluacionLlamadaConfiguracionVersion WITH(NOLOCK)
                WHERE Estado = 1 
                  AND IdPersonalAreaTrabajo = @IdPersonalAreaTrabajo
                ORDER BY FechaCreacion DESC";

                var parametros = new { IdPersonalAreaTrabajo = idPersonalAreaTrabajo };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ConfiguracionEsquemaCalificacionLlamdaDTO>>(resultado);
                }
                return new List<ConfiguracionEsquemaCalificacionLlamdaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 20/11/2025
        /// Version: 1.0
        /// <summary>
        /// Ejecuta el SP y retorna la lista plana sin procesar estructura.
        /// </summary>
        public List<EvaluacionLlamadaJerarquicaDTO> ObtenerDataConfiguracionPorVersion(int idEvaluacionLlamadaConfiguracionVersion, int idPersonalAreaTrabajo)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_EvaluacionLlamadaObtenerConfiguracionPorVersion]", new
                {
                    IdVersion = idEvaluacionLlamadaConfiguracionVersion,
                    IdPersonalAreaTrabajo = idPersonalAreaTrabajo
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<EvaluacionLlamadaJerarquicaDTO>>(resultado);
                }

                return new List<EvaluacionLlamadaJerarquicaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<CalificacionLlamadaDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamiento(int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                var tipoCalificacion = 0;
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerNotaLineamientoPorLlamadaV2]", new { IdLlamadaWebphoneCruceCentral3Cx, tipoCalificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CalificacionLlamadaDTO>>(resultado);
                }
                return Enumerable.Empty<CalificacionLlamadaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<CalificacionLlamadaDTO> </returns>
        public IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionLineamientoHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentralTresCx)
        {
            try
            {
                var tipoCalificacion = 1;
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerHistoricoCalificacionV2]", new
                {
                    IdOportunidad,
                    IdLlamadaWebphoneCruceCentralTresCx,
                    tipoCalificacion
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<HistoricoCalificacionDTO>>(resultado);
                }
                return Enumerable.Empty<HistoricoCalificacionDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<CalificacionLlamadaDTO> </returns>
        public IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionPuntoGeneralHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentralTresCx)
        {
            try
            {
                var tipoCalificacion = 1;
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerHistoricoCalificacionPuntoGeneral]", new
                {
                    IdOportunidad,
                    IdLlamadaWebphoneCruceCentralTresCx,
                    tipoCalificacion
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<HistoricoCalificacionDTO>>(resultado);
                }
                return Enumerable.Empty<HistoricoCalificacionDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<CalificacionLlamadaDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamientoGeneral(int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerNotaLineamientoPorLlamadaV2]", new { IdLlamadaWebphoneCruceCentral3Cx });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CalificacionLlamadaDTO>>(resultado);
                }
                return Enumerable.Empty<CalificacionLlamadaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<CalificacionLlamadaDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionAutomaticaLineamiento(int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                var tipoCalificacion = 1;
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerNotaLineamientoPorLlamadaV2]", new { IdLlamadaWebphoneCruceCentral3Cx, tipoCalificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CalificacionLlamadaDTO>>(resultado);
                }
                return Enumerable.Empty<CalificacionLlamadaDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool CongelarConfiguracion(CongelamientoConfiguracionDTO congelamientoConfiguracion)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_CongelarConfiguracionCalificacionLlamada]", new { congelamientoConfiguracion.EsVigente, congelamientoConfiguracion.DescripcionVersion, congelamientoConfiguracion.Descripcion, congelamientoConfiguracion.Usuario });
                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool GuardarCalificacionLlamada(CalificacionLlamadaManualDTO calificacionLlamada)
        {
            try
            {
                string calificacionesJson = JsonConvert.SerializeObject(calificacionLlamada.Calificaciones);
                string calificacionesPuntoGeneralJson = JsonConvert.SerializeObject(calificacionLlamada.CalificacionesPuntosGenerales);

                var parametros = new DynamicParameters();
                parametros.Add("@IdLlamada", calificacionLlamada.IdLlamada, DbType.Int32);
                parametros.Add("@IdVersion", calificacionLlamada.IdVersion, DbType.Int32);
                parametros.Add("@Calificaciones", calificacionesJson, DbType.String);
                parametros.Add("@CalificacionesPuntosGenerales", calificacionesPuntoGeneralJson, DbType.String);
                parametros.Add("@Usuario", calificacionLlamada.Usuario, DbType.String);

                _dapperRepository.QuerySPDapper("[com].[SP_InsertarCalificacionLlamadaManual]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la calificación de llamada: " + ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool CalificarLlamadaAutomaticamente(CalificacionLlamadaAutomaticaDTO calificacionLlamada)
        {
            try
            {
                string calificacionesJson = JsonConvert.SerializeObject(calificacionLlamada.Calificaciones);
                string calificacionesPuntoGeneralJson = JsonConvert.SerializeObject(calificacionLlamada.CalificacionesPuntosGenerales);
                string calificacionesPuntoCriticoJson = JsonConvert.SerializeObject(calificacionLlamada.CalificacionesPuntosCriticos);
                string calificacionesFaseJson = JsonConvert.SerializeObject(calificacionLlamada.CalificacionesFase);

                var parametros = new DynamicParameters();
                parametros.Add("@IdLlamada", calificacionLlamada.IdLlamada, DbType.Int32);
                parametros.Add("@IdVersion", calificacionLlamada.IdVersion, DbType.Int32);
                parametros.Add("@Calificaciones", calificacionesJson, DbType.String);
                parametros.Add("@CalificacionesPuntosGenerales", calificacionesPuntoGeneralJson, DbType.String);
                parametros.Add("@CalificacionesPuntosCriticos", calificacionesPuntoCriticoJson, DbType.String);
                parametros.Add("@CalificacionesFase", calificacionesFaseJson, DbType.String);
                parametros.Add("@CalificacionesFinalizacionLlamada", calificacionLlamada.InterrupcionLlamada, DbType.String);

                parametros.Add("@Usuario", calificacionLlamada.Usuario, DbType.String);

                _dapperRepository.QuerySPDapper("[com].[SP_InsertarCalificacionLlamadaAutomaticaV2]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la calificación de llamada: " + ex.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActivarConfiguracion(CongelamientoConfiguracionActivaDTO activarVersion)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ActivarConfiguracionEvaluacionLlamada]", new { activarVersion.IdVersion, activarVersion.Usuario, activarVersion.IdPersonalAreaTrabajo });
                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 24/04/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de calificación de la llamada según corresponda.
        /// Ejecuta el procedimiento almacenado [com].[SP_ActualizarEstadoCalificacionLlamada] que actualiza el campo esLlamadaCalificada,
        /// UsuarioModificacion y FechaModificacion en la tabla com.T_LlamadaWebphoneCruceCentralTresCx para el Id de llamada especificado.
        /// </summary>
        /// <param name="estadoLlamada">DTO con IdLlamada, EstadoLlamada y Usuario</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public bool ActualizarEstadoCalificacionLlamada(EstadoLlamadaCalificadaDTO estadoLlamada)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ActualizarEstadoCalificacionLlamada]", new { estadoLlamada.IdLlamada, estadoLlamada.estadoCalificacion, estadoLlamada.Usuario });
                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch (Exception)
            {
                return false;
            }
        }





        /// Autor: Joseph Llanque  
        /// Fecha: 27/05/2025  
        /// Version: 1.0  
        /// <summary>
        /// Guarda la configuración automática del panel de transcripción.
        /// </summary>
        /// <param name="configuracion">DTO con parámetros de configuración</param>
        /// <returns>True si el proceso fue exitoso</returns>
        public bool ConfigurarPanelAutomatico(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                //   string jsonDias = JsonConvert.SerializeObject(configuracion.Dias);
                //   string jsonAsesores = JsonConvert.SerializeObject(configuracion.Asesores);

                //   string jsonFasesEspecificas = configuracion.FasesEspecificas != null && configuracion.FasesEspecificas.Count > 0
                //       ? JsonConvert.SerializeObject(configuracion.FasesEspecificas)
                //       : null;

                //   string jsonFasesRango = configuracion.FasesRango != null
                //? JsonConvert.SerializeObject(new List<FaseRangoDTO> { configuracion.FasesRango })
                //: null;

                //   var parametros = new DynamicParameters();
                //   parametros.Add("@HoraEjecucion", configuracion.HoraEjecucion, DbType.Time);
                //   parametros.Add("@FechaInicioLlamada", configuracion.FechaInicio, DbType.Date);
                //   parametros.Add("@FechaFinLlamada", configuracion.FechaFin, DbType.Date);
                //   parametros.Add("@UsuarioCreacion", configuracion.UsuarioCreacion, DbType.String);
                //   parametros.Add("@JsonDias", jsonDias, DbType.String);
                //   parametros.Add("@JsonAsesores", jsonAsesores, DbType.String);
                //   parametros.Add("@JsonFasesEspecificas", jsonFasesEspecificas, DbType.String);
                //   parametros.Add("@JsonFasesRango", jsonFasesRango, DbType.String);
                //   parametros.Add("@TipoProcesoProgramado", 3, DbType.Int32);


                //   _dapperRepository.QuerySPDapper("[com].[SP_GuardarConfiguracionTranscripcion]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar configuración de transcripción: " + ex.Message);
            }
        }



        /// Autor: Joseph Llanque  
        /// Fecha: 27/05/2025  
        /// Version: 1.0  
        /// <summary>
        /// Guarda la configuración automática del panel de transcripción.
        /// </summary>
        /// <param name="configuracion">DTO con parámetros de configuración</param>
        /// <returns>True si el proceso fue exitoso</returns>
        public bool ConfigurarPanelAutomaticoCalificacion(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                //   string jsonDias = JsonConvert.SerializeObject(configuracion.Dias);
                //   string jsonAsesores = JsonConvert.SerializeObject(configuracion.Asesores);

                //   string jsonFasesEspecificas = configuracion.FasesEspecificas != null && configuracion.FasesEspecificas.Count > 0
                //       ? JsonConvert.SerializeObject(configuracion.FasesEspecificas)
                //       : null;

                //   string jsonFasesRango = configuracion.FasesRango != null
                //? JsonConvert.SerializeObject(new List<FaseRangoDTO> { configuracion.FasesRango })
                //: null;

                //var parametros = new DynamicParameters();
                //parametros.Add("@HoraEjecucion", configuracion.HoraEjecucion, DbType.Time);
                //parametros.Add("@FechaInicioLlamada", configuracion.FechaInicio, DbType.Date);
                //parametros.Add("@FechaFinLlamada", configuracion.FechaFin, DbType.Date);
                //parametros.Add("@UsuarioCreacion", configuracion.UsuarioCreacion, DbType.String);
                //parametros.Add("@JsonDias", jsonDias, DbType.String);
                //parametros.Add("@JsonAsesores", jsonAsesores, DbType.String);
                //parametros.Add("@JsonFasesEspecificas", jsonFasesEspecificas, DbType.String);
                //parametros.Add("@JsonFasesRango", jsonFasesRango, DbType.String);
                //parametros.Add("@TipoProcesoProgramado",1, DbType.Int32);


                //_dapperRepository.QuerySPDapper("[com].[SP_GuardarConfiguracionTranscripcion]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar configuración de transcripción: " + ex.Message);
            }
        }
        /// Autor: Joseph Llanque  
        /// Fecha: 27/05/2025  
        /// Version: 1.0  
        /// <summary>
        /// Guarda la configuración automática del panel de transcripción.
        /// </summary>
        /// <param name="configuracion">DTO con parámetros de configuración</param>
        /// <returns>True si el proceso fue exitoso</returns>
        public bool ConfigurarPanelCalificacionAuto(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                string jsonDias = JsonConvert.SerializeObject(configuracion.Dias);
                string jsonAsesores = JsonConvert.SerializeObject(configuracion.Asesores);

                var parametros = new DynamicParameters();
                parametros.Add("@HoraEjecucion", configuracion.HoraEjecucion, DbType.Time);
                parametros.Add("@FechaInicioLlamada", configuracion.FechaInicio, DbType.Date);
                parametros.Add("@FechaFinLlamada", configuracion.FechaFin, DbType.Date);
                parametros.Add("@UsuarioCreacion", configuracion.UsuarioCreacion, DbType.String);
                parametros.Add("@JsonDias", jsonDias, DbType.String);
                parametros.Add("@JsonAsesores", jsonAsesores, DbType.String);
                parametros.Add("@TipoProcesoProgramado", 4, DbType.Int32);

                _dapperRepository.QuerySPDapper("[com].[SP_GuardarConfiguracionTranscripcion]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar configuración de transcripción: " + ex.Message);
            }
        }
        /// Autor: Joseph Llanque  
        /// Fecha: 27/05/2025  
        /// Version: 1.0  
        /// <summary>
        /// Guarda la configuración automática del panel de transcripción.
        /// </summary>
        /// <param name="configuracion">DTO con parámetros de configuración</param>
        /// <returns>True si el proceso fue exitoso</returns>
        public bool ConfigurarPanelTranscripcionAuto(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                string jsonDias = JsonConvert.SerializeObject(configuracion.Dias);
                string jsonAsesores = JsonConvert.SerializeObject(configuracion.Asesores);

                var parametros = new DynamicParameters();
                parametros.Add("@HoraEjecucion", configuracion.HoraEjecucion, DbType.Time);
                parametros.Add("@FechaInicioLlamada", configuracion.FechaInicio, DbType.Date);
                parametros.Add("@FechaFinLlamada", configuracion.FechaFin, DbType.Date);
                parametros.Add("@UsuarioCreacion", configuracion.UsuarioCreacion, DbType.String);
                parametros.Add("@JsonDias", jsonDias, DbType.String);
                parametros.Add("@JsonAsesores", jsonAsesores, DbType.String);
                parametros.Add("@TipoProcesoProgramado", 2, DbType.Int32); //variable segun configuracion 


                _dapperRepository.QuerySPDapper("[com].[SP_GuardarConfiguracionTranscripcion]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar configuración de transcripción: " + ex.Message);
            }
        }
        /// Autor: Joseph Llanque  
        /// Fecha: 27/05/2025  
        /// Version: 1.0  
        /// <summary>
        /// Guarda la configuración automática del panel de transcripción.
        /// </summary>
        /// <param name="configuracion">DTO con parámetros de configuración</param>
        /// <returns>True si el proceso fue exitoso</returns>
        public bool ActivarConfiguracionTranscripcionAuto(ConfiguracionActivoProcesoDTO configuracion)
        {
            try
            {
                var parametros = new
                {
                    UsuarioModificacion = configuracion.Usuario,
                    IdTipoProcesoProgramado = 2,
                    Activo = configuracion.Activo
                };

                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ActualizarActivoProcesoProgramado]", parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar configuración de transcripción: " + ex.Message);
            }
        }
        /// Autor: Joseph Llanque  
        /// Fecha: 27/05/2025  
        /// Version: 1.0  
        /// <summary>
        /// Guarda la configuración automática del panel de transcripción.
        /// </summary>
        /// <param name="configuracion">DTO con parámetros de configuración</param>
        /// <returns>True si el proceso fue exitoso</returns>
        public bool ActivarConfiguracionCalificacionAuto(ConfiguracionActivoProcesoDTO configuracion)
        {
            try
            {
                var parametros = new
                {
                    UsuarioModificacion = configuracion.Usuario,
                    IdTipoProcesoProgramado = 4,
                    Activo = configuracion.Activo
                };

                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ActualizarActivoProcesoProgramado]", parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar configuración de transcripción: " + ex.Message);
            }
        }



        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionMasivaActiva()
        {
            try
            {
                var configuracionActiva = new List<ConfiguracionMasivaTranscripcionCalificacionDTO>();
                var query = @"SELECT DiaSemana,
		                                FechaFin,
		                                FechaInicio,
		                                HoraEjecucion,
		                                IdFaseOportunidad_Destino,
		                                IdFaseOportunidad,
		                                IdFaseOportunidad_Origen,
		                                IdPersonal,
		                                IdProcesoProgramado,
		                                IdTipoProcesoProgramado 
                                FROM [com].[V_ObtenerConfigurarionMasivaTranscripcionCalificacion] WHERE IdTipoProcesoProgramado=3";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracionActiva = JsonConvert.DeserializeObject<List<ConfiguracionMasivaTranscripcionCalificacionDTO>>(resultado);
                }
                return configuracionActiva;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionMasivaActiva()
        {
            try
            {
                var configuracionActiva = new List<ConfiguracionMasivaTranscripcionCalificacionDTO>();
                var query = @" SELECT DiaSemana,
		                                FechaFin,
		                                FechaInicio,
		                                HoraEjecucion,
		                                IdFaseOportunidad_Destino,
		                                IdFaseOportunidad,
		                                IdFaseOportunidad_Origen,
		                                IdPersonal,
		                                IdProcesoProgramado,
		                                IdTipoProcesoProgramado 
                                FROM [com].[V_ObtenerConfigurarionMasivaTranscripcionCalificacion] WHERE IdTipoProcesoProgramado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracionActiva = JsonConvert.DeserializeObject<List<ConfiguracionMasivaTranscripcionCalificacionDTO>>(resultado);
                }
                return configuracionActiva;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionAuto()
        {
            try
            {
                var configuracionActiva = new List<ConfiguracionMasivaTranscripcionCalificacionDTO>();
                var query = @" SELECT DiaSemana,
		                                FechaFin,
		                                FechaInicio,
		                                HoraEjecucion,
		                                IdFaseOportunidad_Destino,
		                                IdFaseOportunidad_Origen,
		                                IdPersonal,
		                                IdProcesoProgramado,
		                                IdTipoProcesoProgramado,
                                        Activo
                                FROM [com].[V_ObtenerConfigurarionMasivaTranscripcionCalificacion] WHERE IdTipoProcesoProgramado=4";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracionActiva = JsonConvert.DeserializeObject<List<ConfiguracionMasivaTranscripcionCalificacionDTO>>(resultado);
                }
                return configuracionActiva;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionTranscripcionAuto()
        {
            try
            {
                var configuracionActiva = new List<ConfiguracionMasivaTranscripcionCalificacionDTO>();
                var query = @" SELECT DiaSemana,
		                                FechaFin,
		                                FechaInicio,
		                                HoraEjecucion,
		                                IdFaseOportunidad_Destino,
		                                IdFaseOportunidad_Origen,
		                                IdPersonal,
		                                IdProcesoProgramado,
		                                IdTipoProcesoProgramado,
                                        Activo
                                FROM [com].[V_ObtenerConfigurarionMasivaTranscripcionCalificacion] WHERE IdTipoProcesoProgramado=2";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracionActiva = JsonConvert.DeserializeObject<List<ConfiguracionMasivaTranscripcionCalificacionDTO>>(resultado);
                }
                return configuracionActiva;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionTranscripcionAuto()
        {
            try
            {
                List<LlamadaProcesoAutoDTO> configuracion = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerInformacionTranscripcionAutomatica]", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionTranscripcionAutoAtencionCliente(int idPersonalAreaTrabajo)
        {
            try
            {
                List<LlamadaProcesoAutoDTO> configuracion = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_EvaluacionLlamadaObtenerTranscripcionAutomaticaAtencionCliente", new {idPersonalAreaTrabajo=idPersonalAreaTrabajo});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionAuto()
        {
            try
            {
                List<LlamadaProcesoAutoDTO> configuracion = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerInformacionCalificacionAutomatica]", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosEvaluacionLLamadaCalificacionAuto(int idPersonalAreaTrabajo)
        {
            try
            {
                List<LlamadaProcesoAutoDTO> configuracion = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerInformacionCalificacionAutomaticaEvaluacionLlamada]", new { idPersonalAreaTrabajo = idPersonalAreaTrabajo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionAtencionCliente()
        {
            try
            {
                List<LlamadaProcesoAutoDTO> configuracion = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerInformacionCalificacionAutomaticaPorAreaTrabajo]", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public LlamadaProcesoAutoDTO ObtenerDatosConfiguracionCalificacionPorIdLlamada(int idLlamada)
        {
            try
            {
                LlamadaProcesoAutoDTO configuracion = new LlamadaProcesoAutoDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("[com].[SP_ObtenerInformacionCalificacionLlamada]", new { IdLlamadaWebphoneCruceCentralTresCx=idLlamada });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<LlamadaProcesoAutoDTO>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public LlamadaProcesoAutoDTO ObtenerDatosConfiguracionCalificacionPorIdLlamadaV2(int idLlamada, int IdPersonalAreaTrabajo)
        {
            try
            {
                LlamadaProcesoAutoDTO configuracion = new LlamadaProcesoAutoDTO();

                var parametros = new
                {
                    IdLlamadaWebphoneCruceCentralTresCx = idLlamada,
                    IdPersonalAreaTrabajo = IdPersonalAreaTrabajo
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault("[com].[SP_ObtenerInformacionEvaluacionLlamada]", parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<LlamadaProcesoAutoDTO>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerHistoricoLlamadaCompletoPorIdOportunidad(int IdOportunidad)
        {
            try
            {
                List<LlamadaProcesoAutoDTO> HistorialLlamada = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerHistorialLlamadaOportunidad]", new { IdOportunidad = IdOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    HistorialLlamada = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return HistorialLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerHistoricoLlamadaCompletoPorIdOportunidadV2(int IdOportunidad, int IdPersonalAreaTrabajo)
        {
            try
            {
                List<LlamadaProcesoAutoDTO> HistorialLlamada = new List<LlamadaProcesoAutoDTO>();

                var parametros = new
                {
                    IdOportunidad = IdOportunidad,
                    IdPersonalAreaTrabajo = IdPersonalAreaTrabajo
                };

                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerHistorialLlamadaOportunidadEvaluacionLlamada]", parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    HistorialLlamada = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }

                return HistorialLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionTranscripcionMasiva()
        {
            try
            {
                List<LlamadaProcesoAutoDTO> configuracion = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerInformacionTranscripcionMasiva]", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionMasiva()
        {
            try
            {
                List<LlamadaProcesoAutoDTO> configuracion = new List<LlamadaProcesoAutoDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_ObtenerInformacionCalificacionMasiva]", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<LlamadaProcesoAutoDTO>>(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// Autor: Joseph Llanque
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta [com].[SP_ReporteCalificacionClientes] y mapea:
        ///   - ResultSet1: detalle (calificaciones por llamada)
        ///   - ResultSet2: total de registros
        /// </summary>
        public (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporte(ReporteCalificacionRequest req)
        {
            var resultadoQuery = _dapperRepository.QuerySPDapper(
                      "[com].[SP_ReporteCalificacionClienteV2]",
                      new
                      {
                          req.FechaInicio,
                          req.FechaFin,
                          IdsAsesores = (req.IdsAsesores != null && req.IdsAsesores.Any())
                              ? JsonConvert.SerializeObject(req.IdsAsesores)
                              : null,
                          req.IdCentroCosto,
                          req.IdFaseI,
                          req.IdFaseD,
                          req.EstadoActividadCabecera,
                          req.Pagina,
                          req.TamanioPagina
                      }
                  );

            string payload;

            var token = JToken.Parse(resultadoQuery);

            if (token.Type == JTokenType.Array)
            {
                var arr = (JArray)token;
                var first = arr.FirstOrDefault() as JObject;
                payload = (string?)first?["JsonResult"] ?? string.Empty;
            }
            else if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                payload = (string?)obj["JsonResult"] ?? resultadoQuery;
            }
            else
            {
                payload = resultadoQuery;
            }
            if (string.IsNullOrWhiteSpace(payload))
                return (Enumerable.Empty<LlamadaCalificadaRawDTO>(), 0);
            var wrapper = JsonConvert.DeserializeObject<ReporteJsonWrapper>(payload);
            var items = wrapper?.Items ?? new List<LlamadaCalificadaRawDTO>();
            var total = wrapper?.TotalRegistros ?? items.Count;
            return (items, total);



        }
        /// Autor: Lolo Zaa
        /// Fecha: 27/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta [com].[SP_ReporteCalificacionClientes] y mapea:
        ///   - ResultSet1: detalle (calificaciones por llamada)
        ///   - ResultSet2: total de registros
        /// </summary>
        public (IEnumerable<LlamadaCalificadaAtencionClienteRawDTO> Items, int Total) ObtenerReporteAtencionCliente(ReporteCalificacionRequest req)
        {
            var resultadoQuery = _dapperRepository.QuerySPDapper(
                      "[com].[SP_ReporteCalificacionClienteATC]",
                      new
                      {
                          req.FechaInicio,
                          req.FechaFin,
                          IdsAsesores = (req.IdsAsesores != null && req.IdsAsesores.Any())
                              ? JsonConvert.SerializeObject(req.IdsAsesores)
                              : null,
                          req.IdCentroCosto,
                          req.IdFaseI,
                          req.IdFaseD,
                          req.EstadoActividadCabecera,
                          req.Pagina,
                          req.TamanioPagina
                      }
                  );

            string payload;

            var token = JToken.Parse(resultadoQuery);

            if (token.Type == JTokenType.Array)
            {
                var arr = (JArray)token;
                var first = arr.FirstOrDefault() as JObject;
                payload = (string?)first?["JsonResult"] ?? string.Empty;
            }
            else if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                payload = (string?)obj["JsonResult"] ?? resultadoQuery;
            }
            else
            {
                payload = resultadoQuery;
            }
            if (string.IsNullOrWhiteSpace(payload))
                return (Enumerable.Empty<LlamadaCalificadaAtencionClienteRawDTO>(), 0);
            var wrapper = JsonConvert.DeserializeObject<ReporteJsonWrapperAtencionCliente>(payload);
            var items = wrapper?.Items ?? new List<LlamadaCalificadaAtencionClienteRawDTO>();
            var total = wrapper?.TotalRegistros ?? items.Count;
            return (items, total);
        }

        /// Autor: Joseph Llanque
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta [com].[SP_ReporteCalificacionClientes] y mapea:
        ///   - ResultSet1: detalle (calificaciones por llamada)
        ///   - ResultSet2: total de registros
        /// </summary>
        public (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReportePorArea(ReporteCalificacionAreaRequest req)
        {
            var resultadoQuery = _dapperRepository.QuerySPDapper(
                      "[com].[SP_ReporteCalificacionClienteV2]",
                      new
                      {
                          req.FechaInicio,
                          req.FechaFin,
                          IdsAsesores = (req.IdsAsesores != null && req.IdsAsesores.Any())
                              ? JsonConvert.SerializeObject(req.IdsAsesores)
                              : null,
                          req.IdCentroCosto,
                          req.IdFaseI,
                          req.IdFaseD,
                          req.EstadoActividadCabecera,
                          req.Pagina,
                          req.TamanioPagina,
                          req.IdPersonalAreaTrabajo
                      }
                  );

            string payload;

            var token = JToken.Parse(resultadoQuery);

            if (token.Type == JTokenType.Array)
            {
                var arr = (JArray)token;
                var first = arr.FirstOrDefault() as JObject;
                payload = (string?)first?["JsonResult"] ?? string.Empty;
            }
            else if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                payload = (string?)obj["JsonResult"] ?? resultadoQuery;
            }
            else
            {
                payload = resultadoQuery;
            }
            if (string.IsNullOrWhiteSpace(payload))
                return (Enumerable.Empty<LlamadaCalificadaRawDTO>(), 0);
            var wrapper = JsonConvert.DeserializeObject<ReporteJsonWrapper>(payload);
            var items = wrapper?.Items ?? new List<LlamadaCalificadaRawDTO>();
            var total = wrapper?.TotalRegistros ?? items.Count;
            return (items, total);



        }

        /// Autor: Joseph Llanque
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta [com].[SP_ReporteCalificacionClientes] y mapea:
        ///   - ResultSet1: detalle (calificaciones por llamada)
        ///   - ResultSet2: total de registros
        /// </summary>
        public (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporteFase(ReporteCalificacionRequest req)
        {
            var resultadoQuery = _dapperRepository.QuerySPDapper(
                      "[com].[SP_ReporteCalificacionFaseV2]",
                      new
                      {
                          req.FechaInicio,
                          req.FechaFin,
                          IdsAsesores = (req.IdsAsesores != null && req.IdsAsesores.Any())
                              ? JsonConvert.SerializeObject(req.IdsAsesores)
                              : null,
                          req.IdCentroCosto,
                          req.IdFaseI,
                          req.IdFaseD,
                          req.Pagina,
                          req.TamanioPagina
                      }
                  );

            string payload;

            var token = JToken.Parse(resultadoQuery);

            if (token.Type == JTokenType.Array)
            {
                var arr = (JArray)token;
                var first = arr.FirstOrDefault() as JObject;
                payload = (string?)first?["JsonResult"] ?? string.Empty;
            }
            else if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                payload = (string?)obj["JsonResult"] ?? resultadoQuery;
            }
            else
            {
                payload = resultadoQuery;
            }
            if (string.IsNullOrWhiteSpace(payload))
                return (Enumerable.Empty<LlamadaCalificadaRawDTO>(), 0);
            var wrapper = JsonConvert.DeserializeObject<ReporteJsonWrapper>(payload);
            var items = wrapper?.Items ?? new List<LlamadaCalificadaRawDTO>();
            var total = wrapper?.TotalRegistros ?? items.Count;
            return (items, total);



        }

        public IEnumerable<LlamadaCalificadaRawDTO> ObtenerDatosParaPromedioGlobal(ReporteCalificacionGlobalRequest request)
        {
            var resultado = _dapperRepository.QuerySPDapper(
                "[com].[SP_ReporteCalificacionGlobalV2]",
                new
                {
                    request.FechaInicio,
                    request.FechaFin,
                    IdsAsesores = (request.IdsAsesores != null && request.IdsAsesores.Any())
                        ? JsonConvert.SerializeObject(request.IdsAsesores)
                        : null,
                    request.IdCentroCosto,
                    request.IdFaseI,
                    request.IdFaseD,
                    request.EstadoActividadCabecera
                }
            );

            // Parsear resultado JSON (igual que el método existente)
            string payload;
            var token = JToken.Parse(resultado);

            if (token.Type == JTokenType.Array)
            {
                var arr = (JArray)token;
                var first = arr.FirstOrDefault() as JObject;
                payload = (string?)first?["JsonResult"] ?? string.Empty;
            }
            else if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                payload = (string?)obj["JsonResult"] ?? resultado;
            }
            else
            {
                payload = resultado;
            }

            if (string.IsNullOrWhiteSpace(payload))
                return Enumerable.Empty<LlamadaCalificadaRawDTO>();

            var wrapper = JsonConvert.DeserializeObject<ReporteJsonWrapper>(payload);
            return wrapper?.Items ?? new List<LlamadaCalificadaRawDTO>();
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
                var resultado = _dapperRepository.QuerySPDapper(
                    "[com].[SP_ObtenerCalificacionFaseV2]",
                    new { IdLlamadaWebphoneCruceCentralTresCx = idLlamada, TipoEvaluacion = tipoCalificacion }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CalificacionFaseDTO>>(resultado);
                }
                return Enumerable.Empty<CalificacionFaseDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene informacion de llamada de tabla WebPhoneCruceCentralTresCx
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public IEnumerable<InformacionLlamadaTresCxDTO> ObtenerInformacionLlamada(int idLlamada)
        {
            try
            {
                var informacionLlamada = new List<InformacionLlamadaTresCxDTO>();
                var query = @" SELECT AnexoCentral,
		                                DuracionContesto,
		                                DuracionTimbrado,
		                                esLlamadaCalificada,
		                                esLlamadaTranscrita,
		                                EstadoLlamada,
		                                FechaFinLlamada,
		                                FechaInicioLlamada,
		                                IdActividadDetalle,
		                                IdRegistroLlamada,
		                                NombreGrabacion,
		                                OrigenLlamada,
		                                OrigenReal,
		                                SubEstadoLlamada,
		                                TelefonoDestino,
		                                TelefonoDestinoReal,
		                                Tipo,
		                                UrlGrabacion,
		                                UrlGrabacion2,
		                                WebphoneGrabacion FROM com.V_ObtenerInformacionLlamadaCruceCentralTresCx WHERE IdRegistroLlamada=@idLlamada";
                var resultado = _dapperRepository.QueryDapper(query, new { idLlamada = idLlamada });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    informacionLlamada = JsonConvert.DeserializeObject<List<InformacionLlamadaTresCxDTO>>(resultado);
                }
                return informacionLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public async Task<InsertRecomendacionResultDTO> ProcesarRecomendacionesBatch(RecomendacionLlamadaDTO recomendaciones)
        {
            try
            {

                int id = int.Parse(recomendaciones.IdLlamada);
                var json = JsonConvert.SerializeObject(new { recomendaciones = recomendaciones.Recomendaciones ?? new List<string>() });
                var parametros = new
                {
                    IdLlamadaWebphoneCruceCentralTresCx = id,
                    Recomendaciones = json,
                    Usuario = "System-Auto"
                };
                var configuracion = new List<InsertRecomendacionResultDTO>();


                var resultado = _dapperRepository.QuerySPDapper("[com].[SP_InsertarRecomendacionTranscripcion]", parametros);

                if (string.IsNullOrWhiteSpace(resultado) || resultado == "[]")
                    return new InsertRecomendacionResultDTO { IdTranscripcionLlamada = 0, RecomendacionesInsertadas = 0 };

                return resultado.TrimStart().StartsWith("[")
                    ? JsonConvert.DeserializeObject<List<InsertRecomendacionResultDTO>>(resultado).FirstOrDefault()
                    : JsonConvert.DeserializeObject<InsertRecomendacionResultDTO>(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Obtiene informacion de llamada de tabla WebPhoneCruceCentralTresCx
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public IEnumerable<LlamadaWebphoneOcurrenciaDTO> ObtenerOcurrenciaRegistrada(int idOportunidad)
        {
            try
            {
                var informacionLlamada = new List<LlamadaWebphoneOcurrenciaDTO>();
                var query = @" SELECT
                                		IdAlumno,
                                		NombreCliente,
                                		IdLlamada,
                                		IdOportunidad,
                                		IdOcurrenciaPadreAlterno,
                                		IdOcurrenciaActividadAlterno,
                                		IdOcurrenciaAlterno,
                                		OcurrenciaPadreAlterno,
                                		OcurrenciaAlterno,
                                		EstadoOcurrenciaAlterno,
                                		FechaReal
                                FROM
                                		[com].[V_ObtenerHistoricoOcurrencia]
                                WHERE	idOportunidad = @idOportunidad ORDER BY FechaReal Desc;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    informacionLlamada = JsonConvert.DeserializeObject<List<LlamadaWebphoneOcurrenciaDTO>>(resultado);
                }
                return informacionLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<HistoricoOcurrenciaAtencionClienteDto> ObtenerOcurrenciaRegistradaV2(int idOportunidad, int idPersonalAreaTrabajo)
        {
            try
            {
                var informacionLlamada = new List<HistoricoOcurrenciaAtencionClienteDto>();

                var query = @"SELECT
                             IdLlamada,
				             EstadoOcurrencia,
				             IdAlumno,
				             IdOcurrencia,
				             IdOcurrenciaActividad,
				             IdOcurrenciaPadre,
				             IdOportunidad,
				             FechaReal,
				             NombreCliente,
				             Ocurrencia,
				             IdPersonalAreaTrabajo,
				             OcurrenciaPadre,
                    IdOcurrenciaPadreAlterno,
                    IdOcurrenciaActividadAlterno,
                    IdOcurrenciaAlterno,
                    OcurrenciaPadreAlterno,
                    OcurrenciaAlterno

                      FROM
                            ope.V_EvaluacionLlamadaObtenerHistoricoOcurrenciaAtencionCliente
                      WHERE IdOportunidad = @idOportunidad
                        AND IdPersonalAreaTrabajo = @idPersonalAreaTrabajo
                      ORDER BY FechaReal DESC;";

                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    IdOportunidad = idOportunidad,
                    IdPersonalAreaTrabajo = idPersonalAreaTrabajo
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    informacionLlamada = JsonConvert.DeserializeObject<List<HistoricoOcurrenciaAtencionClienteDto>>(resultado);
                }

                return informacionLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la configuración de cambio de fase de oportunidad con criterios y lineamientos desde la vista com.V_ObtenerConfiguracionCambioFaseOportunidad,
        /// filtrando por IdFaseOrigen e IdFaseDestino.
        /// </summary>
        /// <param name="idFaseOrigen">Id de la fase de origen</param>
        /// <param name="idFaseDestino">Id de la fase de destino</param>
        /// <returns>Lista de TransicionFaseOportunidadDTO</returns>
        public IEnumerable<TransicionCambioFaseOportunidadDTO> ObtenerConfiguracionCambioFaseOportunidad(int idFaseOrigen, int idFaseDestino)
        {
            try
            {
                var resultado = new List<TransicionCambioFaseOportunidadDTO>();
                var query = @"
                                SELECT
                                     IdTransicionFaseOportunidad,
                                     IdFaseOrigen,
                                     NombreFaseOrigen,
                                     CodigoFaseOrigen,
                                     IdFaseDestino,
                                     NombreFaseDestino,
                                     CodigoFaseDestino,
                                     IdCriterio,
                                     OrdenCriterio,
                                     NombreCriterio,
                                     IdLineamientoCalificacionFase,
                                     OrdenLineamiento,
                                     NombreLineamientoCalificacionFase,
                                     IdCriticidadCalificacion,
                                     NombreCriticidad
                                FROM com.V_ObtenerConfiguracionCambioFaseOportunidad
                                WHERE IdFaseOrigen = @IdFaseOrigen AND IdFaseDestino = @IdFaseDestino";

                var data = _dapperRepository.QueryDapper(query, new { IdFaseOrigen = idFaseOrigen, IdFaseDestino = idFaseDestino });
                if (!string.IsNullOrEmpty(data) && !data.Equals("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<TransicionCambioFaseOportunidadDTO>>(data);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene informacion de llamada de tabla WebPhoneCruceCentralTresCx
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public IEnumerable<PuntosCriticosLlamadaDiaDto> ObtenerPuntosCriticosPorDia()
        {
            try
            {
                var informacionLlamada = new List<PuntosCriticosLlamadaDiaDto>();
                var query = @" SELECT  FechaReal,
		                                IdLlamadaWebphoneCruceCentralTresCx,
		                                IdPersonal,
		                                PuntoCritico,
		                                ResumenLlamada FROM [com].[V_PuntosCriticosLlamadaDia]
                                WHERE CAST(FechaReal AS DATE)=CAST(GETDATE()-1 AS DATE) order by IdPersonal desc";
                var resultado = _dapperRepository.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    informacionLlamada = JsonConvert.DeserializeObject<List<PuntosCriticosLlamadaDiaDto>>(resultado);
                }
                return informacionLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool InsertarCongelamientoPuntoCritico(int idPersonal, DateTime FechaGeneracion , string PuntoCriticoJson)
        {
            try
            {
                var resultado = JsonConvert.DeserializeObject<ResultadoPuntoCriticoConsolidaddoDTO>(PuntoCriticoJson);
                var puntosCriticosArray = JsonConvert.SerializeObject(resultado.consolidadocritico);

                var parametros = new DynamicParameters();
                parametros.Add("@IdPersonal", idPersonal, DbType.Int32);
                parametros.Add("@FechaGeneracion", FechaGeneracion, DbType.DateTime);
                parametros.Add("@PuntoCriticoJson", puntosCriticosArray, DbType.String);
                parametros.Add("@Usuario", "System-Auto", DbType.String);

                _dapperRepository.QuerySPDapper("[com].[SP_InsertarPuntoCriticoResumenDiario]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la calificación de llamada: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el resumen diario de puntos críticos para un personal y fecha específicos.
        /// Consulta la tabla <c>com.T_PuntoCriticoResumenDiario</c> filtrando por <paramref name="idPersonal"/> y la fecha de generación.
        /// </summary>
        /// <param name="idPersonal">ID del personal para el cual se busca el resumen diario de puntos críticos.</param>
        /// <param name="fechaGeneracion">Fecha para la cual se requiere el resumen diario (solo la parte de la fecha es considerada).</param>
        /// <returns>Lista de <see cref="PuntoCriticoResumenDiarioDTO"/> con los puntos críticos encontrados para el personal y fecha indicados.</returns>
        public IEnumerable<PuntoCriticoResumenDiarioDTO> ObtenerPuntoCriticoDiario(int idPersonal, DateTime fechaGeneracion)
        {
            try
            {
                var informacionLlamada = new List<PuntoCriticoResumenDiarioDTO>();
                var query = @" SELECT  
                              Id,
                              FechaCreacion,
                              FechaGeneracion,
                              FechaModificacion,
                              IdPersonal	,
                              PuntoCritico,
                              Estado	
                              FROM com.T_PuntoCriticoResumenDiario
                                WHERE  IdPersonal=@idPersonal AND CAST(fechaGeneracion AS DATE)=CAST(@fechaGeneracion AS DATE) AND ESTADO=1";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal, fechaGeneracion = fechaGeneracion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    informacionLlamada = JsonConvert.DeserializeObject<List<PuntoCriticoResumenDiarioDTO>>(resultado);
                }
                return informacionLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene extras de criterios por IdLlamada
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>

        public IEnumerable<EvaluacionLlamadaDetalleDTO> ObtenerDetallesEvaluacionPorLlamada(int idLlamada)
{
    try
    {
        var detalleLlamada = new List<EvaluacionLlamadaDetalleDTO>();
        var query = "com.SP_ObtenerDetalleAutomaticaEvaluacionLlamada";

        var resultado = _dapperRepository.QuerySPDapper(query, new 
        {
            IdLlamadaWebphoneCruceCentralTresCx = idLlamada
        });

        if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
        {
            detalleLlamada = JsonConvert.DeserializeObject<List<EvaluacionLlamadaDetalleDTO>>(resultado);
        }

        return detalleLlamada;
    }
    catch (Exception ex)
    {
        throw;
    }
}
        /// Autor: Lolo Zaa
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene extra de puntos generales por IdLlamada
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<EvaluacionPuntoGeneralDetalleDTO> ObtenerDetallesEvaluacionPuntosGeneralesPorLlamada(int idLlamada)
{
    try
    {
        var detalleLlamada = new List<EvaluacionPuntoGeneralDetalleDTO>();
        var query = "com.SP_ObtenerPuntoGeneralDetalleEvaluacionLlamada";

        var resultado = _dapperRepository.QuerySPDapper(query, new
        {
            IdLlamadaWebphoneCruceCentralTresCx = idLlamada
        });

        if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
        {
            detalleLlamada = JsonConvert.DeserializeObject<List<EvaluacionPuntoGeneralDetalleDTO>>(resultado);
        }

        return detalleLlamada;
    }
    catch (Exception ex)
    {
        throw;
    }
}

        /// Autor: Lolo Zaa
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la version de configuracion por IdLlamada
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionPorLlamdaDTO> HistorialVersionCalificacionPorLlamada(int idLlamada)
{
    try
    {
        var historial = new List<ConfiguracionEsquemaCalificacionPorLlamdaDTO>();
        var query = "com.SP_EvaluacionLlamada_ObtenerConfiguracionLineamiento";

        var resultado = _dapperRepository.QuerySPDapper(query, new
        {
            IdLlamadaWebphoneCruceCentralTresCx = idLlamada
        });

        if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
        {
            historial = JsonConvert.DeserializeObject<List<ConfiguracionEsquemaCalificacionPorLlamdaDTO>>(resultado);
        }

        return historial;
    }
    catch (Exception)
    {
        throw;
    }
}
        /// Autor: Lolo Zaa
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la version de configuracion por IdLlamada
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionPorLlamdaDTO> HistorialVersionCalificacionPorLlamadav2(int idLlamada)
        {
            try
            {
                var historial = new List<ConfiguracionEsquemaCalificacionPorLlamdaDTO>();
                var query = "com.SP_EvaluacionLlamadaObtenerEvaluacionLlamadaConfiguracion";

                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    IdLlamadaWebphoneCruceCentralTresCx = idLlamada
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    historial = JsonConvert.DeserializeObject<List<ConfiguracionEsquemaCalificacionPorLlamdaDTO>>(resultado);
                }

                return historial;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<ConfiguracionVigenteJerarquiaDTO> ObtenerConfiguracionPorVersion(
        int idVersion,
        int idPersonalAreaTrabajo
    )
        {
            try
            {
                var configuracion = new List<ConfiguracionVigenteJerarquiaDTO>();
                var resultado = _dapperRepository.QuerySPDapper(
                    "[com].[SP_EvaluacionLlamadaObtenerConfiguracionPorVersion]",
                    new { IdVersion = idVersion, IdPersonalAreaTrabajo = idPersonalAreaTrabajo }
                );
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<
                        List<ConfiguracionVigenteJerarquiaDTO>
                    >(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ConfiguracionVigenteJerarquiaDTO> ObtenerConfiguracionVigentePorArea(
        int idPersonalAreaTrabajo
    )
        {
            try
            {
                var configuracion = new List<ConfiguracionVigenteJerarquiaDTO>();
                var resultado = _dapperRepository.QuerySPDapper(
                    "[com].[SP_EvaluacionLlamadaObtenerConfiguracionVigente]",
                    new { IdPersonalAreaTrabajo = idPersonalAreaTrabajo }
                );
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<
                        List<ConfiguracionVigenteJerarquiaDTO>
                    >(resultado);
                }
                return configuracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 25/01/2025
        /// Version: 1.0
        /// <summary>
        /// Guarda calificación manual en tiempo real usando tablas temporales.
        /// Inserta registro en T_EvaluacionLlamadaTemporal y T_EvaluacionDetalleManualTemporal.
        /// Utiliza IdActividadDetalle + NumeroLlamada como identificadores temporales.
        /// </summary>
        /// <param name="calificacionTemporal">DTO con datos de calificación temporal</param>
        /// <returns>True si la operación fue exitosa</returns>
        public bool GuardarCalificacionLlamadaTemporal(CalificacionLlamadaManualTemporalDTO calificacionTemporal)
        {
            try
            {
                string calificacionesJson = JsonConvert.SerializeObject(calificacionTemporal.Calificaciones);
                string calificacionesPuntoGeneralJson = JsonConvert.SerializeObject(calificacionTemporal.CalificacionesPuntosGenerales);

                var parametros = new DynamicParameters();
                parametros.Add("@idActividadDetalle", calificacionTemporal.IdActividadDetalle, DbType.Int32);
                parametros.Add("@numeroLlamada", calificacionTemporal.NumeroLlamada, DbType.Int32);
                parametros.Add("@idVersion", calificacionTemporal.IdVersion, DbType.Int32);
                parametros.Add("@tipoEvaluacion", calificacionTemporal.TipoEvaluacion, DbType.Boolean);
                parametros.Add("@calificaciones", calificacionesJson, DbType.String);
                parametros.Add("@calificacionesPuntosGenerales", calificacionesPuntoGeneralJson, DbType.String);
                parametros.Add("@usuario", calificacionTemporal.Usuario, DbType.String);

                _dapperRepository.QuerySPDapper("[com].[SP_CalificacionLlamadaTemporal_Insertar]", parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la calificación temporal de llamada: " + ex.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 25/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene las calificaciones temporales desde T_EvaluacionLlamadaTemporal y T_EvaluacionDetalleManualTemporal.
        /// Utiliza IdActividadDetalle + NumeroLlamada para identificar la llamada en tiempo real.
        /// </summary>
        /// <param name="idActividadDetalle">ID de la actividad detalle</param>
        /// <param name="numeroLlamada">Número secuencial de la llamada</param>
        /// <returns>Lista de calificaciones temporales</returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamientoTemporal(int idActividadDetalle, int numeroLlamada)
        {
            try
            {
                var calificaciones = new List<CalificacionLlamadaDTO>();
                var resultado = _dapperRepository.QuerySPDapper(
                    "[com].[SP_CalificacionLlamadaTemporal_Obtener]",
                    new { IdActividadDetalle = idActividadDetalle, NumeroLlamada = numeroLlamada }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    calificaciones = JsonConvert.DeserializeObject<List<CalificacionLlamadaDTO>>(resultado);
                }

                return calificaciones;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las calificaciones temporales: " + ex.Message);
            }
        }


    }


}
