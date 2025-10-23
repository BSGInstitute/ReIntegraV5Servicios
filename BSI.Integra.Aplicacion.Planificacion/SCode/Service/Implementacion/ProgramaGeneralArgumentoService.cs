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
                        InstruccionPieDetalle = item.InstruccionPieDetalle,
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
        public List<ProgramaGeneralArgumentoDTO> ObtenerInformacionProgramaGeneralArgumentoTodo()
        {
            try
            {
                var programaGArgumentos = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerTodo();
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
                        var argumentoDetalleMotivaciones = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacion(item.Id);
                        var argumentoDetalleDto = new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = ag.Id,
                            Detalle = ag.Detalle,
                            InstruccionPieDetalle = ag.InstruccionPieDetalle,
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
                        IdArgumento = item.IdArgumento,
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
                            InstruccionPieDetalle = m.InstruccionPieDetalle,
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
                            InstruccionPieDetalle = detalleInsertado.InstruccionPieDetalle,
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
            try
            {
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
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
