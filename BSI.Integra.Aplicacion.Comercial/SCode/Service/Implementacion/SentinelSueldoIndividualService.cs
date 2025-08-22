using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSueldoIndividualService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoIndividual
    /// </summary>
    public class SentinelSueldoIndividualService : ISentinelSueldoIndividualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSueldoIndividualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSueldoIndividual, SentinelSueldoIndividual>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSueldoIndividualDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSueldoIndividual Add(SentinelSueldoIndividual entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoIndividualRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoIndividual>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSueldoIndividual Update(SentinelSueldoIndividual entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoIndividualRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoIndividual>(modelo);
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
                _unitOfWork.SentinelSueldoIndividualRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoIndividual> Add(List<SentinelSueldoIndividual> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoIndividualRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoIndividual>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoIndividual> Update(List<SentinelSueldoIndividual> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoIndividualRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoIndividual>>(modelo);
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
                _unitOfWork.SentinelSueldoIndividualRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 16/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelSueldoIndividual
        /// </summary>
        /// <returns> List<SentinelSueldoIndividualDTO> </returns>
        public IEnumerable<SentinelSueldoIndividualDTO> ObtenerSentinelSueldoIndividual()
        {
            try
            {
                return _unitOfWork.SentinelSueldoIndividualRepository.ObtenerSentinelSueldoIndividual();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSueldoIndividual para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSueldoIndividualComboDTO> </returns>
        public IEnumerable<SentinelSueldoIndividualComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSueldoIndividualRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Sueldo Promedio asociado a un DNI
        /// </summary>
        /// <param name="dni">Documento de Identificacion</param>
        /// <returns> List<SentinelSueldoIndividualComboDTO> </returns>
        public FloatDTO ObtenerSueldoPromedioPorDni(string dni)
        {
            try
            {
                return _unitOfWork.SentinelSueldoIndividualRepository.ObtenerSueldoPromedioPorDni(dni);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
