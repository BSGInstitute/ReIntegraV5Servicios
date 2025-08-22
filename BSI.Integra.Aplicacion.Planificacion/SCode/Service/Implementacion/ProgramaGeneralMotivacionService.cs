using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralMotivacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralMotivacion
    /// </summary>
    public class ProgramaGeneralMotivacionService : IProgramaGeneralMotivacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralMotivacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralMotivacion, ProgramaGeneralMotivacion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralMotivacionAgendaDTO, ProgramaGeneralMotivacionDetalleAgendaDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProgramaGeneralMotivacion Add(ProgramaGeneralMotivacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralMotivacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralMotivacion Update(ProgramaGeneralMotivacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralMotivacion>(modelo);
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
                _unitOfWork.ProgramaGeneralMotivacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralMotivacion> Add(List<ProgramaGeneralMotivacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralMotivacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralMotivacion> Update(List<ProgramaGeneralMotivacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralMotivacion>>(modelo);
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
                _unitOfWork.ProgramaGeneralMotivacionRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralMotivacion
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionDTO> ObtenerProgramaGeneralMotivacion()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralMotivacionRepository.ObtenerProgramaGeneralMotivacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralMotivacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionComboDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralMotivacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene motivaciones y argumentos para Agenda asociados a un Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralMotivacionDetalleAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var motivaciones = _unitOfWork.ProgramaGeneralMotivacionRepository.ObtenerMotivacionesParaAgendaPorIdOportunidad(idOportunidad);
                var motivacionesDetalle = _mapper.Map<List<ProgramaGeneralMotivacionDetalleAgendaDTO>>(motivaciones);
                var motivacionArgumentoService = new ProgramaGeneralMotivacionArgumentoService(_unitOfWork);
                motivacionesDetalle.ForEach(
                    c => c.Argumentos = motivacionArgumentoService.ObtenerProgramaGeneralMotivacionArgumentoAgendaPorIdMotivacion(c.IdMotivacion).ToList()
                );
                return motivacionesDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
