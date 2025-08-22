using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: WhatsAppPlantillaPorOcurrenciaActividadService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/07/2022
    /// <summary>
    /// Gestión general de T_WhatsAppPlantillaPorOcurrenciaActividad
    /// </summary>
    public class WhatsAppPlantillaPorOcurrenciaActividadService : IWhatsAppPlantillaPorOcurrenciaActividadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public WhatsAppPlantillaPorOcurrenciaActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TWhatsAppPlantillaPorOcurrenciaActividad, WhatsAppPlantillaPorOcurrenciaActividad>(MemberList.None).ReverseMap();
                    cfg.CreateMap<WhatsAppPlantillaPorOcurrenciaActividad, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public WhatsAppPlantillaPorOcurrenciaActividad Add(WhatsAppPlantillaPorOcurrenciaActividad entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppPlantillaPorOcurrenciaActividad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppPlantillaPorOcurrenciaActividad Update(WhatsAppPlantillaPorOcurrenciaActividad entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppPlantillaPorOcurrenciaActividad>(modelo);
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
                _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppPlantillaPorOcurrenciaActividad> Add(List<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppPlantillaPorOcurrenciaActividad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppPlantillaPorOcurrenciaActividad> Update(List<WhatsAppPlantillaPorOcurrenciaActividad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppPlantillaPorOcurrenciaActividad>>(modelo);
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
                _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_WhatsAppPlantillaPorOcurrenciaActividad
        /// </summary>
        /// <returns> List<WhatsAppPlantillaPorOcurrenciaActividadDTO> </returns>
        public IEnumerable<WhatsAppPlantillaPorOcurrenciaActividad> ObtenerWhatsAppPlantillaPorOcurrenciaActividad()
        {
            try
            {
                return _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.ObtenerWhatsAppPlantillaPorOcurrenciaActividad();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_WhatsAppPlantillaPorOcurrenciaActividad para mostrarse en combo.
        /// </summary>
        /// <returns> List<WhatsAppPlantillaPorOcurrenciaActividadComboDTO> </returns>
        public IEnumerable<WhatsAppPlantillaPorOcurrenciaActividadComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Asociacion basado en la Actividad Ocurrencia.
        /// </summary>
        /// <returns> List<WhatsAppPlantillaPorOcurrenciaActividadSinAuditoriaDTO> </returns>
        public List<WhatsAppPlantillaPorOcurrenciaActividadDTO> ObtenerPorIdOcurrenciaActividad(int idActividadOcurrencia)
        {
            try
            {
                return _unitOfWork.WhatsAppPlantillaPorOcurrenciaActividadRepository.ObtenerPorIdOcurrenciaActividad(idActividadOcurrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
