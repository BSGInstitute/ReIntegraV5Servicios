using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataNaturalNacionalService
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoDataNaturalNacional
    /// </summary> 
    public class DataCreditoDataNaturalNacionalService : IDataCreditoDataNaturalNacionalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataNaturalNacionalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataNaturalNacional, DataCreditoDataNaturalNacional>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataNaturalNacional Add(DataCreditoDataNaturalNacional entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataNaturalNacionalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataNaturalNacional>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataNaturalNacional Update(DataCreditoDataNaturalNacional entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataNaturalNacionalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataNaturalNacional>(modelo);
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
                _unitOfWork.DataCreditoDataNaturalNacionalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataNaturalNacional> Add(List<DataCreditoDataNaturalNacional> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataNaturalNacionalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataNaturalNacional>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataNaturalNacional> Update(List<DataCreditoDataNaturalNacional> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataNaturalNacionalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataNaturalNacional>>(modelo);
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
                _unitOfWork.DataCreditoDataNaturalNacionalRepository.Delete(listadoIds, usuario);
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
