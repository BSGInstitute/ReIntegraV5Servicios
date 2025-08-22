using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SemaforoFinancieroDetalleVariableService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_SemaforoFinancieroDetalleVariable
    /// </summary>
    public class SemaforoFinancieroDetalleVariableService : ISemaforoFinancieroDetalleVariableService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SemaforoFinancieroDetalleVariableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSemaforoFinancieroDetalleVariable, SemaforoFinancieroDetalleVariable>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SemaforoFinancieroDetalleVariable Add(SemaforoFinancieroDetalleVariable entidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleVariableRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SemaforoFinancieroDetalleVariable>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SemaforoFinancieroDetalleVariable Update(SemaforoFinancieroDetalleVariable entidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleVariableRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SemaforoFinancieroDetalleVariable>(modelo);
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
                _unitOfWork.SemaforoFinancieroDetalleVariableRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinancieroDetalleVariable> Add(List<SemaforoFinancieroDetalleVariable> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleVariableRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinancieroDetalleVariable>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinancieroDetalleVariable> Update(List<SemaforoFinancieroDetalleVariable> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleVariableRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinancieroDetalleVariable>>(modelo);
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
                _unitOfWork.SemaforoFinancieroDetalleVariableRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SemaforoFinancieroDetalleVariable
        /// </summary>
        /// <returns> List<SemaforoFinancieroDetalleVariableDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleVariableDTO> ObtenerSemaforoFinancieroDetalleVariable()
        {
            return _unitOfWork.SemaforoFinancieroDetalleVariableRepository.ObtenerSemaforoFinancieroDetalleVariable();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SemaforoFinancieroDetalleVariable para mostrarse en combo.
        /// </summary>
        /// <returns> List<SemaforoFinancieroDetalleVariableComboDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleVariableComboDTO> ObtenerCombo()
        {
            return _unitOfWork.SemaforoFinancieroDetalleVariableRepository.ObtenerCombo();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros detallados de T_SemaforoFinancieroDetalleVariable asociados a un IdSemaforoFinancieroDetalle.
        /// </summary>
        /// <param name="idSemaforoFinancieroDetalle">Id de Semaforo Financiero Detalle</param>
        /// <returns> List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerSemaforoFinancieroDetalleVariablePorIdSemaforoFinancieroDetalle(int idSemaforoFinancieroDetalle)
        {
            return _unitOfWork.SemaforoFinancieroDetalleVariableRepository.ObtenerSemaforoFinancieroDetalleVariablePorIdSemaforoFinancieroDetalle(idSemaforoFinancieroDetalle);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de la tabla T_SemaforoFinancieroDetalleVariable por el id
        /// </summary>
        ///<param name="idSemaforoDetalleVariable">id del T_SemaforoFinancieroVariable/param>
        /// <returns> SemaforoFinancieroDetalleVariable </returns>
        public SemaforoFinancieroDetalleVariable ObtenerSemaforoDetalleVariablePorId(int idSemaforoDetalleVariable)
        {
            try
            {
                return _unitOfWork.SemaforoFinancieroDetalleVariableRepository.ObtenerSemaforoDetalleVariablePorId(idSemaforoDetalleVariable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
