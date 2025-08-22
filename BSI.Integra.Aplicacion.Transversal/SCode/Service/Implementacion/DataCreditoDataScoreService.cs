using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Repositorio: DataCreditoDataScoreRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoDataScore
    /// </summary>
    public class DataCreditoDataScoreService : IDataCreditoDataScoreService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataScoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataScore, DataCreditoDataScore>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataScore Add(DataCreditoDataScore entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataScoreRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataScore>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataScore Update(DataCreditoDataScore entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataScoreRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataScore>(modelo);
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
                _unitOfWork.DataCreditoDataScoreRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataScore> Add(List<DataCreditoDataScore> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataScoreRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataScore>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataScore> Update(List<DataCreditoDataScore> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataScoreRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataScore>>(modelo);
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
                _unitOfWork.DataCreditoDataScoreRepository.Delete(listadoIds, usuario);
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
