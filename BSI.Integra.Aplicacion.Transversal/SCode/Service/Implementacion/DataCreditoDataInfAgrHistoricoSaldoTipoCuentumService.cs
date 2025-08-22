using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfAgrHistoricoSaldoTipoCuentumService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfAgrHistoricoSaldoTipoCuentum
    /// </summary>
    public class DataCreditoDataInfAgrHistoricoSaldoTipoCuentumService : IDataCreditoDataInfAgrHistoricoSaldoTipoCuentumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfAgrHistoricoSaldoTipoCuentumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum, DataCreditoDataInfAgrHistoricoSaldoTipoCuentum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfAgrHistoricoSaldoTipoCuentum Add(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataCreditoDataInfAgrHistoricoSaldoTipoCuentum Update(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Add(List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Update(List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum>>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository.Delete(listadoIds, usuario);
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
