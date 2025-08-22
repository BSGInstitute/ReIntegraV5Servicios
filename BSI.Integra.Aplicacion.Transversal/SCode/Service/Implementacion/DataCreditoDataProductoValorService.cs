using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataProductoValorService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoDataProductoValor
    /// </summary>
    public class DataCreditoDataProductoValorService : IDataCreditoDataProductoValorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataProductoValorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataProductoValor, DataCreditoDataProductoValor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataProductoValor Add(DataCreditoDataProductoValor entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataProductoValorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataProductoValor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataProductoValor Update(DataCreditoDataProductoValor entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataProductoValorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataProductoValor>(modelo);
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
                _unitOfWork.DataCreditoDataProductoValorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataProductoValor> Add(List<DataCreditoDataProductoValor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataProductoValorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataProductoValor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataProductoValor> Update(List<DataCreditoDataProductoValor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataProductoValorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataProductoValor>>(modelo);
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
                _unitOfWork.DataCreditoDataProductoValorRepository.Delete(listadoIds, usuario);
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
