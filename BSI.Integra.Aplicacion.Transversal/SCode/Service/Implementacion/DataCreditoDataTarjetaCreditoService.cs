using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoBusquedaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de DataCreditoBusqueda
    /// </summary>
    public class DataCreditoDataTarjetaCreditoService : IDataCreditoDataTarjetaCreditoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataTarjetaCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataTarjetaCredito, DataCreditoDataTarjetaCredito>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataTarjetaCredito Add(DataCreditoDataTarjetaCredito entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataTarjetaCreditoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataTarjetaCredito>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataTarjetaCredito Update(DataCreditoDataTarjetaCredito entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataTarjetaCreditoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataTarjetaCredito>(modelo);
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
                _unitOfWork.DataCreditoDataTarjetaCreditoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataTarjetaCredito> Add(List<DataCreditoDataTarjetaCredito> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataTarjetaCreditoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataTarjetaCredito>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataTarjetaCredito> Update(List<DataCreditoDataTarjetaCredito> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataTarjetaCreditoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataTarjetaCredito>>(modelo);
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
                _unitOfWork.DataCreditoDataTarjetaCreditoRepository.Delete(listadoIds, usuario);
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
