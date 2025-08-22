using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio
    /// </summary>
    public class DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioService : IDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio, DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio Add(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio Update(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> Add(List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> Update(List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository.Delete(listadoIds, usuario);
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
