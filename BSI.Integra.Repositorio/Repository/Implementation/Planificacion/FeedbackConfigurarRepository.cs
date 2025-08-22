using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FeedbackConfigurarRepository
    /// Autor: Klebert Layme.
    /// Fecha: 29/05/2023
    /// <summary>
    /// Gestión general de T_FeedbackConfigurar
    /// </summary>
    public class FeedbackConfigurarRepository : GenericRepository<TFeedbackConfigurar>, IFeedbackConfigurarRepository
    {
        private Mapper _mapper;

        public FeedbackConfigurarRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeedbackConfigurar, FeedbackConfigurar>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalle>(MemberList.None).ReverseMap();

                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFeedbackConfigurar MapeoEntidad(FeedbackConfigurar entidad)
        {
            try
            {
                //crea la entidad padre
                TFeedbackConfigurar modelo = _mapper.Map<TFeedbackConfigurar>(entidad);

                //mapea los hijos
                if (entidad.FeedbackConfigurarDetalles != null && entidad.FeedbackConfigurarDetalles.Count > 0)
                {
                   modelo.TFeedbackConfigurarDetalles = _mapper.Map<List<TFeedbackConfigurarDetalle>>(entidad.FeedbackConfigurarDetalles);
                }


                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFeedbackConfigurar Add(FeedbackConfigurar entidad)
        {
            try
            {
                var AreaCapacitacion = MapeoEntidad(entidad);
                base.Insert(AreaCapacitacion);
                return AreaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFeedbackConfigurar Update(FeedbackConfigurar entidad)
        {
            try
            {
                var AreaCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AreaCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(AreaCapacitacion);
                return AreaCapacitacion;
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


        public IEnumerable<TFeedbackConfigurar> Add(IEnumerable<FeedbackConfigurar> listadoEntidad)
        {
            try
            {
                List<TFeedbackConfigurar> listado = new List<TFeedbackConfigurar>();
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

        public IEnumerable<TFeedbackConfigurar> Update(IEnumerable<FeedbackConfigurar> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFeedbackConfigurar> listado = new List<TFeedbackConfigurar>();
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

        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de FeedbacjConfigurarFiltro.
        /// </summary>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerTodoFeedbackConfigurarFiltro()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var _query = "SELECT Id,Nombre FROM pla.V_TFeedbackConfigurar_Filtro Where Estado = 1";
                var resultado = _dapperRepository.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }


        }
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FeedbackConfigurarFiltro.
        /// </summary>
        /// <returns> List<FeedbackConfigurarFiltroDTO> </returns>
        public IEnumerable<FeedbackConfigurarFiltroDTO> Obtener()
        { 
            try
            {
                List<FeedbackConfigurarFiltroDTO> rpta = new List<FeedbackConfigurarFiltroDTO>();
                var query = @"
                   SELECT Id,IdFeedbackTipo,NombreFeedbackTipo,Nombre FROM pla.V_TFeedbackConfigurar_Filtro Where Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FeedbackConfigurarFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_FeedbackConfigurar por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - FeedbackConfigurar </returns>
        public FeedbackConfigurar ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdFeedbackTipo,
                        Nombre,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM 
                        pla.T_FeedbackConfigurar
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FeedbackConfigurar>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EER-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }


    }
}
