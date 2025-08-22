using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ProveedorCriterioCalificacionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_ProveedorCriterioCalificacion
    /// </summary>
    public class ProveedorCriterioCalificacionService : IProveedorCriterioCalificacionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProveedorCriterioCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProveedorCriterioCalificacion, ProveedorCriterioCalificacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProveedorCriterioCalificacion Add(ProveedorCriterioCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCriterioCalificacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCriterioCalificacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorCriterioCalificacion Update(ProveedorCriterioCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCriterioCalificacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCriterioCalificacion>(modelo);
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
                _unitOfWork.ProveedorCriterioCalificacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCriterioCalificacion> Add(List<ProveedorCriterioCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCriterioCalificacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCriterioCalificacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCriterioCalificacion> Update(List<ProveedorCriterioCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCriterioCalificacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCriterioCalificacion>>(modelo);
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
                _unitOfWork.ProveedorCriterioCalificacionRepository.Delete(listadoIds, usuario);
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
