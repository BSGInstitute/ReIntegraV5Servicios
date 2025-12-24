using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using System;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ComunicacionAcademicaService
    /// Autor: Christopher Sandy D' Paris
    /// Fecha: 22-12-2025
    /// <summary>
    /// </summary>
    public class ComunicacionAcademicaService : IComunicacionAcademicaService
    {
        private IUnitOfWork _unitOfWork;
        public ComunicacionAcademicaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ConfigurarPreferenciaDTO ObtenerOpcionesPreferenciaComunicacion()
        {
            var mediosComunicacion = _unitOfWork.MedioComunicacionRepository.GetAll()
                .Where(x => x.Estado == true)
                .Select(x => new MedioComunicacionDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                })
                .ToList();
            var bloqueHorario = _unitOfWork.BloqueHorarioRepository.GetAll()
                .Where(x => x.Estado == true)
                .Select(x => new BloqueHorarioDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    HoraInicio = x.HoraInicio,
                    HoraFin = x.HoraFin
                })
                .ToList();
            var bloqueHorarioDetalle = _unitOfWork.BloqueHorarioDetalleRepository.GetAll()
                .Where(x => x.Estado == true)
                .Select(x => new BloqueHorarioDetalleDTO
                {
                    Id = x.Id,
                    IdBloqueHorario = x.IdBloqueHorario,
                    HoraInicio = x.HoraInicio,
                    HoraFin = x.HoraFin
                })
                .ToList();
            return new ConfigurarPreferenciaDTO
            {
                MediosComunicacion = mediosComunicacion,
                BloqueHorario = bloqueHorario,
                BloqueHorarioDetalle = bloqueHorarioDetalle
            };
        }

        public object ActualizarPreferenciaComunicacionAlumno(PreferenciaConfiguracionDTO preferencia, string Usuario)
        {
            try
            {
                if (preferencia.MediosComunicacion != null)
                {
                    var mediosExistentes = _unitOfWork.PreferenciaComunicacionAcademicaRepository.GetBy(x => x.IdAlumno == preferencia.IdAlumno).ToList();
                    var idsRecibidos = preferencia.MediosComunicacion.Select(x => x.Id).ToList();
                    var idsAEliminar = mediosExistentes.Where(x => !idsRecibidos.Contains(x.Id)).Select(x => x.Id).ToList();

                    foreach (var id in idsAEliminar)
                    {
                        _unitOfWork.PreferenciaComunicacionAcademicaRepository.Delete(id, Usuario);
                    }

                    foreach (var item in preferencia.MediosComunicacion)
                    {
                        var medio = new PreferenciaComunicacionAcademica()
                        {
                            Id = item.Id,
                            IdAlumno = preferencia.IdAlumno,
                            IdMedioComunicacion = item.IdMedioComunicacion,
                            Estado = true,
                            UsuarioModificacion = Usuario,
                            FechaModificacion = DateTime.Now
                        };
                        if (item.Id == 0)
                        {
                            medio.UsuarioCreacion = Usuario;
                            medio.FechaCreacion = DateTime.Now;
                            _unitOfWork.PreferenciaComunicacionAcademicaRepository.Add(medio);
                        }
                        else
                        {
                            var entidadExistente = mediosExistentes.FirstOrDefault(x => x.Id == item.Id);
                            if (entidadExistente != null)
                            {
                                medio.UsuarioCreacion = entidadExistente.UsuarioCreacion;
                                medio.FechaCreacion = entidadExistente.FechaCreacion;
                                _unitOfWork.PreferenciaComunicacionAcademicaRepository.Update(medio);
                            }
                        }
                    }
                }
                else
                {
                    var mediosExistentes = _unitOfWork.PreferenciaComunicacionAcademicaRepository.GetBy(x => x.IdAlumno == preferencia.IdAlumno).ToList();
                    foreach (var item in mediosExistentes)
                    {
                        _unitOfWork.PreferenciaComunicacionAcademicaRepository.Delete(item.Id, Usuario);
                    }
                }

                if (preferencia.BloqueHorario != null)
                {
                    var horariosExistentes = _unitOfWork.PreferenciaComunicacionAcademicaHorarioRepository.GetBy(x => x.IdAlumno == preference.IdAlumno).ToList();
                    var idsRecibidos = preferencia.BloqueHorario.Select(x => x.Id).ToList();
                    var idsAEliminar = horariosExistentes.Where(x => !idsRecibidos.Contains(x.Id)).Select(x => x.Id).ToList();

                    foreach (var id in idsAEliminar)
                    {
                        _unitOfWork.PreferenciaComunicacionAcademicaHorarioRepository.Delete(id, preferencia.Usuario);
                    }

                    foreach (var item in preferencia.BloqueHorario)
                    {
                        var horario = new PreferenciaComunicacionAcademicaHorario()
                        {
                            Id = item.Id,
                            IdAlumno = preferencia.IdAlumno,
                            IdBloqueHorarioDetalle = item.IdBloqueHorarioDetalle,
                            Estado = true,
                            UsuarioModificacion = preferencia.Usuario,
                            FechaModificacion = DateTime.Now
                        };
                        if (item.Id == 0)
                        {
                            horario.UsuarioCreacion = preferencia.Usuario;
                            horario.FechaCreacion = DateTime.Now;
                            _unitOfWork.PreferenciaComunicacionAcademicaHorarioRepository.Add(horario);
                        }
                        else
                        {
                            var entidadExistente = horariosExistentes.FirstOrDefault(x => x.Id == item.Id);
                            if (entidadExistente != null)
                            {
                                horario.UsuarioCreacion = entidadExistente.UsuarioCreacion;
                                horario.FechaCreacion = entidadExistente.FechaCreacion;
                                _unitOfWork.PreferenciaComunicacionAcademicaHorarioRepository.Update(horario);
                            }
                        }
                    }
                }
                else
                {
                    var horariosExistentes = _unitOfWork.PreferenciaComunicacionAcademicaHorarioRepository.GetBy(x => x.IdAlumno == preferencia.IdAlumno).ToList();
                    foreach (var item in horariosExistentes)
                    {
                        _unitOfWork.PreferenciaComunicacionAcademicaHorarioRepository.Delete(item.Id, Usuario);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PreferenciaConfiguracionDTO ObtenerPreferenciaComunicacionAlumno(int IdAlumno)
        {
            var preferenciaMC = _unitOfWork.PreferenciaComunicacionAcademicaRepository.ObtenerPreferenciaMedioComunicacionByIdAlumno(IdAlumno);
            var preferenciaHC = _unitOfWork.PreferenciaComunicacionAcademicaHorarioRepository.ObtenerPreferenciaHorarioComunicacionByIdAlumno(IdAlumno);
            return new PreferenciaConfiguracionDTO
            {
                MediosComunicacion = preferenciaMC,
                BloqueHorario = preferenciaHC
            };
        }
    }
}
