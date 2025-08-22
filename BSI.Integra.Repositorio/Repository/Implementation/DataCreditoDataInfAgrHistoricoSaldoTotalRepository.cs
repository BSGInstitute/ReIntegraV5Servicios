using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DataCreditoDataInfAgrHistoricoSaldoTotalRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de TDataCreditoDataInfAgrHistoricoSaldoTotal
    /// </summary>
    public class DataCreditoDataInfAgrHistoricoSaldoTotalRepository : GenericRepository<TDataCreditoDataInfAgrHistoricoSaldoTotal>, IDataCreditoDataInfAgrHistoricoSaldoTotalRepository
    {
        private Mapper _mapper;

        public DataCreditoDataInfAgrHistoricoSaldoTotalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDataCreditoDataInfAgrHistoricoSaldoTotal, DataCreditoDataInfAgrHistoricoSaldoTotal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDataCreditoDataInfAgrHistoricoSaldoTotal MapeoEntidad(DataCreditoDataInfAgrHistoricoSaldoTotal entidad)
        {
            try
            {
                //crea la entidad padre
                TDataCreditoDataInfAgrHistoricoSaldoTotal modelo = _mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTotal>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoDataInfAgrHistoricoSaldoTotal Add(DataCreditoDataInfAgrHistoricoSaldoTotal entidad)
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

        public TDataCreditoDataInfAgrHistoricoSaldoTotal Update(DataCreditoDataInfAgrHistoricoSaldoTotal entidad)
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
        public IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTotal> Add(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad)
        {
            try
            {
                List<TDataCreditoDataInfAgrHistoricoSaldoTotal> listado = new List<TDataCreditoDataInfAgrHistoricoSaldoTotal>();
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

        public IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTotal> Update(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDataCreditoDataInfAgrHistoricoSaldoTotal> listado = new List<TDataCreditoDataInfAgrHistoricoSaldoTotal>();
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
