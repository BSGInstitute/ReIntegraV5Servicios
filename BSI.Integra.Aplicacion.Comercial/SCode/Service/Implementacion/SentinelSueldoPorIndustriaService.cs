using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSueldoPorIndustriaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoPorIndustria
    /// </summary>
    public class SentinelSueldoPorIndustriaService : ISentinelSueldoPorIndustriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSueldoPorIndustriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSueldoPorIndustrium, SentinelSueldoPorIndustria>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSueldoPorIndustriaDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSueldoPorIndustria Add(SentinelSueldoPorIndustria entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoPorIndustria>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSueldoPorIndustria Update(SentinelSueldoPorIndustria entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoPorIndustria>(modelo);
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
                _unitOfWork.SentinelSueldoPorIndustriaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoPorIndustria> Add(List<SentinelSueldoPorIndustria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoPorIndustria>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoPorIndustria> Update(List<SentinelSueldoPorIndustria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoPorIndustria>>(modelo);
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
                _unitOfWork.SentinelSueldoPorIndustriaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SentinelSueldoPorIndustria
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDTO> ObtenerSentinelSueldoPorIndustria()
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaRepository.ObtenerSentinelSueldoPorIndustria();
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
        /// Obtiene registros de T_SentinelSueldoPorIndustria para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaComboDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaRepository.ObtenerCombo();
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
        /// <param name="idCargo">Id del Cargo</param>
        /// <param name="idIndustria">Id de la Industria</param>
        /// </summary>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerTipoSueldoIndustria(int idCargo, int idIndustria)
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaRepository.ObtenerTipoSueldoIndustria(idCargo, idIndustria);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
