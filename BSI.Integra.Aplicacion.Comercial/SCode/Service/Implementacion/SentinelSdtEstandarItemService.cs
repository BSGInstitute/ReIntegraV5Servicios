using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSdtEstandarItemService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtEstandarItem
    /// </summary>
    public class SentinelSdtEstandarItemService : ISentinelSdtEstandarItemService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSdtEstandarItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSdtEstandarItem, SentinelSdtEstandarItem>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSdtEstandarItemDTO, SentinelSdtEstandarItem>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSdtEstandarItem Add(SentinelSdtEstandarItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtEstandarItemRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtEstandarItem>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSdtEstandarItem Update(SentinelSdtEstandarItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtEstandarItemRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtEstandarItem>(modelo);
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
                _unitOfWork.SentinelSdtEstandarItemRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtEstandarItem> Add(List<SentinelSdtEstandarItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtEstandarItemRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtEstandarItem>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtEstandarItem> Update(List<SentinelSdtEstandarItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtEstandarItemRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtEstandarItem>>(modelo);
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
                _unitOfWork.SentinelSdtEstandarItemRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SentinelSdtEstandarItem
        /// </summary>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public IEnumerable<SentinelSdtEstandarItemDTO> ObtenerSentinelSdtEstandarItem()
        {
            try
            {
                return _unitOfWork.SentinelSdtEstandarItemRepository.ObtenerSentinelSdtEstandarItem();
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
        /// Obtiene registros de T_SentinelSdtEstandarItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtEstandarItemComboDTO> </returns>
        public IEnumerable<SentinelSdtEstandarItemComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSdtEstandarItemRepository.ObtenerCombo();
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
        /// Obtiene los registros de T_SentinelSdtEstandarItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id del centro de costo</param>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public IEnumerable<SentinelSdtEstandarItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtEstandarItemRepository.ObtenerPorIdSentinel(idSentinel);
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
        /// Mapea una lista de SentinelSdtEstandarItemDTO a una lista de SentinelSdtEstandarItem
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public IEnumerable<SentinelSdtEstandarItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtEstandarItemDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<SentinelSdtEstandarItem>>(items);
                if (entidades != null) entidades.ForEach(p => p.Estado = true);
                return entidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SentinelSdtEstandarItem asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id del centro de costo</param>
        /// <returns> List<SentinelSdtEstandarItemDTO> </returns>
        public List<SentinelSdtEstandarItemDniRucDTO> ObtenerDniRucSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtEstandarItemRepository.ObtenerDniRucSentinel(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
