using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfMicroEvolucionDeudumService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfMicroEvolucionDeudum
    /// </summary>
    public class DataCreditoDataInfMicroEvolucionDeudumService : IDataCreditoDataInfMicroEvolucionDeudumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfMicroEvolucionDeudumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfMicroEvolucionDeudum, DataCreditoDataInfMicroEvolucionDeudum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfMicroEvolucionDeudum Add(DataCreditoDataInfMicroEvolucionDeudum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEvolucionDeudumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroEvolucionDeudum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataInfMicroEvolucionDeudum Update(DataCreditoDataInfMicroEvolucionDeudum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEvolucionDeudumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfMicroEvolucionDeudum>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroEvolucionDeudumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroEvolucionDeudum> Add(List<DataCreditoDataInfMicroEvolucionDeudum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEvolucionDeudumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroEvolucionDeudum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfMicroEvolucionDeudum> Update(List<DataCreditoDataInfMicroEvolucionDeudum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfMicroEvolucionDeudumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfMicroEvolucionDeudum>>(modelo);
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
                _unitOfWork.DataCreditoDataInfMicroEvolucionDeudumRepository.Delete(listadoIds, usuario);
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