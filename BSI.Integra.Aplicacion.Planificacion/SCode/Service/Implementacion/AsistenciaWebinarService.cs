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
    }
}
