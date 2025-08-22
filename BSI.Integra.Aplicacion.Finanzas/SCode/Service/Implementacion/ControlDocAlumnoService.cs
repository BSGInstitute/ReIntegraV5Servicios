using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ControlDocAlumnoService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegalAreaTrabajo
    /// </summary>
    public class ControlDocAlumnoService : IControlDocAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ControlDocAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TControlDocAlumno, ControlDocAlumno>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ControlDocAlumno Add(ControlDocAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocAlumnoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ControlDocAlumno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControlDocAlumno Update(ControlDocAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocAlumnoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ControlDocAlumno>(modelo);
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
                _unitOfWork.DocumentoLegalPaisRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ControlDocAlumno> Add(List<ControlDocAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocAlumnoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ControlDocAlumno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ControlDocAlumno> Update(List<ControlDocAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocAlumnoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ControlDocAlumno>>(modelo);
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
                _unitOfWork.DocumentoLegalPaisRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public ControlDocumentoAlumnoDTO ActualizarControlDocAlumno(ControlDocumentoAlumnoDTO ControlDocumentoAlumnoDTO, string usuario)
        {
            var _repControlDocAlumno = _unitOfWork.ControlDocAlumnoRepository;
            if (_repControlDocAlumno.Exist(ControlDocumentoAlumnoDTO.IdControlDocAlumno))
            {
                var controlDocAlumnoBO = _repControlDocAlumno.FirstById(ControlDocumentoAlumnoDTO.IdControlDocAlumno);
                controlDocAlumnoBO.FechaModificacion = DateTime.Now;
                controlDocAlumnoBO.UsuarioModificacion = usuario;
                controlDocAlumnoBO.IdCriterioCalificacion = ControlDocumentoAlumnoDTO.IdCriterioCalificacion;
                controlDocAlumnoBO.QuienEntrego = ControlDocumentoAlumnoDTO.QuienEntrego;
                controlDocAlumnoBO.FechaEntregaDocumento = ControlDocumentoAlumnoDTO.FechaEntregaDocumento;
                controlDocAlumnoBO.Observaciones = ControlDocumentoAlumnoDTO.Observaciones;
                _repControlDocAlumno.Update(controlDocAlumnoBO);
                _unitOfWork.Commit();
                return ControlDocumentoAlumnoDTO;
            }
            else
            {
                return null;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/03/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza la calificación de matriculados
        /// </summary>
        /// <param name="dtoReporte"></param>
        /// <returns></returns>
        public bool ActualizarCriterioCalificacion(CriterioObservacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ControlDocAlumno controlDocAlumno = _unitOfWork.ControlDocAlumnoRepository.ObtenerPorIdMatriculaCabecera(dto.IdMatriculaCabecera);
                    if (controlDocAlumno != null && controlDocAlumno.Id != 0)
                    {
                        controlDocAlumno.IdCriterioCalificacion = dto.IdCriterioCalificacion;
                        controlDocAlumno.UsuarioModificacion = usuario;
                        controlDocAlumno.FechaModificacion = DateTime.Now;
                        controlDocAlumno.FechaEntregaDocumento = DateTime.Now;
                        _unitOfWork.ControlDocAlumnoRepository.Update(controlDocAlumno);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        throw new BadRequestException("#CDAACC-002@No existe registro");
                    }
                }
                else
                {
                    throw new BadRequestException("#CDAACC-001@No existe Valores en el objeto");
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/03/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza la calificación de matriculados
        /// </summary>
        /// <param name="dtoReporte"></param>
        /// <returns></returns>
        public bool ActualizarMatriculaObservacion(CriterioObservacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ControlDocAlumno controlDocAlumno = _unitOfWork.ControlDocAlumnoRepository.ObtenerPorIdMatriculaCabecera(dto.IdMatriculaCabecera);
                    if (controlDocAlumno != null && controlDocAlumno.Id != 0)
                    {
                        controlDocAlumno.IdMatriculaObservacion = dto.IdCriterioCalificacion;
                        controlDocAlumno.UsuarioModificacion = usuario;
                        controlDocAlumno.FechaModificacion = DateTime.Now;
                        _unitOfWork.ControlDocAlumnoRepository.Update(controlDocAlumno);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        throw new BadRequestException("#CDAAMO-002@No existe registro");
                    }
                }
                else
                {
                    throw new BadRequestException("#CDAAMO-001@No existe Valores en el objeto");
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
