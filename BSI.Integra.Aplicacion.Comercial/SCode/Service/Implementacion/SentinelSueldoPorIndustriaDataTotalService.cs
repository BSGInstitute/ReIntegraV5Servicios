using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSueldoPorIndustriaDataTotalService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoPorIndustriaDataTotal
    /// </summary>
    public class SentinelSueldoPorIndustriaDataTotalService : ISentinelSueldoPorIndustriaDataTotalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSueldoPorIndustriaDataTotalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSueldoPorIndustriaDataTotal, SentinelSueldoPorIndustriaDataTotal>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSueldoPorIndustriaDataTotalDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSueldoPorIndustriaDataTotal Add(SentinelSueldoPorIndustriaDataTotal entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoPorIndustriaDataTotal>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSueldoPorIndustriaDataTotal Update(SentinelSueldoPorIndustriaDataTotal entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSueldoPorIndustriaDataTotal>(modelo);
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
                _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoPorIndustriaDataTotal> Add(List<SentinelSueldoPorIndustriaDataTotal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoPorIndustriaDataTotal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSueldoPorIndustriaDataTotal> Update(List<SentinelSueldoPorIndustriaDataTotal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSueldoPorIndustriaDataTotal>>(modelo);
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
                _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SentinelSueldoPorIndustriaDataTotal
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDataTotalDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataTotalDTO> ObtenerSentinelSueldoPorIndustriaDataTotal()
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSentinelSueldoPorIndustriaDataTotal();
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
        /// Obtiene registros de T_SentinelSueldoPorIndustriaDataTotal para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDataTotalComboDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataTotalComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerCombo();
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
        /// Obtiene registros de T_SentinelSueldoPorIndustriaDataTotal para mostrarse en combo.
        /// </summary>
        /// <param name="idCargo">Id del Cargo</param>
        /// <param name="idIndustria">Id de la Industria</param>
        /// <returns> List<SentinelSueldoPorIndustriaDataTotalComboDTO> </returns>
        public ValorIntDTO ObtenerSueldoPorCargoIndustria(int idCargo, int idIndustria)
        {
            try
            {
                return _unitOfWork.SentinelSueldoPorIndustriaDataTotalRepository.ObtenerSueldoPorCargoIndustria(idCargo, idIndustria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
