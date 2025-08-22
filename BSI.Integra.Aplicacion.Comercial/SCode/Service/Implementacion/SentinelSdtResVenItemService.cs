using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSdtResVenItemService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtResVenItem
    /// </summary>
    public class SentinelSdtResVenItemService : ISentinelSdtResVenItemService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSdtResVenItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSdtResVenItem, SentinelSdtResVenItem>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSdtResVenItemDTO, SentinelSdtResVenItem>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSdtResVenItem Add(SentinelSdtResVenItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtResVenItemRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtResVenItem>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSdtResVenItem Update(SentinelSdtResVenItem entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtResVenItemRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtResVenItem>(modelo);
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
                _unitOfWork.SentinelSdtResVenItemRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtResVenItem> Add(List<SentinelSdtResVenItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtResVenItemRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtResVenItem>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtResVenItem> Update(List<SentinelSdtResVenItem> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtResVenItemRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtResVenItem>>(modelo);
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
                _unitOfWork.SentinelSdtResVenItemRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SentinelSdtResVenItem
        /// </summary>
        /// <returns> List<SentinelSdtResVenItemDTO> </returns>
        public IEnumerable<SentinelSdtResVenItemDTO> ObtenerSentinelSdtResVenItem()
        {
            try
            {
                return _unitOfWork.SentinelSdtResVenItemRepository.ObtenerSentinelSdtResVenItem();
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
        /// Obtiene registros de T_SentinelSdtResVenItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtResVenItemComboDTO> </returns>
        public IEnumerable<SentinelSdtResVenItemComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSdtResVenItemRepository.ObtenerCombo();
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
        /// Obtiene los registros de T_SentinelSdtResVenItem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtResVenItemDTO> </returns>
        public IEnumerable<SentinelSdtResVenItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtResVenItemRepository.ObtenerPorIdSentinel(idSentinel);
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
        /// Mapea una lista de SentinelSdtResVenItemDTO a una lista de SentinelSdtResVenItem
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<SentinelSdtResVenItem> </returns>
        public IEnumerable<SentinelSdtResVenItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtResVenItemDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<SentinelSdtResVenItem>>(items);
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
        /// <summary>
        /// Obtiene Los Datos Vencidos Para El detalle de Sentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtResVenItemDatosVencidosDTO> ObtenerDatosVencidos(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtResVenItemRepository.ObtenerDatosVencidos(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
