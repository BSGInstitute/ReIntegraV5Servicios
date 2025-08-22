using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ClasificacionPersonaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 23/07/2022
    /// <summary>
    /// Gestión general de T_ClasificacionPersona
    /// </summary>
    public class ClasificacionPersonaService : IClasificacionPersonaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperClasificacionPersona;

        public ClasificacionPersonaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TClasificacionPersona, ClasificacionPersona>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperClasificacionPersona = new Mapper(config);
        }

        #region Metodos Base
        public ClasificacionPersona Add(ClasificacionPersona entidad)
        {
            try
            {
                var modelo = _unitOfWork.ClasificacionPersonaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ClasificacionPersona>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClasificacionPersona Update(ClasificacionPersona entidad)
        {
            try
            {
                var modelo = _unitOfWork.ClasificacionPersonaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ClasificacionPersona>(modelo);
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
                _unitOfWork.ClasificacionPersonaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ClasificacionPersona> Add(List<ClasificacionPersona> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ClasificacionPersonaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ClasificacionPersona>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ClasificacionPersona> Update(List<ClasificacionPersona> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ClasificacionPersonaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ClasificacionPersona>>(modelo);
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
                _unitOfWork.ClasificacionPersonaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 23/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ClasificacionPersona
        /// </summary>
        /// <returns> List<ClasificacionPersonaDTO> </returns>
        public IEnumerable<ClasificacionPersonaDTO> ObtenerClasificacionPersona()
        {
            try
            {
                return _unitOfWork.ClasificacionPersonaRepository.ObtenerClasificacionPersona();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 23/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ClasificacionPersona para mostrarse en combo.
        /// </summary>
        /// <returns> List<ClasificacionPersonaComboDTO> </returns>
        public IEnumerable<ClasificacionPersonaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ClasificacionPersonaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id de la taba CalisifacionPersona por el tipo de persona y el IdAlumno.
        /// </summary>
        /// <param name="tipo">Tipo persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> ClasificacionPersonaIdDTO </returns>
        public ValorIntDTO IdClasificacionPersonaPorTipoYIdAlumno(int tipo, int idAlumno)
        {
            try
            {
                return _unitOfWork.ClasificacionPersonaRepository.ObtenerIdClasificacionPersonaPorTipoYIdAlumno(tipo, idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
