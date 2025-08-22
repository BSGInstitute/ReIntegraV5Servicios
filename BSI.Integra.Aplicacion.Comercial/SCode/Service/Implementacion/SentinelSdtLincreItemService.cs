using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSdtLincreItemService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtLincreItem
    /// </summary>
    public class SentinelSdtLincreItemService : ISentinelSdtLincreItemService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSdtLincreItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSdtLincreItem, SentinelSdtLincreItem>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSdtLincreItemDTO, SentinelSdtLincreItem>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }

        public SentinelSdtLincreItemService()
        {
        }
        #region Metodos Base
        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.SentinelSdtLincreItemRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSdtLincreItem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelLineaCreditoDatosAlumnoDTO> </returns>
        public IEnumerable<SentinelLineaCreditoDatosAlumnoDTO> ObtenerLineaCreditoPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtLincreItemRepository.ObtenerLineaCreditoPorIdSentinel(idSentinel);
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
        /// Obtiene los registros de T_SentinelSdtLincreItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtLincreItemDTO> </returns>
        public IEnumerable<SentinelSdtLincreItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtLincreItemRepository.ObtenerPorIdSentinel(idSentinel);
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
        /// Mapea una lista de SentinelSdtLincreItemDTO a una lista de SentinelSdtLincreItem
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<SentinelSdtLincreItem> </returns>
        public IEnumerable<SentinelSdtLincreItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtLincreItemDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<SentinelSdtLincreItem>>(items);
                if (entidades != null) entidades.ForEach(p => p.Estado = true);
                return entidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SentinelSdtLincreItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtLincreItemDTO> </returns>
        public List<AlumnosSentinelLineasCreditoDTO> ObtenerLineaDeCredito(int idSentinel)
        {

            try
            {
                return _unitOfWork.SentinelSdtLincreItemRepository.ObtenerLineaDeCredito(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
