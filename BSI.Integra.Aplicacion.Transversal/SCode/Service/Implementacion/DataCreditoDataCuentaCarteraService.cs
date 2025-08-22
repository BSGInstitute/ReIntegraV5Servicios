using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataCuentaCarteraService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoDataCuentaCartera
    /// </summary>
    public class DataCreditoDataCuentaCarteraService : IDataCreditoDataCuentaCarteraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataCuentaCarteraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataCuentaCartera, DataCreditoDataCuentaCartera>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataCuentaCartera Add(DataCreditoDataCuentaCartera entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaCarteraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataCuentaCartera>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataCuentaCartera Update(DataCreditoDataCuentaCartera entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaCarteraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataCuentaCartera>(modelo);
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
                _unitOfWork.DataCreditoDataCuentaCarteraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataCuentaCartera> Add(List<DataCreditoDataCuentaCartera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaCarteraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataCuentaCartera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataCuentaCartera> Update(List<DataCreditoDataCuentaCartera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataCuentaCarteraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataCuentaCartera>>(modelo);
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
                _unitOfWork.DataCreditoDataCuentaCarteraRepository.Delete(listadoIds, usuario);
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
