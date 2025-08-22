using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DataCreditoDataTarjetaCreditoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoDataTarjetaCredito
    /// </summary>
    public class DataCreditoDataTarjetaCreditoRepository : GenericRepository<TDataCreditoDataTarjetaCredito>, IDataCreditoDataTarjetaCreditoRepository
    {
        private Mapper _mapper;
        public DataCreditoDataTarjetaCreditoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDataCreditoDataTarjetaCredito, DataCreditoDataTarjetaCredito>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDataCreditoDataTarjetaCredito MapeoEntidad(DataCreditoDataTarjetaCredito entidad)
        {
            try
            {
                //crea la entidad padre
                TDataCreditoDataTarjetaCredito modelo = _mapper.Map<TDataCreditoDataTarjetaCredito>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoDataTarjetaCredito Add(DataCreditoDataTarjetaCredito entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                base.Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoDataTarjetaCredito Update(DataCreditoDataTarjetaCredito entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;

                base.Update(actualizarEntidad);
                return actualizarEntidad;
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
        public IEnumerable<TDataCreditoDataTarjetaCredito> Add(IEnumerable<DataCreditoDataTarjetaCredito> listadoEntidad)
        {
            try
            {
                List<TDataCreditoDataTarjetaCredito> listado = new List<TDataCreditoDataTarjetaCredito>();
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

        public IEnumerable<TDataCreditoDataTarjetaCredito> Update(IEnumerable<DataCreditoDataTarjetaCredito> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDataCreditoDataTarjetaCredito> listado = new List<TDataCreditoDataTarjetaCredito>();
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
    }
}
