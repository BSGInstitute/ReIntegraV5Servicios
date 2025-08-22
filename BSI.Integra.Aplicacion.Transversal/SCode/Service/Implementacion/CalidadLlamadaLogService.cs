using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CalidadLlamadaLogService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_CalidadLlamadaLog
    /// </summary>
    public class CalidadLlamadaLogService : ICalidadLlamadaLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CalidadLlamadaLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCalidadLlamadaLog, CalidadLlamadaLog>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CalidadLlamadaLog Add(CalidadLlamadaLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadLlamadaLogRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CalidadLlamadaLog>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CalidadLlamadaLog Update(CalidadLlamadaLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadLlamadaLogRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CalidadLlamadaLog>(modelo);
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
                _unitOfWork.CalidadLlamadaLogRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CalidadLlamadaLog> Add(List<CalidadLlamadaLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadLlamadaLogRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CalidadLlamadaLog>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CalidadLlamadaLog> Update(List<CalidadLlamadaLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CalidadLlamadaLogRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CalidadLlamadaLog>>(modelo);
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
                _unitOfWork.CalidadLlamadaLogRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CalidadLlamadaLog
        /// </summary>
        /// <returns> List<CalidadLlamadaLogDTO> </returns>
        public IEnumerable<CalidadLlamadaLogDTO> ObtenerCalidadLlamadaLog()
        {
            try
            {
                return _unitOfWork.CalidadLlamadaLogRepository.ObtenerCalidadLlamadaLog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
