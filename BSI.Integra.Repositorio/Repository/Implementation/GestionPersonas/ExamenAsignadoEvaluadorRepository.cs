using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ExamenAsignadoEvaluadorRepository : GenericRepository<TExamenAsignadoEvaluador>, IExamenAsignadoEvaluadorRepository
    {
        private Mapper _mapper;
        public ExamenAsignadoEvaluadorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExamenAsignadoEvaluador, ExamenAsignadoEvaluador>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenAsignadoEvaluador, ExamenAsignadoEvaluadorDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenAsignadoEvaluador, TExamenAsignadoEvaluador>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TExamenAsignadoEvaluador MapeoEntidad(ExamenAsignadoEvaluador entidad)
        {
            try
            {
                TExamenAsignadoEvaluador modelo = _mapper.Map<TExamenAsignadoEvaluador>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenAsignadoEvaluador Add(ExamenAsignadoEvaluador entidad)
        {
            try
            {
                var ExamenAsignadoEvaluador = MapeoEntidad(entidad);
                base.Insert(ExamenAsignadoEvaluador);
                return ExamenAsignadoEvaluador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenAsignadoEvaluador Update(ExamenAsignadoEvaluador entidad)
        {
            try
            {
                var ExamenAsignadoEvaluador = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ExamenAsignadoEvaluador.RowVersion = entidadExistente.RowVersion;

                base.Update(ExamenAsignadoEvaluador);
                return ExamenAsignadoEvaluador;
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
        public IEnumerable<TExamenAsignadoEvaluador> Add(IEnumerable<ExamenAsignadoEvaluador> listadoEntidad)
        {
            try
            {
                List<TExamenAsignadoEvaluador> listado = new List<TExamenAsignadoEvaluador>();
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
        public IEnumerable<TExamenAsignadoEvaluador> Update(IEnumerable<ExamenAsignadoEvaluador> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TExamenAsignadoEvaluador> listado = new List<TExamenAsignadoEvaluador>();
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
        /// Obtiene un registro de T_ExamenAsignadoEvaluador por el Primary Key
        /// </summary>
        /// <returns>ExamenAsignadoEvaluador o Nulo</returns>
        public ExamenAsignadoEvaluador? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPersonal,
	                    IdPostulante,
	                    IdExamen,
	                    IdProcesoSeleccion,
	                    EstadoExamen,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_ExamenAsignadoEvaluador
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenAsignadoEvaluador>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 17/06/2024
        /// <summary>
        /// Obtiene lista de evaluaciones de evaluador asignadas
        /// </summary>
        /// <param name="filtro"> Filtro de búsqueda </param>
        /// <returns> Lista de objeto DTO:  List<EvaluacionesAsignadasEvaluador> </returns>
        public List<EvaluacionesAsignadasEvaluadorDTO> ObtenerListaEvaluacionEvaluador(int idPostulante, int idProcesoSeleccion)
        {
            try
            {
                List<EvaluacionesAsignadasEvaluadorDTO> rpta = new List<EvaluacionesAsignadasEvaluadorDTO>();
                string query = @"SELECT Id, IdPostulante, IdProcesoSeleccion, IdExamen, IdGrupoComponenteEvaluacion, IdEvaluacion, Evaluacion, MostrarEvaluacionAgrupado, MostrarEvaluacionPorGrupo, MostrarEvaluacionPorComponente, EstadoExamen, RequiereTiempo, DuracionMinutos, Instrucciones
                    FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerExamenAsignado]
                    WHERE Estado = 1 AND IdPostulante = @IdPostulante AND IdProcesoSeleccion = @IdProcesoSeleccion";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPostulante = idPostulante, IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EvaluacionesAsignadasEvaluadorDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de preguntas asignadas con a un test
        /// </summary>
        /// <param name="test"> Información de evaluación </param>
        /// <returns> Lista de objeto DTO: List<PreguntaTestDTO> </returns>
        public List<PreguntaTestDTO> ObtenerPreguntasTest(TestInformacionDTO test)
        {
            try
            {
                var query = string.Empty;
                List<PreguntaTestDTO> listaTest = new List<PreguntaTestDTO>();

                if (test.MostrarEvaluacionAgrupado)
                {
                    query = @"
						SELECT IdEvaluacion, 
							   IdGrupoComponenteEvaluacion, 
							   IdExamenAsignado, 
							   IdExamen, 
							   IdPostulante, 
							   IdProcesoSeleccion, 
							   IdPregunta, 
							   EnunciadoPregunta, 
							   NroOrdenPregunta, 
							   IdPreguntaTipo, 
							   PreguntaTipo, 
							   IdTipoRespuesta, 
							   TipoRespuesta
						FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerPreguntas]
						WHERE IdProcesoSeleccion = @IdProcesoSeleccion
						AND IdEvaluacion = @IdTest AND Estado = 1
						AND IdPostulante = @IdPostulante ";
                }
                else if (test.MostrarEvaluacionPorGrupo)
                {
                    query = @"
						SELECT IdEvaluacion, 
							   IdGrupoComponenteEvaluacion, 
							   IdExamenAsignado, 
							   IdExamen, 
							   IdPostulante, 
							   IdProcesoSeleccion, 
							   IdPregunta, 
							   EnunciadoPregunta, 
							   NroOrdenPregunta, 
							   IdPreguntaTipo, 
							   PreguntaTipo,
							   IdTipoRespuesta,
							   TipoRespuesta
						FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerPreguntas]
						WHERE IdProcesoSeleccion = @IdProcesoSeleccion
						AND IdGrupoComponenteEvaluacion = @IdTest AND Estado = 1
						AND IdPostulante = @IdPostulante ";
                }
                else if (test.MostrarEvaluacionPorComponente)
                {
                    query = @"
						SELECT IdEvaluacion, 
							   IdGrupoComponenteEvaluacion, 
							   IdExamenAsignado, 
							   IdExamen, 
							   IdPostulante, 
							   IdProcesoSeleccion, 
							   IdPregunta, 
							   EnunciadoPregunta, 
							   NroOrdenPregunta, 
							   IdPreguntaTipo, 
							   PreguntaTipo, 
							   IdTipoRespuesta, 
							   TipoRespuesta
						FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerPreguntas]
						WHERE IdProcesoSeleccion = @IdProcesoSeleccion
						AND IdExamen = @IdTest AND Estado = 1
						AND IdPostulante = @IdPostulante ";
                }
                var repuesta = _dapperRepository.QueryDapper(query, new { test.IdProcesoSeleccion, test.IdTest, test.IdPostulante });

                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
                {
                    listaTest = JsonConvert.DeserializeObject<List<PreguntaTestDTO>>(repuesta)!;
                }

                return listaTest;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
		/// Fecha: 17/06/2024
		/// <summary>
		/// obtiene lista de respuesta de preguntas asignadas a un test
		/// </summary>
		/// <param name="IdExamen"> Id de Examen </param>
		/// <param name="IdPregunta"> Id de Pregunta</param>
		/// <returns> Lista de objeto DTO : List<RespuestasTestDTO> </returns>
		public List<RespuestasTestDTO> ObtenerListaPreguntasRespuestaTest(int IdExamen, int IdPregunta)
        {
            List<RespuestasTestDTO> rpta = new List<RespuestasTestDTO>();
            var query = "SELECT IdPregunta, IdRespuesta, NroOrden, EnunciadoRespuesta FROM gp.V_TAsignacionPreguntaExamen_ObtenerRespuestasPreguntas WHERE IdExamen = @IdExamen AND IdPregunta = @IdPregunta AND Estado = 1";
            var repuesta = _dapperRepository.QueryDapper(query, new { IdExamen, IdPregunta });

            if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<RespuestasTestDTO>>(repuesta)!;
            }
            return rpta;
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 03/11/2021
        /// <summary>
        /// Obtiene lista de evaluaciones del portal por proceso
        /// </summary>
        /// <param name="filtro"> Filtro de búsqueda </param>
        /// <returns> Lista de objeto DTO:  List<EvaluacionPortalPostulante> </returns>
        public List<EvaluacionPortalPostulante> ObtenerEvaluacionesPortalPostulante(EvaluacionPostulanteFiltroReporteDTO filtro)
        {
            try
            {
                List<EvaluacionPortalPostulante> rpta = new List<EvaluacionPortalPostulante>();
                string query = string.Empty;
                string resultado = string.Empty;
                if (filtro.FiltroPorPostulante == true)
                {
                    query = "SELECT IdProcesoSeleccion,IdPostulante,IdExamen,IdPespecifico,IdProgramaGeneral,IdAlumno,CantidadConfigurado,CantidadResuelto,PuntajeCurso FROM gp.V_ObtenerPuntajeCursoPostulante WHERE IdPostulante IN @IdPostulante AND IdExamen IS NOT NULL";
                    resultado = _dapperRepository.QueryDapper(query, new { IdPostulante = filtro.IdsPostulantes });
                }
                else
                {
                    query = "SELECT IdProcesoSeleccion,IdPostulante,IdExamen,IdPespecifico,IdProgramaGeneral,IdAlumno,CantidadConfigurado,CantidadResuelto,PuntajeCurso FROM gp.V_ObtenerPuntajeCursoPostulante WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamen IS NOT NULL";
                    resultado = _dapperRepository.QueryDapper(query, new { filtro.IdProcesoSeleccion });
                }

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EvaluacionPortalPostulante>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="idPostulante"></param> 
        /// <param name="idExamen"></param> 
        /// <summary>
        /// Obtiene un registro de T_ExamenAsignadoEvaluador por el Primary Key
        /// </summary>
        /// <returns>ExamenAsignadoEvaluador o Nulo</returns>
        public ExamenAsignadoEvaluador? ObtenerPorIdPostulanteIdExamen(int idPostulante, int idExamen)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPersonal,
	                    IdPostulante,
	                    IdExamen,
	                    IdProcesoSeleccion,
	                    EstadoExamen,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_ExamenAsignadoEvaluador
                    WHERE IdPostulante = @IdPostulante 
                        AND IdExamen = @IdExamen
                        AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulante = idPostulante, IdExamen = idExamen });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenAsignadoEvaluador>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }


        /// Autor: Eliot Arias F.
		/// Fecha: 05/11/2024
		/// <summary>
		/// Obtiene examenes asignados a evaluador
		/// </summary>
		/// <param name="idProcesoSeleccion"> Id de Proceso de Selección </param>
		/// <returns> Lista de Objeto DTO : List<ConfiguracionAsignacionExamenV2DTO> o null</returns>
		public List<ConfiguracionAsignacionExamenV2DTO> ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(int idProcesoSeleccion)
        {
            try
            {
                var query = "SELECT Id, IdProcesoSeleccion, IdEvaluacion, IdExamen, NroOrden FROM [gp].[V_TConfiguracionAsignacionExamenV2] WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1 AND EsCalificadoPorPostulante = 0";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<ConfiguracionAsignacionExamenV2DTO>>(resultado);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
