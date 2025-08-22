using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfMicroImagenTendenciaEndeudamientoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfMicroImagenTendenciaEndeudamiento
    /// </summary>
    public class DataCreditoDataInfMicroImagenTendenciaEndeudamientoService : IDataCreditoDataInfMicroImagenTendenciaEndeudamientoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfMicroImagenTendenciaEndeudamientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento, DataCreditoDataInfMicroImagenTendenciaEndeudamiento>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfMicroImagenTendenciaEndeudamiento Add(DataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroImagenTendenciaEndeudamiento>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataInfMicroImagenTendenciaEndeudamiento Update(DataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroImagenTendenciaEndeudamiento>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> Add(List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> Update(List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento>>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository.Delete(listadoIds, usuario);
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