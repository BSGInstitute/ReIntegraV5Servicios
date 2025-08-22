using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteCursoPortalNotasHistoricoRepository : GenericRepository<TCriterioEvaluacionProceso>, IPostulanteCursoPortalNotasHistoricoRepository
    {
        private Mapper _mapper;
        public PostulanteCursoPortalNotasHistoricoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, PostulanteCursoPortalNotasHistorico>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteCursoPortalNotasHistorico, PostulanteCursoPortalNotasHistoricoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteCursoPortalNotasHistorico, TCriterioEvaluacionProceso>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCriterioEvaluacionProceso MapeoEntidad(PostulanteCursoPortalNotasHistorico entidad)
        {
            try
            {
                TCriterioEvaluacionProceso modelo = _mapper.Map<TCriterioEvaluacionProceso>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCriterioEvaluacionProceso Add(PostulanteCursoPortalNotasHistorico entidad)
        {
            try
            {
                var PostulanteCursoPortalNotasHistorico = MapeoEntidad(entidad);
                base.Insert(PostulanteCursoPortalNotasHistorico);
                return PostulanteCursoPortalNotasHistorico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCriterioEvaluacionProceso Update(PostulanteCursoPortalNotasHistorico entidad)
        {
            try
            {
                var PostulanteCursoPortalNotasHistorico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteCursoPortalNotasHistorico.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteCursoPortalNotasHistorico);
                return PostulanteCursoPortalNotasHistorico;
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
        public IEnumerable<TCriterioEvaluacionProceso> Add(IEnumerable<PostulanteCursoPortalNotasHistorico> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacionProceso> listado = new List<TCriterioEvaluacionProceso>();
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
        public IEnumerable<TCriterioEvaluacionProceso> Update(IEnumerable<PostulanteCursoPortalNotasHistorico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacionProceso> listado = new List<TCriterioEvaluacionProceso>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_PostulanteCursoPortalNotasHistorico por el Primary Key
        /// </summary>
        /// <returns>PostulanteCursoPortalNotasHistorico o Nulo</returns>
        public PostulanteCursoPortalNotasHistorico? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPostulanteProcesoSeleccion,
	                    IdPGeneral,
	                    OrdenFilaCapitulo,
	                    OrdenFilaSesion,
	                    GrupoPregunta,
	                    Calificacion,
	                    IdUsuario,
	                    IdAlumno,
	                    IdPEspecifico,
	                    AccesoPrueba,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    IdMigracion
                    FROM gp.T_PostulanteCursoPortalNotasHistorico
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteCursoPortalNotasHistorico>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Repositorio: PostulanteCursoPortalNotasHistoricoRepositorio 
		/// Autor: Flavio R.M.F
		/// Fecha: 24/06/2024
		/// <summary>
		/// Obtiene lista de notas de postulante
		/// </summary>
		/// <param name="idAlumno">FK de T_Alumno</param>
		/// <param name="idPespecifico">FK de T_Pespecifico</param>
		/// <returns> List<PostulanteCursoPortalNotasHistoricoDTO> </returns>
		public List<PostulanteCursoPortalNotasHistoricoDTO> ObtenerNotasAnteriores(int idAlumno, int idPespecifico)
        {
            try
            {
                List<PostulanteCursoPortalNotasHistoricoDTO> rpta = new List<PostulanteCursoPortalNotasHistoricoDTO>();
                string query = string.Empty;
                query = @"SELECT  
							Id,
							IdPGeneral,
							OrdenFilaCapitulo,
							OrdenFilaSesion,
							GrupoPregunta,
							Calificacion,
							IdUsuario,
							IdAlumno,
							IdPespecifico,
							AccesoPrueba
						FROM gp.V_ObtenerNotasPortal_Postulante WHERE IdAlumno = @IdAlumno AND IdPespecifico = @IdPespecifico";
                var resultado = _dapperRepository.QueryDapper(query, new { IdAlumno = idAlumno, IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PostulanteCursoPortalNotasHistoricoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
		/// Fecha: 26/06/2024
		/// <summary>
		/// Obtiene lista de notas de postulante
		/// </summary>
		/// <param name="idPGeneral">FK de T_Pgeneral</param>
		/// <param name="idUsuario">Id de Usuario de portal</param>
		/// <returns>List<PostulanteVideoVisualizacionDTO></returns>
		public List<PostulanteVideoVisualizacionDTO> ObtenerVisualizacionVideoAnteriores(string idUsuario, int idPGeneral)
        {
            try
            {
                List<PostulanteVideoVisualizacionDTO> listaNotas = new List<PostulanteVideoVisualizacionDTO>();
                string query = string.Empty;
                query = @"SELECT  
							Id,
							IdPGeneral,
							IdPrincipal,
							IdUsuario
						FROM gp.V_ObtenerVisualizacionVideosPortal_Postulante WHERE IdUsuario = @IdUsuario AND IdPGeneral = @IdPGeneral";
                var respuesta = _dapperRepository.QueryDapper(query, new { IdUsuario = idUsuario, IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaNotas = JsonConvert.DeserializeObject<List<PostulanteVideoVisualizacionDTO>>(respuesta)!;
                }
                return listaNotas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
		/// Fecha: 24/06/2024
		/// <summary>
		/// Elimina físicamente registros anteriores de postulante notas y video
		/// </summary>
		/// <param name="idUsuario">Id de Usuario</param>
		/// <param name="idPGeneral">Id de Programa General </param>
		/// <param name="listaIdNota">Lista de Id de Notas</param>
		/// <param name="listaIdVideo">Lista de Id de Videos</param>
		/// <returns>bool</returns>
		public bool EliminarFisicamenteAnterioresNotas(string idUsuario, int idPGeneral, List<int> listaIdNota, List<int> listaIdVideo)
        {
            try
            {
                if (listaIdNota.Any() && listaIdVideo.Any() && idUsuario.Length > 0 && idPGeneral > 0)
                {
                    var filtros = new
                    {
                        IdUsuario = idUsuario,
                        IdPGeneral = idPGeneral,
                        ListaIdNotas = string.Join(",", listaIdNota),
                        ListaIdVideo = string.Join(",", listaIdVideo)
                    };
                    string sp = "gp.SP_EliminarFisicamenteNotasCursoPortal";
                    var respuesta = _dapperRepository.QuerySPFirstOrDefault(sp, filtros);
                    if (!string.IsNullOrEmpty(respuesta) && !respuesta.Equals("null"))
                    {
                        var rpta = JsonConvert.DeserializeObject<BoolDTO>(respuesta)!;
                        return rpta.Valor.GetValueOrDefault();
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
