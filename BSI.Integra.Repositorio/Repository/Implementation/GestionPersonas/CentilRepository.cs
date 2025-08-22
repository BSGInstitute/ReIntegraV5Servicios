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
    public class CentilRepository : GenericRepository<TCentil>, ICentilRepository
    {
        private Mapper _mapper;
        public CentilRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCentil, Centil>(MemberList.None).ReverseMap();
                cfg.CreateMap<Centil, CentilDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<Centil, TCentil>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCentil MapeoEntidad(Centil entidad)
        {
            try
            {
                TCentil modelo = _mapper.Map<TCentil>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCentil Add(Centil entidad)
        {
            try
            {
                var Centil = MapeoEntidad(entidad);
                Insert(Centil);
                return Centil;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCentil Update(Centil entidad)
        {
            try
            {
                var Centil = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Centil.RowVersion = entidadExistente.RowVersion;

                Update(Centil);
                return Centil;
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
        public IEnumerable<TCentil> Add(IEnumerable<Centil> listadoEntidad)
        {
            try
            {
                List<TCentil> listado = new List<TCentil>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TCentil> Update(IEnumerable<Centil> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCentil> listado = new List<TCentil>();
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

        public IEnumerable<StringDTO> ObtenerVersionesCentil()
        {
            try
            {
                IEnumerable<StringDTO> rpta = new List<StringDTO>();
                var query = @"SELECT DISTINCT Version AS Valor FROM gp.T_Centil";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<StringDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtiene centiles asignados a un grupo de evaluacion
        /// </summary>
        /// <param name="idExamenTest"></param>
        /// <returns></returns>
        public List<CentilDTO> ObtenerCentilesSinExamenTest()
        {
            try
            {
                List<CentilDTO> rpta = new List<CentilDTO>();
                var query = @"
                    SELECT Id,
		                IdExamenTest,
		                IdGrupoComponenteEvaluacion,
		                IdExamen,
		                IdSexo,
		                ValorMinimo,
		                ValorMaximo,
		                Centil,
		                CentilLetra,
		                CentilAdicional,
		                Version,
		                EsVigente 
		                FROM gp.T_Centil
		            WHERE Estado = 1
		                AND IdExamenTest IS NULL";

                var listaCentilDB = _dapperRepository.QueryDapper(query, null);
                if (!listaCentilDB.Contains("[]") && !string.IsNullOrEmpty(listaCentilDB))
                {
                    rpta = JsonConvert.DeserializeObject<List<CentilDTO>>(listaCentilDB)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CentilDTO> ObtenerCentilesPorEvaluacion(int idEvaluacion) 
        {
            try
            {
                List<CentilDTO> Centiles = new List<CentilDTO>();
                var campos = "Id,IdExamen,IdExamenTest,IdGrupoComponenteEvaluacion,IdSexo,ValorMinimo,ValorMaximo,Centil,CentilLetra,UsuarioModificacion";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerCentiles WHERE Estado=1 AND IdExamenTest=@idExamenTest";
                var listaCentilDB = _dapperRepository.QueryDapper(_query, new { IdExamenTest = idEvaluacion });
                if (!listaCentilDB.Contains("[]") && !string.IsNullOrEmpty(listaCentilDB))
                {
                    Centiles = JsonConvert.DeserializeObject<List<CentilDTO>>(listaCentilDB);
                }
                return Centiles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<CentilDTO> ObtenerGrupoEvaluacionDesglosadoPorComponente(int idGrupoComponenteEvaluacion)
        {
            try
            {
                List<CentilDTO> Centiles = new List<CentilDTO>();
                var campos = "Id, IdExamen,IdExamenTest,IdGrupoComponenteEvaluacion,IdSexo,ValorMinimo,ValorMaximo,Centil,CentilLetra,UsuarioModificacion";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerCentiles WHERE Estado=1 AND IdGrupoComponenteEvaluacion = @IdGrupoComponenteEvaluacion";
                var listaCentilDB = _dapperRepository.QueryDapper(_query, new { IdGrupoComponenteEvaluacion = idGrupoComponenteEvaluacion });
                if (!listaCentilDB.Contains("[]") && !string.IsNullOrEmpty(listaCentilDB))
                {
                    Centiles = JsonConvert.DeserializeObject<List<CentilDTO>>(listaCentilDB);
                }
                return Centiles;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene centiles asignados a una evaluacion
        /// </summary>
        /// <param name="idExamenTest"></param>
        /// <returns></returns>
        public List<CentilDTO> ObtenerCentilesEvaluacion(int idExamenTest)
        {
            try
            {
                List<CentilDTO> Centiles = new List<CentilDTO>();
                var campos = "Id,IdExamen,IdExamenTest,IdGrupoComponenteEvaluacion,IdSexo,ValorMinimo,ValorMaximo,Centil,CentilLetra,UsuarioModificacion";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerCentiles WHERE Estado=1 AND IdExamenTest=@idExamenTest";
                var listaCentil = _dapperRepository.QueryDapper(_query, new { IdExamenTest = idExamenTest });
                if (!listaCentil.Contains("[]") && !string.IsNullOrEmpty(listaCentil))
                {
                    Centiles = JsonConvert.DeserializeObject<List<CentilDTO>>(listaCentil);
                }
                return Centiles;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
