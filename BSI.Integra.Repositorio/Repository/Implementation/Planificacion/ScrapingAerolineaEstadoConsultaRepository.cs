using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ScrapingAerolineaEstadoConsultaRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_ScrapingAerolineaEstadoConsulta
    /// </summary>
    public class ScrapingAerolineaEstadoConsultaRepository : GenericRepository<TScrapingAerolineaEstadoConsultum>, IScrapingAerolineaEstadoConsultaRepository
    {
        private Mapper _mapper;

        public ScrapingAerolineaEstadoConsultaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TScrapingAerolineaEstadoConsultum, ScrapingAerolineaEstadoConsulta>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TScrapingAerolineaEstadoConsultum MapeoEntidad(ScrapingAerolineaEstadoConsulta entidad)
        {
            try
            {
                TScrapingAerolineaEstadoConsultum modelo = _mapper.Map<TScrapingAerolineaEstadoConsultum>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaEstadoConsultum Add(ScrapingAerolineaEstadoConsulta entidad)
        {
            try
            {
                var ScrapingAerolineaEstadoConsulta = MapeoEntidad(entidad);
                base.Insert(ScrapingAerolineaEstadoConsulta);
                return ScrapingAerolineaEstadoConsulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TScrapingAerolineaEstadoConsultum Update(ScrapingAerolineaEstadoConsulta entidad)
        {
            try
            {
                var ScrapingAerolineaEstadoConsulta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ScrapingAerolineaEstadoConsulta.RowVersion = entidadExistente.RowVersion;

                base.Update(ScrapingAerolineaEstadoConsulta);
                return ScrapingAerolineaEstadoConsulta;
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
        public IEnumerable<TScrapingAerolineaEstadoConsultum> Add(IEnumerable<ScrapingAerolineaEstadoConsulta> listadoEntidad)
        {
            try
            {
                List<TScrapingAerolineaEstadoConsultum> listado = new List<TScrapingAerolineaEstadoConsultum>();
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
        public IEnumerable<TScrapingAerolineaEstadoConsultum> Update(IEnumerable<ScrapingAerolineaEstadoConsulta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TScrapingAerolineaEstadoConsultum> listado = new List<TScrapingAerolineaEstadoConsultum>();
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
        /// Obtiene ScrapingAerolineaEstadoConsulta por id.
        /// </summary>
        /// <returns>ScrapingAerolineaEstadoConsulta</returns>
        public ScrapingAerolineaEstadoConsulta? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                        SELECT Id,
		                    Nombre,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion 
	                    FROM pla.T_ScrapingAerolineaEstadoConsulta
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ScrapingAerolineaEstadoConsulta>(resultado)!;
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



