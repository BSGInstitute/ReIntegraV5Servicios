using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: LogService
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de T_Log
    /// </summary>
    public class LogService : ILogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public LogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TLog, Log>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public Log Add(Log entidad)
        {
            try
            {
                var modelo = _unitOfWork.LogRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Log>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Log Update(Log entidad)
        {
            try
            {
                var modelo = _unitOfWork.LogRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Log>(modelo);
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
                _unitOfWork.LogRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Log> Add(List<Log> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LogRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Log>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Log> Update(List<Log> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LogRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Log>>(modelo);
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
                _unitOfWork.LogRepository.Delete(listadoIds, usuario);
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
