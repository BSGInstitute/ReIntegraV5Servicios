using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfMicroEndeudamientoActualService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfMicroEndeudamientoActual
    /// </summary>
    public class DataCreditoDataInfMicroEndeudamientoActualService : IDataCreditoDataInfMicroEndeudamientoActualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfMicroEndeudamientoActualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfMicroEndeudamientoActual, DataCreditoDataInfMicroEndeudamientoActual>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfMicroEndeudamientoActual Add(DataCreditoDataInfMicroEndeudamientoActual entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEndeudamientoActualRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroEndeudamientoActual>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataCreditoDataInfMicroEndeudamientoActual Update(DataCreditoDataInfMicroEndeudamientoActual entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEndeudamientoActualRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroEndeudamientoActual>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroEndeudamientoActualRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DataCreditoDataInfMicroEndeudamientoActual> Add(List<DataCreditoDataInfMicroEndeudamientoActual> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEndeudamientoActualRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroEndeudamientoActual>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DataCreditoDataInfMicroEndeudamientoActual> Update(List<DataCreditoDataInfMicroEndeudamientoActual> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEndeudamientoActualRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroEndeudamientoActual>>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroEndeudamientoActualRepository.Delete(listadoIds, usuario);
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