using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EstadoActividadDetalleRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_EstadoActividadDetalle
    /// </summary>
    public class EstadoActividadDetalleRepository : GenericRepository<TEstadoActividadDetalle>, IEstadoActividadDetalleRepository
    {
        private Mapper _mapper;

        public EstadoActividadDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoActividadDetalle, EstadoActividadDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TEstadoActividadDetalle MapeoEntidad(EstadoActividadDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TEstadoActividadDetalle modelo = _mapper.Map<TEstadoActividadDetalle>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoActividadDetalle Add(EstadoActividadDetalle entidad)
        {
            try
            {
                var EstadoActividadDetalle = MapeoEntidad(entidad);
                base.Insert(EstadoActividadDetalle);
                return EstadoActividadDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoActividadDetalle Update(EstadoActividadDetalle entidad)
        {
            try
            {
                var EstadoActividadDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoActividadDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoActividadDetalle);
                return EstadoActividadDetalle;
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


        public IEnumerable<TEstadoActividadDetalle> Add(IEnumerable<EstadoActividadDetalle> listadoEntidad)
        {
            try
            {
                List<TEstadoActividadDetalle> listado = new List<TEstadoActividadDetalle>();
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

        public IEnumerable<TEstadoActividadDetalle> Update(IEnumerable<EstadoActividadDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoActividadDetalle> listado = new List<TEstadoActividadDetalle>();
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


        public List<ComboDTO> ObtenerDetalleActividadFiltroCodigo()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre FROM mkt.T_EstadoActividadDetalle WHERE estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
