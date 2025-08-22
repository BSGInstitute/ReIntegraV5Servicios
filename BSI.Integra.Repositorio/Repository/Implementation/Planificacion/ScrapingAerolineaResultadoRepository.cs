using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ScrapingAerolineaResultadoRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_ScrapingAerolineaResultado
    /// </summary>
    public class ScrapingAerolineaResultadoRepository : GenericRepository<TScrapingAerolineaResultado>, IScrapingAerolineaResultadoRepository
    {
        private Mapper _mapper;

        public ScrapingAerolineaResultadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TScrapingAerolineaResultado, ScrapingAerolineaResultado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TScrapingAerolineaResultado MapeoEntidad(ScrapingAerolineaResultado entidad)
        {
            try
            {
                TScrapingAerolineaResultado modelo = _mapper.Map<TScrapingAerolineaResultado>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaResultado Add(ScrapingAerolineaResultado entidad)
        {
            try
            {
                var ScrapingAerolineaResultado = MapeoEntidad(entidad);
                base.Insert(ScrapingAerolineaResultado);
                return ScrapingAerolineaResultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaResultado Update(ScrapingAerolineaResultado entidad)
        {
            try
            {
                var ScrapingAerolineaResultado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ScrapingAerolineaResultado.RowVersion = entidadExistente.RowVersion;

                base.Update(ScrapingAerolineaResultado);
                return ScrapingAerolineaResultado;
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
        public IEnumerable<TScrapingAerolineaResultado> Add(IEnumerable<ScrapingAerolineaResultado> listadoEntidad)
        {
            try
            {
                List<TScrapingAerolineaResultado> listado = new List<TScrapingAerolineaResultado>();
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
        public IEnumerable<TScrapingAerolineaResultado> Update(IEnumerable<ScrapingAerolineaResultado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TScrapingAerolineaResultado> listado = new List<TScrapingAerolineaResultado>();
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
        /// Obtiene ScrapingAerolineaResultado por id.
        /// </summary>
        /// <returns>ScrapingAerolineaResultado</returns>
        public ScrapingAerolineaResultado? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                Precio,
		                IdScrapingAerolineaConfiguracion,
		                IdScrapingPagina,
		                IdCentroCosto,
		                IdPEspecifico,
		                NroGrupoCronograma,
		                NroSesionGrupo,
		                IdCiudadOrigen,
		                IdCiudadDestino,
		                EsActual,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
	                FROM pla.T_ScrapingAerolineaResultado
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ScrapingAerolineaResultado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#SARR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}



