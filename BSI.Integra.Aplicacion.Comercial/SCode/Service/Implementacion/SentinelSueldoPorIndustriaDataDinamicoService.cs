using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSueldoPorIndustriaDataDinamicoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoPorIndustriaDataDinamico
    /// </summary>
    public class SentinelSueldoPorIndustriaDataDinamicoService : ISentinelSueldoPorIndustriaDataDinamicoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSueldoPorIndustriaDataDinamicoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSueldoPorIndustriaDataDinamico, SentinelSueldoPorIndustriaDataDinamico>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSueldoPorIndustriaDataDinamicoDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSueldoPorIndustriaDataDinamico Add(SentinelSueldoPorIndustriaDataDinamico entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoPorIndustriaDataDinamico>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSueldoPorIndustriaDataDinamico Update(SentinelSueldoPorIndustriaDataDinamico entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoPorIndustriaDataDinamico>(modelo);
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
                _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoPorIndustriaDataDinamico> Add(List<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoPorIndustriaDataDinamico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoPorIndustriaDataDinamico> Update(List<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoPorIndustriaDataDinamico>>(modelo);
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
                _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SentinelSueldoPorIndustriaDataDinamico
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDataDinamicoDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataDinamicoDTO> ObtenerSentinelSueldoPorIndustriaDataDinamico()
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.ObtenerSentinelSueldoPorIndustriaDataDinamico();
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
        /// Obtiene registros de T_SentinelSueldoPorIndustriaDataDinamico para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDataDinamicoComboDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataDinamicoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.ObtenerCombo();
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
        /// Obtiene registros de T_SentinelSueldoPorIndustriaDataDinamico para mostrarse en combo.
        /// </summary>
        /// <param name="idCargo">Argumentos relacionados a la Empresa e Industria</param>
        /// <param name="idIndustria">Argumentos relacionados a la Empresa e Industria</param>
        /// <param name="idTamanio">Argumentos relacionados a la Empresa e Industria</param>
        /// <returns> List<SentinelSueldoPorIndustriaDataDinamicoComboDTO> </returns>
        public ValorIntDTO ObtenerValorSueldoIndustria(int idCargo, int idIndustria, int idTamanio)
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaDataDinamicoRepository.ObtenerValorSueldoIndustria(idCargo, idIndustria, idTamanio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
