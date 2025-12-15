using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class AsistenciaWebinarService : IAsistenciaWebinarService
    {
        private IUnitOfWork _unitOfWork;

        public AsistenciaWebinarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public object AsistenciaWebinar(WebinarAlumnoAsistenciaDTO asistencia)
        {
            if(!_unitOfWork.MatriculaCabeceraRepository.Exist(asistencia.IdMatriculaCabecera))
            {
                throw new Exception("La matrícula no es válida.");
            }
            if (!_unitOfWork.PEspecificoSesionRepository.Exist(asistencia.IdPEspecificoSesion))
            {
                throw new Exception("El webinar no es válido.");
            }
            if (_unitOfWork.PEspecificoSesionRepository.EsWebinarPasado(asistencia.IdPEspecificoSesion))
            {
                throw new Exception("El webinar ya finalizó");
            }
            var webinarConfirmacion = _unitOfWork.ConfirmacionWebinarRepository.ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(asistencia.IdMatriculaCabecera, asistencia.IdPEspecificoSesion);
            if (_unitOfWork.PEspecificoSesionRepository.ObtenerPorId(asistencia.IdPEspecificoSesion).EsWebinarConfirmado is false)
            {
                throw new Exception("Este webinar ya fue cancelado");
            }

            if (webinarConfirmacion != null)
            {
                if (webinarConfirmacion.Confirmo != asistencia.EstadoAsistencia)
                {
                    webinarConfirmacion.Confirmo = asistencia.EstadoAsistencia;
                    webinarConfirmacion.Asistio = false;
                    webinarConfirmacion.UsuarioModificacion = "system";
                    webinarConfirmacion.FechaModificacion = DateTime.Now;
                } else
                {
                    if (webinarConfirmacion.Confirmo) {
                        throw new Exception("Su participación en este webinar ya fue confirmada. Solo puede realizar esta acción una vez por webinar.");
                    }
                    else
                    {
                        throw new Exception("Su participación en este webinar ya fue cancelada. No puede realizar esta acción nuevamente.");
                    }
                }
            }
            else
            {
                webinarConfirmacion = new AsistenciaConfirmacionWebinarDTO()
                {
                    IdMatriculaCabecera = asistencia.IdMatriculaCabecera,
                    IdPEspecificoSesion = asistencia.IdPEspecificoSesion,
                    Confirmo = asistencia.EstadoAsistencia,
                    Asistio = false,
                    Estado = true,
                    UsuarioCreacion = "system",
                    UsuarioModificacion = "system",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
            }
            if (webinarConfirmacion == null)
            {
                throw new Exception("Ocurrió un inconveniente, por favor intente nuevamente.");
            }
            if (webinarConfirmacion.Id != 0 && webinarConfirmacion.Id != null)
            {
                var update = _unitOfWork.ConfirmacionWebinarRepository.Actualizar(webinarConfirmacion);
            }
            else
            {
                var insert = _unitOfWork.ConfirmacionWebinarRepository.Insertar(webinarConfirmacion);
            }

            if (!webinarConfirmacion.Confirmo)
            {
                return "Su participación en este webinar ya fue cancelada.";
            }
            return "Su participación en este webinar se confirmo. Agradecemos su participación.";
        }
        public RptaConfirmacionWebinarAutomaticaDTO ConfirmacionWebinarAutomatica(int IdPEspecificoSesion)
        {
            try
            {
                if (_unitOfWork.PEspecificoSesionRepository.EsWebinarPasado(IdPEspecificoSesion))
                {
                    throw new Exception("El webinar ya finalizó");
                }
                var sesion = _unitOfWork.PGeneralRepository.ObtenerWebinarPorIdPEspecificoSesion(IdPEspecificoSesion);
                if (sesion is null)
                {
                    throw new Exception("Webinar no encontrado");
                }
                var programaEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(IdPEspecificoSesion);
                if (programaEspecificoSesion.EsWebinarConfirmado is false) {
                    throw new Exception("Este webinar ya fue cancelado");
                } else if (sesion.TotalParticipantesConfirmados > 0) {
                    if (programaEspecificoSesion.EsWebinarConfirmado is true)
                    {
                        throw new Exception("Este webinar ya fue confirmado");
                    }
                    programaEspecificoSesion.EsWebinarConfirmado = true;
                } else {
                    programaEspecificoSesion.EsWebinarConfirmado = null;
                }
                programaEspecificoSesion.UsuarioModificacion = "SYSTEM";
                programaEspecificoSesion.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoSesionRepository.Update(programaEspecificoSesion);
                _unitOfWork.Commit();
                string msg = programaEspecificoSesion.EsWebinarConfirmado is true ? "Webinar confirmado" : "Webinar por confirmar, sin participantes";
                return new RptaConfirmacionWebinarAutomaticaDTO
                {
                    Estado = true,
                    Mensaje = msg,
                };
            }
            catch (Exception ex)
            {
                return new RptaConfirmacionWebinarAutomaticaDTO
                {
                    Estado = false,
                    Mensaje = ex.Message,
                };
            }
        }
    }
}
