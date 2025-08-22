using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CalidadProcesamientoService
    /// Autor: Gilmer  quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de CalidadProcesamiento
    /// </summary>
    public class CalidadProcesamientoService : ICalidadProcesamientoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CalidadProcesamientoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCalidadProcesamiento, CalidadProcesamiento>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public CalidadProcesamiento Add(CalidadProcesamiento entidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadProcesamientoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CalidadProcesamiento>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CalidadProcesamiento Update(CalidadProcesamiento entidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadProcesamientoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CalidadProcesamiento>(modelo);
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
                _unitOfWork.CalidadProcesamientoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CalidadProcesamiento> Add(List<CalidadProcesamiento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadProcesamientoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CalidadProcesamiento>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CalidadProcesamiento> Update(List<CalidadProcesamiento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadProcesamientoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CalidadProcesamiento>>(modelo);
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
                _unitOfWork.CalidadProcesamientoRepository.Delete(listadoIds, usuario);
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
