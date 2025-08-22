using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ProveedorSubCriterioCalificacionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_ProveedorSubCriterioCalificacion
    /// </summary>
    public class ProveedorSubCriterioCalificacionService : IProveedorSubCriterioCalificacionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProveedorSubCriterioCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProveedorSubCriterioCalificacion, ProveedorSubCriterioCalificacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProveedorSubCriterioCalificacion Add(ProveedorSubCriterioCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorSubCriterioCalificacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorSubCriterioCalificacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorSubCriterioCalificacion Update(ProveedorSubCriterioCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorSubCriterioCalificacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorSubCriterioCalificacion>(modelo);
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
                _unitOfWork.ProveedorSubCriterioCalificacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorSubCriterioCalificacion> Add(List<ProveedorSubCriterioCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorSubCriterioCalificacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorSubCriterioCalificacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorSubCriterioCalificacion> Update(List<ProveedorSubCriterioCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorSubCriterioCalificacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorSubCriterioCalificacion>>(modelo);
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
                _unitOfWork.ProveedorSubCriterioCalificacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProveedorSubCriterioCalificacion
        /// </summary>
        /// <returns> List<ProveedorSubCriterioCalificacionDTO> </returns>
        public object ObtenerSubCriterioCalificacion()
        {
            try
            {
                return _unitOfWork.ProveedorSubCriterioCalificacionRepository.ObtenerSubCriterioCalificacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
