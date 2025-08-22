using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DataCreditoDataInfMicroVectorSaldoMoraRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de TDataCreditoDataInfMicroVectorSaldoMora
    /// </summary>
    public class DataCreditoDataInfMicroVectorSaldoMoraRepository : GenericRepository<TDataCreditoDataInfMicroVectorSaldoMora>, IDataCreditoDataInfMicroVectorSaldoMoraRepository
    {
        private Mapper _mapper;
        public DataCreditoDataInfMicroVectorSaldoMoraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDataCreditoDataInfMicroVectorSaldoMora, DataCreditoDataInfMicroVectorSaldoMora>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDataCreditoDataInfMicroVectorSaldoMora MapeoEntidad(DataCreditoDataInfMicroVectorSaldoMora entidad)
        {
            try
            {
                //crea la entidad padre
                TDataCreditoDataInfMicroVectorSaldoMora modelo = _mapper.Map<TDataCreditoDataInfMicroVectorSaldoMora>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoDataInfMicroVectorSaldoMora Add(DataCreditoDataInfMicroVectorSaldoMora entidad)
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
        public TDataCreditoDataInfMicroVectorSaldoMora Update(DataCreditoDataInfMicroVectorSaldoMora entidad)
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
        public IEnumerable<TDataCreditoDataInfMicroVectorSaldoMora> Add(IEnumerable<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad)
        {
            try
            {
                List<TDataCreditoDataInfMicroVectorSaldoMora> listado = new List<TDataCreditoDataInfMicroVectorSaldoMora>();
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
        public IEnumerable<TDataCreditoDataInfMicroVectorSaldoMora> Update(IEnumerable<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDataCreditoDataInfMicroVectorSaldoMora> listado = new List<TDataCreditoDataInfMicroVectorSaldoMora>();
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
