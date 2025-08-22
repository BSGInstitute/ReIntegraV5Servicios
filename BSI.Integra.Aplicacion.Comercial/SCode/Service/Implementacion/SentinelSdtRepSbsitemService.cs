using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSdtRepSbsitemService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtRepSbsitem
    /// </summary>
    public class SentinelSdtRepSbsitemService : ISentinelSdtRepSbsitemService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSdtRepSbsitemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSdtRepSbsitem, SentinelSdtRepSbsitem>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSdtRepSbsitemDTO, SentinelSdtRepSbsitem>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }

        public SentinelSdtRepSbsitemService()
        {
        }
        #region Metodos Base
        public SentinelSdtRepSbsitem Add(SentinelSdtRepSbsitem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtRepSbsitemRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtRepSbsitem>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSdtRepSbsitem Update(SentinelSdtRepSbsitem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtRepSbsitemRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtRepSbsitem>(modelo);
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
                _unitOfWork.SentinelSdtRepSbsitemRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtRepSbsitem> Add(List<SentinelSdtRepSbsitem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtRepSbsitemRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtRepSbsitem>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtRepSbsitem> Update(List<SentinelSdtRepSbsitem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtRepSbsitemRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtRepSbsitem>>(modelo);
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
                _unitOfWork.SentinelSdtRepSbsitemRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelSdtRepSbsitem
        /// </summary>
        /// <returns> List<SentinelSdtRepSbsitemDTO> </returns>
        public IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerSentinelSdtRepSbsitem()
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerSentinelSdtRepSbsitem();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSdtRepSbsitem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtRepSbsitemComboDTO> </returns>
        public IEnumerable<SentinelSdtRepSbsitemComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSdtRepSbsitem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtRepSbsitemComboDTO> </returns>
        public IEnumerable<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerLineaDeudaPorIdSentinel(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SentinelSdtRepSbsitem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id se Sentinel</param>
        /// <returns> List<SentinelSdtRepSbsitemDTO> </returns>
        public IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerPorIdSentinel(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea una lista de SentinelSdtRepSbsitemDTO a una lista de SentinelSdtRepSbsitem
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public IEnumerable<SentinelSdtRepSbsitem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtRepSbsitemDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<SentinelSdtRepSbsitem>>(items);
                if (entidades != null) entidades.ForEach(p => p.Estado = true);
                return entidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de com.SP_SentinelLineasDeudasByAlumno asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelLineaDeudaDatosAlumnoDTO> </returns>
        public List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeuda(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerLineaDeuda(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene la linea de deuda de un Contacto Por IdSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVigente(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerLineaDeudaVigente(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene la linea de deuda de un Contacto Por IdSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVencida(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerLineaDeudaVencida(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/11/2022
        /// <summary>
        /// Obtiene La Linea de Deuda Para Detalle de Sentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtRepSbsitemLineaDeudaDTO> ObtenerLineaDeudaSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtRepSbsitemRepository.ObtenerLineaDeudaSentinel(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
