using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio para gestión de catálogo T_ModalidadPago
    /// </summary>
    public class ModalidadPagoRepository : GenericRepository<TModalidadPago>, IModalidadPagoRepository
    {
        private Mapper _mapper;

        public ModalidadPagoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModalidadPago, ModalidadPago>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TModalidadPago MapeoEntidad(ModalidadPago entidad)
        {
            try
            {
                TModalidadPago modelo = _mapper.Map<TModalidadPago>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModalidadPago Add(ModalidadPago entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModalidadPago Update(ModalidadPago entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modelo.RowVersion = entidadExistente.RowVersion;
                base.Update(modelo);
                return modelo;
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

        public IEnumerable<TModalidadPago> Add(IEnumerable<ModalidadPago> listadoEntidad)
        {
            try
            {
                List<TModalidadPago> listado = new List<TModalidadPago>();
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

        public IEnumerable<TModalidadPago> Update(IEnumerable<ModalidadPago> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModalidadPago> listado = new List<TModalidadPago>();
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

        public IEnumerable<ModalidadPagoDTO> ObtenerModalidadesPago()
        {
            try
            {
                List<ModalidadPagoDTO> listaTipos = new List<ModalidadPagoDTO>();

                var _query = @"SELECT Id, Nombre FROM fin.T_ModalidadPago WHERE Estado = 1";

                var queryResultado = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaTipos = JsonConvert.DeserializeObject<List<ModalidadPagoDTO>>(queryResultado);
                }
                return listaTipos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
