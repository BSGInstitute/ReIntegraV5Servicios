using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfMicroAnalisisVectorService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfMicroAnalisisVector
    /// </summary>
    public class DataCreditoDataInfMicroAnalisisVectorService : IDataCreditoDataInfMicroAnalisisVectorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfMicroAnalisisVectorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfMicroAnalisisVector, DataCreditoDataInfMicroAnalisisVector>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfMicroAnalisisVector Add(DataCreditoDataInfMicroAnalisisVector entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroAnalisisVectorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroAnalisisVector>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataInfMicroAnalisisVector Update(DataCreditoDataInfMicroAnalisisVector entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroAnalisisVectorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroAnalisisVector>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroAnalisisVectorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroAnalisisVector> Add(List<DataCreditoDataInfMicroAnalisisVector> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroAnalisisVectorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroAnalisisVector>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroAnalisisVector> Update(List<DataCreditoDataInfMicroAnalisisVector> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroAnalisisVectorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroAnalisisVector>>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroAnalisisVectorRepository.Delete(listadoIds, usuario);
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