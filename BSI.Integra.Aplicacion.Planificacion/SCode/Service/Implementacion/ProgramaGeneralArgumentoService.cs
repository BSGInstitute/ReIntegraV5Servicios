using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ProgramaGeneralArgumentoService : IProgramaGeneralArgumentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralArgumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralArgumento, ProgramaGeneralArgumento>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralArgumentoDTO, ProgramaGeneralArgumento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralArgumento, ProgramaGeneralArgumentoDTO>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        public ProgramaGeneralArgumentoDTO ObtenerInformacionProgramaGeneralArgumento(int idProgramaGeneralArgumento)
        {
            try
            {
                var programaGArgumento = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerPorId(idProgramaGeneralArgumento);
                var modalidades = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoModalidad(idProgramaGeneralArgumento);
                var argumentoDetalles = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalle(idProgramaGeneralArgumento);
                var modalidadesDto = modalidades.Select(m => new ProgramaGeneralArgumentoModalidadDTO
                {
                    Id = m.Id,
                    IdModalidad = m.IdModalidadCurso,
                    Nombre = m.Nombre
                }).ToList();

                List<ProgramaGeneralArgumentoDetalleDTO> argumentoDetalleDtoList = new();
                foreach (var item in argumentoDetalles)
                {
                    var argumentoDetalleMotivaciones = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacion(item.Id);
                    var argumentoDetalleDto = new ProgramaGeneralArgumentoDetalleDTO
                    {
                        Id = item.Id,
                        Detalle = item.Detalle,
                        Motivacion = new PGArgumentoDetalleMotivacionDTO
                        {
                            Id = argumentoDetalleMotivaciones.IdProgramaGeneralMotivacion,
                            Nombre = argumentoDetalleMotivaciones.NombreMotivacion
                        }
                    };
                    argumentoDetalleDtoList.Add(argumentoDetalleDto);
                }

                return new ProgramaGeneralArgumentoDTO
                {
                    Id = programaGArgumento.Id,
                    IdPGeneral = programaGArgumento.IdPgeneral,
                    Nombre = programaGArgumento.Nombre,
                    Descripcion = programaGArgumento.Descripcion,
                    EsVisibleAgenda = programaGArgumento.EsVisibleAgenda,
                    Modalidades = modalidadesDto,
                    ArgumentoDetalle = argumentoDetalleDtoList
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProgramaGeneralArgumentoDTO> ObtenerInformacionProgramaGeneralArgumentoTodo(int idPGeneral)
        {
            try
            {
                var programaGArgumentos = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerTodo(idPGeneral);
                List<ProgramaGeneralArgumentoDTO> todo = new();
                foreach (var item in programaGArgumentos)
                {
                    var _modalidades = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoModalidad(item.Id);
                    var modalidadesDto = _modalidades.Select(m => new ProgramaGeneralArgumentoModalidadDTO
                    {
                        Id = m.Id,
                        IdModalidad = m.IdModalidadCurso,
                        Nombre = m.Nombre
                    }).ToList();
                    var argumentoDetalles = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalle(item.Id);
                    List<ProgramaGeneralArgumentoDetalleDTO> argumentoDetalleDtoList = new();
                    foreach (var ag in argumentoDetalles)
                    {
                        var argumentoDetalleMotivaciones = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacion(ag.Id);
                        var argumentoDetalleDto = new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = ag.Id,
                            Detalle = ag.Detalle,
                            Motivacion = new PGArgumentoDetalleMotivacionDTO
                            {
                                Id = argumentoDetalleMotivaciones.IdProgramaGeneralMotivacion,
                                Nombre = argumentoDetalleMotivaciones.NombreMotivacion
                            }
                        };
                        argumentoDetalleDtoList.Add(argumentoDetalleDto);
                    }
                    var obj = new ProgramaGeneralArgumentoDTO
                    {
                        Id = item.Id,
                        IdPGeneral = item.IdPGeneral,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        EsVisibleAgenda = item.EsVisibleAgenda,
                        Modalidades = modalidadesDto,
                        ArgumentoDetalle = argumentoDetalleDtoList
                    };
                    todo.Add(obj);
                }
                return todo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArgumentoMotivacionProgramaGeneralDTO ObtenerArgumentoMotivacionByIdPGeneral(int idPGeneral)
        {
            try
            {
                var programaGArgumentos = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerTodo(idPGeneral);

                List<ArgumentoMotivacionEstructuraDTO> estructuraCurricular = new();
                List<ArgumentoMotivacionEstructuraDTO> garantiaDePrograma = new();
                List<ArgumentoMotivacionEstructuraDTO> demostracionDeValor = new();
                List<ArgumentoMotivacionEstructuraDTO> aspectosDiferenciadores = new();
                List<ArgumentoMotivacionEstructuraDTO> argumentosDePerdidaPotencial = new();

                ArgumentoMotivacionEstructuraDTO ConstruirArgumento(ProgramaGeneralArgumentoDTO item)
                {
                    var modalidades = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoModalidad(item.Id);
                    var modalidadesDto = modalidades.Select(m => new ProgramaGeneralArgumentoModalidadDTO
                    {
                        Id = m.Id,
                        IdModalidad = m.IdModalidadCurso,
                        Nombre = m.Nombre
                    }).ToList();

                    var detalles = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalle(item.Id);
                    var detalleDtoList = new List<ProgramaGeneralArgumentoDetalleDTO>();

                    foreach (var ag in detalles)
                    {
                        var motivacion = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacion(ag.Id);
                        detalleDtoList.Add(new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = ag.Id,
                            Detalle = ag.Detalle,
                            Motivacion = new PGArgumentoDetalleMotivacionDTO
                            {
                                Id = motivacion.IdProgramaGeneralMotivacion,
                                Nombre = motivacion.NombreMotivacion
                            }
                        });
                    }

                    return new ArgumentoMotivacionEstructuraDTO
                    {
                        Argumento = item,
                        Modalidades = modalidadesDto,
                        ArgumentoDetalle = detalleDtoList
                    };
                }

                foreach (var item in programaGArgumentos)
                {
                    var nombre = item.Nombre.ToLower().Trim();

                    switch (nombre)
                    {
                        case "estructura curricular":
                            estructuraCurricular.Add(ConstruirArgumento(item));
                            break;
                        case "garantia de programa":
                            garantiaDePrograma.Add(ConstruirArgumento(item));
                            break;
                        case "demostracion de valor":
                            demostracionDeValor.Add(ConstruirArgumento(item));
                            break;
                        case "aspectos diferenciadores":
                            aspectosDiferenciadores.Add(ConstruirArgumento(item));
                            break;
                        case "argumentos de perdida potencial":
                            argumentosDePerdidaPotencial.Add(ConstruirArgumento(item));
                            break;
                    }
                }
                return new ArgumentoMotivacionProgramaGeneralDTO
                {
                    GarantiaDePrograma = garantiaDePrograma,
                    EstruturaCurricular = estructuraCurricular,
                    DemostracionDeValor = demostracionDeValor,
                    AspectosDiferenciadores = aspectosDiferenciadores,
                    ArgumentosDePerdidaPotencial = argumentosDePerdidaPotencial,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProgramaGeneralArgumentoDTO> Obtener()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralArgumentoRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ProgramaGeneralArgumentoDTO Insertar(ProgramaGeneralArgumentoDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad), "El objeto entidad no puede ser nulo");

                ProgramaGeneralArgumento PGArgumento = new()
                {
                    IdPgeneral = entidad.IdPGeneral,
                    Nombre = entidad.Nombre,
                    Descripcion = entidad.Descripcion,
                    EsVisibleAgenda = entidad.EsVisibleAgenda,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                };

                var respuesta = _unitOfWork.ProgramaGeneralArgumentoRepository.Add(PGArgumento);
                _unitOfWork.Commit();

                if (respuesta == null || respuesta.Id <= 0)
                    throw new BadRequestException("No se pudo registrar el argumento");
                List<ProgramaGeneralArgumentoModalidad> modalidadesGuardadas = new();

                if (entidad.Modalidades != null && entidad.Modalidades.Any())
                {
                    List<ProgramaGeneralArgumentoModalidad> listaModalidades = new();

                    foreach (var m in entidad.Modalidades)
                    {
                        ProgramaGeneralArgumentoModalidad modalidad = new()
                        {
                            IdProgramaGeneralArgumento = respuesta.Id,
                            IdModalidadCurso = m.IdModalidad,
                            Nombre = m.Nombre,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        listaModalidades.Add(modalidad);
                    }

                    var insertados = _unitOfWork.ProgramaGeneralArgumentoModalidadRepository.AddList(listaModalidades);
                    _unitOfWork.Commit();

                    modalidadesGuardadas.AddRange(
                        insertados.Select(x => new ProgramaGeneralArgumentoModalidad
                        {
                            Id = x.Id,
                            IdProgramaGeneralArgumento = x.IdProgramaGeneralArgumento,
                            IdModalidadCurso = x.IdModalidadCurso,
                            Nombre = x.Nombre,
                            Estado = x.Estado,
                            FechaCreacion = x.FechaCreacion,
                            FechaModificacion = x.FechaModificacion,
                            UsuarioCreacion = x.UsuarioCreacion,
                            UsuarioModificacion = x.UsuarioModificacion
                        })
                        .ToList()
                    );
                }
                List<ProgramaGeneralArgumentoDetalleDTO> argumentoDetalleDtoList = new();

                if (entidad.ArgumentoDetalle != null && entidad.ArgumentoDetalle.Any())
                {
                    foreach (var m in entidad.ArgumentoDetalle)
                    {
                        if (m.Motivacion == null || m.Motivacion.Id <= 0)
                            throw new BadRequestException("La motivación es requerida y debe tener un Id válido.");

                        ProgramaGeneralArgumentoDetalle argumentoDetalle = new()
                        {
                            IdProgramaGeneralArgumento = respuesta.Id,
                            Detalle = m.Detalle,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };

                        var detalleInsertado = _unitOfWork.ProgramaGeneralArgumentoDetalleRepository.Add(argumentoDetalle);
                        _unitOfWork.Commit();

                        if (detalleInsertado == null || detalleInsertado.Id <= 0)
                            throw new BadRequestException("No se pudo registrar el detalle del argumento");

                        ProgramaGeneralArgumentoDetalleMotivacion motivacion = new()
                        {
                            IdProgramaGeneralArgumentoDetalle = detalleInsertado.Id,
                            IdProgramaGeneralMotivacion = m.Motivacion.Id,
                            NombreMotivacion = m.Motivacion.Nombre,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };

                        var motivacionInsertada = _unitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository.Add(motivacion);
                        _unitOfWork.Commit();

                        argumentoDetalleDtoList.Add(new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = detalleInsertado.Id,
                            Detalle = detalleInsertado.Detalle,
                            Motivacion = new PGArgumentoDetalleMotivacionDTO
                            {
                                Id = motivacionInsertada.Id,
                                Nombre = motivacionInsertada.NombreMotivacion
                            }
                        });
                    }
                }

                _unitOfWork.Commit();
                var dtoRespuesta = new ProgramaGeneralArgumentoDTO
                {
                    Id = respuesta.Id,
                    IdPGeneral = respuesta.IdPgeneral,
                    Nombre = respuesta.Nombre,
                    Descripcion = respuesta.Descripcion,
                    EsVisibleAgenda = respuesta.EsVisibleAgenda,
                    Modalidades = modalidadesGuardadas.Select(mm => new ProgramaGeneralArgumentoModalidadDTO
                    {
                        Id = mm.Id,
                        IdModalidad = mm.IdModalidadCurso,
                        Nombre = mm.Nombre
                    }).ToList(),
                    ArgumentoDetalle = argumentoDetalleDtoList
                };

                return dtoRespuesta;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public ProgramaGeneralArgumentoDTO Actualizar(ProgramaGeneralArgumentoDTO entidad, string usuario)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad), "El objeto entidad no puede ser nulo");


            var idArgumento = entidad.Id > 0
                ? entidad.Id : 0;

            if (idArgumento <= 0)
                throw new BadRequestException("El Id del argumento es requerido.");

            try
            {
               
                var argumento = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerPorId(idArgumento);
                if (argumento == null)
                    throw new NotFoundException("El argumento no existe en base de datos");

                argumento.Nombre = entidad.Nombre;
                argumento.Descripcion = entidad.Descripcion;
                argumento.EsVisibleAgenda = entidad.EsVisibleAgenda;
                argumento.FechaModificacion = DateTime.Now;
                argumento.UsuarioModificacion = usuario;

                _unitOfWork.ProgramaGeneralArgumentoRepository.Update(argumento);

                var actualesModalidades = _unitOfWork
                    .ProgramaGeneralArgumentoRepository
                    .ObtenerProgramaGeneralArgumentoModalidad(argumento.Id)
                    .ToList();

                var nuevasModalidades = (entidad.Modalidades ?? new List<ProgramaGeneralArgumentoModalidadDTO>()).ToList();

        
                var idsModalidadesAEliminar = actualesModalidades
                    .Where(act => !nuevasModalidades.Any(n => n.Id == act.Id))
                    .Select(x => x.Id)
                    .ToList();

                if (idsModalidadesAEliminar.Any())
                    _unitOfWork.ProgramaGeneralArgumentoModalidadRepository.Delete(idsModalidadesAEliminar, usuario);

                var modalidadesAInsertar = nuevasModalidades
                    .Where(n => n.Id == 0)
                    .Select(n => new ProgramaGeneralArgumentoModalidad
                    {
                        IdProgramaGeneralArgumento = argumento.Id,
                        IdModalidadCurso = n.IdModalidad,
                        Nombre = n.Nombre,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    })
                    .ToList();

                if (modalidadesAInsertar.Any())
                    _unitOfWork.ProgramaGeneralArgumentoModalidadRepository.AddList(modalidadesAInsertar);

   
                var modalidadesAActualizar = nuevasModalidades.Where(x => x.Id > 0).ToList();
                if (modalidadesAActualizar.Any())
                {
                    var entidadesActualizar = new List<ProgramaGeneralArgumentoModalidad>();
                    foreach (var m in modalidadesAActualizar)
                    {
                        var existente = actualesModalidades.FirstOrDefault(e => e.Id == m.Id);
                        if (existente != null)
                        {
                            existente.Nombre = m.Nombre;
                            existente.IdModalidadCurso = m.IdModalidad;
                            existente.FechaModificacion = DateTime.Now;
                            existente.UsuarioModificacion = usuario;
                            entidadesActualizar.Add(existente);
                        }
                    }
                    if (entidadesActualizar.Any())
                        _unitOfWork.ProgramaGeneralArgumentoModalidadRepository.Update(entidadesActualizar);
                }

   
                var actualesDetalles = _unitOfWork
                    .ProgramaGeneralArgumentoRepository
                    .ObtenerProgramaGeneralArgumentoDetalle(argumento.Id)
                    .ToList();

                var nuevosDetalles = (entidad.ArgumentoDetalle ?? new List<ProgramaGeneralArgumentoDetalleDTO>()).ToList();

                var idsDetallesAEliminar = actualesDetalles
                    .Where(act => !nuevosDetalles.Any(n => n.Id == act.Id))
                    .Select(x => x.Id)
                    .ToList();

                foreach (var idDet in idsDetallesAEliminar)
                {
          
                    var motiv = _unitOfWork
                        .ProgramaGeneralArgumentoRepository
                        .ObtenerProgramaGeneralArgumentoDetalleMotivacion(idDet);

                    if (motiv != null)
                    {
                   
                        _unitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository.Delete(motiv.Id, usuario);

                 
                    }

                    _unitOfWork.ProgramaGeneralArgumentoDetalleRepository.Delete(idDet, usuario);
                }


                foreach (var nuevo in nuevosDetalles.Where(x => x.Id == 0))
                {
                    if (nuevo.Motivacion == null || nuevo.Motivacion.Id <= 0)
                        throw new BadRequestException("La motivación es requerida y debe tener un Id válido.");

                    var det = new ProgramaGeneralArgumentoDetalle
                    {
                        IdProgramaGeneralArgumento = argumento.Id,
                        Detalle = nuevo.Detalle,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };

                    var detInsertado = _unitOfWork.ProgramaGeneralArgumentoDetalleRepository.Add(det);

            
                    _unitOfWork.Commit();

                    var mot = new ProgramaGeneralArgumentoDetalleMotivacion
                    {
                        IdProgramaGeneralArgumentoDetalle = detInsertado.Id, 
                        IdProgramaGeneralMotivacion = nuevo.Motivacion.Id,
                        NombreMotivacion = nuevo.Motivacion.Nombre,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };
                    _unitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository.Add(mot);
                }

                foreach (var dto in nuevosDetalles.Where(x => x.Id > 0))
                {
                    var det = actualesDetalles.FirstOrDefault(d => d.Id == dto.Id);
                    if (det == null) continue;

                    det.Detalle = dto.Detalle;
                    det.FechaModificacion = DateTime.Now;
                    det.UsuarioModificacion = usuario;

                    _unitOfWork.ProgramaGeneralArgumentoDetalleRepository.Update(det);

                    if (dto.Motivacion == null || dto.Motivacion.Id <= 0)
                        throw new BadRequestException("La motivación es requerida para el detalle.");

                    var motivExist = _unitOfWork
                        .ProgramaGeneralArgumentoRepository
                        .ObtenerProgramaGeneralArgumentoDetalleMotivacion(det.Id);

                    if (motivExist == null)
                    {
                        var motNew = new ProgramaGeneralArgumentoDetalleMotivacion
                        {
                            IdProgramaGeneralArgumentoDetalle = det.Id,
                            IdProgramaGeneralMotivacion = dto.Motivacion.Id,
                            NombreMotivacion = dto.Motivacion.Nombre,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario
                        };
                        _unitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository.Add(motNew);
                    }
                    else
                    {
                        motivExist.IdProgramaGeneralMotivacion = dto.Motivacion.Id;
                        motivExist.NombreMotivacion = dto.Motivacion.Nombre;
                        motivExist.FechaModificacion = DateTime.Now;
                        motivExist.UsuarioModificacion = usuario;

                        _unitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository.Update(motivExist);
                    }
                }

                _unitOfWork.Commit();

                var respModalidades = _unitOfWork
                    .ProgramaGeneralArgumentoRepository
                    .ObtenerProgramaGeneralArgumentoModalidad(argumento.Id)
                    .Select(mm => new ProgramaGeneralArgumentoModalidadDTO
                    {
                        Id = mm.Id,
                        IdModalidad = mm.IdModalidadCurso,
                        Nombre = mm.Nombre
                    })
                    .ToList();

                var respDetalles = _unitOfWork
                    .ProgramaGeneralArgumentoRepository
                    .ObtenerProgramaGeneralArgumentoDetalle(argumento.Id)
                    .Select(d =>
                    {
                        var motiv = _unitOfWork
                            .ProgramaGeneralArgumentoRepository
                            .ObtenerProgramaGeneralArgumentoDetalleMotivacion(d.Id);

                        return new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = d.Id,
                            Detalle = d.Detalle,
                            Motivacion = motiv == null
                                ? null
                                : new PGArgumentoDetalleMotivacionDTO
                                {
                                    Id = motiv.IdProgramaGeneralMotivacion,
                                    Nombre = motiv.NombreMotivacion
                                }
                        };
                    })
                    .ToList();

                return new ProgramaGeneralArgumentoDTO
                {
                    Id = argumento.Id,
                    IdPGeneral = argumento.IdPgeneral,
                    Nombre = argumento.Nombre,
                    Descripcion = argumento.Descripcion,
                    EsVisibleAgenda = argumento.EsVisibleAgenda,
                    Modalidades = respModalidades,
                    ArgumentoDetalle = respDetalles
                };
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }



        public IEnumerable<ProgramaGeneralArgumentoMotivacionDTO> ObtenerMotivaciones(int IdPGeneral)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerMotivaciones(IdPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var idProgramaGeneralArgumento = id;
                var programaGArgumento = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerPorId(idProgramaGeneralArgumento);
                if (programaGArgumento == null)
                {
                    throw new NotFoundException("El argumento no existe");
                }
                var modalidades = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoModalidad(idProgramaGeneralArgumento);
                if (modalidades != null && modalidades.Any())
                {
                    foreach (var item in modalidades)
                    {
                        _unitOfWork.ProgramaGeneralArgumentoModalidadRepository.Delete(item.Id, usuario);
                    }
                }
                var argumentoDetalles = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalle(idProgramaGeneralArgumento);
                foreach (var item in argumentoDetalles)
                {
                    var argumentoDetalleMotivaciones = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacion(item.Id);
                    if (argumentoDetalleMotivaciones != null)
                    {
                        _unitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository.Delete(argumentoDetalleMotivaciones.Id, usuario);
                    }
                    _unitOfWork.ProgramaGeneralArgumentoDetalleRepository.Delete(item.Id, usuario);
                }
                _unitOfWork.ProgramaGeneralArgumentoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }
    }
}
