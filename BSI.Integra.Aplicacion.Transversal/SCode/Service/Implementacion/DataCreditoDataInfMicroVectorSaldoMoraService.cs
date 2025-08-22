using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfMicroVectorSaldoMoraService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfMicroVectorSaldoMora
    /// </summary>
    public class DataCreditoDataInfMicroVectorSaldoMoraService : IDataCreditoDataInfMicroVectorSaldoMoraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfMicroVectorSaldoMoraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfMicroVectorSaldoMora, DataCreditoDataInfMicroVectorSaldoMora>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfMicroVectorSaldoMora Add(DataCreditoDataInfMicroVectorSaldoMora entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroVectorSaldoMoraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroVectorSaldoMora>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataCreditoDataInfMicroVectorSaldoMora Update(DataCreditoDataInfMicroVectorSaldoMora entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroVectorSaldoMoraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroVectorSaldoMora>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroVectorSaldoMoraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroVectorSaldoMora> Add(List<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroVectorSaldoMoraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroVectorSaldoMora>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroVectorSaldoMora> Update(List<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroVectorSaldoMoraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroVectorSaldoMora>>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroVectorSaldoMoraRepository.Delete(listadoIds, usuario);
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