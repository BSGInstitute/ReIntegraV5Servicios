using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoLogService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de DataCreditoLog
    /// </summary>

    public class DataCreditoLogService : IDataCreditoLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public DataCreditoLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoLog, DataCreditoLog>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoLog Add(DataCreditoLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoLogRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoLog>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoLog Update(DataCreditoLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoLogRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoLog>(modelo);
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
                _unitOfWork.DataCreditoLogRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoLog> Add(List<DataCreditoLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoLogRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoLog>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoLog> Update(List<DataCreditoLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoLogRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoLog>>(modelo);
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
                _unitOfWork.DataCreditoLogRepository.Delete(listadoIds, usuario);
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
