using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataCuentaAhorroService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoDataCuentaAhorro
    /// </summary>
    public class DataCreditoDataCuentaAhorroService : IDataCreditoDataCuentaAhorroService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataCuentaAhorroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataCuentaAhorro, DataCreditoDataCuentaAhorro>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataCuentaAhorro Add(DataCreditoDataCuentaAhorro entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaAhorroRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataCuentaAhorro>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataCuentaAhorro Update(DataCreditoDataCuentaAhorro entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaAhorroRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataCuentaAhorro>(modelo);
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
                _unitOfWork.DataCreditoDataCuentaAhorroRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataCuentaAhorro> Add(List<DataCreditoDataCuentaAhorro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaAhorroRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataCuentaAhorro>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataCuentaAhorro> Update(List<DataCreditoDataCuentaAhorro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaAhorroRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataCuentaAhorro>>(modelo);
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
                _unitOfWork.DataCreditoDataCuentaAhorroRepository.Delete(listadoIds, usuario);
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
