using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ScrapingAerolineaConfiguracionRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_ScrapingAerolineaConfiguracion
    /// </summary>
    public class ScrapingAerolineaConfiguracionRepository : GenericRepository<TScrapingAerolineaConfiguracion>, IScrapingAerolineaConfiguracionRepository
    {
        private Mapper _mapper;

        public ScrapingAerolineaConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TScrapingAerolineaConfiguracion, ScrapingAerolineaConfiguracion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TScrapingAerolineaConfiguracion MapeoEntidad(ScrapingAerolineaConfiguracion entidad)
        {
            try
            {
                TScrapingAerolineaConfiguracion modelo = _mapper.Map<TScrapingAerolineaConfiguracion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaConfiguracion Add(ScrapingAerolineaConfiguracion entidad)
        {
            try
            {
                var ScrapingAerolineaConfiguracion = MapeoEntidad(entidad);
                base.Insert(ScrapingAerolineaConfiguracion);
                return ScrapingAerolineaConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaConfiguracion Update(ScrapingAerolineaConfiguracion entidad)
        {
            try
            {
                var ScrapingAerolineaConfiguracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ScrapingAerolineaConfiguracion.RowVersion = entidadExistente.RowVersion;

                base.Update(ScrapingAerolineaConfiguracion);
                return ScrapingAerolineaConfiguracion;
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
        public IEnumerable<TScrapingAerolineaConfiguracion> Add(IEnumerable<ScrapingAerolineaConfiguracion> listadoEntidad)
        {
            try
            {
                List<TScrapingAerolineaConfiguracion> listado = new List<TScrapingAerolineaConfiguracion>();
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
        public IEnumerable<TScrapingAerolineaConfiguracion> Update(IEnumerable<ScrapingAerolineaConfiguracion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TScrapingAerolineaConfiguracion> listado = new List<TScrapingAerolineaConfiguracion>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ScrapingAerolineaConfiguracion por id.
        /// </summary>
        /// <returns>ScrapingAerolineaConfiguracion</returns>
        public ScrapingAerolineaConfiguracion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                    IdPEspecifico AS IdPespecifico,
		                    IdCentroCosto,
		                    NroGrupoSesion,
		                    NroGrupoCronograma,
		                    IdScrapingAerolineaEstadoConsulta,
		                    IdCiudadOrigen,
		                    IdCiudadDestino,
		                    FechaIda,
		                    FechaRetorno,
		                    PrecisionIda,
		                    NroFrecuencia,
		                    TipoFrecuencia,
		                    TipoVuelo,
		                    FechaEjecucion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion,
		                    IdFur,
		                    PrecisionRetorno,
		                    TienePasajeComprado 
	                    FROM pla.T_ScrapingAerolineaConfiguracion
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ScrapingAerolineaConfiguracion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#SACR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}



