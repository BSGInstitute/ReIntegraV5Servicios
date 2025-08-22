using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FeedbackTipoRepository
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 12/05/2023
    /// <summary>
    /// Gestión general de T_FeedbackTipo
    /// </summary>
    public class FeedbackTipoRepository: GenericRepository<TFeedbackTipo>, IFeedbackTipoRepository
    {
        private Mapper _mapper;

        public FeedbackTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeedbackTipo, FeedbackTipo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFeedbackTipo MapeoEntidad(FeedbackTipo entidad)
        {
            try
            {
                TFeedbackTipo modelo = _mapper.Map<TFeedbackTipo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFeedbackTipo Add(FeedbackTipo entidad)
        {
            try
            {
                var FeedbackTipo = MapeoEntidad(entidad);
                base.Insert(FeedbackTipo);
                return FeedbackTipo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFeedbackTipo Update(FeedbackTipo entidad)
        {
            try
            {
                var FeedbackTipo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FeedbackTipo.RowVersion = entidadExistente.RowVersion;

                base.Update(FeedbackTipo);
                return FeedbackTipo;
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
        public IEnumerable<TFeedbackTipo> Add(IEnumerable<FeedbackTipo> listadoEntidad)
        {
            try
            {
                List<TFeedbackTipo> listado = new List<TFeedbackTipo>();
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
        public IEnumerable<TFeedbackTipo> Update(IEnumerable<FeedbackTipo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFeedbackTipo> listado = new List<TFeedbackTipo>();
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
        /// Autor: Christian Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FeedbackTipo.
        /// </summary>
        /// <returns> List<FeedbackTipo> </returns>
        public FeedbackTipo ObtenerPorId(int id)
        {
            try
            {
                FeedbackTipo rpta = new();
                var query = @"SELECT
                                        Id,
                                        Nombre,
                                        Estado,
		                                UsuarioCreacion,
		                                UsuarioModificacion,
		                                FechaCreacion,
		                                FechaModificacion,
		                                RowVersion,
		                                IdMigracion
                                    FROM pla.T_FeedbackTipo WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FeedbackTipo>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FeedbackTipo.
        /// </summary>
        /// <returns> List<FeedbackTipo> </returns>
        public IEnumerable<FeedbackTipoDTO> Obtener()
        {
            try
            {
                List<FeedbackTipoDTO> rpta = new();
                var query = "SELECT Id, Nombre FROM pla.T_FeedbackTipo WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<FeedbackTipoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
