
using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class PespecificoParticipacionExpositorService : IPespecificoParticipacionExpositorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PespecificoParticipacionExpositorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TPespecificoSesion, PEspecificoSesion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 14/09/2023
        /// <summary>
        /// Obtiene todos los registro de expositor y pr
        /// </summary>
        /// <returns> Lista ExpositorDTO</returns>
        public CombosPEspecificoExpositorDTO ObtenerCombosProgramaEspecificoProveedor()
        {
            try
            {
                var listaCombos = new CombosPEspecificoExpositorDTO()
                {
                    Estados = _unitOfWork.EstadoPespecificoRepository.ObtenerCombo(),
                    CiudadesBs = _unitOfWork.RegionCiudadRepository.ObtenerCiudadBsCombo(),
                    Modalidades = _unitOfWork.ModalidadCursoRepository.ObtenerCombo(),
                    Areas = _unitOfWork.AreaCapacitacionRepository.ObtenerCombo(),
                    Subareas = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro(),
                    PGenerales = _unitOfWork.PGeneralRepository.ObtenerProgramaGeneralPadre(null),
                    PEspecificos = _unitOfWork.PEspecificoRepository.ObtenerProgramasEspecificosPadres(null),
                    CentroCostos = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPadres(null),
                    ProovedorHonorarios = _unitOfWork.ProveedorRepository.ObtenerNombreProveedorParaHonorario(),
                    Expositores = _unitOfWork.ExpositorRepository.ObtenerCombo()
                };
                return listaCombos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 14/09/2023
        /// <summary>
        /// Obtiene todos los registro de expositor y pr
        /// </summary>
        /// <returns> Lista ExpositorDTO</returns>
        public IEnumerable<PEspecificoHistorialParticipacionDocenteDTO> GenerarReporteParticipacionExpositor(ParticipacionExpositorFiltroDTO dto)
        {
            try
            {
                return _unitOfWork.PespecificoParticipacionExpositorRepository.ObtenerHistorialParticipacion(dto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 14/09/2023
        /// <summary>
        /// Permite actualizar los registros de TPEspecificoParticipacionExpositor
        /// </summary>
        /// <returns> Lista ExpositorDTO</returns>
        public bool ActualizarProveedorConfirmacion(PEE_ProveedorOperacionesGrupoConfirmadoDTO dto, string usuario)
        {
            try
            {
                var expositor = _unitOfWork.PespecificoParticipacionExpositorRepository.ObtenerPorId(dto.Id);
                if(expositor != null)
                {
                    expositor.IdProveedorOperacionesGrupoConfirmado = dto.IdProveedorOperacionesGrupoConfirmado;
                    expositor.UsuarioModificacion = usuario;
                    expositor.FechaModificacion = DateTime.Now;
                    _unitOfWork.PespecificoParticipacionExpositorRepository.Update(expositor);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    throw new BadRequestException("No existe el registro solicitado.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Autor: Christian A. Quispe Mamani
        /// Fecha: 14/09/2023
        /// <summary>
        /// Permite actualizar los registros de TPEspecificoParticipacionExpositor
        /// </summary>
        /// <returns> Lista ExpositorDTO</returns>
        public bool ActualizarProveedor(ParticipacionExpositorDTO dto, string usuario)
        {
            try
            {
                var expositor = _unitOfWork.PespecificoParticipacionExpositorRepository.ObtenerPorId(dto.Id);
                if (expositor != null)
                {
                    expositor.IdProveedorFurHonorario = dto.IdProveedorFur;
                    expositor.UsuarioModificacion = usuario;
                    expositor.FechaModificacion = DateTime.Now;
                    _unitOfWork.PespecificoParticipacionExpositorRepository.Update(expositor);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    throw new BadRequestException("No existe el registro solicitado.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 14/09/2023
        /// <summary>
        /// Permite actualizar los registros de TEspecificoAprobacionCalificacion
        /// </summary>
        /// <returns> Lista ExpositorDTO</returns>
        public bool ActualizarRegistroNotas(int idCursoActual, string usuario)
        {
            try
            {
                return _unitOfWork.PespecificoParticipacionExpositorRepository.ActualizarRegistroNotas(idCursoActual, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 14/09/2023
        /// <summary>
        /// Permite actualizar los registros de TEspecificoAprobacionCalificacion
        /// </summary>
        /// <returns> Lista ExpositorDTO</returns>
        public bool ActualizarRegistroAsistencia(int idCursoActual, string usuario)
        {
            try
            {
                return _unitOfWork.PespecificoParticipacionExpositorRepository.ActualizarRegistroAsistencias(idCursoActual, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
