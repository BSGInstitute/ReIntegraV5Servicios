using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionOportunidadLogService
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de T_AsignacionOportunidadLog
    /// </summary>
    public class AsignacionOportunidadLogService : IAsignacionOportunidadLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AsignacionOportunidadLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionOportunidadLog, AsignacionOportunidadLog>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public AsignacionOportunidadLog Add(AsignacionOportunidadLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadLogRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionOportunidadLog>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionOportunidadLog Update(AsignacionOportunidadLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadLogRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionOportunidadLog>(modelo);
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
                _unitOfWork.AsignacionOportunidadLogRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionOportunidadLog> Add(List<AsignacionOportunidadLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadLogRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionOportunidadLog>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionOportunidadLog> Update(List<AsignacionOportunidadLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionOportunidadLogRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionOportunidadLog>>(modelo);
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
                _unitOfWork.AsignacionOportunidadLogRepository.Delete(listadoIds, usuario);
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
