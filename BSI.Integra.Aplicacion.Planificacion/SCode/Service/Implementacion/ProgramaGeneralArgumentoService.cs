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
                if (entidad != null)
                {
                    ProgramaGeneralArgumento PGArgumento = new()
                    {
                        IdPgeneral = entidad.IdPGeneral,
                        Nombre = entidad.NombreArgumento,
                        Descripcion = entidad.DescripcionArgumento,
                        EsVisibleAgenda = entidad.EsVisibleAgenda,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.ProgramaGeneralArgumentoRepository.Add(PGArgumento);
                    _unitOfWork.Commit();
                    if (respuesta.Id == null) throw new BadRequestException("No se pudo registrar el argumento");
                    if (respuesta != null && entidad.Modalidades.Count() != 0)
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
                        _unitOfWork.ProgramaGeneralArgumentoModalidadRepository.AddList(listaModalidades);
                        _unitOfWork.Commit();
                    }
                    if (respuesta != null && entidad.ArgumentoDetalle.Count() != 0)
                    {
                        foreach (var m in entidad.ArgumentoDetalle)
                        {
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
                            var _argumentoDetalle = _unitOfWork.ProgramaGeneralArgumentoDetalleRepository.Add(argumentoDetalle);
                            _unitOfWork.Commit();
                            if (_argumentoDetalle.Id == null) throw new BadRequestException("No se pudo registrar el detalle del argumento");
                            ProgramaGeneralArgumentoDetalleMotivacion motivacion = new()
                            {
                                IdProgramaGeneralArgumentoDetalle = _argumentoDetalle.Id,
                                IdProgramaGeneralMotivacion = m.IdMotivacion,
                                NombreMotivacion = m.NombreMotivacion,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                            };
                            _unitOfWork.ProgramaGeneralArgumentoDetalleMotivacionRepository.Add(motivacion);
                            _unitOfWork.Commit();
                        }
                    }
                    _unitOfWork.Commit();
                    var respuestaFinal = new ProgramaGeneralArgumentoDTO
                    {
                        Id = respuesta.Id,
                        IdPGeneral = respuesta.IdPgeneral,
                        NombreArgumento = respuesta.Nombre,
                        DescripcionArgumento = respuesta.Descripcion,
                        EsVisibleAgenda = respuesta.EsVisibleAgenda,
                    };
                    return _mapper.Map<ProgramaGeneralArgumentoDTO>(respuesta);
                }
                else
                {
                    throw new ArgumentNullException("El objeto entidad no puede ser nulo");
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public ProgramaGeneralArgumentoDTO Actualizar(ProgramaGeneralArgumentoDTO dto, string usuario)
        {
            try
            {
                ProgramaGeneralArgumento entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        ProgramaGeneralArgumentoDTO? rto = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerPorId(dto.Id);
                        if (rto != null && rto.Id != 0)
                        {
                            entidad.IdPgeneral = dto.IdPGeneral;
                            entidad.Nombre = dto.NombreArgumento;
                            entidad.Descripcion = dto.DescripcionArgumento;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                        }
                        else
                            throw new ArgumentNullException("No se encontro el registro a actualizar");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                {
                    throw new ArgumentNullException("El objeto entidad no puede ser nulo");
                }
                var respuest = _unitOfWork.ProgramaGeneralArgumentoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralArgumentoDTO>(respuest);
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
