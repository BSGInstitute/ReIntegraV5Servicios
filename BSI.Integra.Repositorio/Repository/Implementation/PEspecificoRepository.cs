using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PEspecificoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_PEspecifico
    /// </summary>
    public class PEspecificoRepository : GenericRepository<TPespecifico>, IPEspecificoRepository
    {
        private Mapper _mapper;
        private const string _camposBase = @"
            Id,
	        Nombre,
	        Codigo,
	        IdCentroCosto,
	        Frecuencia,
	        EstadoP,
	        Tipo,
	        TipoAmbiente,
	        Categoria,
	        IdProgramaGeneral,
	        Ciudad,
	        FechaInicio,
	        FechaTermino,
	        FechaInicioV,
	        FechaTerminoV,
	        CodigoBanco,
	        FechaInicioP,
	        FechaTerminoP,
	        FrecuenciaId,
	        EstadoPId AS EstadoPid,
	        TipoId,
	        CategoriaId,
	        OrigenPrograma,
	        IdCiudad,
	        CoordinadoraAcademica,
	        CoordinadoraCobranza,
	        Duracion,
	        ActualizacionAutomatica,
	        IdCursoMoodle,
	        CursoIndividual,
	        IdSesion_Inicio AS IdSesionInicio,
	        IdExpositor_Referencia AS IdExpositorReferencia,
	        IdAmbiente,
	        UrlDocumentoCronograma,
	        IdEstadoPEspecifico AS IdEstadoPespecifico,
	        Estado,
	        UsuarioCreacion,
	        UsuarioModificacion,
	        FechaCreacion,
	        FechaModificacion,
	        RowVersion,
	        IdMigracion,
	        UrlDocumentoCronogramaGrupos,
	        IdTroncalPartner,
	        IdCursoMoodlePrueba,
	        IdCursoRA AS IdCursoRa,
	        IdProveedor,
	        IdProveedorCalificaProyecto,
	        ObservacionCursoFinalizado,
	        EsEspecial,
            CreditosTeoricos,
            CreditosPracticos,
            CreditosTotales,
            HorasTeoricas,
            HorasPracticas,
            HorasTotales,
            IdPeriodoLectivo,
            UrlCronogramaSemanal,
            IdTipoProgramaCarrera,
            IdCiclo";
        public PEspecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecifico, PEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConfigurarWebinar, ConfigurarWebinar>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCursoPespecifico, CursoPespecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSolicitudAlumno, SolicitudAlumno>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPespecifico MapeoEntidad(PEspecifico entidad)
        {
            try
            {
                var modelo = _mapper.Map<TPespecifico>(entidad);
                if (entidad.ConfigurarWebinars != null && entidad.ConfigurarWebinars.Count() > 0)
                {
                    modelo.TConfigurarWebinars = _mapper.Map<ICollection<TConfigurarWebinar>>(entidad.ConfigurarWebinars);
                }
                if (entidad.CursoPespecificos != null && entidad.CursoPespecificos.Count() > 0)
                {
                    modelo.TCursoPespecificos = _mapper.Map<ICollection<TCursoPespecifico>>(entidad.CursoPespecificos);
                }
                if (entidad.FeedbackGrupoPreguntaProgramaEspecificos != null && entidad.FeedbackGrupoPreguntaProgramaEspecificos.Count() > 0)
                {
                    modelo.TFeedbackGrupoPreguntaProgramaEspecificos = _mapper.Map<ICollection<TFeedbackGrupoPreguntaProgramaEspecifico>>(entidad.FeedbackGrupoPreguntaProgramaEspecificos);
                }
                if (entidad.SolicitudAlumnos != null && entidad.SolicitudAlumnos.Count() > 0)
                {
                    modelo.TSolicitudAlumnos = _mapper.Map<ICollection<TSolicitudAlumno>>(entidad.SolicitudAlumnos);
                }
                if (entidad.SolicitudOperacionesAccesoTemporalDetalles != null && entidad.SolicitudOperacionesAccesoTemporalDetalles.Count() > 0)
                {
                    modelo.TSolicitudOperacionesAccesoTemporalDetalles = _mapper.Map<ICollection<TSolicitudOperacionesAccesoTemporalDetalle>>(entidad.SolicitudOperacionesAccesoTemporalDetalles);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en MapeoEntidad(PEspecifico)", ex);
            }
        }
        public TPespecifico Add(PEspecifico entidad)
        {
            try
            {
                var PEspecifico = MapeoEntidad(entidad);
                Insert(PEspecifico);
                return PEspecifico;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Add(PEspecifico)", ex);
            }
        }
        public TPespecifico Update(PEspecifico entidad)
        {
            try
            {
                var PEspecifico = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PEspecifico.RowVersion = entidadExistente.RowVersion;
                Update(PEspecifico);
                return PEspecifico;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Update(PEspecifico)", ex);
            }
        }
        public bool Delete(int id, string usuario)
        {
            try
            {
                return base.Delete(id, usuario); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Delete(int, string)", ex);
            }
        }
        public IEnumerable<TPespecifico> Add(IEnumerable<PEspecifico> listadoEntidad)
        {
            try
            {
                List<TPespecifico> listado = new();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Add(IEnumerable<PEspecifico>)", ex);
            }
        }
        public IEnumerable<TPespecifico> Update(IEnumerable<PEspecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecifico> listado = new();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Update(IEnumerable<PEspecifico>)", ex);
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
                throw new Exception("Error en Delete(IEnumerable<int>, string)", ex);
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PEspecifico.
        /// </summary>
        /// <returns> List<PEspecifico> </returns>
        public IEnumerable<PEspecifico> Obtener()
        {
            try
            {
                List<PEspecifico> rpta = new List<PEspecifico>();
                string query = $"SELECT {_camposBase} FROM pla.T_PEspecifico WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecifico>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Obtener()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PEspecifico.
        /// </summary>
        /// <returns> List<PEspecifico> </returns>
        public PEspecifico? ObtenerPorId(int id)
        {
            try
            {
                PEspecifico rpta = new();
                string query = $"SELECT {_camposBase} FROM pla.T_PEspecifico WHERE Estado = 1 AND Id=@id";
                string resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PEspecifico>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-OPI-001@Error en ObtenerPorId(): {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PEspecifico.
        /// </summary>
        /// <returns> IEnumerable<PEspecifico> </returns>
        public List<PEspecifico> ObtenerPorIds(IEnumerable<int> ids)
        {
            try
            {
                List<PEspecifico> rpta = new();
                string query = $"SELECT {_camposBase} FROM pla.T_PEspecifico WHERE Estado = 1 AND Id IN @ids";
                string resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecifico>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIds: {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion de todos los ProgramaEspecifico: Id, Nombre y Programa General
        /// </summary>
        /// <returns> List<PEspecificoPGeneralFiltroDTO> </returns>
        public List<PEspecificoPGeneralFiltroDTO> ObtenerCombo()
        {
            try
            {
                List<PEspecificoPGeneralFiltroDTO> rpta = new();
                string query = "SELECT Id, name as Nombre, pg as IdProgramaGeneral FROM pla.V_ListaProgramaEspecifico";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerFiltro()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/04/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion de todos los ProgramaEspecifico: Id, Nombre y Programa General
        /// </summary>
        /// <returns> List<PEspecificoPGeneralFiltroDTO> </returns>
        public List<PEspecificoPGeneralFiltroDTO> ObtenerListaProgramaEspecificoParaTabla()
        {
            try
            {
                var rpta = new List<PEspecificoPGeneralFiltroDTO>();
                var query = "SELECT Id, Nombre, IdProgramaGeneral FROM pla.V_ListaProgramaEspecificoParaTabla WHERE Estado=1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerListaProgramaEspecificoParaTabla()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.00
        /// <summary>
        /// Obtiene registros de T_PEspecifico para mostrarse en combo.
        /// </summary>
        /// <returns> IEnumerable<PEspecificoPGeneralFiltroDTO> </returns>
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltroPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PEspecificoPGeneralFiltroDTO> rpta = new();
                string query = @"SELECT Id, Nombre, IdProgramaGeneral FROM pla.V_PEspecificoFiltro WHERE IdProgramaGeneral=@idPGeneral";
                string resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerFiltroPorIdPGeneral()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PEspecifico para mostrarse en combo.
        /// </summary>
        /// <returns> IEnumerable<PEspecificoPGeneralFiltroDTO> </returns>
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltro()
        {
            try
            {
                List<PEspecificoPGeneralFiltroDTO> rpta = new();
                string query = @"SELECT Id, Nombre, IdProgramaGeneral FROM pla.V_PEspecificoFiltro";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerFiltro()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PEspecifico para mostrarse en combo.
        /// </summary>
        /// <returns> IEnumerable<PEspecificoPGeneralFiltroDTO> </returns>
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerComboSinValidacion()
        {
            try
            {
                List<PEspecificoPGeneralFiltroDTO> rpta = new();
                string query = @"SELECT Id, Nombre, IdProgramaGeneral FROM pla.T_PEspecifico WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerFiltro()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion detallada de PEspecifico asociada a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> PEspecificoPorIdCentroCostoDTO </returns>
        public PEspecificoPorIdCentroCostoDTO? ObtenerPorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                string query = @"
                        SELECT Id,
                                Nombre,
                                EstadoP,
                                Tipo,
                                TipoAmbiente,
                                Categoria,
                                IdProgramaGeneral,
                                Ciudad,
                                EstadoPId,
                                TipoId,
                                IdCiudad,
                                Duracion,
                                CursoIndividual,
                                IdSesion_Inicio AS IdSesionInicio,
                                IdExpositor_Referencia AS IdExpositorReferencia,
                                IdAmbiente,
                                UrlDocumentoCronograma,
                                FechaHoraInicio,UrlDocumentoCronogramaGrupos
                        FROM pla.V_TPEspecifico_ObtenerPEspecificos
                        WHERE Estado = 1 AND IdCentroCosto = @idCentroCosto";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PEspecificoPorIdCentroCostoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPorIdCentroCosto()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 10/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion detallada de PEspecifico asociada a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> ProgramaEspecificoPorCentroCostoDTO </returns>
        public async Task<PEspecificoPorIdCentroCostoDTO> ObtenerPorIdCentroCostoAsync(int idCentroCosto)
        {
            try
            {
                PEspecificoPorIdCentroCostoDTO rpta = new PEspecificoPorIdCentroCostoDTO();
                string query = @"
                        SELECT Id,
                                Nombre,
                                EstadoP,
                                Tipo,
                                TipoAmbiente,
                                Categoria,
                                IdProgramaGeneral,
                                Ciudad,
                                EstadoPId,
                                TipoId,
                                IdCiudad,
                                Duracion,
                                CursoIndividual,
                                IdSesion_Inicio AS IdSesionInicio,
                                IdExpositor_Referencia AS IdExpositorReferencia,
                                IdAmbiente,
                                UrlDocumentoCronograma,
                                FechaHoraInicio,UrlDocumentoCronogramaGrupos
                        FROM pla.V_TPEspecifico_ObtenerPEspecificos
                        WHERE Estado = 1 AND IdCentroCosto = @idCentroCosto";
                string resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PEspecificoPorIdCentroCostoDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPorIdCentroCostoAsync()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion detallada de PEspecifico asociada a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> IntDTO </returns>
        public IntDTO ObtenerIdPGeneralPorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                IntDTO idProgramaGeneral = new();
                string query = @"SELECT TOP 1 IdProgramaGeneral AS Valor
                            FROM pla.T_PEspecifico
                            WHERE Estado = 1 AND IdCentroCosto = @idCentroCosto;";
                string resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    idProgramaGeneral = JsonConvert.DeserializeObject<IntDTO>(resultadoQuery)!;
                }
                return idProgramaGeneral;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerIdPGeneralPorIdCentroCosto()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Valor de las SeccionesDocumento asociados a un Centro de Costo.
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> List<SeccionEtiquetaDTO> </returns>
        public List<SeccionEtiquetaDTO> ObtenerSeccionEtiquetaPorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                List<SeccionEtiquetaDTO> rpta = new();
                string query = @"
                            SELECT
	                            Valor,
	                            IdPlantillaPW,
	                            IdSeccionPW,
	                            IdCentroCosto
                            FROM pla.V_SeccionesPlantillaTemplate
                            WHERE
	                            EstadoPEspecifico = 1
	                            AND EstadoPGeneral = 1
	                            AND EstadoPGeneralDocumento = 1
	                            AND EstadoDocumento = 1
	                            AND EstadoDocumentoSeccion = 1
	                            AND IdCentroCosto = @IdCentroCosto;";
                string resultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SeccionEtiquetaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerSeccionEtiquetaPorIdCentroCosto()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Valor de las SeccionesDocumento asociados a un Centro de Costo.
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> List<SeccionEtiquetaDTO> </returns>
        public async Task<List<SeccionEtiquetaDTO>> ObtenerSeccionEtiquetaPorIdCentroCostoAsync(int idCentroCosto)
        {
            try
            {
                List<SeccionEtiquetaDTO> rpta = new List<SeccionEtiquetaDTO>();
                string query = @"
                            SELECT
	                            Valor,
	                            IdPlantillaPW,
	                            IdSeccionPW,
	                            IdCentroCosto
                            FROM pla.V_SeccionesPlantillaTemplate
                            WHERE
	                            EstadoPEspecifico = 1
	                            AND EstadoPGeneral = 1
	                            AND EstadoPGeneralDocumento = 1
	                            AND EstadoDocumento = 1
	                            AND EstadoDocumentoSeccion = 1
	                            AND IdCentroCosto = @IdCentroCosto;";
                string resultado = await _dapperRepository.QueryDapperAsync(query, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SeccionEtiquetaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerSeccionEtiquetaPorIdCentroCostoAsync()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de fechas de inicio de programas segun el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<PEspecificoPorIdPGeneral> </returns>
        public List<PEspecificoPorIdPGeneral> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PEspecificoPorIdPGeneral> rpta = new();
                string query = @"SELECT 
               	                    Id,
	                                Nombre,
	                                Ciudad,
	                                Tipo,
	                                Duracion,
	                                EstadoPId,
	                                FechaCreacion,
	                                IdCategoria,
                                    CentroCosto
                                FROM pla.V_ListaProgramaEspecificoPorIdPrograma
                                WHERE IdPGeneral = @idPGeneral";
                string resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPorIdPGeneral>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPorIdPGeneral()", ex);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de fechas de inicio de programas segun el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<PEspecificoPorIdPGeneral> </returns>
        public async Task<List<PEspecificoPorIdPGeneral>> ObtenerPorIdPGeneralAsync(int idPGeneral)
        {
            try
            {
                List<PEspecificoPorIdPGeneral> rpta = new();
                string query = @"SELECT 
                            Id,
                            Nombre,
                            Ciudad,
                            Tipo,
                            Duracion,
                            EstadoPId,
                            FechaCreacion,
                            IdCategoria,
                            CentroCosto
                        FROM pla.V_ListaProgramaEspecificoPorIdPrograma
                        WHERE IdPGeneral = @idPGeneral";

                string resultado = await _dapperRepository.QueryDapperAsync(query, new { idPGeneral });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPorIdPGeneral>>(resultado) ?? new List<PEspecificoPorIdPGeneral>();
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPorIdPGeneral()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico.
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <param name="idProgramaEspecifico"></param>
        /// <returns> FechaInicioProgramaEspecificoDTO </returns>
        public FechaInicioProgramaEspecificoDTO FechaProgramaEspecifico(int idProgramaGeneral, int idProgramaEspecifico)
        {
            try
            {
                FechaInicioProgramaEspecificoDTO rpta = new();
                string query = "pla.SP_ObtenerFechaInicioProgramaEspecifico";
                string resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idPrograma = idProgramaGeneral, idEspecifico = idProgramaEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<FechaInicioProgramaEspecificoDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en FechaProgramaEspecifico()", ex);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico.
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <param name="idProgramaEspecifico"></param>
        /// <returns> FechaInicioProgramaEspecificoDTO </returns>
        public async Task<FechaInicioProgramaEspecificoDTO> FechaProgramaEspecificoAsync(int idProgramaGeneral, int idProgramaEspecifico)
        {
            try
            {
                FechaInicioProgramaEspecificoDTO respuesta = new FechaInicioProgramaEspecificoDTO();
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync("pla.SP_ObtenerFechaInicioProgramaEspecifico", new { idPrograma = idProgramaGeneral, idEspecifico = idProgramaEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<FechaInicioProgramaEspecificoDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// verifica si existe o no el pespecifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns> bool </returns>
        public bool ExisteId(int idPEspecifico)
        {
            try
            {
                IntDTO rpta = new();
                string query = "SELECT Id as Valor FROM pla.T_PEspecifico WHERE Estado = 1 AND Id = @idPEspecifico";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return rpta.Valor != 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ExisteId()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna templates con id de migracion para remplazar en las etiquetas de la agenda
        /// </summary>
        /// <param name="idPlantillaPW">Id de migracion de la Plantilla PW</param>
        /// <param name="idSeccionPW">Id de migracion de la Seccion PW</param>
        /// <param name="idCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <returns> SeccionEtiquetaDTO </returns>
        public SeccionEtiquetaDTO? ObtenerContenidoTemplate(Guid idPlantillaPW, Guid idSeccionPW, int idCentroCosto)
        {
            try
            {
                string query = @"SELECT
                                    Valor,
                                    IdPlantillaPW,
                                    IdSeccionPW,
                                    IdCentroCosto 
                                FROM pla.V_SeccionesPlantillaTemplate
                                WHERE EstadoPEspecifico = 1 
                                    AND EstadoPGeneral = 1 
                                    AND EstadoPGeneralDocumento = 1 
                                    AND EstadoDocumento = 1
                                    AND EstadoDocumentoSeccion = 1 
                                    AND IdCentroCosto=@IdCentroCosto 
                                    AND IdPlantillaPW=@IdPlantillaPW 
                                    AND IdSeccionPW=@IdSeccionPW";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCentroCosto = idCentroCosto, IdPlantillaPW = idPlantillaPW, IdSeccionPW = idSeccionPW });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<SeccionEtiquetaDTO>(resultado)!;
                }
                return null;
            
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerContenidoTemplate()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el PEspecifico asociado a la Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> PEspecificoInformacionDTO </returns>
        public PEspecificoInformacionDTO ObtenerPespecificoPorOportunidad(int idOportunidad)
        {
            try
            {
                PEspecificoInformacionDTO rpta = new();
                var query = @"
                        SELECT
	                        Id, 
                            Nombre,
                            Codigo,
                            IdCentroCosto,
                            Tipo,
                            Categoria,
                            CodigoBanco,
                            Ciudad,
                            IdProgramaGeneral,
                            EstadoP
                        FROM mkt.V_PespecificoOportunidad
                        WHERE IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PEspecificoInformacionDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPespecificoPorOportunidad()", ex);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el TipoId de Programa Especifico mediante IdCentroCosto
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns>PEspecificoIdTipoDTO</returns>
        public PEspecificoIdTipoDTO ObtenerTipoIdPorIdCentroCosto(int idCentroCosto)
        {
            try
            {
                PEspecificoIdTipoDTO rpta = new();
                string query = "SELECT TipoId FROM pla.V_TPEspecifico_IdTipo WHERE IdCentroCosto = @IdCentroCosto AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PEspecificoIdTipoDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerTipoIdPorIdCentroCosto()", ex);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdCiudad por el programa específico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns>IntDTO</returns>
        public IntDTO ObtenerIdCiudad(int idPEspecifico)
        {
            try
            {
                IntDTO rpta = new();
                string query = "SELECT IdCiudad AS Valor FROM pla.V_ObtenerCiudadPorPEspecifico WHERE IdPEspecifico = @idPEspecifico AND EstadoPEspecifico=1 AND EstadoRegionCiudad=1";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerIdCiudad()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Etiqueta de la mensajeria de agenda por id sección, id plantilla, id centro costo
        /// </summary>
        /// <param name="idSeccion"></param>
        /// <param name="idPlantilla"></param>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        /// <exception"></exception>
        public StringDTO ObtenerSeccionEtiquetaAgendaMensaje(string idSeccion, string idPlantilla, int idCentroCosto)
        {
            try
            {
                StringDTO rpta = new();
                string query = @"SELECT Valor FROM pla.V_SeccionesPlantillaTemplate 
                            WHERE EstadoPEspecifico = 1 
                                AND EstadoPGeneral = 1 
                                AND EstadoPGeneralDocumento = 1 
                                AND EstadoDocumento = 1 
                                AND EstadoDocumentoSeccion = 1 
                                AND IdCentroCosto=@IdCentroCosto 
                                AND IdPlantillaPW=@IdPlantilla 
                                AND IdSeccionPW=@IdSeccion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCentroCosto = idCentroCosto, IdPlantilla = idPlantilla, IdSeccion = idSeccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerSeccionEtiquetaAgendaMensaje()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el periodo duracion del programa especifico
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico del que se desea obtener la duracion (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena del periodo y duracion</returns>
        public PeriodoDuracionProgramaEspecificoDTO ObtenerPeriodoDuracion(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                PeriodoDuracionProgramaEspecificoDTO rpta = new();
                string resultado = _dapperRepository.QuerySPFirstOrDefault("ope.SP_ObtenerDuracionProgramaEspecifico", new
                {
                    IdPEspecifico = idPEspecifico,
                    IdMatriculaCabecera = idMatriculaCabecera
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PeriodoDuracionProgramaEspecificoDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerDuracionProgramaEspecifico()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url de acceso a la sesion online
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico del cual se desae obtener la URL de acceso a la sesion (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena con la URL de la sesion online</returns>
        public string ObtenerUrlAccesoSesionOnline(int idPEspecifico)
        {
            try
            {
                StringDTO rpta = new();
                string query = "ope.SP_ObtenerUrlAccesoSesionOnline";
                string resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerUrlAccesoSesionOnline()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico del cual se desea averiguar las sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena formateada con el conjunto de sesiones del curso mencionado</returns>
        public List<ConjuntoSesionProgramaEspecificoDTO> ObtenerConjuntoSesionProgramaEspecifico(int idPEspecifico)
        {
            try
            {
                List<ConjuntoSesionProgramaEspecificoDTO> rpta = new();
                string query = "ope.SP_ObtenerConjuntoSesionProgramaEspecifico";
                string resultado = _dapperRepository.QuerySPDapper(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConjuntoSesionProgramaEspecificoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerConjuntoSesionProgramaEspecifico()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico que tienen una sesion en base a la fecha actual + cantidad de dias dentro de la semana actual
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico que se desea saber su proximo conjunto de sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el momento de la consulta</param>
        /// <returns>Cadena formateada con el proximo conjunto de sesion</returns>
        public List<ConjuntoSesionProgramaEspecificoDTO> ObtenerProximoConjuntoSesionProgramaEspecifico(int idPEspecifico, int cantidadDias)
        {
            try
            {
                List<ConjuntoSesionProgramaEspecificoDTO> rpta = new();
                string query = "ope.SP_ObtenerProximoConjuntoSesionProgramaEspecifico";
                string resultado = _dapperRepository.QuerySPDapper(query, new { IdPEspecifico = idPEspecifico, CantidadDias = cantidadDias });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConjuntoSesionProgramaEspecificoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerProximoConjuntoSesionProgramaEspecifico()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico que tienen una sesion en base a la fecha actual + cantidad de dias dentro de la semana actual
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico que se desea saber su proximo conjunto de sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el momento de la consulta</param>\
        /// <returns>Cadena formateada con el proximo conjunto de sesion Webex</returns>
        public List<ConjuntoSesionProgramaEspecificoDTO> ObtenerProximoConjuntoSesionProgramaEspecificoWebex(int idPEspecifico, int cantidadDias)
        {
            try
            {
                List<ConjuntoSesionProgramaEspecificoDTO> rpta = new();
                string query = "ope.SP_ObtenerProximoConjuntoSesionProgramaEspecificoWebex";
                string resultado = _dapperRepository.QuerySPDapper(query, new { IdPEspecifico = idPEspecifico, CantidadDias = cantidadDias });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConjuntoSesionProgramaEspecificoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerProximoConjuntoSesionProgramaEspecificoWebex()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de un programa especifico por matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns>Objetoo de tipo PEspecificoValorDTO</returns>
        public PEspecificoValorDTO ObtenerDetalle(int idMatriculaCabecera)
        {
            try
            {
                PEspecificoValorDTO rpta = new();
                string query = "ope.SP_ObtenerDetallePEspecifico";
                string resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PEspecificoValorDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerDetalle()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias segun la fecha actual</param>
        /// <param name="cantidadHoras">Cantidad de horas segun la fecha actual</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerUrlQuejaSugerenciaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras)
        {
            try
            {
                // TODO (To Do: Por hacer)
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerUrlQuejaSugerenciaNDiasNHora()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias segun la fecha actual</param>
        /// <param name="cantidadHoras">Cantidad de horas segun la fecha actual</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerNombreCursoEncuestaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras)
        {
            try
            {
                // TODO (To Do: Por hacer)
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerNombreCursoEncuestaNDiasNHora()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <param name="cantidadDias">Cantidad de dias segun la fecha actual</param>
        /// <param name="cantidadHoras">Cantidad de horas segun la fecha actual</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerUrlEncuestaNDiasNHora(int idMatriculaCabecera, int cantidadDias, int cantidadHoras)
        {
            try
            {
                // TODO (To Do: Por hacer)
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerUrlEncuestaNDiasNHora()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la url de queja sugerencia por n dias n horas (Actualmente se ha visto que la funcion esta vacia)
        /// </summary>
        /// <param name="id">Id aun no determinado</param>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena vacia</returns>
        public string ObtenerFechaEmisionUltimoCertificado(int id, int idMatriculaCabecera)
        {
            try
            {
                // TODO (To Do: Por hacer)
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerFechaEmisionUltimoCertificado()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el PEspecífico de una nueva aula virtual
        /// </summary>
        /// <returns>List<PEspecificoPGeneralFiltroDTO></returns>
        public List<PEspecificoPGeneralFiltroDTO> ObtenerPEspecificoNuevaAulaVirtual()
        {
            try
            {
                List<PEspecificoPGeneralFiltroDTO> rpta = new();
                string query = "SELECT Id, Nombre, IdProgramaGeneral FROM pla.V_TPEspecificoNuevoAulaVirtual_DataBasica";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPEspecificoNuevaAulaVirtual()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene cursos relacionados de irca por programa especifico
        /// </summary>
        /// <returns>List<PEspecificoNuevoAulaVirtualDTO></returns>
        public List<PEspecificoNuevoAulaVirtualDTO> ObtenerPEspecificoNuevoAulaVirtualTipo()
        {
            try
            {
                List<PEspecificoNuevoAulaVirtualDTO> rpta = new();
                var query = @"SELECT 
                                IdPEspecifico, NombrePEspecifico, IdCentroCosto, EstadoP, Modalidad, IdPGeneral, Ciudad, IdCursoMoodle, IdCursoMoodlePrueba, 
                                TipoPEspecifico, IdPEspecificoHijo, NombrePEspecificoHijo
                            FROM 
                                pla.V_PEspecificoNuevoAulaVirtualTipoPadreHijoIndividual";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoNuevoAulaVirtualDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPEspecificoNuevoAulaVirtualTipo()", ex);
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 17/01/2023
        /// <summary>
        /// Permite obtener los Id del programa especifico, nombre completo del centro de costo enviandole como parametro 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PEspecificoComboDTO> ObtenerPEspecificoPorCentroCosto(string nombre)
        {
            try
            {
                List<PEspecificoComboDTO> rpta = new();
                var query = "SELECT IdPEspecifico, Nombre FROM pla.T_ObtenerIdPEspecificoPorCentroCosto WHERE Nombre LIKE @Nombre AND EstadoPEspecificio=1 AND EstadoCentroCosto=1";
                var resultado = _dapperRepository.QueryDapper(query, new { Nombre = $"%{nombre}%" });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPEspecificoPorCentroCosto()", ex);
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 23/01/2023
        /// <summary>
        /// Filtra programas especificos por el nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<ComboDTO> ObtenerPorNombreAutocomplete(string valor)
        {
            try
            {
                List<ComboDTO> rpta = new();
                string query = "SELECT Id, Nombre FROM pla.T_PEspecifico WHERE Nombre LIKE @Nombre AND Estado=1";
                string resultado = _dapperRepository.QueryDapper(query, new { Nombre = $"%{valor}%" });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPorNombreAutocomplete()", ex);
            }
        }
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cursos relacionados por idPgeneral
        /// </summary>
        /// <returns>List<PEspecificoComboDTO></returns>
        public List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoPorIdPGeneral(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoComboDTO> rpta = new();
                string _query = "SELECT IdPEspecifico, Nombre FROM ope.V_ObtenerPEspecifico_Relacionado WHERE Id=@idPEspecifico AND IdPEspecifico NOT IN (SELECT IdPEspecifico FROM OPE.T_PEspecificoMatriculaAlumno WHERE IdMatriculaCabecera = @idMatriculaCabecera )";
                string resultado = _dapperRepository.QueryDapper(_query, new { idPEspecifico, idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPEspecificoRelacionadoPorIdPGeneral()", ex);
            }
        }
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene sesiones relacionados por Pgeneral
        /// </summary>
        /// <returns>List<PEspecificoComboDTO></returns>
        public List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoPGeneral(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoComboDTO> rpta = new();
                string query = @"SELECT IdPEspecifico, Nombre FROM ope.V_ObtenerPEspecifico_Relacionado_PGeneral 
                                WHERE Id=@idPEspecifico 
                                AND IdPEspecifico NOT IN (SELECT IdPEspecifico FROM OPE.T_PEspecificoMatriculaAlumno WHERE IdMatriculaCabecera=@idMatriculaCabecera)";
                string resultado = _dapperRepository.QueryDapper(query, new { idPEspecifico, idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPEspecificoRelacionadoPGeneral()", ex);
            }
        }
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 02/09/2023
        /// Version: 1.0
        /// <summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="esCursoDSig"></param>
        /// </summary>
        /// <returns>List<PEspecificoComboDTO></returns>
        public List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoIrca(int idPEspecifico, int idMatriculaCabecera, bool esCursoDSig)
        {
            try
            {
                List<PEspecificoComboDTO> rpta = new();
                string query = "ope.SP_ObtenerPEspecifico_RelacionadoIrca";
                string resultado = _dapperRepository.QuerySPDapper(query, new { IdPespecifico = idPEspecifico, IdMatriculaCabecera = idMatriculaCabecera, EsProgramaDSIG = esCursoDSig });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerPEspecificoRelacionadoIrca()", ex);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/01/2023
        /// <summary>
        /// Obtiene el nombre del PEspecifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public string ObtenerNombrePEspecifico(int idPEspecifico)
        {
            try
            {
                StringDTO rpta = new();
                string query = "SELECT Valor From pla.V_PEspecificoNombre WHERE Id = @idPEspecifico";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerNombrePEspecifico()", ex);
            }
        }
        /// Autor: Griselberto huaman
        /// Fecha: 23/01/2023
        /// <summary>
        /// Obtiene cursos de centro costo por Programa especifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public List<CursosCentroCostoDTO> ObtenerCursosCentroCosto(int idPEspecifico = 0)
        {
            try
            {
                List<CursosCentroCostoDTO> rpta = new();
                string condicion = "";
                if (idPEspecifico > 0)
                    condicion += " AND IdPEspecifico = @idPEspecifico ";
                string query = $@"
                        SELECT 
                            Id, 
                            IdPEspecifico, 
                            NombreCursoEspecifico, 
                            Duracion, 
                            Orden, 
                            FechaCreacion, 
                            UsuarioCreacion, 
                            NombrePEspecifico, 
                            FechaModificacion, 
                            UsuarioModificacion  
                        FROM pla.V_CursosCentroCosto_ProgramaEspecifico 
                        WHERE 
                            EstadoCursoEspecifico = 1 AND
                            EstadoProgramaEspecifico = 1 
                            {condicion}
                        ORDER BY Orden";

                string resultado = _dapperRepository.QueryDapper(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CursosCentroCostoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerCursosCentroCosto()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/01/2023
        /// <summary>
        /// Obtiene lista de programas especificos padre mediante filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IEnumerable<ProgramaEspecificoPadreIndividualDTO> ObtenerProgramaEspecificoPadreIndividualFiltro(PEspecificoFiltroSPDTO filtro)
        {
            try
            {
                string query = "pla.SP_ProgramaEspecificoPadreIndividualFiltro";
                string resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    filtro.IdProgramaEspecifico,
                    filtro.IdCentroCosto,
                    filtro.CodigoBs,
                    filtro.IdEstadoPEspecifico,
                    filtro.IdModalidadCurso,
                    filtro.IdPGeneral,
                    filtro.IdArea,
                    filtro.IdSubArea,
                    filtro.IdCentroCostoD
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaEspecificoPadreIndividualDTO>>(resultado)!;
                }
                return new List<ProgramaEspecificoPadreIndividualDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-OPEPIF-001@Error en ObtenerProgramaEspecificoPadreIndividualFiltro: {ex.Message}", ex);
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 29/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Combo de Programas Especificos Adicionales (CUROS IRCA) 
        /// </summary>
        /// <returns> Lista DTO - List<PEspecificoComboDTO> - rpta </returns>
        public IEnumerable<ComboDTO> ObtenerProgramasEspecificosAdicional()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                string query = @"
                                SELECT 
                                    Id, Nombre 
                                FROM 
                                    [pla].[V_TPespecificosAdicionalesReducido]";
                var resultado = _dapperRepository.QueryDapper(query, "");
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerProgramasEspecificosAdicional()", ex);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Combo completo de Programa Especifico
        /// </summary>
        /// <returns> Lista DTO - List<PEspecificoComboDTO> - rpta </returns>
        public IEnumerable<ComboDTO> ObtenerProgramaEspecifico()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                string query = @"
                                SELECT 
                                    Id, Nombre 
                                FROM 
                                    [pla].[V_PEspecificoInformacion]";
                var resultado = _dapperRepository.QueryDapper(query, "");
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerProgramaEspecifico()", ex);
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y NombreCompleto de Pespecifico filtrado por el idPegeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista DTO - ComboDTO - rpta </returns>
        public IEnumerable<ComboDTO> ObtenerProgramaEspecificoPorIdPGeneral(List<int> idPGeneral)
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                            SELECT 
                                Id, Nombre 
                            from 
                                [pla].[V_PEspecificoInformacion] 
                            WHERE 
                                IdPGeneral IN @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerProgramaEspecificoAutocomplete()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PEspecifico para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltroPorTipo(bool aplicaTipo)
        {
            try
            {
                string codicionTipo = aplicaTipo ? "AND Tipo = 1" : string.Empty;
                string query = $@"
                    SELECT DISTINCT
	                    IdPEspecifico AS Id, 
                        PEspecifico AS Nombre, 
                        IdPGeneral AS IdProgramaGeneral
                    FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                    WHERE
	                    Estado = 1
	                    AND RowNumber = 1 {codicionTipo}";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                return new List<PEspecificoPGeneralFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroPorTipo(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PEspecifico para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public async Task<IEnumerable<PEspecificoPGeneralFiltroDTO>> ObtenerFiltroPorTipoAsync(bool aplicaTipo)
        {
            try
            {
                string codicionTipo = aplicaTipo ? "AND Tipo = 1" : string.Empty;
                string query = $@"
                    SELECT DISTINCT
	                    IdPEspecifico AS Id, 
                        PEspecifico AS Nombre, 
                        IdPGeneral AS IdProgramaGeneral
                    FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                    WHERE
	                    Estado = 1
	                    AND RowNumber = 1 {codicionTipo}";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                return new List<PEspecificoPGeneralFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroPorTipoAsync(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PEspecifico para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerPEspecificoHijoFiltro()
        {
            try
            {
                string query = "SELECT Id, Nombre, IdProgramageneral AS IdProgramaGeneral FROM pla.V_ObtenerPEspecificoHijoFiltro";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return new List<PEspecificoPGeneralFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPEspecificoHijoFiltro(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PEspecifico para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public async Task<IEnumerable<PEspecificoPGeneralFiltroDTO>> ObtenerPEspecificoHijoFiltroAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre, IdProgramageneral AS IdProgramaGeneral FROM pla.V_ObtenerPEspecificoHijoFiltro";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoPGeneralFiltroDTO>>(resultado)!;
                }
                return new List<PEspecificoPGeneralFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPEspecificoHijoFiltroAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PEspecifico para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public IEnumerable<PEspecificoRelacionadoFiltroDTO> ObtenerListaPEspecificosRelacionados()
        {
            try
            {
                var query = "SELECT Id, Nombre, Modalidad, Codigo FROM pla.V_TListaPEspecificos_Relacionados WHERE Estado=1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoRelacionadoFiltroDTO>>(resultado)!;
                }
                return new List<PEspecificoRelacionadoFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerListaPEspecificosRelacionados(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene PEspecifico para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public async Task<IEnumerable<PEspecificoRelacionadoFiltroDTO>> ObtenerListaPEspecificosRelacionadosAsync()
        {
            try
            {
                var query = "SELECT Id, Nombre, Modalidad, Codigo FROM pla.V_TListaPEspecificos_Relacionados WHERE Estado=1 ORDER BY Id DESC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoRelacionadoFiltroDTO>>(resultado)!;
                }
                return new List<PEspecificoRelacionadoFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerListaPEspecificosRelacionadosAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Configuracion Webinar por programas especificos
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public DatosConfiguracionProgramasWebexDTO? ObtenerConfiguracionWebinarPEspecifico(int idPEspecifico)
        {
            try
            {
                var query = @"
                    SELECT
	                    IdPEspecifico,
	                    IdTiempoFrecuencia,
	                    Valor,
	                    IdTiempoFrecuenciaCorreo,
	                    ValorFrecuenciaCorreo,
	                    IdPlantillaFrecuenciaCorreo,
	                    IdTiempoFrecuenciaWhatsapp,
	                    ValorFrecuenciaWhatsapp,
	                    IdPlantillaFrecuenciaWhatsapp,
	                    IdTiempoFrecuenciaCorreoConfirmacion,
	                    ValorFrecuenciaCorreoConfirmacion,
	                    IdPlantillaCorreoConfirmacion,
	                    IdTiempoFrecuenciaCorreoDocente,
	                    ValorFrecuenciaDocente,
	                    IdPlantillaDocente,
	                    FechaInicio,
	                    FechaFin,
	                    IdFrecuencia
                    FROM pla.V_ObtenerConfiguracionWebinarPEspecifico
                    WHERE
	                    IdPEspecifico = @IdPEspecifico
	                    AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DatosConfiguracionProgramasWebexDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerConfiguracionWebinarPEspecifico(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de programa especifico por codigo
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns>Objeto</returns>
        public DatosProgramaEspecificoDTO? ObtenerProgramaEspecificoPorCodigo(int idPEspecifico)
        {
            try
            {
                var query = "SELECT Id, Nombre, Duracion, IdCiudad, Tipo, IdSesion_Inicio FROM pla.V_ProgramaEspecificoPorCodigo WHERE Estado=1 AND Id=@IdPespecifico;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DatosProgramaEspecificoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDatosProgramaEspecificoCodigo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para la generacion de PDF
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns>Registro PEspecifico</returns>
        public RegistroProgramaEspecificoDTO? ObtenerRegistroPespecificoPorId(int idPespecifico)
        {
            try
            {
                string query = "SELECT Id, Nombre, Codigo, IdCentroCosto, EstadoP, Tipo, IdProgramaGeneral, Ciudad, CursoIndividual FROM pla.V_ObtenerRegistroPespecifico WHERE Estado=1 AND Id=@IdPespecifico;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<RegistroProgramaEspecificoDTO>(resultado)!;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerRegistroPespecificoPorId(): {ex.Message}", ex);
            }
        }
        /// Autor: Giancarlo Romero
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id,Nombre de los programas especificos por el valor
        /// </summary> 
        /// <returns> Lista DTO - List<ComboDTO> -rpta </returns>
        public List<ComboDTO> ObtenerPEspecificoWebinar()
        {
            try
            {
                var query = "SELECT Id,Nombre FROM pla.V_TPEspecifico_Webinar where Estado=1 ORDER BY Id DESC";
                var PEspecifico = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ComboDTO>>(PEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos pgeneral para pespecifico
        /// </summary>
        /// <param name="idProgramaGeneral">Id Programa General</param>
        /// <returns>Registro Datos Pgeneral</returns>
        public DatosPGeneralDTO ObtenerDatosPGeneralParaPEspecifico(int idProgramaGeneral)
        {
            try
            {
                string query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Codigo,
	                    IdArea,
	                    IdSubArea,
	                    IdCategoria
                    FROM pla.V_TPGeneral_ObtenerDatosParaPespecifico
                    WHERE
	                    Estado = 1
	                    AND Id = @idProgramaGeneral";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<DatosPGeneralDTO>(resultado)!;
                return new DatosPGeneralDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDatosPGeneralParaPEspecifico(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las categorias de las ciudades mediante IdCiudad y IdCategoriaPrograma
        /// </summary>
        /// <param name="idCiudad"></param>
        /// <param name="idCategoriaPrograma"></param>
        /// <returns></returns>
        public CategoriaCiudadDTO ObtenerCiudadCategoria(int idCiudad, int idCategoriaPrograma)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdCategoriaPrograma,
	                    IdCiudad,
	                    TroncalCompleto,
	                    IdRegionCiudad
                    FROM pla.V_TCategoriaCiudad_ObtenerCategorias
                    WHERE
	                    Estado = 1
	                    AND IdCiudad = @idCiudad
	                    AND IdCategoriaPrograma = @idCategoriaPrograma;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCiudad, idCategoriaPrograma });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<CategoriaCiudadDTO>(resultado)!;
                return new CategoriaCiudadDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCiudadCategoria(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// <summary>
        /// Obtiene numero de grupos en las sesiones
        /// </summary>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerGruposSesiones(int idPadre)
        {
            try
            {
                var query = "pla.SP_ObtenerGruposSesiones";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdPespecifico = idPadre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerGruposSesiones(): {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Obtiene los grupos de edicion disponibles por programa especifico
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        public IEnumerable<ComboDTO> ObtenerGrupoEdicionDisponible(int idPEspecifico)
        {
            try
            {
                var query = $@"
                    SELECT Id, 
                            Nombre
                    FROM ope.V_ObtenerGrupoEdicionDisponiblePorPEspecifico
                    WHERE IdPEspecifico = @idPEspecifico
                        OR IdPEspecifico IS NULL
                    ORDER BY Id ASC;";
                var resultado = _dapperRepository.QueryDapper(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene numero de grupos en las sesiones individuales
        /// </summary>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerGruposSesionesIndividuales(int idPadre)
        {
            try
            {
                var query = "SELECT DISTINCT Id, Nombre FROM pla.V_TPespecificoSesion_ObtenerNumeroGrupo WHERE Estado = 1 AND IdPEspecifico = @idPadre";
                var resultado = _dapperRepository.QueryDapper(query, new { idPadre });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-OGSI-001@Error en ObtenerCombo() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtine Cronograma PEspecifico Grupo Sesion Individual
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="numeroGrupo"></param>
        /// <returns> Lista DTO - IEnumerable<CronogramaGrupoDTO> </returns>
        public IEnumerable<CronogramaGrupoDTO> ObtenerCronogramaPEspecificoGrupoSesionIndividual(int idPEspecifico, int numeroGrupo)
        {
            try
            {
                var query = @"SELECT DISTINCT 
                                Id, FechaHoraInicio, Duracion, DuracionTotal, Curso, IdExpositor, IdProveedor, IdAmbiente, IdCiudad, PEspecificoHijoId, 
                                Tipo, IdModalidadCurso, Comentario, EsSesionInicio, IdCentroCosto, Grupo, GrupoSesion, TieneFur, MostrarPortalWeb 
                              FROM 
                                [pla].[V_ObtenerCronogramaGrupoDuplicadoSesionIndividual] 
                              WHERE 
                                Estado = 1 AND PEspecificoHijoId = @idPEspecifico AND Grupo = @numeroGrupo ORDER BY FechaHoraInicio ASC, PEspecificoHijoId ASC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPEspecifico, numeroGrupo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CronogramaGrupoDTO>>(resultado)!;
                }
                return new List<CronogramaGrupoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-OCPEGSI-001@Error en ObtenerCombo() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene Lista de sesiones mediante programa especifico y numero de grupo
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <param name="numeroGrupo"></param>
        /// <returns></returns>
        public IEnumerable<CronogramaGrupoDTO> ObtenerCronogramaPEspecificoGrupo(int idPEspecifico, List<int> listaPespecifico, int numeroGrupo)
        {
            try
            {
                string condicion = string.Empty;
                if (listaPespecifico.Count() > 0)
                {
                    condicion = @" AND PEspecificoHijoId IN @listaPespecifico ";
                }
                string query = @$"
                        SELECT DISTINCT Id, FechaHoraInicio, Duracion, DuracionTotal, Curso, IdExpositor, IdProveedor, IdAmbiente, IdCiudad, PEspecificoHijoId, Tipo, IdModalidadCurso, Comentario, EsSesionInicio, IdCentroCosto, Grupo, GrupoSesion, TieneFur, MostrarPortalWeb, IdCiclo, IdPeriodoLectivo
                        FROM [pla].[V_ObtenerCronogramaGrupoDuplicado] 
                        WHERE Estado=1 AND PEspecificoPadreId=@idPEspecifico AND Grupo=@numeroGrupo {condicion}
                        ORDER BY FechaHoraInicio ASC, PEspecificoHijoId ASC;";
                var resultado = _dapperRepository.QueryDapper(query, new { idPEspecifico, numeroGrupo, listaPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CronogramaGrupoDTO>>(resultado)!;
                }
                return new List<CronogramaGrupoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-OCPEG-001@Error en ObtenerCronogramaPEspecificoGrupo() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene los datos de duracion pespecifico
        /// </summary>
        /// <param name="idPespecificoPadre">Id del programa especifico padre</param>
        /// <returns></returns>
        public IEnumerable<DatosProgramaEspecificoDuracionDTO> ObtenerDatosDuracionPorIdPespecificoPadre(int idPespecificoPadre)
        {
            try
            {
                string query = "SELECT PEspecificoHijoId AS Id, Nombre, IdProgramaGeneral, Duracion FROM pla.V_VerificarDuracionProgramaespecifico WHERE Estado=1 AND PEspecificoPadreId=@idPespecificoPadre;";
                var resultado = _dapperRepository.QueryDapper(query, new { idPespecificoPadre });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<DatosProgramaEspecificoDuracionDTO>>(resultado)!;
                }
                return new List<DatosProgramaEspecificoDuracionDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-ODDP-001@Error en ObtenerDatosDuracionPespecifico() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene los datos de duracion pespecifico
        /// </summary>
        /// <param name="idPespecificoPadre">Id del programa especifico padre</param>
        /// <returns></returns>
        public IEnumerable<ReporteAmbienteDTO> ObtenerExcelReporteAmbiente(FiltroReporteAmbienteDTO filtro)
        {
            try
            {
                var query = "pla.SP_GenerarReporteAmbiente";
                var resultado = _dapperRepository.QuerySPDapper(query, filtro);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ReporteAmbienteDTO>>(resultado)!;
                }
                return new List<ReporteAmbienteDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-OERA-001@Error en ObtenerExcelReporteAmbiente() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/04/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion de todos los ProgramaEspecifico: Id, Nombre y Programa General
        /// </summary>
        /// <returns> List<PEspecificoPGeneralFiltroDTO> </returns>
        public DatosListaPespecificoDTO? ObtenerDatosCompletosPespecificoPorId(int idPespecifico)
        {
            try
            {
                var rpta = new List<PEspecificoPGeneralFiltroDTO>();
                var query = @"SELECT Id, Nombre, Codigo, IdCentroCosto, EstadoP, EstadoPId, TipoId, Tipo, IdProgramaGeneral, Ciudad, CursoIndividual, CodigoBanco, OrigenPrograma, Duracion, ActualizacionAutomatica, IdCursoMoodle, IdExpositor_Referencia, IdCiudad, IdAmbiente, UrlDocumentoCronograma FROM pla.V_ListaProgramaEspecificoParaTabla WHERE Estado=1 AND Id=@idPespecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DatosListaPespecificoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PeS-ODCPePI-001@Error en ObtenerDatosCompletosPespecificoPorId() {ex.Message}", ex);
            }
        }
        /// <summary>
        /// Obtiene la fecha de inicio de un programa especifico
        /// </summary>
        /// <param name="idProgramaEspecifico">Id del programa especifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="usuario">Usuario que realiza la modificacion</param>
        /// <returns>Objeto de clase IntDTO</returns>
        public IntDTO EliminarFrecuenciaWebinar(int idProgramaEspecifico, string usuario = "SYSTEM")
        {
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("pla.SP_EliminarConfiguracionCreacionWebinar", new { IdProgramaEspecifico = idProgramaEspecifico, Usuario = usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return new IntDTO();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IntDTO InsertarFrecuenciaWebinar(ParametrosInsertaFrecuenciaDTO dto)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("pla.SP_InsertarConfiguracionCreacionWebinar", new
                {
                    IdProgramaEspecifico = dto.IdPespecifico,
                    IdTiempoFrecuencia = dto.IdTiempoFrecuencia.GetValueOrDefault(),
                    ValorTiempoFrecuencia = dto.ValorTiempoFrecuencia.GetValueOrDefault(),
                    IdTiempoFrecuenciaCorreo = dto.IdTiempoFrecuenciaCorreo.GetValueOrDefault(),
                    ValorFrecuenciaCorreo = dto.ValorFrecuenciaCorreo.GetValueOrDefault(),
                    IdTiempoFrecuenciaWhatsapp = dto.IdTiempoFrecuenciaWhatsapp.GetValueOrDefault(),
                    ValorFrecuenciaWhatsapp = dto.ValorFrecuenciaWhatsapp.GetValueOrDefault(),
                    IdPlantillaFrecuenciaCorreo = dto.IdPlantillaFrecuenciaCorreo.GetValueOrDefault(),
                    IdPlantillaFrecuenciaWhatsapp = dto.IdPlantillaFrecuenciaWhatsapp.GetValueOrDefault(),
                    IdTiempoFrecuenciaCorreoConfirmacion = dto.IdTiempoFrecuenciaCorreoConfirmacion.GetValueOrDefault(),
                    ValorFrecuenciaCorreoConfirmacion = dto.ValorFrecuenciaCorreoConfirmacion.GetValueOrDefault(),
                    IdPlantillaCorreoConfirmacion = dto.IdPlantillaCorreoConfirmacion.GetValueOrDefault(),
                    IdTiempoFrecuenciaCorreoDocente = dto.IdTiempoFrecuenciaCorreoDocente.GetValueOrDefault(),
                    ValorFrecuenciaDocente = dto.ValorFrecuenciaDocente.GetValueOrDefault(),
                    IdPlantillaDocente = dto.IdPlantillaDocente.GetValueOrDefault(),
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return new IntDTO();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/06/2023
        /// <summary>
        /// Valida los cruces entre fechas y docentes en los cronogramas
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="fechas"></param>
        /// <returns></returns>
        public List<CruceSesionPEspecificoDTO> ValidarFechaExpositorCruce(DocenteAmbientePEspecificoDTO dto, IEnumerable<PEspecificoSesionFechasDTO> fechas)
        {
            try
            {
                List<CruceSesionPEspecificoDTO> registros = new List<CruceSesionPEspecificoDTO>();
                string query = @"SELECT DISTINCT 
                            IdPEspecifico, Curso,NombreCentroCosto, Ambiente, Expositor,Proveedor, Duracion, FechaHoraInicio, FechaFin, IdAmbiente,IdExpositor,IdProveedor 
                            FROM pla.V_ObtenerInformacionSesionesPEspecifico 
                            WHERE (
                                @FechaHoraInicio BETWEEN FechaHoraInicio AND FechaFin
                                OR @FechaHoraFin BETWEEN FechaHoraInicio AND FechaFin
                            ) AND Estado = 1 AND IdPEspecifico != @IdPEspecifico
                            AND EstadoPEspecifico !='Concluido' AND EstadoPEspecifico != 'Cancelado'";

                foreach (var item in fechas)
                {
                    string resultado;
                    if (dto.IdAmbiente != null && dto.IdProveedor == null)
                    {
                        query = $"{query} AND IdAmbiente = @IdAmbiente";
                        resultado = _dapperRepository.QueryDapper(query, new { item.FechaHoraInicio, item.FechaHoraFin, IdPEspecifico = dto.Id, dto.IdAmbiente });
                    }
                    else if (dto.IdProveedor != null && dto.IdAmbiente == null)
                    {
                        query = $"{query} AND IdProveedor = @IdProveedor";
                        resultado = _dapperRepository.QueryDapper(query, new { item.FechaHoraInicio, item.FechaHoraFin, IdPEspecifico = dto.Id, dto.IdProveedor });
                    }
                    else
                    {
                        query = $"{query} AND (IdAmbiente = @IdAmbiente OR IdProveedor = @IdProveedor)";
                        resultado = _dapperRepository.QueryDapper(query, new { item.FechaHoraInicio, item.FechaHoraFin, IdPEspecifico = dto.Id, dto.IdAmbiente, dto.IdProveedor });
                    }
                    if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    {
                        registros.AddRange(JsonConvert.DeserializeObject<IEnumerable<CruceSesionPEspecificoDTO>>(resultado)!);
                    }
                }
                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-VFEC-001@Error en ValidarFechaExpositorCruce(): {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene programas especificos hijos, grupo de cronograma
        /// </summary>
        /// <returns> Lista DTO - List<PEspecificoGrupoDTO> </returns>
        public IEnumerable<PEspecificoGrupoDTO> ObtenerPEspecificoGruposPorPEspecificoPadre()
        {
            try
            {
                var query = "SELECT DISTINCT Id, Nombre, IdPEspecificoPadre FROM [pla].[V_TPEspecifico_ObtenerPEspecificosHijosGrupo] WHERE Estado = 1 AND RowNumber = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoGrupoDTO>>(resultado)!;
                }
                return new List<PEspecificoGrupoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PER-OPEGPPEP-002@Error en ObtenerPEspecificoGruposPorPEspecificoPadre() {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 19/07/2023
        /// <summary>
        /// Obtiene el programa especifico y su webinar segun el id del programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objetos de clase ComboGenericoDTO</returns>
        public IEnumerable<ComboDTO> ObtenerPEspecificoWebinarPorIdPGeneral(int idPGeneral)
        {
            try
            {
                string query = @"SELECT Id, Nombre FROM pla.V_TPEspecifico_Webinar WHERE IdPGeneral=@IdPGeneral";
                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultadoDB) && resultadoDB != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultadoDB);
                return new List<ComboDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edmundo LM
        /// Fecha: 24/07/2023
        /// <summary>
        /// Ejecuta Procedimiento Almacenado [pla].[SP_ProgramaEspecificoFiltro]
        /// </summary>
        /// <param name="ProgramaEspecificoMaterialFiltroDTO">Parametros para el filtro (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista todos los programas especificos Padre e individuales con filtros especificos</returns>

        public List<ProgramaEspecificoMaterialDTO> ObtenerPorFiltro(ProgramaEspecificoMaterialFiltroDTO filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro.IdProgramaEspecifico))
                {
                    filtro.IdProgramaEspecifico = "";
                }
                if (string.IsNullOrEmpty(filtro.IdCentroCosto))
                {
                    filtro.IdCentroCosto = "";
                }
                if (string.IsNullOrEmpty(filtro.CodigoBs))
                {
                    filtro.CodigoBs = "";
                }
                if (string.IsNullOrEmpty(filtro.IdEstadoPEspecifico))
                {
                    filtro.IdEstadoPEspecifico = "";
                }
                if (string.IsNullOrEmpty(filtro.IdModalidadCurso))
                {
                    filtro.IdModalidadCurso = "";
                }
                if (string.IsNullOrEmpty(filtro.IdPGeneral))
                {
                    filtro.IdPGeneral = "";
                }
                if (string.IsNullOrEmpty(filtro.IdArea))
                {
                    filtro.IdArea = "";
                }
                if (string.IsNullOrEmpty(filtro.IdSubArea))
                {
                    filtro.IdSubArea = "";
                }
                var query = "pla.SP_ProgramaEspecificoFiltro";
                var res = _dapperRepository.QuerySPDapper(query, new { filtro.IdProgramaEspecifico, filtro.IdCentroCosto, filtro.CodigoBs, filtro.IdEstadoPEspecifico, filtro.IdModalidadCurso, filtro.IdPGeneral, filtro.IdArea, filtro.IdSubArea });
                var rpta = JsonConvert.DeserializeObject<List<ProgramaEspecificoMaterialDTO>>(res);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de programas específicos padre
        /// </summary>
        /// <returns>List<PEspecificoProgramaGeneralFiltroDTO></returns>
        public List<PEspecificoProgramaGeneralFiltroDTO> ObtenerProgramasEspecificosPadres(int? tipo)
        {
            try
            {
                var query = "";
                List<PEspecificoProgramaGeneralFiltroDTO> Listado = new List<PEspecificoProgramaGeneralFiltroDTO>();
                if (tipo.HasValue)
                {
                    query = $@"SELECT DISTINCT IdPEspecifico AS Id, PEspecifico AS Nombre, IdPGeneral AS IdPGeneral
                            FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                            WHERE Estado = 1 AND RowNumber = 1 AND Tipo = 1;";
                }
                else
                {
                    query = $@"SELECT DISTINCT IdPEspecifico AS Id, PEspecifico AS Nombre, IdPGeneral AS IdPGeneral
                            FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                            WHERE Estado = 1 AND RowNumber = 1";
                }
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    Listado = JsonConvert.DeserializeObject<List<PEspecificoProgramaGeneralFiltroDTO>>(res);
                }
                return Listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns>Duracion del programa especifico</returns>
        public string? ObtenerDuracionProgramaEspecificoModulo(int IdPespecifico, int IdMatriculaCabecera)
        {
            try
            {
                var query = "SELECT Duracion AS Valor FROM pla.V_EstructuraEspecificaDuracion WHERE idmatriculacabecera=@IdMatriculaCabecera AND IdPGeneralHijo=@IdPespecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPespecifico, IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!.Valor;
                }
                else
                {
                    query = "SELECT Duracion AS Valor FROM pla.V_Pespecifico_DuracionModulo WHERE Id=@IdPespecifico and IdMatriculaCabecera=@IdMatriculaCabecera";
                    resultado = _dapperRepository.FirstOrDefault(query, new { IdPespecifico, IdMatriculaCabecera });
                    if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    {
                        return JsonConvert.DeserializeObject<StringDTO>(resultado)!.Valor;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Margiory Ramirez 
        /// Fecha: 31/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los centro de Costo Precencial
        /// <returns>List<PespecificoCentroCostoDTO></returns>
        public PespecificoCentroCostoDTO ObtenerCentroCostoPresencial(string nombre, string ciudad)
        {
            try
            {
                string _query = "Select IdCentroCosto from pla.V_ObtenerCentroCosto where EstadoP = 'Lanzamiento' AND UPPER(NombreCorto) = @Nombre AND UPPER(Ciudad) = @Ciudad";
                var _queryRespuesta = _dapperRepository.FirstOrDefault(_query, new { Nombre = nombre, Ciudad = ciudad });
                if (_queryRespuesta != "null")
                    return JsonConvert.DeserializeObject<PespecificoCentroCostoDTO>(_queryRespuesta);
                else
                    return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Margiory Ramirez 
        /// Fecha: 31/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los centro de Costo Online
        /// <returns>List<PespecificoCentroCostoDTO></returns>
        public PespecificoCentroCostoDTO ObtenerCentroCostoOnline(string nombre)
        {
            try
            {
                string _query = "Select IdCentroCosto from pla.V_ObtenerCentroCosto where EstadoP = 'Lanzamiento' AND UPPER(NombreCorto) = @Nombre AND Tipo like '%Online%'";
                var _queryRespuesta = _dapperRepository.FirstOrDefault(_query, new { Nombre = nombre });
                if (_queryRespuesta != "null")
                    return JsonConvert.DeserializeObject<PespecificoCentroCostoDTO>(_queryRespuesta);
                else
                    return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        //Ficha Datos Personal
        #region
        public List<PEspecificoNuevoAulaVirtualDTO> ObtenerPEspecificoPersonalNuevoAulaVirtualTipo()
        {
            try
            {
                var listaResultado = new List<PEspecificoNuevoAulaVirtualDTO>();
                var query = @"SELECT IdPEspecifico,
                                NombrePEspecifico,
		                        IdCentroCosto,
		                        EstadoP,
		                        Modalidad,
		                        IdPGeneral,
                                Ciudad,
		                        IdCursoMoodle,
		                        IdCursoMoodlePrueba,
		                        TipoPEspecifico,
                                IdPEspecificoHijo,
                                IdPEspecificoPadre,
                                NombrePEspecificoPadre
                            FROM pla.V_PEspecificoNuevoAulaVirtualSinRestriccionTipoPadreHijoIndividual";
                var queryRespuesta = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    listaResultado = JsonConvert.DeserializeObject<List<PEspecificoNuevoAulaVirtualDTO>>(queryRespuesta);
                }

                return listaResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion


        public IEnumerable<PEspecificoDetalleFechaByPGeneral> ObtenerFiltroV2PorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PEspecificoDetalleFechaByPGeneral> rpta = new();
                string query = @"SELECT IdPEspecifico as Id, Nombre, IdProgramaGeneral as IdPGeneral,FechaInicio FROM pla.V_ObtenerPEspecificoDetalleFiltro WHERE IdProgramaGeneral=@idPGeneral";
                string resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoDetalleFechaByPGeneral>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerFiltroPorIdPGeneral()", ex);
            }
        }
        public PEspecificoDetalleFechaByPGeneral ObtenerFechaInicioCursoPorIdPEspeficico(int idPEspecifico)
        {
            try
            {
                PEspecificoDetalleFechaByPGeneral rpta = new();
                string query = @"SELECT IdPEspecifico as Id, Nombre, IdProgramaGeneral as IdPGeneral,FechaInicio FROM pla.V_ObtenerPEspecificoDetalleFiltro WHERE IdPEspecifico =@idPEspecifico";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PEspecificoDetalleFechaByPGeneral>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerFiltroPorIdPGeneral()", ex);
            }
        }


		public IEnumerable<PEspecificoByPGeneral> ObtenerPEspecificoByProgramaGeneral(int idPGeneral)
		{
			try
			{
				List<PEspecificoByPGeneral> rpta = new List<PEspecificoByPGeneral>();
				var query = @"
                    SELECT Id AS  IdPEspecifico , Nombre AS NombrePEspecifico ,IdProgramaGeneral AS IdPGeneral FROM pla.T_PEspecifico  WHERE IdProgramaGeneral = @idPGeneral AND Nombre NOT LIKE '%Sesion%' ORDER BY IdPEspecifico DESC";
				var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
				if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
				{
					rpta = JsonConvert.DeserializeObject<List<PEspecificoByPGeneral>>(resultado);

				}
				return rpta;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        /// Autor: Max Mantilla Rodriguez.
        /// Fecha: 14/10/2025
        /// Version: 1.0
        /// <summary>
        /// Inserta configuración predeterminada de resumenes para matrículas validas en la tabla pla.T_MatriculaConfiguracionResumenPrograma
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns>bool</returns>
        public bool ActualizarConfiguracionPEspecificoAlumnoResumen(int idPEspecifico, string usuario)
        {
            try
            {
                FechaInicioProgramaEspecificoDTO rpta = new();
                string query = "pla.SP_MatriculaConfiguracionResumenPrograma_ConfiguracionPredeterminada";
                string resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idPEspecifico = idPEspecifico, usuario = usuario });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}