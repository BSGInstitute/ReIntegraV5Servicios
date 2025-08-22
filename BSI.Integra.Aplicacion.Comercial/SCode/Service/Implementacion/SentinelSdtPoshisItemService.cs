using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSdtPoshisItemService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtPoshisItem
    /// </summary>
    public class SentinelSdtPoshisItemService : ISentinelSdtPoshisItemService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSdtPoshisItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSdtPoshisItem, SentinelSdtPoshisItem>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSdtPoshisItemDTO, SentinelSdtPoshisItem>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSdtPoshisItem Add(SentinelSdtPoshisItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtPoshisItemRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtPoshisItem>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSdtPoshisItem Update(SentinelSdtPoshisItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtPoshisItemRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtPoshisItem>(modelo);
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
                _unitOfWork.SentinelSdtPoshisItemRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtPoshisItem> Add(List<SentinelSdtPoshisItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtPoshisItemRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtPoshisItem>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtPoshisItem> Update(List<SentinelSdtPoshisItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtPoshisItemRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtPoshisItem>>(modelo);
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
                _unitOfWork.SentinelSdtPoshisItemRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SentinelSdtPoshisItem
        /// </summary>
        /// <returns> List<SentinelSdtPoshisItemDTO> </returns>
        public IEnumerable<SentinelSdtPoshisItemDTO> ObtenerSentinelSdtPoshisItem()
        {
            try
            {
                return _unitOfWork.SentinelSdtPoshisItemRepository.ObtenerSentinelSdtPoshisItem();
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
        /// Obtiene registros de T_SentinelSdtPoshisItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtPoshisItemComboDTO> </returns>
        public IEnumerable<SentinelSdtPoshisItemComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSdtPoshisItemRepository.ObtenerCombo();
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
        /// Obtiene los registros de T_SentinelSdtPoshisItem relacionados al IdSentinel.
        /// </summary>
        /// <param name=idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtPoshisItemDTO> </returns>
        public IEnumerable<SentinelSdtPoshisItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtPoshisItemRepository.ObtenerPorIdSentinel(idSentinel);
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
        /// Mapea una lista de SentinelSdtPoshisItemDTO a una lista de SentinelSdtPoshisItem
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<SentinelSdtPoshisItem> </returns>
        public IEnumerable<SentinelSdtPoshisItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtPoshisItemDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<SentinelSdtPoshisItem>>(items);
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
        /// Obtiene los registros de T_SentinelSdtPoshisItem relacionados al IdSentinel.
        /// </summary>
        /// <param name=idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtPoshisItemDTO> </returns>
        public List<SentinelSdtPoshisItemPosicionHistoriaDTO> ObtenerPosicionHistoria(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtPoshisItemRepository.ObtenerPosicionHistoria(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
