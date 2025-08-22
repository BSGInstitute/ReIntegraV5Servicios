using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SentinelSdtInfGenService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtInfGen
    /// </summary>
    public class SentinelSdtInfGenService : ISentinelSdtInfGenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SentinelSdtInfGenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSentinelSdtInfGen, SentinelSdtInfGen>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SentinelSdtInfGenDTO, SentinelSdtInfGen>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SentinelSdtInfGen Add(SentinelSdtInfGen entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtInfGenRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtInfGen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SentinelSdtInfGen Update(SentinelSdtInfGen entidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtInfGenRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SentinelSdtInfGen>(modelo);
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
                _unitOfWork.SentinelSdtInfGenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtInfGen> Add(List<SentinelSdtInfGen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtInfGenRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtInfGen>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SentinelSdtInfGen> Update(List<SentinelSdtInfGen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SentinelSdtInfGenRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SentinelSdtInfGen>>(modelo);
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
                _unitOfWork.SentinelSdtInfGenRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SentinelSdtInfGen
        /// </summary>
        /// <returns> List<SentinelSdtInfGenDTO> </returns>
        public IEnumerable<SentinelSdtInfGenDTO> ObtenerSentinelSdtInfGen()
        {
            try
            {
                return _unitOfWork.SentinelSdtInfGenRepository.ObtenerSentinelSdtInfGen();
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
        /// Obtiene registros de T_SentinelSdtInfGen para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtInfGenComboDTO> </returns>
        public IEnumerable<SentinelSdtInfGenComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SentinelSdtInfGenRepository.ObtenerCombo();
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
        /// Obtiene los registros de T_SentinelSdtInfGen asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtInfGenDTO> </returns>
        public IEnumerable<SentinelSdtInfGenDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtInfGenRepository.ObtenerPorIdSentinel(idSentinel);
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
        /// Mapea una lista de SentinelSdtInfGenDTO a una lista de SentinelSdtInfGen
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<SentinelSdtInfGen> </returns>
        public IEnumerable<SentinelSdtInfGen> MapeoEntidadesDesdeListaDTO(List<SentinelSdtInfGenDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<SentinelSdtInfGen>>(items);
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
        /// Obtiene datos generales relacionados al idSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtInfGenDatosGeneralesDTO> ObtenerDatosGenerales(int idSentinel)
        {
            try
            {
                return _unitOfWork.SentinelSdtInfGenRepository.ObtenerDatosGenerales(idSentinel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
