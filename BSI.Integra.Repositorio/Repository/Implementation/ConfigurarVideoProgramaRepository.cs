using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfigurarVideoProgramaRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 01/09/2022
    /// <summary>
    /// Gestión general de T_ConfigurarVideoPrograma
    /// </summary>
    public class ConfigurarVideoProgramaRepository : GenericRepository<TConfigurarVideoPrograma>, IConfigurarVideoProgramaRepository
    {
        private Mapper _mapper;

        public ConfigurarVideoProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfigurarVideoPrograma, ConfigurarVideoPrograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSesionConfigurarVideo, SesionConfigurarVideo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfigurarVideoPrograma MapeoEntidad(ConfigurarVideoPrograma entidad)
        {
            try
            {
                //crea la entidad padre
                TConfigurarVideoPrograma modelo = _mapper.Map<TConfigurarVideoPrograma>(entidad);

                //mapea los hijos
                if (entidad.SesionConfigurarVideos != null && entidad.SesionConfigurarVideos.Count > 0)
                    modelo.TSesionConfigurarVideos = _mapper.Map<List<TSesionConfigurarVideo>>(entidad.SesionConfigurarVideos);


                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfigurarVideoPrograma Add(ConfigurarVideoPrograma entidad)
        {
            try
            {
                var ConfigurarVideoPrograma = MapeoEntidad(entidad);
                base.Insert(ConfigurarVideoPrograma);
                return ConfigurarVideoPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfigurarVideoPrograma Update(ConfigurarVideoPrograma entidad)
        {
            try
            {
                var ConfigurarVideoPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfigurarVideoPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfigurarVideoPrograma);
                return ConfigurarVideoPrograma;
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


        public IEnumerable<TConfigurarVideoPrograma> Add(IEnumerable<ConfigurarVideoPrograma> listadoEntidad)
        {
            try
            {
                List<TConfigurarVideoPrograma> listado = new List<TConfigurarVideoPrograma>();
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

        public IEnumerable<TConfigurarVideoPrograma> Update(IEnumerable<ConfigurarVideoPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfigurarVideoPrograma> listado = new List<TConfigurarVideoPrograma>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>PreEstructuraCapituloProgramaDTO</returns>
        public List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoPrograma(int idPGeneral)
        {
            List<PreEstructuraCapituloProgramaDTO> rpta = new List<PreEstructuraCapituloProgramaDTO>();
            string query = @"SELECT Id,
                                       IdConfigurarVideoPrograma,
                                       IdPGeneral IdPgeneral,
                                       Nombre,
                                       Titulo,
                                       Contenido,
                                       NombreTitulo,
                                       IdSeccionTipoDetalle_PW,
                                       NumeroFila,
                                       VideoId,
                                       Archivo,
                                       NroDiapositivas,
                                       ConImagenVideo,
                                       ImagenVideoNombre,
                                       ImagenVideoAncho,
                                       ImagenVideoAlto,
                                       ConImagenDiapositiva,
                                       ImagenDiapositivaNombre,
                                       ImagenDiapositivaAncho,
                                       ImagenDiapositivaAlto,
                                       ImagenVideoPosicionX,
                                       ImagenVideoPosicionY,
                                       ImagenDiapositivaPosicionX,
                                       ImagenDiapositivaPosicionY,
                                       Minuto,
                                       IdTipoVista,
                                       NroDiapositiva,
                                       ConLogoVideo,
                                       ConLogoDiapositiva,
                                       TotalSegundos,
                                       VideoIdBrightcove,
                                       VideoIdVimeo,
                                       ReproduccionVideo,
                                       DescargaVideo
                            FROM pla.V_ListadoEstructuraPrograma
                            WHERE IdPGeneral = @IdPGeneral
                            ORDER BY NumeroFila,
                                     IdSeccionTipoDetalle_PW;";
            string queryDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaDTO>>(queryDB);
                return rpta;
            }
            else
            {
                return null;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general y numero de fila
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Encuestas = PreEstructuraCapituloProgramaDTO</returns>
        public List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEncuestas(int idPGeneral, int numeroFila)
        {
            List<PreEstructuraCapituloProgramaDTO> rpta = new List<PreEstructuraCapituloProgramaDTO>();
            string _query = "Select Id,IdPGeneral IdPgeneral,Nombre,Titulo,Contenido,NombreTitulo,IdSeccionTipoDetalle_PW,NumeroFila,TotalSegundos From pla.V_ExamenesEstructuraPorPrograma Where IdPGeneral=@idPGeneral AND NumeroFila=@numeroFila";
            string query = _dapperRepository.QueryDapper(_query, new { idPGeneral = idPGeneral, numeroFila = numeroFila });
            if (!string.IsNullOrEmpty(query) && query != "null")
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaDTO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general y numero de fila
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Evaluaciones = PreEstructuraCapituloProgramaDTO</returns>
        public List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEvaluaciones(int idPGeneral, int numeroFila)
        {
            List<PreEstructuraCapituloProgramaDTO> rpta = new List<PreEstructuraCapituloProgramaDTO>();
            string _query = "Select Id,IdPGeneral IdPgeneral,Nombre,Titulo,Contenido,NombreTitulo,IdSeccionTipoDetalle_PW,NumeroFila,TotalSegundos From pla.V_EvaluacionTrabajoEstructuraPorPrograma Where IdPGeneral=@idPGeneral AND NumeroFila=@numeroFila";
            string query = _dapperRepository.QueryDapper(_query, new { idPGeneral = idPGeneral, numeroFila = numeroFila });
            if (!string.IsNullOrEmpty(query) && query != "null")
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaDTO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros relacionados al IdPGeneral
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>  IEnumerable<ConfigurarVideoPrograma> </returns> 
        public IEnumerable<ConfigurarVideoPrograma> ObtenerPorIdPGeneral(int idPGeneral)
        {
            string _query = @"SELECT Id,
                                   IdPGeneral IdPgeneral,
                                   IdDocumentoSeccionPw,
                                   VideoId,
                                   TotalMinutos,
                                   Archivo,
                                   NroDiapositivas,
                                   Configurado,
                                   Estado,
                                   FechaCreacion,
                                   FechaModificacion,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   ConImagenVideo,
                                   ImagenVideoNombre,
                                   ImagenVideoAncho,
                                   ImagenVideoAlto,
                                   ConImagenDiapositiva,
                                   ImagenDiapositivaNombre,
                                   ImagenDiapositivaAncho,
                                   ImagenDiapositivaAlto,
                                   NumeroFila,
                                   Token,
                                   ImagenVideoPosicionX,
                                   ImagenVideoPosicionY,
                                   ImagenDiapositivaPosicionX,
                                   ImagenDiapositivaPosicionY,
                                   VideoIdBrightcove,
                                   Activo
                            FROM pla.T_ConfigurarVideoPrograma
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral;";
            string query = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(query) && query != "[]")
                return JsonConvert.DeserializeObject<IEnumerable<ConfigurarVideoPrograma>>(query);
            return new List<ConfigurarVideoPrograma>();
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Eliminacion lógica Configuracion Video segun Id Programa General
        /// </summary>
        /// <param name="idProgramaGeneral"></param>        
        /// <returns></returns>
        public void EliminarConfiguracionVideo(int idProgramaGeneral)
        {
            try
            {
                var query = _dapperRepository.QuerySPFirstOrDefault("[pla].[SP_EliminarConfiguracionesVideo]", new { IdProgramaGeneral = idProgramaGeneral });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 17/07/2023
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idDocumentoSeccionPw">Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)</param>
        /// <param name="numeroFila">Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public ConfigurarVideoProgramaDTO ObtenerConfigurarVideoPrograma(int idPGeneral, int idDocumentoSeccionPw, int numeroFila)
        {
            string query = @"SELECT Id,
                                   IdPGeneral IdPgeneral,
                                   IdDocumentoSeccionPw,
                                   VideoId,
                                   VideoIdBrightcove,
                                   VideoIdVimeo,
                                   ReproduccionVideo,
                                   DescargaVideo,
                                   TotalMinutos,
                                   Archivo,
                                   NroDiapositivas,
                                   Configurado,
                                   ConImagenVideo,
                                   ImagenVideoNombre,
                                   ImagenVideoAncho,
                                   ImagenVideoAlto,
                                   ConImagenDiapositiva,
                                   ImagenDiapositivaNombre,
                                   ImagenDiapositivaAncho,
                                   ImagenDiapositivaAlto,
                                   ImagenVideoPosicionX,
                                   ImagenVideoPosicionY,
                                   ImagenDiapositivaPosicionX,
                                   ImagenDiapositivaPosicionY,
                                   NumeroFila
                            FROM pla.V_TConfigurarVideoProgramaValidacionCampos
                            WHERE IdPGeneral = @IdPGeneral
                                  AND IdDocumentoSeccionPw = @IdDocumentoSeccionPw
                                  AND NumeroFila = @NumeroFila;";
            string queryDb = _dapperRepository.FirstOrDefault(query, new { IdPGeneral = idPGeneral, IdDocumentoSeccionPw = idDocumentoSeccionPw, NumeroFila = numeroFila });
            if (!string.IsNullOrEmpty(query) && !query.Equals("null"))
                return JsonConvert.DeserializeObject<ConfigurarVideoProgramaDTO>(queryDb);
            return null;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 17/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general y numero de fila
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Encuestas = PreEstructuraCapituloProgramaDTO</returns>
        public ConfigurarVideoPrograma ObtenerPorId(int id)
        {
            string _query = @"SELECT Id,
                                   IdPGeneral IdPgeneral,
                                   IdDocumentoSeccionPw,
                                   VideoId,
                                   TotalMinutos,
                                   Archivo,
                                   NroDiapositivas,
                                   Configurado,
                                   Estado,
                                   FechaCreacion,
                                   FechaModificacion,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   ConImagenVideo,
                                   ImagenVideoNombre,
                                   ImagenVideoAncho,
                                   ImagenVideoAlto,
                                   ConImagenDiapositiva,
                                   ImagenDiapositivaNombre,
                                   ImagenDiapositivaAncho,
                                   ImagenDiapositivaAlto,
                                   NumeroFila,
                                   Token,
                                   ImagenVideoPosicionX,
                                   ImagenVideoPosicionY,
                                   ImagenDiapositivaPosicionX,
                                   ImagenDiapositivaPosicionY,
                                   VideoIdBrightcove,
                                   Activo
                            FROM pla.T_ConfigurarVideoPrograma
                            WHERE Estado = 1
                                  AND Id = @Id;";
            string query = _dapperRepository.FirstOrDefault(_query, new { Id = id });
            if (!string.IsNullOrEmpty(query) && query != "null")
                return JsonConvert.DeserializeObject<ConfigurarVideoPrograma>(query);
            return null;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros asociados al VideoId
        /// </summary>
        /// <param name="videoId"> Id del Video </param>
        /// <returns> IEnumerable<ConfigurarVideoPrograma> </returns>
        public IEnumerable<ConfigurarVideoPrograma> ObtenerPorVideoId(string videoId)
        {
            string _query = @"SELECT Id,
                                   IdPGeneral IdPgeneral,
                                   IdDocumentoSeccionPw,
                                   VideoId,
                                   TotalMinutos,
                                   Archivo,
                                   NroDiapositivas,
                                   Configurado,
                                   Estado,
                                   FechaCreacion,
                                   FechaModificacion,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   ConImagenVideo,
                                   ImagenVideoNombre,
                                   ImagenVideoAncho,
                                   ImagenVideoAlto,
                                   ConImagenDiapositiva,
                                   ImagenDiapositivaNombre,
                                   ImagenDiapositivaAncho,
                                   ImagenDiapositivaAlto,
                                   NumeroFila,
                                   Token,
                                   ImagenVideoPosicionX,
                                   ImagenVideoPosicionY,
                                   ImagenDiapositivaPosicionX,
                                   ImagenDiapositivaPosicionY,
                                   VideoIdBrightcove,
                                   Activo
                            FROM pla.T_ConfigurarVideoPrograma
                            WHERE Estado = 1
                                  AND VideoId = @VideoId;";
            string query = _dapperRepository.QueryDapper(_query, new { VideoId = videoId });
            if (!string.IsNullOrEmpty(query) && query != "[]")
                return JsonConvert.DeserializeObject<IEnumerable<ConfigurarVideoPrograma>>(query);
            return new List<ConfigurarVideoPrograma>();
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public IEnumerable<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaDescargaSinDatos(int idPGeneral)
        {

            string query = @"SELECT Id,
                                       IdConfigurarVideoPrograma,
                                       IdPGeneral,
                                       Nombre,
                                       Titulo,
                                       Contenido,
                                       NombreTitulo,
                                       NumeroFila,
                                       NroDiapositivas,
                                       TotalSegundos
                                FROM pla.V_ListadoEstructuraProgramaSinDatos
                                WHERE IdPGeneral = @IdPGeneral
                                ORDER BY NumeroFila;";
            string queryDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
                return JsonConvert.DeserializeObject<IEnumerable<PreEstructuraCapituloProgramaDTO>>(queryDB);
            else
                return new List<PreEstructuraCapituloProgramaDTO>();
        }
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public IEnumerable<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaDescarga(int idPGeneral)
        {
            string query = @"SELECT Id,
                                       IdConfigurarVideoPrograma,
                                       IdPGeneral IdPgeneral,
                                       Nombre,
                                       Titulo,
                                       Contenido,
                                       NombreTitulo,
                                       IdSeccionTipoDetalle_PW,
                                       NumeroFila,
                                       VideoId,
                                       Archivo,
                                       NroDiapositivas,
                                       ConImagenVideo,
                                       ImagenVideoNombre,
                                       ImagenVideoAncho,
                                       ImagenVideoAlto,
                                       ConImagenDiapositiva,
                                       ImagenDiapositivaNombre,
                                       ImagenDiapositivaAncho,
                                       ImagenDiapositivaAlto,
                                       ImagenVideoPosicionX,
                                       ImagenVideoPosicionY,
                                       ImagenDiapositivaPosicionX,
                                       ImagenDiapositivaPosicionY,
                                       Minuto,
                                       IdTipoVista,
                                       NroDiapositiva,
                                       ConLogoVideo,
                                       ConLogoDiapositiva,
                                       TotalSegundos
                                FROM pla.V_ListadoEstructuraProgramaDescarga
                                WHERE IdPGeneral = @IdPGeneral
                                ORDER BY NumeroFila,
                                         IdSeccionTipoDetalle_PW;";
            string queryDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
                return JsonConvert.DeserializeObject<IEnumerable<PreEstructuraCapituloProgramaDTO>>(queryDB);
            else
                return new List<PreEstructuraCapituloProgramaDTO>(); ;

        }
        /// Autor: Max Mantilla.
        /// Fecha: 2026-01-26
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general para Tutor Virtual
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>PreEstructuraCapituloProgramaTutorVirtualDTO</returns>
        public List<PreEstructuraCapituloProgramaTutorVirtualDTO> ObtenerPreConfigurarVideoProgramaTutorVirtual(int idPGeneral)
        {
            List<PreEstructuraCapituloProgramaTutorVirtualDTO> rpta = new List<PreEstructuraCapituloProgramaTutorVirtualDTO>();
            string query = "ia.SP_TutorVirtualEstructuraProgramaAonline";
            string queryDB = _dapperRepository.QuerySPDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<PreEstructuraCapituloProgramaTutorVirtualDTO>>(queryDB);
                return rpta;
            }
            else
            {
                return null;
            }
        }
        /// Autor: Max Mantilla.
        /// Fecha: 2026-01-26
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de los videos procesador para Tutor Virtual
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>ProcesamientoTranscripcionVideoOnlineDTO</returns>
        public List<ProcesamientoTranscripcionVideoOnlineDTO> ObtenerProcesamientoVideosAonline()
        {
            List<ProcesamientoTranscripcionVideoOnlineDTO> rpta = new List<ProcesamientoTranscripcionVideoOnlineDTO>();
            string query = "ia.SP_TutorVirtualProcesamientoVideoAonline";
            string queryDB = _dapperRepository.QuerySPDapper(query, null);
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ProcesamientoTranscripcionVideoOnlineDTO>>(queryDB);
                return rpta;
            }
            else
            {
                return null;
            }
        }
        /// Autor: Max Mantilla.
        /// Fecha: 2026-01-26
        /// Versión: 1.0
        /// <summary>
        /// Obtiene nombre del curso general del que se procesó el video
        /// </summary>
        /// <param name="Video">VideoInfoDTO</param>
        /// <returns>string</returns>
        public string ObtenerNombreCursoProcesamientoVideosAonline(VideoInfoDTO Video)
        {
            if (Video.Plataforma == 1)
            {
                var VideoIdBrightcove = Video.VideoId;
                string query = @"SELECT TOP 1 PG.Nombre AS Valor 
                        FROM pla.T_ConfigurarVideoPrograma AS CVP
                        INNER JOIN pla.T_PGeneral AS PG ON PG.Id=CVP.IdPGeneral AND PG.Estado=1 AND PG.TutorVirtualActivo=1
                        WHERE CVP.VideoIdBrightcove=@VideoIdBrightcove AND CVP.Estado=1";
                string queryDB = _dapperRepository.FirstOrDefault(query, new { VideoIdBrightcove });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
                {
                    var respuesta = JsonConvert.DeserializeObject<StringDTO>(queryDB);
                    return respuesta.Valor;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                var VideoIdVimeo = Video.VideoId;
                string query = @"SELECT TOP 1 PG.Nombre AS Valor 
                        FROM pla.T_ConfigurarVideoPrograma AS CVP
                        INNER JOIN pla.T_PGeneral AS PG ON PG.Id=CVP.IdPGeneral AND PG.Estado=1 AND PG.TutorVirtualActivo=1
                        WHERE CVP.VideoIdVimeo=@VideoIdVimeo AND CVP.Estado=1";
                string queryDB = _dapperRepository.FirstOrDefault(query, new { VideoIdVimeo });
                if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
                {
                    var respuesta = JsonConvert.DeserializeObject<StringDTO>(queryDB);
                    return respuesta.Valor;
                }
                else
                {
                    return "";
                }
            }
           
        }
    }
}
