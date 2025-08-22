using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class GrupoComponenteEvaluacionRepository : GenericRepository<TGrupoComponenteEvaluacion>, IGrupoComponenteEvaluacionRepository
    {
        private Mapper _mapper;
        public GrupoComponenteEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoComponenteEvaluacion, GrupoComponenteEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<GrupoComponenteEvaluacion, GrupoComponenteEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<GrupoComponenteEvaluacion, TGrupoComponenteEvaluacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGrupoComponenteEvaluacion MapeoEntidad(GrupoComponenteEvaluacion entidad)
        {
            try
            {
                TGrupoComponenteEvaluacion modelo = _mapper.Map<TGrupoComponenteEvaluacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TGrupoComponenteEvaluacion Add(GrupoComponenteEvaluacion entidad)
        {
            try
            {
                var GrupoComponenteEvaluacion = MapeoEntidad(entidad);
                base.Insert(GrupoComponenteEvaluacion);
                return GrupoComponenteEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TGrupoComponenteEvaluacion Update(GrupoComponenteEvaluacion entidad)
        {
            try
            {
                var GrupoComponenteEvaluacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GrupoComponenteEvaluacion.RowVersion = entidadExistente.RowVersion;

                base.Update(GrupoComponenteEvaluacion);
                return GrupoComponenteEvaluacion;
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
        public IEnumerable<TGrupoComponenteEvaluacion> Add(IEnumerable<GrupoComponenteEvaluacion> listadoEntidad)
        {
            try
            {
                List<TGrupoComponenteEvaluacion> listado = new List<TGrupoComponenteEvaluacion>();
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
        public IEnumerable<TGrupoComponenteEvaluacion> Update(IEnumerable<GrupoComponenteEvaluacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGrupoComponenteEvaluacion> listado = new List<TGrupoComponenteEvaluacion>();
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
        /// Obtiene un registro de T_GrupoComponenteEvaluacion por el Primary Key
        /// </summary>
        /// <returns>GrupoComponenteEvaluacion o Nulo</returns>
        public GrupoComponenteEvaluacion? ObtenerPorId(int id)
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
                    FROM gp.T_GrupoComponenteEvaluacion
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<GrupoComponenteEvaluacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene lista de calificacion y centiles por IdProcesoSeleccion
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerGrupoPorIds(List<int> idsGrupos)
        {
            try
            {
                var query = $@"
                    SELECT Id,
	                    Nombre
                    FROM gp.T_GrupoComponenteEvaluacion
                    WHERE Id IN @IdsGrupos AND Estado = 1";
                var res = _dapperRepository.QueryDapper(query, new { IdsGrupos = idsGrupos });
                return JsonConvert.DeserializeObject<List<ComboDTO>>(res)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ComboDTO> ObtenerGrupoPorIdEvaluacion(int idEvaluacion)
        {
            try
            {
                var query = $@"
                    SELECT Id,
	                    Nombre
                    FROM gp.T_GrupoComponenteEvaluacion
                    WHERE IdEvaluacion = @IdEvaluacion AND Estado = 1";
                var res = _dapperRepository.QueryDapper(query, new { IdEvaluacion = idEvaluacion });
                return JsonConvert.DeserializeObject<List<ComboDTO>>(res)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public List<GrupoEvaluacionDTO> ObtenerGrupoEvaluacion(int idEvaluacion)
        {
            try
            {
                List<GrupoEvaluacionDTO> EvaluacionGrupo = new List<GrupoEvaluacionDTO>();
                var campos = "Id,Nombre,IdEvaluacion,IdExamen,NombreExamen,IdFormula,Factor,RequiereCentil ";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerGrupoComponenteEvaluacion where IdEvaluacion=" + idEvaluacion;
                var dataDB = _dapperRepository.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<GrupoEvaluacionDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<GrupoComponenteDTO> ObtenerGrupoEvaluacionDesglosadoPorComponente(int idEvaluacion)
        {
            try
            {
                List<GrupoComponenteDTO> EvaluacionGrupo = new List<GrupoComponenteDTO>();
                var campos = "Id,Nombre,IdEvaluacion,IdExamen,NombreExamen,FactorComponente as Factor,RequiereCentil ";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerGrupoComponenteEvaluacion where IdEvaluacion=" + idEvaluacion;
                var dataDB = _dapperRepository.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<GrupoComponenteDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //parte de victor

        public IEnumerable<ComboDTO> ObtenerComboPorId(int idExamenTest)
        {
            try
            {
                IEnumerable<ComboDTO> listagrupo = new List<ComboDTO>();

                var query = @"SELECT * 
                            FROM gp.T_Examen ex
                            INNER JOIN gp.T_GrupoComponenteEvaluacion gce
                            ON ex.IdGrupoComponenteEvaluacion = gce.Id 
                            WHERE ex.IdExamenTest = @IdExamenTest
                              AND ex.Estado = 1
                              AND gce.Estado = 1;
                            ";
                var resultado = _dapperRepository.QueryDapper(query, new { IdExamenTest = idExamenTest });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listagrupo = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return listagrupo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
