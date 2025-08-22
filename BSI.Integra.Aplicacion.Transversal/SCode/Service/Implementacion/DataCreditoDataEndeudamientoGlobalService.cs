using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataEndeudamientoGlobalService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoDataEndeudamientoGlobal
    /// </summary>
    public class DataCreditoDataEndeudamientoGlobalService : IDataCreditoDataEndeudamientoGlobalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataEndeudamientoGlobalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataEndeudamientoGlobal, DataCreditoDataEndeudamientoGlobal>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataEndeudamientoGlobal Add(DataCreditoDataEndeudamientoGlobal entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataEndeudamientoGlobalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataEndeudamientoGlobal>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataEndeudamientoGlobal Update(DataCreditoDataEndeudamientoGlobal entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataEndeudamientoGlobalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataEndeudamientoGlobal>(modelo);
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
                _unitOfWork.DataCreditoDataEndeudamientoGlobalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataEndeudamientoGlobal> Add(List<DataCreditoDataEndeudamientoGlobal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataEndeudamientoGlobalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataEndeudamientoGlobal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataEndeudamientoGlobal> Update(List<DataCreditoDataEndeudamientoGlobal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataEndeudamientoGlobalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataEndeudamientoGlobal>>(modelo);
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
                _unitOfWork.DataCreditoDataEndeudamientoGlobalRepository.Delete(listadoIds, usuario);
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
