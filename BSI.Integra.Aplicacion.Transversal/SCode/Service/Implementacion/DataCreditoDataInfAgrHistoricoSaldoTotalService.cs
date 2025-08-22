using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfAgrHistoricoSaldoTotalService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfAgrHistoricoSaldoTotal
    /// </summary>
    public class DataCreditoDataInfAgrHistoricoSaldoTotalService : IDataCreditoDataInfAgrHistoricoSaldoTotalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfAgrHistoricoSaldoTotalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfAgrHistoricoSaldoTotal, DataCreditoDataInfAgrHistoricoSaldoTotal>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfAgrHistoricoSaldoTotal Add(DataCreditoDataInfAgrHistoricoSaldoTotal entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTotalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrHistoricoSaldoTotal>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataInfAgrHistoricoSaldoTotal Update(DataCreditoDataInfAgrHistoricoSaldoTotal entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTotalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrHistoricoSaldoTotal>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTotalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrHistoricoSaldoTotal> Add(List<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTotalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrHistoricoSaldoTotal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrHistoricoSaldoTotal> Update(List<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTotalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrHistoricoSaldoTotal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTotalRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
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
