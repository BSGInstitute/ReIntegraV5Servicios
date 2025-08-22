using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoDataInfAgrEvolucionDeudaTrimestreService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/09/2022
    /// <summary>
    /// Gestión general de DataCreditoDataInfAgrEvolucionDeudaTrimestre
    /// </summary>
    public class DataCreditoDataInfAgrEvolucionDeudaTrimestreService : IDataCreditoDataInfAgrEvolucionDeudaTrimestreService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoDataInfAgrEvolucionDeudaTrimestreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoDataInfAgrEvolucionDeudaTrimestre, DataCreditoDataInfAgrEvolucionDeudaTrimestre>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoDataInfAgrEvolucionDeudaTrimestre Add(DataCreditoDataInfAgrEvolucionDeudaTrimestre entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrEvolucionDeudaTrimestre>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataCreditoDataInfAgrEvolucionDeudaTrimestre Update(DataCreditoDataInfAgrEvolucionDeudaTrimestre entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoDataInfAgrEvolucionDeudaTrimestre>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> Add(List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrEvolucionDeudaTrimestre>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> Update(List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoDataInfAgrEvolucionDeudaTrimestre>>(modelo);
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
                _unitOfWork.DataCreditoDataInfAgrEvolucionDeudaTrimestreRepository.Delete(listadoIds, usuario);
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
