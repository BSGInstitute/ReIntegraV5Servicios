using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelRepLegItemService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión general de T_SentinelRepLegItem
    /// </summary>
    public class SentinelRepLegItemService : ISentinelRepLegItemService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelRepLegItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelRepLegItem, SentinelRepLegItem>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelRepLegItemDTO, SentinelRepLegItem>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelRepLegItem Add(SentinelRepLegItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepLegItemRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelRepLegItem>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelRepLegItem Update(SentinelRepLegItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepLegItemRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelRepLegItem>(modelo);
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
                _unitOfWork.SentinelRepLegItemRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelRepLegItem> Add(List<SentinelRepLegItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepLegItemRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelRepLegItem>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelRepLegItem> Update(List<SentinelRepLegItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelRepLegItemRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelRepLegItem>>(modelo);
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
                _unitOfWork.SentinelRepLegItemRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelRepLegItem
        /// </summary>
        /// <returns> List<SentinelRepLegItemDTO> </returns>
        public IEnumerable<SentinelRepLegItemDTO> ObtenerSentinelRepLegItem()
        {
            try
            {
                return _unitOfWork.SentinelRepLegItemRepository.ObtenerSentinelRepLegItem();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelRepLegItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelRepLegItemComboDTO> </returns>
        public IEnumerable<SentinelRepLegItemComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelRepLegItemRepository.ObtenerCombo();
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
        /// Obtiene los registros de T_SentinelRepLegItem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelRepLegItemDTO> </returns>
        public IEnumerable<SentinelRepLegItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelRepLegItemRepository.ObtenerPorIdSentinel(idSentinel);
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
        /// Mapea una lista de SentinelRepLegItemDTO a una lista de SentinelRepLegItem
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<SentinelRepLegItem> </returns>
        public IEnumerable<SentinelRepLegItem> MapeoEntidadesDesdeListaDTO(List<SentinelRepLegItemDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<SentinelRepLegItem>>(items);
                if (entidades != null) entidades.ForEach(p => p.Estado = true);
                return entidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
