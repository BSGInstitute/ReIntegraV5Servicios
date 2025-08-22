using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ScrapingAerolineaResultadoDetalleRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_ScrapingAerolineaResultadoDetalle
    /// </summary>
    public class ScrapingAerolineaResultadoDetalleRepository : GenericRepository<TScrapingAerolineaResultadoDetalle>, IScrapingAerolineaResultadoDetalleRepository
    {
        private Mapper _mapper;

        public ScrapingAerolineaResultadoDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TScrapingAerolineaResultadoDetalle, ScrapingAerolineaResultadoDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TScrapingAerolineaResultadoDetalle MapeoEntidad(ScrapingAerolineaResultadoDetalle entidad)
        {
            try
            {
                TScrapingAerolineaResultadoDetalle modelo = _mapper.Map<TScrapingAerolineaResultadoDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaResultadoDetalle Add(ScrapingAerolineaResultadoDetalle entidad)
        {
            try
            {
                var ScrapingAerolineaResultadoDetalle = MapeoEntidad(entidad);
                base.Insert(ScrapingAerolineaResultadoDetalle);
                return ScrapingAerolineaResultadoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaResultadoDetalle Update(ScrapingAerolineaResultadoDetalle entidad)
        {
            try
            {
                var ScrapingAerolineaResultadoDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ScrapingAerolineaResultadoDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ScrapingAerolineaResultadoDetalle);
                return ScrapingAerolineaResultadoDetalle;
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
        public IEnumerable<TScrapingAerolineaResultadoDetalle> Add(IEnumerable<ScrapingAerolineaResultadoDetalle> listadoEntidad)
        {
            try
            {
                List<TScrapingAerolineaResultadoDetalle> listado = new List<TScrapingAerolineaResultadoDetalle>();
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
        public IEnumerable<TScrapingAerolineaResultadoDetalle> Update(IEnumerable<ScrapingAerolineaResultadoDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TScrapingAerolineaResultadoDetalle> listado = new List<TScrapingAerolineaResultadoDetalle>();
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
        /// Obtiene ScrapingAerolineaResultadoDetalle por id.
        /// </summary>
        /// <returns>ScrapingAerolineaResultadoDetalle</returns>
        public ScrapingAerolineaResultadoDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                    IdScrapingAerolineaResultado,
		                    NroVuelo,
		                    IdProveedor,
		                    NombreAerolinea,
		                    IdVueloTipoTramo,
		                    IdCiudadOrigen,
		                    IdCiudadDestino,
		                    EsIda,
		                    FechaSalida,
		                    FechaLlegada,
		                    Clase,
		                    AplicaMochila,
		                    AplicaEquipajeMano,
		                    AplicaEquipajeBodega,
		                    DuracionMinuto,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion,
		                    NombreCiudadOrigen,
		                    NombreCiudadDestino,
		                    IdPadre 
	                    FROM pla.T_ScrapingAerolineaResultadoDetalle
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ScrapingAerolineaResultadoDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#SARDR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}



