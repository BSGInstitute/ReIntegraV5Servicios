using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FeedbackConfigurarGrupoPreguntaRepository
    /// Autor: Klebert Layme.
    /// Fecha: 27/05/2023
    /// <summary>
    /// Gestión general de TFeedbackGrupoPreguntaProgramaEspecifico
    /// </summary>
    public class FeedbackConfigurarGrupoPreguntaRepository : GenericRepository<TFeedbackConfigurarGrupoPreguntum>, IFeedbackConfigurarGrupoPreguntaRepository
    {
        private Mapper _mapper;

        public FeedbackConfigurarGrupoPreguntaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeedbackConfigurarGrupoPreguntum, FeedbackConfigurarGrupoPregunta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFeedbackConfigurarGrupoPreguntum MapeoEntidad(FeedbackConfigurarGrupoPregunta entidad)
        {
            try
            {
                TFeedbackConfigurarGrupoPreguntum modelo = _mapper.Map<TFeedbackConfigurarGrupoPreguntum>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFeedbackConfigurarGrupoPreguntum Add(FeedbackConfigurarGrupoPregunta entidad)
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
        public TFeedbackConfigurarGrupoPreguntum Update(FeedbackConfigurarGrupoPregunta entidad)
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

        public IEnumerable<TFeedbackConfigurarGrupoPreguntum> Add(IEnumerable<FeedbackConfigurarGrupoPregunta> listadoEntidad)
        {
            try
            {
                List<TFeedbackConfigurarGrupoPreguntum> listado = new List<TFeedbackConfigurarGrupoPreguntum>();
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

        public IEnumerable<TFeedbackConfigurarGrupoPreguntum> Update(IEnumerable<FeedbackConfigurarGrupoPregunta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFeedbackConfigurarGrupoPreguntum> listado = new List<TFeedbackConfigurarGrupoPreguntum>();
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
        /// Autor: Klebert Layme.
        /// Fecha: 27/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de FeedbackConfigurarGrupoPreguntaDTO.
        /// </summary>
        /// <returns> List<FeedbackConfigurarGrupoPreguntaDTO> </returns>
        public List<FeedbackConfigurarGrupoPreguntaDTO> ObtenerFeedbackConfigurar()
        {
            try
            {
                var _query = "SELECT Id,IdFeedbackConfigurar,Nombre FROM pla.V_listaFeedbackConfigurarGrupoPregunta";
                var areaTrabajoDB = _dapperRepository.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<FeedbackConfigurarGrupoPreguntaDTO>>(areaTrabajoDB)!;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Klebert Layme.
        /// Fecha: 27/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de FeedbackConfigurarGrupoPreguntaDTO.
        /// </summary>
        /// <returns> List<FeedbackConfigurarGrupoPreguntaDTO> </returns>
        public FeedbackConfigurarGrupoPreguntaDTO? ObtenerFeedbackConfigurarPorId(int id)
        {
            try
            {
                var query = "SELECT Id,IdFeedbackConfigurar,Nombre FROM pla.V_listaFeedbackConfigurarGrupoPregunta WHERE Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FeedbackConfigurarGrupoPreguntaDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Klebert Layme.
        /// Fecha: 27/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de FeedbackConfigurarGrupoPreguntaDTO por id.
        /// </summary>
        /// <returns> List<FeedbackConfigurarGrupoPreguntaDTO> </returns>
        public FeedbackConfigurarGrupoPregunta? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,IdFeedbackConfigurar,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion, Estado
                    FROM pla.T_FeedbackConfigurarGrupoPregunta
                    WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FeedbackConfigurarGrupoPregunta>(resultado)!;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
