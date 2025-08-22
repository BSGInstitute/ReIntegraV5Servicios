using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SemaforoFinancieroVariableService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_SemaforoFinancieroVariable
    /// </summary>
    public class SemaforoFinancieroVariableService : ISemaforoFinancieroVariableService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SemaforoFinancieroVariableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSemaforoFinancieroVariable, SemaforoFinancieroVariable>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SemaforoFinancieroVariable Add(SemaforoFinancieroVariable entidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroVariableRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SemaforoFinancieroVariable>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SemaforoFinancieroVariable Update(SemaforoFinancieroVariable entidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroVariableRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SemaforoFinancieroVariable>(modelo);
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
                _unitOfWork.SemaforoFinancieroVariableRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinancieroVariable> Add(List<SemaforoFinancieroVariable> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroVariableRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinancieroVariable>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinancieroVariable> Update(List<SemaforoFinancieroVariable> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroVariableRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinancieroVariable>>(modelo);
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
                _unitOfWork.SemaforoFinancieroVariableRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SemaforoFinancieroVariable
        /// </summary>
        /// <returns> List<SemaforoFinancieroVariableDTO> </returns>
        public IEnumerable<SemaforoFinancieroVariableDTO> ObtenerSemaforoFinancieroVariable()
        {
            return _unitOfWork.SemaforoFinancieroVariableRepository.ObtenerSemaforoFinancieroVariable();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SemaforoFinancieroVariable para mostrarse en combo.
        /// </summary>
        /// <returns> List<SemaforoFinancieroVariableComboDTO> </returns>
        public IEnumerable<SemaforoFinancieroVariableComboDTO> ObtenerCombo()
        {
            return _unitOfWork.SemaforoFinancieroVariableRepository.ObtenerCombo();
        }
    }
}
