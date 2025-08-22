using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ExamenTestRepository : GenericRepository<TExamenTest>, IExamenTestRepository
    {
        private Mapper _mapper;
        public ExamenTestRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExamenTest, ExamenTest>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenTest, ExamenTestDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExamenTest, TExamenTest>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TExamenTest MapeoEntidad(ExamenTest entidad)
        {
            try
            {
                TExamenTest modelo = _mapper.Map<TExamenTest>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenTest Add(ExamenTest entidad)
        {
            try
            {
                var ExamenTest = MapeoEntidad(entidad);
                base.Insert(ExamenTest);
                return ExamenTest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TExamenTest Update(ExamenTest entidad)
        {
            try
            {
                var ExamenTest = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ExamenTest.RowVersion = entidadExistente.RowVersion;

                base.Update(ExamenTest);
                return ExamenTest;
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
        public IEnumerable<TExamenTest> Add(IEnumerable<ExamenTest> listadoEntidad)
        {
            try
            {
                List<TExamenTest> listado = new List<TExamenTest>();
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
        public IEnumerable<TExamenTest> Update(IEnumerable<ExamenTest> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TExamenTest> listado = new List<TExamenTest>();
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
        /// Obtiene un registro de T_ExamenTest por el Primary Key
        /// </summary>
        /// <returns>ExamenTest o Nulo</returns>
        public ExamenTest? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
		                Nombre,
		                NombreAbreviado,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EsCalificadoPorPostulante,
		                MostrarEvaluacionAgrupado,
		                MostrarEvaluacionPorGrupo,
		                MostrarEvaluacionPorComponente,
		                RequiereCentil,
		                IdFormulaPuntaje,
		                CalificarEvaluacion,
		                EsCalificacionAgrupada,
		                Factor,
		                IdEvaluacionCategoria
                    FROM gp.T_ExamenTest
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ExamenTest>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 23/10/2024
        /// <summary>
        /// Obtiene un registro de T_ExamenTest
        /// </summary>
        /// <returns>ExamenTest o Nulo</returns>
        public IEnumerable<ExamenTestResumidoDTO> Obtener()
        {
            try
            {
                IEnumerable<ExamenTestResumidoDTO> rpta = new List<ExamenTestResumidoDTO>();

                var query = @"
                    SELECT
                        Id,
                        Nombre,
                        NombreAbreviado
                    FROM gp.T_ExamenTest
                    WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ExamenTestResumidoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EvaluacionAgrupadaComponenteDTO> ObtenerEvaluacionAgrupado(int idEvaluacion)
        {
            try
            {
                List<EvaluacionAgrupadaComponenteDTO> EvaluacionGrupo = new List<EvaluacionAgrupadaComponenteDTO>();
                var campos = "IdAsignacionPreguntaExamen,IdComponente,NombreComponente,IdGrupoComponenteEvaluacion,NombreGrupoComponenteEvaluacion,IdEvaluacion,NombreEvaluacion,IdPregunta,EnunciadoPregunta,NroOrden ";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerEvaluacionAgrupadaExamen where IdEvaluacion=" + idEvaluacion + " order by IdEvaluacion, IdGrupoComponenteEvaluacion,IdComponente,NroOrden";
                var dataDB = _dapperRepository.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<EvaluacionAgrupadaComponenteDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public IEnumerable<ComboDTO> ObtenerComponentes(int idEvaluacion)
        {
            try
            {
                IEnumerable<ComboDTO> componentes = new List<ComboDTO>();

                var query = @"
                    SELECT Id, Nombre
                    FROM gp.T_Examen
                    WHERE Estado = 1 
                    AND IdExamenTest = @IdEvaluacion;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdEvaluacion = idEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    componentes = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return componentes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EstructuraBasicaDTO> ObtenerEvaluacionNoAsignadoProcesoSeleccion(int IdProcesoSeleccion)
        {
            try
            {
                List<EstructuraBasicaDTO> ProcesoSeleccion = new List<EstructuraBasicaDTO>();
                var listaProcesoDB = _dapperRepository.QuerySPDapper("gp.SP_EvaluacionesNoAsociadosConfiguracion", new { IdProcesoSeleccion });
                if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
                {
                    ProcesoSeleccion = JsonConvert.DeserializeObject<List<EstructuraBasicaDTO>>(listaProcesoDB);
                }
                return ProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EvaluacionAsignadoProcesoDTO> ObtenerEvaluacionAsignadoProcesoSeleccion(int IdProcesoSeleccion)
        {
            try
            {
                List<EvaluacionAsignadoProcesoDTO> ProcesoSeleccion = new List<EvaluacionAsignadoProcesoDTO>();
                var listaProcesoDB = _dapperRepository.QuerySPDapper("gp.SP_EvaluacionesAsociadosConfiguracion", new { IdProcesoSeleccion });
                if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
                {
                    ProcesoSeleccion = JsonConvert.DeserializeObject<List<EvaluacionAsignadoProcesoDTO>>(listaProcesoDB);
                }
                return ProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<NombreEvaluacionAgrupadaComponenteDTO> ObtenerNombreEvaluacionPuntaje(int IdProcesoSeleccion)
        {
            try
            {
                List<NombreEvaluacionAgrupadaComponenteDTO> EvaluacionGrupo = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                var campos = "IdProcesoSeleccion,CalificacionTotal,IdEvaluacion,NombreEvaluacion,IdGrupo,NombreGrupo,IdComponente,NombreComponente";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerExamenesProcesoSeleccion where IdProcesoSeleccion=" + IdProcesoSeleccion;
                var dataDB = _dapperRepository.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<NombreEvaluacionAgrupadaComponenteDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
