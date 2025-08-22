using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoConsultumService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de T_DataCreditoConsulta
    /// </summary>
    public class DataCreditoConsultumService : IDataCreditoConsultumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoConsultumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoConsultum, DataCreditoConsultum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoConsultum Add(DataCreditoConsultum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoConsultumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoConsultum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoConsultum Update(DataCreditoConsultum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoConsultumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoConsultum>(modelo);
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
                _unitOfWork.DataCreditoConsultumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoConsultum> Add(List<DataCreditoConsultum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoConsultumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoConsultum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoConsultum> Update(List<DataCreditoConsultum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoConsultumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoConsultum>>(modelo);
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
                _unitOfWork.DataCreditoConsultumRepository.Delete(listadoIds, usuario);
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
