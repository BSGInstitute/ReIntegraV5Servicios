using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteFlujoCongeladoPorDiumService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteFlujoCongeladoPorDium
    /// </summary>
    public class ReporteFlujoCongeladoPorDiumService : IReporteFlujoCongeladoPorDiumService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteFlujoCongeladoPorDiumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReporteFlujoCongeladoPorDium, ReporteFlujoCongeladoPorDium>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.ReporteFlujoCongeladoPorDiumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteFlujoCongeladoPorDium> Add(List<ReporteFlujoCongeladoPorDium> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReporteFlujoCongeladoPorDiumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReporteFlujoCongeladoPorDium>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteFlujoCongeladoPorDium> Update(List<ReporteFlujoCongeladoPorDium> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReporteFlujoCongeladoPorDiumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReporteFlujoCongeladoPorDium>>(modelo);
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
                _unitOfWork.ReporteFlujoCongeladoPorDiumRepository.Delete(listadoIds, usuario);
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
