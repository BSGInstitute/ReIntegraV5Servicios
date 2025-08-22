using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfAgrResumenEndeudamientoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfAgrResumenEndeudamiento
    /// </summary>
    public class DataCreditoDataInfAgrResumenEndeudamientoService : IDataCreditoDataInfAgrResumenEndeudamientoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfAgrResumenEndeudamientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfAgrResumenEndeudamiento, DataCreditoDataInfAgrResumenEndeudamiento>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfAgrResumenEndeudamiento Add(DataCreditoDataInfAgrResumenEndeudamiento entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenEndeudamientoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrResumenEndeudamiento>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataInfAgrResumenEndeudamiento Update(DataCreditoDataInfAgrResumenEndeudamiento entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenEndeudamientoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrResumenEndeudamiento>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrResumenEndeudamientoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrResumenEndeudamiento> Add(List<DataCreditoDataInfAgrResumenEndeudamiento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenEndeudamientoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrResumenEndeudamiento>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrResumenEndeudamiento> Update(List<DataCreditoDataInfAgrResumenEndeudamiento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenEndeudamientoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrResumenEndeudamiento>>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrResumenEndeudamientoRepository.Delete(listadoIds, usuario);
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