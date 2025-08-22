using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfAgrResumenSaldoSectorService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de TDataCreditoDataInfAgrResumenSaldoSector
    /// </summary>
    public class DataCreditoDataInfAgrResumenSaldoSectorService : IDataCreditoDataInfAgrResumenSaldoSectorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfAgrResumenSaldoSectorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfAgrResumenSaldoSector, DataCreditoDataInfAgrResumenSaldoSector>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfAgrResumenSaldoSector Add(DataCreditoDataInfAgrResumenSaldoSector entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenSaldoSectorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrResumenSaldoSector>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataInfAgrResumenSaldoSector Update(DataCreditoDataInfAgrResumenSaldoSector entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenSaldoSectorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrResumenSaldoSector>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrResumenSaldoSectorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrResumenSaldoSector> Add(List<DataCreditoDataInfAgrResumenSaldoSector> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenSaldoSectorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrResumenSaldoSector>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrResumenSaldoSector> Update(List<DataCreditoDataInfAgrResumenSaldoSector> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrResumenSaldoSectorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrResumenSaldoSector>>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrResumenSaldoSectorRepository.Delete(listadoIds, usuario);
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
