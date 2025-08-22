using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfAgrComposicionPortafolioService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de TDataCreditoDataInfAgrComposicionPortafolio
    /// </summary>
    public class DataCreditoDataInfAgrComposicionPortafolioService : IDataCreditoDataInfAgrComposicionPortafolioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfAgrComposicionPortafolioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfAgrComposicionPortafolio, DataCreditoDataInfAgrComposicionPortafolio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfAgrComposicionPortafolio Add(DataCreditoDataInfAgrComposicionPortafolio entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrComposicionPortafolioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrComposicionPortafolio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataCreditoDataInfAgrComposicionPortafolio Update(DataCreditoDataInfAgrComposicionPortafolio entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrComposicionPortafolioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrComposicionPortafolio>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrComposicionPortafolioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DataCreditoDataInfAgrComposicionPortafolio> Add(List<DataCreditoDataInfAgrComposicionPortafolio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrComposicionPortafolioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrComposicionPortafolio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DataCreditoDataInfAgrComposicionPortafolio> Update(List<DataCreditoDataInfAgrComposicionPortafolio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrComposicionPortafolioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrComposicionPortafolio>>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrComposicionPortafolioRepository.Delete(listadoIds, usuario);
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
