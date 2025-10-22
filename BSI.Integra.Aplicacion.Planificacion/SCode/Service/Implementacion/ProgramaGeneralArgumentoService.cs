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
                if (entidad == null)
                    throw new ArgumentNullException("El objeto entidad no puede ser nulo");
                var argumentoExistente = _unitOfWork.ProgramaGeneralArgumentoRepository.FirstById(entidad.Id);
                if (argumentoExistente == null)
                    throw new BadRequestException($"No se encontró el argumento con Id {entidad.Id}");
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
    }
}
