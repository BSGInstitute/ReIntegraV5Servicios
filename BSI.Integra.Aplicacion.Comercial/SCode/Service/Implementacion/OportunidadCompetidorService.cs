using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: OportunidadCompetidorService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_OportunidadCompetidor
    /// </summary>
    public class OportunidadCompetidorService : IOportunidadCompetidorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public OportunidadCompetidorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TOportunidadCompetidor, OportunidadCompetidor>(MemberList.None).ReverseMap();
                    cfg.CreateMap<OportunidadCompetidorDTO, ComboDTO>(MemberList.None);
                    cfg.CreateMap<OportunidadCompetidorDTO, OportunidadCompetidor>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public OportunidadCompetidor Add(OportunidadCompetidor entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadCompetidorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadCompetidor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadCompetidor Update(OportunidadCompetidor entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadCompetidorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadCompetidor>(modelo);
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
                _unitOfWork.OportunidadCompetidorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadCompetidor> Add(List<OportunidadCompetidor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadCompetidorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadCompetidor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadCompetidor> Update(List<OportunidadCompetidor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadCompetidorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadCompetidor>>(modelo);
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
                _unitOfWork.OportunidadCompetidorRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OportunidadCompetidor
        /// </summary>
        /// <returns> List<OportunidadCompetidorDTO> </returns>
        public IEnumerable<OportunidadCompetidorDTO> ObtenerOportunidadCompetidor()
        {
            try
            {
                return _unitOfWork.OportunidadCompetidorRepository.ObtenerOportunidadCompetidor();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OportunidadCompetidor para mostrarse en combo.
        /// </summary>
        /// <returns> List<OportunidadCompetidorComboDTO> </returns>
        public IEnumerable<OportunidadCompetidorComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OportunidadCompetidorRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OportunidadCompetidor asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadCompetidorAgendaDTO> </returns>
        public IEnumerable<OportunidadCompetidorAgendaDTO> ObtenerOportunidadCompetidorPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadCompetidorRepository.ObtenerOportunidadCompetidorPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_OportunidadCompetidor asociado a un Id.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de OportunidadCompetidor</param>
        /// <returns> OportunidadCompetidorDTO </returns>
        public OportunidadCompetidorDTO ObtenerOportunidadCompetidorPorId(int idOportunidadCompetidor)
        {
            try
            {
                return _unitOfWork.OportunidadCompetidorRepository.ObtenerOportunidadCompetidorPorId(idOportunidadCompetidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de un OportunidadCompetidorDTO a OportunidadCompetido
        /// </summary>
        /// <returns> OportunidadCompetidor </returns>
        public OportunidadCompetidor MapeoEntidadDesdeDTO(OportunidadCompetidorDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<OportunidadCompetidor>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public OportunidadCompetidor ObtenerPorId(int idOportunidadCompetidor)
        {
            try
            {
                return _unitOfWork.OportunidadCompetidorRepository.ObtenerPorId(idOportunidadCompetidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
