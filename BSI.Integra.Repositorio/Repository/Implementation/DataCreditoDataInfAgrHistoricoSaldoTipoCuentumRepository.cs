using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/09/2022
    /// <summary>
    /// Gestión general de TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum
    /// </summary>
    public class DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository : GenericRepository<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum>, IDataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository
    {
        private Mapper _mapper;
        public DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum, DataCreditoDataInfAgrHistoricoSaldoTipoCuentum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum MapeoEntidad(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad)
        {
            try
            {
                //crea la entidad padre
                TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum modelo = _mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum Add(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad)
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

        public TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum Update(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad)
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
        public IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Add(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad)
        {
            try
            {
                List<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listado = new List<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum>();
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

        public IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Update(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listado = new List<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum>();
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
