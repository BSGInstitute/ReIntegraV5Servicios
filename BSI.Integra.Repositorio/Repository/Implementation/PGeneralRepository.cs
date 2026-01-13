using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PGeneralRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_PGeneral
    /// </summary>
    public class PGeneralRepository : GenericRepository<TPgeneral>, IPGeneralRepository
    {
        private Mapper _mapper;

        public PGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneral, PGeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralParametroSeoPw, PgeneralParametroSeoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaAreaRelacionadum, ProgramaAreaRelacionadum>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralExpositor, PGeneralExpositor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCarreraPreRequisitoPgeneral, CarreraPreRequisitoPgeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSuscripcionProgramaGeneral, SuscripcionProgramaGeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralModalidad, PgeneralModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralVersionPrograma, PgeneralVersionPrograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConfiguracionBeneficioProgramaGeneral, ConfiguracionBeneficioProgramaGeneral>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPgeneral MapeoEntidad(PGeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneral modelo = _mapper.Map<TPgeneral>(entidad);

                //mapea los hijos
                if (entidad.PGeneralParametroSeoPw != null && entidad.PGeneralParametroSeoPw.Count > 0)
                    modelo.TPgeneralParametroSeoPws = _mapper.Map<List<TPgeneralParametroSeoPw>>(entidad.PGeneralParametroSeoPw);

                if (entidad.ProgramaAreaRelacionada != null && entidad.ProgramaAreaRelacionada.Count > 0)
                    modelo.TProgramaAreaRelacionada = _mapper.Map<List<TProgramaAreaRelacionadum>>(entidad.ProgramaAreaRelacionada);

                if (entidad.PGeneralExpositor != null && entidad.PGeneralExpositor.Count > 0)
                    modelo.TPgeneralExpositors = _mapper.Map<List<TPgeneralExpositor>>(entidad.PGeneralExpositor);

                if (entidad.CarreraPreRequisitoPgeneral != null && entidad.CarreraPreRequisitoPgeneral.Count > 0)
                    modelo.TCarreraPreRequisitoPgenerals = _mapper.Map<List<TCarreraPreRequisitoPgeneral>>(entidad.CarreraPreRequisitoPgeneral);

                if (entidad.SuscripcionProgramaGeneral != null && entidad.SuscripcionProgramaGeneral.Count > 0)
                    modelo.TSuscripcionProgramaGenerals = _mapper.Map<List<TSuscripcionProgramaGeneral>>(entidad.SuscripcionProgramaGeneral);

                if (entidad.PgeneralModalidad != null && entidad.PgeneralModalidad.Count > 0)
                    modelo.TPgeneralModalidads = _mapper.Map<List<TPgeneralModalidad>>(entidad.PgeneralModalidad);

                if (entidad.PgeneralVersionPrograma != null && entidad.PgeneralVersionPrograma.Count > 0)
                    modelo.TPgeneralVersionProgramas = _mapper.Map<List<TPgeneralVersionPrograma>>(entidad.PgeneralVersionPrograma);

                if (entidad.ConfiguracionBeneficioProgramaGenerals != null && entidad.ConfiguracionBeneficioProgramaGenerals.Count > 0)
                    modelo.TConfiguracionBeneficioProgramaGenerals = _mapper.Map<List<TConfiguracionBeneficioProgramaGeneral>>(entidad.ConfiguracionBeneficioProgramaGenerals);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneral Add(PGeneral entidad)
        {
            try
            {
                var PGeneral = MapeoEntidad(entidad);
                base.Insert(PGeneral);
                return PGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneral Update(PGeneral entidad)
        {
            try
            {
                var PGeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PGeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(PGeneral);
                return PGeneral;
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


        public IEnumerable<TPgeneral> Add(IEnumerable<PGeneral> listadoEntidad)
        {
            try
            {
                List<TPgeneral> listado = new List<TPgeneral>();
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

        public IEnumerable<TPgeneral> Update(IEnumerable<PGeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneral> listado = new List<TPgeneral>();
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

        public List<ProgramasPorCodigoPaisComboDTO> ObtenerProgramasPorCodigoPais(int codigoPais)
        {
            try
            {
                List<ProgramasPorCodigoPaisComboDTO> rpta = new List<ProgramasPorCodigoPaisComboDTO>();

                var query = "ope.SP_Agenda_ObtenerProgramasPorCodigoPais";
                var resultado = _dapperRepository.QuerySPDapper(query, new { CodigoPais = codigoPais});

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<ProgramasPorCodigoPaisComboDTO>>(resultado);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>
        public IEnumerable<PGeneralComboDTO> ObtenerCombo()
        {
            try
            {
                var rpta = new List<PGeneralComboDTO>();

                var query = "SELECT id, Nombre, IdArea, IdSubArea, IdCategoria, IdTipoPrograma FROM pla.T_PGeneral WHERE Estado = 1 and IdArea is not null";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<PGeneralComboDTO>>(resultado);

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>
        public IEnumerable<PGeneralComboDTO> ObtenerComboPorIdArea(int IdArea)
        {
            try
            {
                var rpta = new List<PGeneralComboDTO>();

                var query = "SELECT id, Nombre, IdArea, IdSubArea, IdCategoria, IdTipoPrograma FROM pla.T_PGeneral WHERE IdArea=@IdArea and Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdArea });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<PGeneralComboDTO>>(resultado);

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerPGeneralLanzamientoPorEjecucion()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM pla.V_ObtenerPGeneralLanzamientoPorEjecucion";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCentroCostoPorIdPgeneralFicha(int idPgeneral, int? idPais, int? idCiudad)
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                string condicion = string.Empty;
                if (idPais != null)
                {
                    condicion = " AND IdPais = @idPais";
                }
                if (idCiudad != null)
                {
                    //condicion = " AND tc.Id = @idCiudad";
                }
                var query = @$"
                        SELECT Id, Nombre, IdProgramaGeneral, IdPais, IdCiudad, EstadoPId
                        FROM com.V_CentroCostoPredictiva
                        WHERE IdProgramaGeneral = @idPgeneral {condicion}";
                var resultado = _dapperRepository.QueryDapper(query, new { idPgeneral, idPais, idCiudad });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene el Id, Nombre y IdSubAreaCapacitacion
        /// </summary>
        /// <returns>Lista de objetos de clase PGeneralSubAreaCapacitacionFiltroDTO</returns>
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerProgramaSubAreaFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new PGeneralSubAreaCapacitacionFiltroDTO { Id = x.Id, Nombre = x.Nombre, IdSubAreaCapacitacion = x.IdSubArea }).ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerProgramaGeneralPorSubAreaId(int id)
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.IdSubArea == id, x => new PGeneralSubAreaCapacitacionFiltroDTO { Id = x.Id, Nombre = x.Nombre, IdSubAreaCapacitacion = x.IdSubArea }).ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public IEnumerable<ProgramaGeneralComboDTO> ObtenerComboUrl()
        {
            try
            {
                List<ProgramaGeneralComboDTO> rpta = new List<ProgramaGeneralComboDTO>();

                var query = "SELECT Id, Nombre, urlVersion FROM pla.T_PGeneral WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProgramaGeneralComboDTO> ProgramaGneralconUrlVersion()
        {
            try
            {
                List<ProgramaGeneralComboDTO> rpta = new List<ProgramaGeneralComboDTO>();

                var query = "SELECT IdProgramaGeneral AS id , Nombre, urlVersion  FROM mkt.V_ObtenerPGeneralUrlVersion ";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProgramaGeneralComboDTO> ProgramaGneralconPEspecifico()
        {
            try
            {
                List<ProgramaGeneralComboDTO> rpta = new List<ProgramaGeneralComboDTO>();

                var query = "SELECT Id, Nombre, urlVersion  FROM mkt.V_ProgramaGneralconPEspecifico ";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PGeneral
        /// </summary>
        /// <returns> List<PGeneralAlternoDTO> </returns>
        public IEnumerable<PGeneralAlternoDTO> ObtenerPGeneral()
        {
            try
            {
                List<PGeneralAlternoDTO> rpta = new List<PGeneralAlternoDTO>();
                var query = @"SELECT Id, IdPGeneral, Nombre, Pw_ImgPortada, Pw_ImgPortadaAlf, Pw_ImgSecundaria, Pw_ImgSecundariaAlf, IdPartner, IdArea, IdSubArea, IdCategoria, Pw_estado, Pw_mostrarBSPlay, pw_duracion, 
                            IdBusqueda, IdChatZopim, Pg_titulo, Codigo, UrlImagenPortadaFr, UrlBrochurePrograma, UrlPartner, UrlVersion, Pw_tituloHtml, EsModulo, NombreCorto, IdPagina, ChatActivo, Estado, 
                            FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, Pw_DescripcionGeneral, TieneProyectoDeAplicacion, IdTipoPrograma, CodigoPartner, LogoPrograma, UrlLogoPrograma
                            FROM pla.T_PGeneral
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralAlternoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene atributos principales de T_PGeneral asociados a un Identificador
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralAtributosPrincipalesDTO </returns>
        public PGeneralAtributosPrincipalesDTO ObtenerPGeneralAtributosPrincipalesPorId(int idPGeneral)
        {
            try
            {
                PGeneralAtributosPrincipalesDTO rpta = new PGeneralAtributosPrincipalesDTO();
                var query = @"
                    SELECT Id,IdPGeneral,Nombre,Pw_ImgPortada AS PwImgPortada,Pw_ImgPortadaAlf AS PwImgPortadaAlf,Pw_ImgSecundaria AS PwImgSecundaria,
	                    Pw_ImgSecundariaAlf AS PwImgSecundariaAlf,IdPartner,IdArea,IdSubArea,IdCategoria,Pw_estado AS PwEstado,
	                    Pw_mostrarBSPlay AS PwMostrarBSPlay,pw_duracion AS PwDuracion,IdBusqueda,IdChatZopim,Pg_titulo AS PgTitulo,
	                    Codigo,UrlImagenPortadaFr,UrlBrochurePrograma,UrlPartner,UrlVersion,Pw_tituloHtml AS PwTituloHtml,EsModulo,
	                    NombreCorto,IdPagina,ChatActivo
                    FROM pla.T_PGeneral
                    WHERE Estado = 1 AND Id = @idPGeneral";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PGeneralAtributosPrincipalesDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cabecera Speech para Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> PGeneralCabeceraSpeechAgendaDTO </returns>
        public PGeneralCabeceraSpeechAgendaDTO ObtenerCabeceraSpeechAgenda(int idOportunidad, int idCentroCosto)
        {
            try
            {
                PGeneralCabeceraSpeechAgendaDTO cabeceraSpeech = new PGeneralCabeceraSpeechAgendaDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerCabeceraSpeech", new { idOportunidad, idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    cabeceraSpeech = JsonConvert.DeserializeObject<PGeneralCabeceraSpeechAgendaDTO>(resultado)!;
                }
                return cabeceraSpeech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Publico Objetivo para Agenda
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<PGeneralPublicoObjetivoParaAgendaDTO> </returns>
        public IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoProgramaParaAgenda(int idCentroCosto, int idOportunidad)
        {
            try
            {
                List<PGeneralPublicoObjetivoParaAgendaDTO> publicoObjetivo = new List<PGeneralPublicoObjetivoParaAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_ObtenerPublicoObjetivoProgramaGeneral", new { idCentroCosto, idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    publicoObjetivo = JsonConvert.DeserializeObject<List<PGeneralPublicoObjetivoParaAgendaDTO>>(resultadoStoreProcedure);
                }
                return publicoObjetivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin R.
        /// Fecha: 09/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Publico Objetivo para Agenda Nueva V3
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<PGeneralPublicoObjetivoParaAgendaDTO> </returns>
        public IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(int idCentroCosto, int idOportunidad)
        {
            try
            {
                List<PGeneralPublicoObjetivoParaAgendaDTO> publicoObjetivo = new List<PGeneralPublicoObjetivoParaAgendaDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_ObtenerPublicoObjetivoProgramaGeneralAgendaV3", new { idCentroCosto, idOportunidad });
                if (!string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]"))
                {
                    publicoObjetivo = JsonConvert.DeserializeObject<List<PGeneralPublicoObjetivoParaAgendaDTO>>(resultadoStoreProcedure);
                }
                return publicoObjetivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene PGeneral Documento asociado a un Id de Programa General
        /// </summary>
        /// <param name="id">Id del Programa General</param>
        /// <returns> PgeneralDocumentoSeccionDTO </returns>
        public PgeneralDocumentoSeccionDTO ObtenerPgeneralDocumentoPorId(int id)
        {
            try
            {
                string query = @"
                                SELECT 
                                    Id, Nombre, pw_duracion 
                                FROM 
                                    pla.V_TPgeneral_PorIdBusqueda 
                                WHERE 
                                    Id = @Id";
                var queryPrgrama = _dapperRepository.FirstOrDefault(query, new { Id = id });
                return JsonConvert.DeserializeObject<PgeneralDocumentoSeccionDTO>(queryPrgrama)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre del ProgramaGeneral asociado a un IdBusqueda
        /// </summary>
        /// <param name="idBusqueda">Id de Busqueda</param>
        /// <returns> PGeneralNombreDTO </returns>
        public PGeneralNombreDTO ObtenerPGeneralPorIdBusqueda(int idBusqueda)
        {
            try
            {
                var programaGeneral = new PGeneralNombreDTO();
                string query = "SELECT Id,Nombre FROM pla.V_TPgeneral_PorIdBusqueda WHERE IdBusqueda = @idBusqueda";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idBusqueda });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    programaGeneral = JsonConvert.DeserializeObject<PGeneralNombreDTO>(resultadoQuery);
                }
                return programaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene programa general, area y subArea por centro costo
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <returns> PGeneralAreaSubAreaDTO </returns>
        public PGeneralAreaSubAreaDTO ObtenerPGeneralPEspecificoPorCentroCosto(int idCentroCosto)
        {
            try
            {
                var areaPrograma = new PGeneralAreaSubAreaDTO();
                string query = @"
                    SELECT IdProgramaGeneral, IdArea, IdSubArea
                    FROM pla.V_ObtenerPGeneralPEspecificoPorCentroCosto
                    WHERE IdCentroCosto = @IdCentroCosto
	                    AND EstadoPEspecifico = 1
	                    AND EstadoPGeneral = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    areaPrograma = JsonConvert.DeserializeObject<PGeneralAreaSubAreaDTO>(resultadoQuery);
                }
                return areaPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los padreEspecifico e hijoEspecifico de un programa general con restriccion de Lanzamiento y Estado
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<PadrePespecificoHijoDTO> </returns>
        public List<PadrePespecificoHijoDTO> ObtenerPadreHijoEspecificoV2(int idPGeneral)
        {
            try
            {
                var areaPrograma = new List<PadrePespecificoHijoDTO>();
                string query = @"
                    SELECT Id,IdPEspecificoPadre,IdPespecificoHijo
                    FROM pla.V_ObtenerPadreEspecificoHijoEspecificoV2
                    WHERE IdProgramaGeneral = @idPGeneral AND EstadoPGeneral = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    areaPrograma = JsonConvert.DeserializeObject<List<PadrePespecificoHijoDTO>>(resultadoQuery);
                }
                return areaPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las frecuencias de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<FrecuenciaProgramaGeneralDTO> </returns>
        public List<FrecuenciaProgramaGeneralDTO> ObtenerFrecuenciasPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var areaPrograma = new List<FrecuenciaProgramaGeneralDTO>();
                string query = @"
                    SELECT IdPEspecifico, Nombre
                    FROM pla.V_ObtenerFrecuenciaPGeneral
                    WHERE IdProgramaGeneral = @idPGeneral
	                    AND EstadoPEspecifico = 1
	                    AND EstadoPGeneral = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    areaPrograma = JsonConvert.DeserializeObject<List<FrecuenciaProgramaGeneralDTO>>(resultadoQuery);
                }
                return areaPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones de un programa general validando las sesiones configuradas para si visualización
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>List<PEspecificoSesionDTO></returns>  
        public List<PEspecificoSesionDTO> ObtenerSesionesProgramaGeneralValidadoVisualizacionAgenda(int idPGeneral)
        {
            try
            {
                var areaPrograma = new List<PEspecificoSesionDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("pla.SP_ObtenerSesionesValidadoVisualizacion", new { idPGeneral });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    areaPrograma = JsonConvert.DeserializeObject<List<PEspecificoSesionDTO>>(resultadoQuery);
                }
                return areaPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Lista Sesiones Programa General: List<PEspecificoSesionDTO></returns>  
        public List<PEspecificoSesionDTO> ObtenerSesionesPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                var areaPrograma = new List<PEspecificoSesionDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("pla.SP_GetPEspecificoSesion", new { idPGeneral });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    areaPrograma = JsonConvert.DeserializeObject<List<PEspecificoSesionDTO>>(resultadoQuery);
                }
                return areaPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de monto de programa por idarea y subarea
        /// </summary>
        /// <param name="filtros">Objeto tipo Dictionary<string, string> </param>
        /// <returns> Lista Monto Pago Programa General: List<MontoPagoProgramaDTO></returns>  
        public List<MontoPagoProgramaDTO> ObtenerResumenProgramaV2(Dictionary<string, string> filtros)
        {
            try
            {
                var filtro = this.ObtenerFiltro(filtros);
                List<MontoPagoProgramaDTO> lista = new List<MontoPagoProgramaDTO>();
                var query = "SELECT Id, Precio, PrecioLetras, IdMoneda, SimboloMoneda, Matricula, Cuotas, NroCuotas, NombrePrograma, DuracionPrograma, IdPrograma, IdTipoPago, TipoPago, IdPais, Descripcion, VisibleWeb, Paquete, IdArea, IdSubArea FROM pla.V_TMontoPagoPrograma_ObtenerMontoPagoProgramaGeneral " + filtro.ToString() + " AND Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<MontoPagoProgramaDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un string, que es el filtro sql que se genera dependiendo de lo que se le envie
        /// </summary>
        /// <param name="filtros">Objeto tipo Dictionary<string, string> </param>
        /// <returns> Objeto: Filtro</returns>  
        private string ObtenerFiltro(Dictionary<string, string> filtros)
        {
            string filtro = string.Empty;
            int c = 0;
            foreach (var prop in filtros)
            {
                if (prop.Key == "codigoPais")
                {
                    continue;
                }
                if (prop.Key != null && prop.Value != null)
                {
                    if (c == 0)
                    {
                        filtro += " WHERE ";
                    }
                    else if (c > 0)
                    {
                        filtro += " AND ";
                    }

                    if (prop.Value.Contains(","))
                    {
                        filtro += prop.Key + " IN (" + prop.Value + ")";
                        c++;
                    }
                    else
                    {
                        filtro += " " + prop.Key + " = " + prop.Value + "";
                        c++;
                    }
                }
            }
            return filtro;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener modalidades por programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<ModalidadProgramaDTO> </returns>
        public List<ModalidadProgramaDTO> ObtenerModalidadesPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                var areaPrograma = new List<ModalidadProgramaDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("pla.SP_ObtenerModalidadesPorPrograma", new { idPGeneral });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    areaPrograma = JsonConvert.DeserializeObject<List<ModalidadProgramaDTO>>(resultadoQuery);
                }
                return areaPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de secciones de un programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<InformacionProgramaDTO> </returns>
        public List<InformacionProgramaDTO> ObtenerSeccionesInformacionProgramaPorProgramaGeneral(int idPGeneral)
        {
            try
            {
                var areaPrograma = new List<InformacionProgramaDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("pla.SP_SeccionesInformacionPrograma", new { idPGeneral });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    areaPrograma = JsonConvert.DeserializeObject<List<InformacionProgramaDTO>>(resultadoQuery);
                }
                return areaPrograma;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de las Personas a Enviar Correo
        /// </summary>
        /// <param name="idPersonal"> Id del Personal </param>
        /// <returns> List<CorreosGmailDTO> </returns>
        public List<CorreosGmailDTO> ObtenerCorreosIdPersonalAprobacion(int idPersonal)
        {
            try
            {
                List<CorreosGmailDTO> Correos = new List<CorreosGmailDTO>();
                var _correos = _dapperRepository.QuerySPDapper("gp.SP_ObtenerCorreosByIdPersonaAprobacionCronograma", new { idPersonal });
                Correos = JsonConvert.DeserializeObject<List<CorreosGmailDTO>>(_correos);
                return Correos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de un programa general por el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<PGeneralAlternoDTO> </returns>
        public PGeneralAlternoDTO ObtenerPGeneralPorId(int idPGeneral)
        {
            try
            {
                PGeneralAlternoDTO rpta = new PGeneralAlternoDTO();
                var query = @"
                        SELECT * FROM pla.T_PGeneral
                        WHERE IdPGeneral = @idPGeneral AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PGeneralAlternoDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdSubArea de la tabla TPGeneral por el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<PGeneralAlternoDTO> </returns>
        public PGeneralAreaSubAreaDTO ObtenerAreaSubAreaPorIdPGeneral(int idPGeneral)
        {
            try
            {
                string queryPGeneral = "Select IdArea, IdSubArea from pla.V_TPGeneral_AreaSubArea where Id=@Id and Estado=1";
                var programaGeneral = _dapperRepository.FirstOrDefault(queryPGeneral, new { Id = idPGeneral });
                return JsonConvert.DeserializeObject<PGeneralAreaSubAreaDTO>(programaGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene atributos principales de T_PGeneral
        /// </summary>
        /// <returns> PGeneralPrincipalDTO </returns>
        public IEnumerable<PGeneralPrincipalDTO> ObtenerTodoGrid()
        {
            try
            {
                var rpta = new List<PGeneralPrincipalDTO>();
                var query = @"
                        SELECT Id,IdPGeneral,Nombre,Pw_ImgPortada AS PwImgPortada,Pw_ImgPortadaAlf AS PwImgPortadaAlf,Pw_ImgSecundaria AS PwImgSecundaria,
	                        Pw_ImgSecundariaAlf AS PwImgSecundariaAlf,IdPartner,IdArea,IdSubArea,IdCategoria,Pw_estado AS PwEstado,
	                        Pw_mostrarBSPlay AS PwMostrarBSPlay,pw_duracion AS PwDuracion,IdBusqueda,IdChatZopim,Pg_titulo AS PgTitulo,
	                        Codigo,UrlImagenPortadaFr,UrlBrochurePrograma,UrlPartner,UrlVersion,Pw_tituloHtml AS PwTituloHtml,EsModulo,
	                        NombreCorto,IdPagina,ChatActivo
                        FROM pla.T_PGeneral WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralPrincipalDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 09/10/2022
        /// Version: 1.0
        /// <summary>
        /// Se obtienen los centros de costos que no esten asociados a un GrupoFiltroProgramaCritico
        /// </summary>
        /// <returns>Lista de objetos de clase PGeneralProgramaCriticoSubAreaDTO</returns>
        public List<PGeneralProgramaCriticoSubAreaDTO> ObtenerPGeneralProgramaCriticoPorSubArea()
        {
            try
            {
                List<PGeneralProgramaCriticoSubAreaDTO> listaPGeneral = new List<PGeneralProgramaCriticoSubAreaDTO>();
                var registrosBO = _dapperRepository.QuerySPDapper("com.SP_ObtenerPGeneralProgramaCriticoPorSubArea", new { });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    listaPGeneral = JsonConvert.DeserializeObject<List<PGeneralProgramaCriticoSubAreaDTO>>(registrosBO);
                }
                return listaPGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id, Nombre y IdSubAreaCapacitacion, sin limitar solo los que se muestran en el portal web
        /// </summary>
        /// <returns>Lista de objetos de tipo (PGeneralSubAreaCapacitacionFiltroDTO)</returns>
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerProgramaSubAreaFiltroTodo()
        {
            try
            {
                List<PGeneralSubAreaCapacitacionFiltroDTO> rtpa = new List<PGeneralSubAreaCapacitacionFiltroDTO>();
                var query = "SELECT Id, Nombre, IdSubArea AS IdSubAreaCapacitacion FROM pla.T_PGeneral WHERE Estado = 1 ORDER BY Nombre ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rtpa = JsonConvert.DeserializeObject<List<PGeneralSubAreaCapacitacionFiltroDTO>>(resultado)!;
                }
                return rtpa;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Inerta un Programa General mediante un procedure con IdBusqueda,IdPgeneral y Id con el Id 
        /// de troncalTPgeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public int InsertaPGeneralSinIdentity(PGeneral programa)
        {
            try
            {

                var query = _dapperRepository.QuerySPDapper("pla.SP_InsertarPGeneral", new
                {
                    IdPGeneral = programa.IdPgeneral,
                    Nombre = programa.Nombre,
                    Pw_ImgPortada = programa.PwImgPortada,
                    Pw_ImgPortadaAlf = programa.PwImgPortadaAlf,
                    Pw_ImgSecundaria = programa.PwImgSecundaria,
                    Pw_ImgSecundariaAlf = programa.PwImgSecundariaAlf,
                    IdPartner = programa.IdPartner,
                    IdArea = programa.IdArea,
                    IdSubArea = programa.IdSubArea,
                    IdCategoria = programa.IdCategoria,
                    Pw_estado = programa.PwEstado,
                    Pw_mostrarBSPlay = programa.PwMostrarBsplay,
                    pw_duracion = programa.PwDuracion,
                    IdBusqueda = programa.IdBusqueda,
                    IdChatZopim = programa.IdChatZopim,
                    Pg_titulo = programa.PgTitulo,
                    Codigo = programa.Codigo,
                    UrlImagenPortadaFr = programa.UrlImagenPortadaFr,
                    UrlBrochurePrograma = programa.UrlBrochurePrograma,
                    UrlPartner = programa.UrlPartner,
                    UrlVersion = programa.UrlVersion,
                    Pw_tituloHtml = programa.PwTituloHtml,
                    EsModulo = programa.EsModulo,
                    NombreCorto = programa.NombreCorto,
                    IdPagina = programa.IdPagina,
                    ChatActivo = programa.ChatActivo,
                    IdTipoPrograma = programa.IdTipoPrograma,
                    PwDescripcionGeneral = programa.PwDescripcionGeneral,
                    TieneCertificadoModular = programa.TieneCertificadoModular,
                    CertificadoRequierePago = programa.CertificadoRequierePago,
                    UsuarioCreacion = programa.UsuarioCreacion,
                    UsuarioModificacion = programa.UsuarioModificacion,
                    CreditosTeoricos = programa.CreditosTeoricos,
                    CreditosPracticos = programa.CreditosPracticos,
                    CreditosTotales = programa.CreditosTotales,
                    HorasTeoricas = programa.HorasTeoricas,
                    HorasPracticas = programa.HorasPracticas,
                    HorasTotales = programa.HorasTotales,
                    IdTipoProgramaCarrera = programa.IdTipoProgramaCarrera

                });
                int idPgeneral = JsonConvert.DeserializeObject<List<PgeneralIdDTO>>(query).FirstOrDefault().Id;
                return idPgeneral;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// Version: 2.0
        /// <summary>
        /// Obtiene la lista de nombres de los programas (activos)  registradas en el sistema 
        ///  y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>Lista de objetos de tipo ComboDTO</returns>
        public IEnumerable<ComboDTO> ObtenerProgramasFiltro()
        {
            try
            {

                string query = "SELECT Id, Nombre FROM pla.V_TPGeneral_Nombre WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerProgramasFiltro(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// Version: 2.0
        /// <summary>
        /// Obtiene la lista de nombres de los programas (activos)  registradas en el sistema 
        ///  y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>Lista de objetos de tipo ComboDTO</returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerProgramasFiltroAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.V_TPGeneral_Nombre WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PFR-OPF-001@Error en ObtenerPGeneralesFiltro() {ex.Message}", ex);
                throw new Exception($"Error en ObtenerProgramasFiltro(): {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los beneficios por matricula
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera que se desea obtener los beneficios (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con los beneficios por version segun la matricula cabecera enviada</returns>
        public string ObtenerBeneficiosVersion(int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new DetalleMatriculaDTO();
                var query = $@"ope.SP_ObtenerDetalleMatriculaBeneficios";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<DetalleMatriculaDTO>(resultado);
                }

                return this.ObtenerBeneficiosPorVersion(resultadoFinal.IdPGeneral, resultadoFinal.IdPaquete, resultadoFinal.IdCodigoPais);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtenere beneficios por version
        /// </summary>
        /// <param name="id">Id de la del programa general del cual se desea obtener los beneficios (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idCodigoPais">Id del pais del cual se desea obtener los beneficios (PK de la tabla conf.T_Pais)</param>
        /// <returns>Cadena con los beneficios por version</returns>
        public string ObtenerBeneficiosPorVersion(int id, int idPaquete, int idCodigoPais = 0)
        {
            try
            {
                var beneficios = this.ObtenerBeneficios(id, idCodigoPais);

                if (idPaquete == 0)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete).ToList();
                }
                else if (idPaquete == 1)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete).ToList();
                }
                else if (idPaquete == 2)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete || x.Paquete == 1).ToList();
                }
                else if (idPaquete == 3)
                {
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete || x.Paquete == 1 || x.Paquete == 2).ToList();
                }
                else
                {//ultimo caso
                    beneficios = beneficios.Where(x => x.Paquete == idPaquete).ToList();
                }
                //extramelos lo que estan de màs
                beneficios = beneficios.Where(x => !x.Titulo.Contains("Todos los beneficios de la ")).ToList();

                var nombrePaquete = "";
                if (idPaquete == 0)
                {
                    nombrePaquete = "Sin version";
                }
                else if (idPaquete == 1)
                {
                    nombrePaquete = "Version basica:";
                }
                else if (idPaquete == 2)
                {
                    nombrePaquete = "Version profesional:";
                }
                else if (idPaquete == 3)
                {
                    nombrePaquete = "Version gerencial";
                }

                var stringHtml = $@"
                                <span>{nombrePaquete}</span>";

                beneficios = beneficios.OrderBy(x => x.OrdenBeneficio).ToList();

                if (idPaquete == 0)
                {
                    stringHtml += "<div style='font-size:11pt;font-family:Calibri,sans-serif'>";
                    foreach (var item in beneficios)
                    {
                        stringHtml += item.Titulo;
                    }
                    stringHtml += "</div>";
                }
                else
                {
                    stringHtml += "<ul style = 'font-size:11pt;font-family:Calibri,sans-serif'> ";
                    foreach (var item in beneficios)
                    {
                        item.Titulo = item.Titulo.Replace("<p>", "");
                        item.Titulo = item.Titulo.Replace("</p>", "");
                        stringHtml += $@"
                                  <li> {item.Titulo} </li>
                                ";
                    }
                    stringHtml += "</ul>";
                }
                return stringHtml;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener montos
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General del que se desea obtener los montos (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idPais">Id del Pais del cual se desea obtener los beneficios (PK de la tabla conf.T_Pais)</param>
        /// <returns>Lista de objetos del tipo BeneficioDTO</returns>
        public List<BeneficioDTO> ObtenerBeneficios(int idPGeneral, int idPais = 0)
        {

            var listaBeneficios = new List<BeneficioDTO>();

            var montos = this.ObtenerMontosPorId(idPGeneral);
            var montosPorPais = montos.Where(s => s.Pais.Equals(idPais)).OrderBy(x => x.Paquete).ToList();
            if (montosPorPais.Count() == 0)
            {
                var result1 = montos.Where(s => s.Pais.Equals(0)).OrderBy(x => x.Paquete).ToList();
                if (result1.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    // Tipo 1
                    listaBeneficios.AddRange(ObtenerBeneficiosPGeneralTipo1(idPGeneral));
                }
                else
                {
                    // Tipo 2
                    listaBeneficios.Add(ObtenerBeneficiosPGeneralTipo2(idPGeneral));
                }
            }
            else
            {
                if (montosPorPais.Where(x => x.Paquete == 0).ToList().Count() == 0)
                {
                    listaBeneficios.AddRange(ObtenerBeneficiosPGeneralTipo1(idPGeneral, idPais)); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB);

                }
                else
                {
                    listaBeneficios.Add(ObtenerBeneficiosPGeneralTipo2(idPGeneral)); //JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB).FirstOrDefault();
                }
            }
            return listaBeneficios;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los paquetes, nombre paquete,precio y pais de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Monto Pago modalidad: List<MontoPagoModalidadDTO></returns>   
        public List<MontoPagoModalidadDTO> ObtenerMontosPorId(int idPGeneral)
        {
            List<MontoPagoModalidadDTO> montoPagoModalidad = new List<MontoPagoModalidadDTO>();
            var montoPagoModalidadDB = _dapperRepository.QuerySPDapper("pla.SP_MontoPago", new { idPGeneral });
            montoPagoModalidad = JsonConvert.DeserializeObject<List<MontoPagoModalidadDTO>>(montoPagoModalidadDB)!;
            return montoPagoModalidad;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los beneficios por programa general y pais tipo 1
        /// </summary>
        /// <param name="idPGeneral">Id del PGeneral </param>
        /// <param name="codigoPais">Codigo del pais </param>
        /// <returns> Lista de objeto:BeneficioDTO</returns>   
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1(int idPGeneral, int codigoPais = 0)
        {
            List<BeneficioDTO> beneficios = new List<BeneficioDTO>();
            //var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @idPGeneral And CodigoPais = @codigoPais AND EstadoMontoPago = 1 AND EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL";
            var query = "SELECT Paquete, Titulo, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @idPGeneral And CodigoPais = @codigoPais";
            var beneficiosDB = _dapperRepository.QueryDapper(query, new { idPGeneral, codigoPais });
            if (!beneficiosDB.Contains("[]"))
            {
                beneficios = JsonConvert.DeserializeObject<List<BeneficioDTO>>(beneficiosDB)!;
            }

            return beneficios;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el titulo de un beneficio por programa general tipo 2
        /// </summary>
        /// <param name="idPGeneral">Id del PGeneral </param>
        /// <returns>Objeto del tipo BeneficioDTO</returns>   
        public BeneficioDTO ObtenerBeneficiosPGeneralTipo2(int idPGeneral)
        {
            BeneficioDTO beneficio = new BeneficioDTO();
            var query = "SELECT Titulo FROM pla.V_BeneficiosProgramaTipo2 WHERE TituloDocumentoSeccion = @nombre AND IdProgramaGeneral = @idPGeneral AND EstadoDocumentoSeccion = 1 AND EstadoProgramaGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoProgramaGeneral = 1";
            var beneficiosDB = _dapperRepository.FirstOrDefault(query, new { idPGeneral, nombre = "Beneficios" });
            if (!string.IsNullOrEmpty(beneficiosDB) && !beneficiosDB.Equals("null"))
            {
                beneficio = JsonConvert.DeserializeObject<BeneficioDTO>(beneficiosDB)!;
            }
            return beneficio;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la version por matricula
        /// </summary>
        /// <param name="IdMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con la version formateada</returns>
        public string ObtenerVersion(int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new StringDTO();
                var query = $@"ope.SP_ObtenerVersionMatriculaAlumno";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    resultadoFinal = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la duracion del programa en meses
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la duracion del programa en meses</returns>
        public string ObtenerDuracionMeses(int idMatriculaCabecera)
        {
            try
            {
                var resultadoFinal = new StringDTO();
                var query = $@"ope.SP_ObtenerDuracionPGeneralMatriculaCabecera";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return resultadoFinal.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos irca del alumno asociados al IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la duracion del programa en meses</returns>
        public List<PGeneralCursoIrcaDTO> ObtenerCursosIrcaAlumno(int idMatriculaCabecera)
        {
            try
            {
                var listacurso = new List<PGeneralCursoIrcaDTO>();
                string _query = "Select NombreCurso, EstadoCurso from pla.V_PgeneralCursosIrca Where IdMatriculaCabecera = @IdMatriculaCabecera";
                string queryPrograma = _dapperRepository.QueryDapper(_query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    listacurso = JsonConvert.DeserializeObject<List<PGeneralCursoIrcaDTO>>(queryPrograma);
                }
                return listacurso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos de Preguntas Frecuentes
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns>ProgramaCentroCostoDTO</returns>
        public ProgramaCentroCostoDTO ObtenerDatosPFrecuentes(int idCentroCosto)
        {
            try
            {
                ProgramaCentroCostoDTO programaCentroCostoDTO = new ProgramaCentroCostoDTO();
                string queryPEspecifico = @"SELECT 
                                                IdPGeneral, IdArea, IdSubArea, TipoId 
                                          FROM 
                                                pla.V_TPGeneral_ObtenerDatosPFrecuentes 
                                          WHERE 
                                                IdCentroCosto = @IdCentroCosto AND EstadoPG = 1 AND EstadoPE = 1";
                var programaEspecifico = _dapperRepository.FirstOrDefault(queryPEspecifico, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(programaEspecifico) && !programaEspecifico.Contains("null"))
                {
                    programaCentroCostoDTO = JsonConvert.DeserializeObject<ProgramaCentroCostoDTO>(programaEspecifico)!;
                }
                return programaCentroCostoDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Codigo Partner por idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculacabecera"></param>
        /// <returns> string </returns>
        public string? ObtenerCodigoPartner(int idMatriculacabecera)
        {
            try
            {
                string query = @"SELECT 
                                    CodigoPartner AS Valor 
                                FROM 
                                    pla.V_ObtenerCodigoPartner 
                                WHERE 
                                    Id = @IdMatriculacabecera";
                string queryPrograma = _dapperRepository.FirstOrDefault(query, new { IdMatriculacabecera = idMatriculacabecera });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<StringDTO>(queryPrograma)!.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int? ObtenerPdu(int idMatriculacabecera)
        {
            try
            {
                string query = @"SELECT 
                                     Pdu AS Valor 
                                 FROM 
                                     pla.V_ObtenerPduCodigoPartner 
                                 WHERE 
                                     Id = @IdMatriculacabecera";
                string queryPrograma = _dapperRepository.FirstOrDefault(query, new { IdMatriculacabecera = idMatriculacabecera });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<IntDTO>(queryPrograma)!.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int? ObtenerPduPorIdPGeneral(int IdPgeneral)
        {
            try
            {
                string query = @"SELECT top 1 
                                     Pdu as Valor
                                 FROM 
                                     pla.T_PgeneralCodigoPartner 
                                 WHERE 
                                     IdPgeneral = @IdPgeneral and Estado=1 order by Pdu desc";
                string queryPrograma = _dapperRepository.FirstOrDefault(query, new { IdPgeneral = IdPgeneral });
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<IntDTO>(queryPrograma)?.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/04/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="id"> Id Programa General </param>
        /// <returns> PGeneral </returns>
        public PGeneral ObtenerPorId(int id)
        {
            try
            {
                PGeneral rpta = new();
                var query = @"SELECT Id,
                                       IdPGeneral,
                                       Nombre,
                                       Pw_ImgPortada AS PwImgPortada,
                                       Pw_ImgPortadaAlf AS PwImgPortadaAlf,
                                       Pw_ImgSecundaria AS PwImgSecundaria,
                                       Pw_ImgSecundariaAlf AS PwImgSecundariaAlf,
                                       IdPartner,
                                       IdArea,
                                       IdSubArea,
                                       IdCategoria,
                                       Pw_estado AS PwEstado,
                                       Pw_mostrarBSPlay as PwMostrarBsplay,
                                       pw_duracion AS PwDuracion,
                                       IdBusqueda,
                                       IdChatZopim,
                                       Pg_titulo AS PgTitulo,
                                       Codigo,
                                       UrlImagenPortadaFr,
                                       UrlBrochurePrograma,
                                       UrlPartner,
                                       UrlVersion,
                                       Pw_tituloHtml AS PwTituloHtml,
                                       EsModulo,
                                       NombreCorto,
                                       IdPagina,
                                       ChatActivo,
                                       Estado,
                                       FechaCreacion,
                                       FechaModificacion,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       Pw_DescripcionGeneral AS PwDescripcionGeneral,
                                       TieneProyectoDeAplicacion,
                                       IdTipoPrograma,
                                       CodigoPartner,
                                       LogoPrograma,
                                       UrlLogoPrograma,
                                       FechaInicioAsincronico,
                                       AsignaVenta,
                                       TieneCertificadoModular,
                                       CertificadoRequierePago,
                                       IdPGeneralBase AS IdPgeneralBase,
                                       IdPGeneralPeriodoAsincronico AS IdPgeneralPeriodoAsincronico,
                                       CreditosTeoricos,
                                       CreditosPracticos,
                                       CreditosTotales,
                                       HorasTeoricas,
                                       HorasPracticas,
                                       HorasTotales,
                                       IdTipoProgramaCarrera
                                FROM pla.T_PGeneral WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PGeneral>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/04/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPGeneral"> Id Programa General </param>
        /// <returns> PGeneral </returns>
        public async Task<PGeneral> ObtenerPorIdAsync(int id)
        {
            try
            {
                PGeneral rpta = new PGeneral();
                var query = @"SELECT Id,
                                       IdPGeneral,
                                       Nombre,
                                       Pw_ImgPortada AS PwImgPortada,
                                       Pw_ImgPortadaAlf AS PwImgPortadaAlf,
                                       Pw_ImgSecundaria AS PwImgSecundaria,
                                       Pw_ImgSecundariaAlf AS PwImgSecundariaAlf,
                                       IdPartner,
                                       IdArea,
                                       IdSubArea,
                                       IdCategoria,
                                       Pw_estado AS PwEstado,
                                       Pw_mostrarBSPlay as PwMostrarBsplay,
                                       pw_duracion AS PwDuracion,
                                       IdBusqueda,
                                       IdChatZopim,
                                       Pg_titulo AS PgTitulo,
                                       Codigo,
                                       UrlImagenPortadaFr,
                                       UrlBrochurePrograma,
                                       UrlPartner,
                                       UrlVersion,
                                       Pw_tituloHtml AS PwTituloHtml,
                                       EsModulo,
                                       NombreCorto,
                                       IdPagina,
                                       ChatActivo,
                                       Estado,
                                       FechaCreacion,
                                       FechaModificacion,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       Pw_DescripcionGeneral AS PwDescripcionGeneral,
                                       TieneProyectoDeAplicacion,
                                       IdTipoPrograma,
                                       CodigoPartner,
                                       LogoPrograma,
                                       UrlLogoPrograma,
                                       FechaInicioAsincronico,
                                       AsignaVenta
                                FROM pla.T_PGeneral WHERE Estado =1 AND Id=@Id";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PGeneral>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene seccion especifica dependiendo del parametro
        /// </summary>
        /// <param name="idPGeneral"> Id Programa General </param>
        /// <param name="seccion"> Sección </param>
        /// <returns> ProgramaSeccionIndividualDTO </returns>
        public ProgramaSeccionIndividualDTO SeccionIndividualPGeneral(int idPGeneral, string seccion)
        {
            try
            {
                ProgramaSeccionIndividualDTO valor = new ProgramaSeccionIndividualDTO();

                string query = @"SELECT 
                                    Titulo, Contenido, IdSeccionTipoDetalle_PW as IdSeccionTipoDetallePW, NumeroFila, Cabecera, PiePagina, OrdenWeb 
                                FROM pla.V_ListaSeccionesPorIdPrograma_Documento 
                                WHERE 
                                    IdPGeneral = @IdPGeneral AND Titulo = @Seccion";
                string queryPrograma = _dapperRepository.FirstOrDefault(query, new { IdPGeneral = idPGeneral, Seccion = seccion });
                if (!string.IsNullOrEmpty(queryPrograma) && queryPrograma != "null")
                {
                    valor = JsonConvert.DeserializeObject<ProgramaSeccionIndividualDTO>(queryPrograma)!;
                    return valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene seccion especifica dependiendo del parametro
        /// </summary>
        /// <param name="idPGeneral"> Id Programa General </param>
        /// <param name="seccion"> Sección </param>
        /// <returns> ProgramaSeccionIndividualDTO </returns>
        public async Task<ProgramaSeccionIndividualDTO> SeccionIndividualPGeneralAsync(int idPGeneral, string seccion)
        {
            try
            {
                ProgramaSeccionIndividualDTO valor = new ProgramaSeccionIndividualDTO();

                string query = @"SELECT 
                                    Titulo, Contenido, IdSeccionTipoDetalle_PW as IdSeccionTipoDetallePW, NumeroFila, Cabecera, PiePagina, OrdenWeb 
                                FROM pla.V_ListaSeccionesPorIdPrograma_Documento 
                                WHERE 
                                    IdPGeneral = @IdPGeneral AND Titulo = @Seccion";
                string queryPrograma = await _dapperRepository.FirstOrDefaultAsync(query, new { IdPGeneral = idPGeneral, Seccion = seccion });
                if (!string.IsNullOrEmpty(queryPrograma) && queryPrograma != "null")
                {
                    valor = JsonConvert.DeserializeObject<ProgramaSeccionIndividualDTO>(queryPrograma)!;
                    return valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los programas generales por id area
        /// </summary>
        /// <param name="listaAreas">Lista de indices de las areas (PK de la tabla pla.T_AreaCapacitacion)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerTodosPorIdArea(List<int> listaAreas)
        {
            try
            {
                string listAreas = string.Join(",", listaAreas);

                List<IdDTO> programaGeneral = new List<IdDTO>();
                var _query = "SELECT Id FROM com.V_TPGeneral_ObtenerIds AS PG INNER JOIN (SELECT Item FROM conf.F_Splitstring(@listAreas,',')) AS L ON PG.IdArea = L.item WHERE PG.Estado = 1 GROUP BY Id";
                var programaGeneralDB = _dapperRepository.QueryDapper(_query, new { listAreas });
                if (!string.IsNullOrEmpty(programaGeneralDB) && !programaGeneralDB.Contains("[]") && programaGeneralDB != null && programaGeneralDB != "null")
                {
                    programaGeneral = JsonConvert.DeserializeObject<List<IdDTO>>(programaGeneralDB);
                }
                return programaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los programas generales por id sub area
        /// </summary>
        /// <param name="listaSubAreas">Lista de indices de las subareas (PK de la tabla pla.T_SubAreaCapacitacion)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerTodosPorIdSubArea(List<int> listaSubAreas)
        {
            try
            {
                string listSubAreas = string.Join(",", listaSubAreas);

                List<IdDTO> programaGeneral = new List<IdDTO>();
                var _query = string.Empty;
                _query = "SELECT Id FROM com.V_TPGeneral_ObtenerIds AS PG INNER JOIN (SELECT Item FROM conf.F_Splitstring(@listSubAreas,',')) AS L ON PG.IdSubArea = L.item WHERE PG.Estado = 1 GROUP BY Id";
                var programaGeneralDB = _dapperRepository.QueryDapper(_query, new { listSubAreas });
                if (!string.IsNullOrEmpty(programaGeneralDB) && !programaGeneralDB.Contains("[]") && programaGeneralDB != null && programaGeneralDB != "null")
                {
                    programaGeneral = JsonConvert.DeserializeObject<List<IdDTO>>(programaGeneralDB);
                }
                return programaGeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Programa para Cuotas
        /// </summary>
        /// <param name="idMatricula"></param>
        /// <returns> CuotasProgramaDTO </returns>
        public CuotasProgramaDTO ObtenerProgramaParaCuotas(int idMatricula)
        {
            try
            {
                string queryCuotasProgramaDTO = @"SELECT 
                                                    IdBusqueda, NombreCurso, IdPespecifico, NombrePespecifico, IdMatricula, CodigoMatricula, TipoPrograma, DuracionPgeneral, 
                                                    DuracionPespecifico, NumeroCuotas, WebMoneda, TotalPagar, WebTotalPagar, WebTipoCambio, EstadoCronogramaMod 
                                                FROM 
                                                    com.V_CuotascronogramaPorMatricula 
                                                WHERE 
                                                    IdMatricula=@IdMatricula and EstadoCronogramaMod = 1";
                var cuotasProgramaDTO = _dapperRepository.FirstOrDefault(queryCuotasProgramaDTO, new { IdMatricula = idMatricula });
                return JsonConvert.DeserializeObject<CuotasProgramaDTO>(cuotasProgramaDTO)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Programa General por IdBusqueda
        /// </summary>
        /// <param name="idBusqueda"></param>
        /// <returns> PgeneralDocumentoSeccionDTO </returns>
        public PgeneralDocumentoSeccionDTO ObtenePgeneralPorIdBusqueda(int idBusqueda)
        {
            try
            {
                string queryPrograma = @"SELECT 
                                            Id,Nombre
                                         FROM 
                                            pla.V_TPgeneral_PorIdBusqueda 
                                         WHERE 
                                            IdBusqueda = @IdBusqueda";
                var query = _dapperRepository.FirstOrDefault(queryPrograma, new { IdBusqueda = idBusqueda });
                return JsonConvert.DeserializeObject<PgeneralDocumentoSeccionDTO>(query)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Programa general Para Documentos por medio del Id Alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> InformacionProgramaDocumentosDTO </returns>
        public InformacionProgramaDocumentosDTO ObtenerPGeneralParaDocumentosPorIdAlumno(int idAlumno)
        {
            try
            {
                string queryPGeneral = "SELECT TOP 1 pe.IdProgramaGeneral, pg.Nombre, pg.UrlBrochurePrograma, pe.Tipo, pe.Ciudad, pe.UrlDocumentoCronograma, op.Id AS IdOportunidad, op.IdAlumno, fo.Codigo AS CodigoFase, op.IdActividadDetalle_Ultima AS IdActividadDetalle " +
                                        "FROM com.T_Oportunidad AS op " +
                                        "INNER JOIN conf.T_ClasificacionPersona AS CLA ON CLA.Id = op.IdClasificacionPersona and IdTipoPersona = 1 " +
                                        "INNER JOIN pla.T_PEspecifico AS pe ON op.IdCentroCosto=pe.IdCentroCosto " +
                                        "INNER JOIN pla.T_PGeneral AS pg ON pe.IdProgramaGeneral=pg.Id " +
                                        "INNER JOIN pla.T_FaseOportunidad AS fo ON op.IdFaseOportunidad=fo.Id " +
                                        "WHERE CLA.IdTablaOriginal = @IdAlumno " +
                                        "ORDER BY op.FechaCreacion DESC";
                var programaGeneral = _dapperRepository.FirstOrDefault(queryPGeneral, new { IdAlumno = idAlumno });
                return JsonConvert.DeserializeObject<InformacionProgramaDocumentosDTO>(programaGeneral)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los programas generales asociados con la subárea de capacitación
        /// </summary>
        /// <param></param>
        /// <returns>List<PGeneralSubAreaCapacitacionFiltroDTO></returns>
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<PGeneralSubAreaCapacitacionFiltroDTO> ProgramasGenerales = new List<PGeneralSubAreaCapacitacionFiltroDTO>();
                string _query = "Select Id,Nombre,IdSubAreaCapacitacion from pla.V_TPGeneral_FiltroCompuestoAsociadoArea";
                string queryPrograma = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(queryPrograma) && !queryPrograma.Contains("[]") && queryPrograma != null && queryPrograma != "null")
                {
                    ProgramasGenerales = JsonConvert.DeserializeObject<List<PGeneralSubAreaCapacitacionFiltroDTO>>(queryPrograma);
                }
                return ProgramasGenerales;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtener modalidades por programa general
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModalidadProgramaDTO> ObtenerFechaInicioProgramaGeneral(int idPGeneral, int IdCodigoPais)
        {
            try
            {
                List<ModalidadProgramaDTO> modalidadPrograma = new List<ModalidadProgramaDTO>();
                var resumenProgramaBD = _dapperRepository.QuerySPDapper("pla.SP_ObtenerFechaInicioPrograma", new { idPGeneral, IdCodigoPais });

                if (resumenProgramaBD.Contains("[]"))
                {
                    return modalidadPrograma;
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<ModalidadProgramaDTO>>(resumenProgramaBD);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Valida si el T_PGeneral es Curso Padre
        /// </summary>
        /// <param name="idPGeneral"> Id De programa general</param>
        /// <returns> bool </returns>
        public bool ProgramaGeneralPadre(int idPGeneral)
        {
            try
            {
                var _query = string.Empty;
                _query = "SELECT Id FROM pla.V_ConultarProgramaPadrePorIdPGeneral WHERE  Id = @IdPGeneral";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdPGeneral = idPGeneral });
                return (!string.IsNullOrEmpty(respuesta) && respuesta != "null");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Valida si el T_PGeneral es Curso Padre
        /// </summary>
        /// <param name="idPGeneral"> Id De programa general</param>
        /// <returns> bool </returns>
        public async Task<bool> ProgramaGeneralPadreAsync(int idPGeneral)
        {
            try
            {
                var _query = string.Empty;
                _query = "SELECT Id FROM pla.V_ConultarProgramaPadrePorIdPGeneral WHERE  Id = @IdPGeneral";
                var respuesta = await _dapperRepository.FirstOrDefaultAsync(_query, new { IdPGeneral = idPGeneral });
                return (!string.IsNullOrEmpty(respuesta) && respuesta != "null");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Valida si el T_PGeneral es Curso Tecnico
        /// </summary>
        /// <param name="idPGeneral"> Id De programa general</param>
        /// <returns> bool </returns>
        public bool ProgramaGeneralEsTecnico(int idPGeneral)
        {
            try
            {
                var _query = string.Empty;
                _query = "SELECT TOP 1 pg.Id FROM  pla.T_PGeneral pg WHERE pg.Estado=1 AND pg.Codigo LIKE '%CETPRO%' AND pg.Id= @IdPGeneral";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdPGeneral = idPGeneral });
                return (!string.IsNullOrEmpty(respuesta) && respuesta != "null");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Valida si el T_PGeneral es Curso Tecnico
        /// </summary>
        /// <param name="idPGeneral"> Id De programa general</param>
        /// <returns> bool </returns>
        public async Task<bool> ProgramaGeneralEsTecnicoAsync(int idPGeneral)
        {
            try
            {
                var _query = string.Empty;
                _query = "SELECT TOP 1 pg.Id FROM  pla.T_PGeneral pg WHERE pg.Estado=1 AND pg.Codigo LIKE '%CETPRO%' AND pg.Id= @IdPGeneral";
                var respuesta = await _dapperRepository.FirstOrDefaultAsync(_query, new { IdPGeneral = idPGeneral });
                return (!string.IsNullOrEmpty(respuesta) && respuesta != "null");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de cursos Hijo por el IdPGeneral 
        /// </summary>
        /// <param name="idPGeneral"> Id De programa general</param>
        /// <returns>List<ListaCursosPorProgramaDTO></returns>
        public List<ListaCursosPorProgramaDTO> ListaCursosHijoPorIdPGeneral(int idPGeneral)
        {

            try
            {
                var _query = string.Empty;
                List<ListaCursosPorProgramaDTO> cursosHijos = new List<ListaCursosPorProgramaDTO>();
                _query = "SELECT Id,IdHijo,Curso FROM pla.V_ListaCursosPorProgramaId WHERE  Id = @IdPGeneral ORDER BY Orden";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    cursosHijos = JsonConvert.DeserializeObject<List<ListaCursosPorProgramaDTO>>(respuesta);
                    return cursosHijos;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la duración del programa por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id De programa general</param>
        /// <returns> int </returns>
        public int ObtenerDuracionCursoHijo(int idPGeneral)
        {

            try
            {
                var _query = string.Empty;
                var duracion = new ValorIntDTO();
                _query = "SELECT Duracion as Valor FROM pw.V_PW_ListaCursosProgramaGeneral WHERE  IdHijo = @IdPGeneral ORDER BY Orden";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { IdPGeneral = idPGeneral });

                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                {
                    duracion = JsonConvert.DeserializeObject<ValorIntDTO>(respuesta);
                    return duracion.Valor.Value;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la estructura del contenido del programa por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> Id De programa general</param>
        /// <returns> List<ContenidoHijoDTO>  </returns>
        public List<ContenidoHijoDTO> ContenidoEstructuraHijoPadre(int idPGeneral)
        {

            try
            {
                var _query = string.Empty;
                List<ContenidoHijoDTO> cursosHijos = new List<ContenidoHijoDTO>();
                _query = "SELECT Contenido,Documento,NumeroFila from pw.V_PW_EstructuraCarreraTecnicaPortal where IdPGeneral=@IdPGeneral ORDER BY NumeroFila";
                var respuesta = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    cursosHijos = JsonConvert.DeserializeObject<List<ContenidoHijoDTO>>(respuesta);
                    return cursosHijos;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: PGeneralRepositorio
        /// Autor: Margiory Ramirez
        /// Fecha: 20/02/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza AsignaVenta sobre la tabla Pgeneral
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        public bool ActualizarPgeneral(PGeneralAsignaVentaDTO Pgneral)
        {
            try
            {
                var query = "mkt.SP_ActualizarPgeneral";
                var respuesta = _dapperRepository.QuerySPDapper(query, new { UsuarioModificacion = Pgneral.UsuarioModificacion, AsignaVenta = Pgneral.AsignaVenta, Id = Pgneral.Id });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<PGeneralSubAreaFiltroDTO> ObtenerPGeneralSubArea()
        {
            try
            {
                IEnumerable<PGeneralSubAreaFiltroDTO> programasGenerales = new List<PGeneralSubAreaFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT  Id, Nombre, IdSubArea FROM pla.V_TPGeneral_ObtenerDatos WHERE Estado = 1 Order by Id desc";
                var pgeneralDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<IEnumerable<PGeneralSubAreaFiltroDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene programas generales para filtro
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public IEnumerable<PGeneralSubAreaFiltroDTO> ObtenerFiltroPorTipo(bool aplicaTipo)
        {
            try
            {
                string codicionTipo = aplicaTipo ? "AND Tipo = 1" : string.Empty;
                string query = $@"
                    SELECT DISTINCT
	                    IdPGeneral AS Id,
	                    PGeneral AS Nombre,
	                    IdSubArea
                    FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                    WHERE
	                    Estado = 1
	                    AND RowNumber = 1 {codicionTipo}";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralSubAreaFiltroDTO>>(resultado)!;
                }
                return new List<PGeneralSubAreaFiltroDTO>();
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
        /// Obtiene programas generales para filtro
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public async Task<IEnumerable<PGeneralSubAreaFiltroDTO>> ObtenerFiltroPorTipoAsync(bool aplicaTipo)
        {
            try
            {
                string codicionTipo = aplicaTipo ? "AND Tipo = 1" : string.Empty;
                string query = $@"
                    SELECT DISTINCT
	                    IdPGeneral AS Id,
	                    PGeneral AS Nombre,
	                    IdSubArea
                    FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                    WHERE
	                    Estado = 1
	                    AND RowNumber = 1 {codicionTipo}";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralSubAreaFiltroDTO>>(resultado)!;
                }
                return new List<PGeneralSubAreaFiltroDTO>();
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
        /// Obtiene programas generales cursos
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public IEnumerable<PGeneralHijoDTO> ObtenerPgeneralCursos(int idProgramaGeneral)
        {
            try
            {
                string query = $@"SELECT Id, IdPGeneral_Hijo FROM pla.T_PGeneralASubPGeneral WHERE Estado=1 AND IdPGeneral_Padre=@idProgramaGeneral ORDER BY Orden ASC";
                string resultado = _dapperRepository.QueryDapper(query, new { idProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralHijoDTO>>(resultado)!;
                }
                return new List<PGeneralHijoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPgeneralCursos(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna datos de Programa General Mediante IdProgramaGeneral para Programa Especifico
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <returns></returns>
        public PGeneralDatosDTO? ObtenerPGeneralParaPEspecifico(int idProgramaGeneral)
        {
            try
            {
                string query = "SELECT Id, Nombre, Codigo, IdArea, IdSubArea, IdCategoria FROM pla.V_GetProgramaGeneral WHERE Estado=1 AND Id=@idProgramaGeneral;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PGeneralDatosDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGR-OPGPE001@Error en ObtenerPGeneralParaPEspecifico(), {ex.Message}", ex);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Giancarlo Romero
        /// Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza AsignaVenta sobre la tabla Pgeneral
        /// </summary>
        /// <returns> List<PGeneralPublicoObjetivoDTO> </returns>
        /// 
        public IEnumerable<PgeneralWebinarDTO> ObtenerPGeneralWebinar()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdTipoPrograma FROM pla.V_TPGeneral_Webinar WHERE Estado=1 ORDER BY Id DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralWebinarDTO>>(resultado)!;
                }
                return new List<PgeneralWebinarDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGR-LPP001@Error en ListarProgramasPanel(), {ex.Message}", ex);
            }
        }
        /// Repositorio: PGeneralRepositorio
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 23/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> Lista PGeneralDTO </returns>
        public List<PGeneralDTO> ObtenerTodo()
        {
            try
            {
                var query = @"SELECT Id,
                                       IdPGeneral,
                                       Nombre,
                                       Pw_ImgPortada AS PwImgPortada,
                                       Pw_ImgPortadaAlf AS PwImgPortadaAlf,
                                       Pw_ImgSecundaria AS PwImgSecundaria,
                                       Pw_ImgSecundariaAlf AS PwImgSecundariaAlf,
                                       IdPartner,
                                       IdArea,
                                       IdSubArea,
                                       IdCategoria,
                                       Pw_estado AS PwEstado,
                                       Pw_mostrarBSPlay as PwMostrarBsplay,
                                       pw_duracion AS PwDuracion,
                                       IdBusqueda,
                                       IdChatZopim,
                                       Pg_titulo AS PgTitulo,
                                       Codigo,
                                       UrlImagenPortadaFr,
                                       UrlBrochurePrograma,
                                       UrlPartner,
                                       UrlVersion,
                                       Pw_tituloHtml AS PwTituloHtml,
                                       EsModulo,
                                       NombreCorto,
                                       IdPagina,
                                       ChatActivo,
                                       Estado,
                                       FechaCreacion,
                                       FechaModificacion,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       Pw_DescripcionGeneral AS PwDescripcionGeneral,
                                       TieneProyectoDeAplicacion,
                                       IdTipoPrograma,
                                       CodigoPartner,
                                       LogoPrograma,
                                       UrlLogoPrograma,
                                       FechaInicioAsincronico,
                                       AsignaVenta,
                                       TieneCertificadoModular,
                                       CertificadoRequierePago,
                                       IdPGeneralBase AS IdPgeneralBase,
                                       IdPGeneralPeriodoAsincronico AS IdPgeneralPeriodoAsincronico
                                FROM pla.T_PGeneral WHERE Estado =1 order by id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PGeneralDTO>>(resultado)!;
                }
                return new List<PGeneralDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el programa por el Id de programa especifico
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <returns></returns>
        public ProgramaGeneralTroncalDTO? ObtenerProgramaGeneralParaPespecificoPorId(int idProgramaGeneral)
        {
            try
            {
                string query = $"SELECT Id, Nombre, Codigo, IdTroncalPartner, Duracion, IdArea, IdSubArea, IdCategoria, NombreCategoria FROM pla.V_TTroncalPgeneral_ObtenerInformacion WHERE Estado=1 AND Id=@IdProgramaGeneral;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProgramaGeneral = idProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralTroncalDTO>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 08/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Periodos para los programas Generales Asincronos de T_PGeneralPeriodoAsincronico
        /// </summary>
        /// <returns> IEnumerable<PGeneralPeriodoAsincronicoDTO></PGeneralPeriodoAsincronicoDTO> </returns>
        public async Task<IEnumerable<PGeneralPeriodoAsincronicoDTO>> ObtenerPgeneralPeriodoAsincronicoAsync()
        {
            try
            {
                string queryPGeneralPeriodos = @"SELECT Id, Nombre, Periodo FROM pla.T_PGeneralPeriodoAsincronico WHERE Estado = 1";
                var programaGeneralPeriodo = await _dapperRepository.QueryDapperAsync(queryPGeneralPeriodos, null);
                if (!string.IsNullOrEmpty(programaGeneralPeriodo) && !programaGeneralPeriodo.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<List<PGeneralPeriodoAsincronicoDTO>>(programaGeneralPeriodo);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista de Programas  registradas en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns> Lista DTO - List<PGeneralAlternoDTO>() </returns>
        public IEnumerable<PGeneralDTO> ListarProgramaGeneral(FiltroProgramaGeneralDTO filtro)
        {
            try
            {
                var query = "pla.SP_General_PanelFiltro";
                var resultado = _dapperRepository.QuerySPDapper(query, new { filtro.IdArea, filtro.IdSubArea, filtro.IdPgeneral, filtro.IdPartner });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralDTO>>(resultado)!;
                }
                return new List<PGeneralDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGR-LPG-001@Error en ListarProgramaGeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de inicio del programa Online con el PEspecifico en Lanzamiento
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> DateTime </returns>
        public List<PgeneralFechaOnlineDTO> ObtenerPGeneralFechaInicioOnline(int idPGeneral)
        {
            try
            {
                var query = "ope.SP_HoraInicioPgeneralOnline";
                var respuestaDapper = _dapperRepository.QuerySPDapper(query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<List<PgeneralFechaOnlineDTO>>(respuestaDapper)!;
                }
                return new List<PgeneralFechaOnlineDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<PgeneralFechaPresencialDTO> ObtenerPGeneralFechaInicioPresencial(int idPGeneral)
        {
            try
            {
                List<PgeneralFechaPresencialDTO> rpta = new List<PgeneralFechaPresencialDTO>();
                var query = "ope.SP_HoraInicioPgeneralPresencial";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralFechaPresencialDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<PgeneralFechaAonlineDTO> ObtenerPgeneralFechaInicioAonline(int IdProgramaGeneral)
        {
            try
            {
                List<PgeneralFechaAonlineDTO> parametrosSEO = new List<PgeneralFechaAonlineDTO>();
                var _query = string.Empty;
                _query = "SELECT FechaInicioAsincronico FROM pla.T_PGeneral WHERE id = @Id";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { Id = IdProgramaGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    parametrosSEO = JsonConvert.DeserializeObject<List<PgeneralFechaAonlineDTO>>(respuestaDapper);
                }

                return parametrosSEO;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// Gretel Canasa
        /// <summary>
        ///  Obtiene la lista Programa Generales (activos) con el Id, Nombre, Area, SubArea, Categoria  ordenados descendentemente
        ///  para un programa.
        /// </summary>
        /// <returns></returns>
        public List<PGeneralMontoPagoPanelDTO> ListarProgramaGeneralParaMontoPago()
        {
            try
            {
                List<PGeneralMontoPagoPanelDTO> rpta = new();
                var query = @"SELECT Id,Nombre,IdArea,IdSubArea,IdCategoria FROM pla.V_TMontoPago_ProgramasGenerales WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralMontoPagoPanelDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ListarProgramaGeneralParaMontoPago: {ex.Message}", ex);
            }
        }
        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de programas generales padre
        /// </summary>
        /// <returns>List<ProgramaGeneralSubAreaFiltroDTO></returns>
        public List<ProgramaGeneralSubAreaFiltroDTO> ObtenerProgramaGeneralPadre(int? tipo)
        {
            try
            {
                var query = "";
                List<ProgramaGeneralSubAreaFiltroDTO> Listado = new List<ProgramaGeneralSubAreaFiltroDTO>();
                if (tipo.HasValue)
                {
                    query = $@"SELECT DISTINCT IdPGeneral AS Id, PGeneral AS Nombre, IdSubArea
						FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
						WHERE Estado = 1 AND RowNumber = 1 AND Tipo = 1;";
                }
                else
                {
                    query = $@"SELECT DISTINCT IdPGeneral AS Id, PGeneral AS Nombre, IdSubArea
						FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
						WHERE Estado = 1 AND RowNumber = 1;";
                }
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    Listado = JsonConvert.DeserializeObject<List<ProgramaGeneralSubAreaFiltroDTO>>(res);
                }
                return Listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Valida si el programa general es de tipo padre para indicar que se asigne proyecto de aplicacion
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> bool </returns>
        public bool ValidarPRogramaPadreParaProyectoAPlicacion(int idPGeneral)
        {
            string query = "SELECT IdPGeneral, TieneProyectoDeAplicacion, IdTipoPrograma FROM pla.V_ValidarProgramaGeneralProyectoAplicacion WHERE IdPGeneral=@IdPGeneral";
            string queryDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
            // retorna vacio o nulo significa que no es programa padre
            if (string.IsNullOrEmpty(queryDB) && queryDB.Equals("[]"))
                return false;
            else
                return true;
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 19/07/2023
        /// <summary>
        /// Obtiene los registros de Fecha, hora y estado sesion (Modulo Informacion Webinar)
        /// </summary>
        /// <param name="filtro">Objeto de clase WebinarReporteFiltroDTO</param>
        /// <returns>Lista de objetos de clase WebinarDDetalleSesionDTO</returns>
        public IEnumerable<WebinarDetalleSesionDTO> ObtenerWebinarPorFiltro(WebinarReporteFiltroDTO filtro)
        {
            try
            {
                var filtros = new
                {
                    ListaPGeneral = filtro.ListaPGeneral == null ? "" : string.Join(",", filtro.ListaPGeneral.Select(x => x)),
                    ListaPEspecifico = filtro.ListaPEspecifico == null ? "" : string.Join(",", filtro.ListaPEspecifico.Select(x => x)),
                    EstadoSesion = filtro.EstadoSesion,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    FechaPorDefecto = filtro.FechaPorDefecto,
                    CodigoMatricula = filtro.CodigoMatricula,
                    IdCentroCosto = filtro.IdCentroCosto
                };
                string query = string.Empty;
                query = "pla.SP_ObtenerInformacionSesionesWebinar";
                var WebinarDB = _dapperRepository.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(WebinarDB) && !WebinarDB.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<WebinarDetalleSesionDTO>>(WebinarDB);
                return new List<WebinarDetalleSesionDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public WebinarDetalleSesionDTO? ObtenerWebinarPorIdPEspecificoSesion(int IdPEspecificoSesion)
        {
            try
            {
                WebinarDetalleSesionDTO rpta = new WebinarDetalleSesionDTO();

                var _query = "pla.SP_ObtenerInformacionSesionWebinarPorIdEspecificoSesion @IdPEspecificoSesion";
                var query = _dapperRepository.FirstOrDefault(_query, new { IdPEspecificoSesion });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    rpta = JsonConvert.DeserializeObject<WebinarDetalleSesionDTO>(query);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// <summary>
        /// Obtiene el nombre del programa general por el id pespecifico
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns>Nombre del programa general</returns>
        public string ObtenerNombrePorIdPespecifico(int idPespecifico)
        {
            try
            {
                string _query = "SELECT Nombre AS Valor FROM pla.V_TPgeneral_NombrePorIdPespecifico WHERE IdPespecifico = @idPespecifico";
                string resultado = _dapperRepository.FirstOrDefault(_query, new { idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!.Valor;
                }
                return "";
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
        /// obtiene el idPagina de un programa general por centro de costo
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public PgeneralIdPaginaDTO ObtenerIdPagina(int idCentroCosto)
        {
            try
            {
                PgeneralIdPaginaDTO pgeneralIdPagina = new PgeneralIdPaginaDTO()
                {
                    Id = 0,
                    IdPagina = 0
                };
                string _query = "SELECT IdPagina FROM pla.V_ObtenerIdPagina WHERE IdCentroCosto = @idCentroCosto AND EstadoProgramaGeneral = 1 AND EstadoProgramaEspecifico = 1";
                var registrosDB = _dapperRepository.FirstOrDefault(_query, new { idCentroCosto });
                pgeneralIdPagina = JsonConvert.DeserializeObject<PgeneralIdPaginaDTO>(registrosDB);
                return pgeneralIdPagina;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

