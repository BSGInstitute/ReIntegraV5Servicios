using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
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
    public class ProgramaGeneralProblemaDetalleService : IProgramaGeneralProblemaDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralProblemaDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalleDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalleDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaFactorSubSolucionAsignadum, ProgramaGeneralProblemaFactorSubSolucionAsignada>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralProblemaFactorSubSolucionAsignadum, ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO, ProgramaGeneralProblemaFactorSubSolucionAsignada>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public ProgramaGeneralProblemaDetalleDTO Insertar(ProgramaGeneralProblemaDetalleDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ProgramaGeneralProblemaDetalle entidad = new()
                    {
                        IdPgeneral = dto.IdPGeneral,
                        IdProgramaGeneralProblemaFactor = dto.IdProgramaGeneralProblemaFactor,
                        IdProgramaGeneralProblemaFactorDetalle = dto.IdProgramaGeneralProblemaFactorDetalle,
                        AplicaNombreDetalle = dto.AplicaNombreDetalle,
                        AplicaTituloDetalle = dto.AplicaTituloDetalle,
                        AplicaPieDePagina = dto.AplicaPieDePagina,
                        AplicaDescripcionSolucion = dto.AplicaDescripcionSolucion,
                        AplicaTituloSolucion = dto.AplicaTituloSolucion,
                        AplicaSubTituloSolucion = dto.AplicaSubTituloSolucion,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.ProgramaGeneralProblemaDetalleRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;

                    var resultado = _mapper.Map<ProgramaGeneralProblemaDetalleDTO>(respuesta);

                    if (dto.Soluciones != null && dto.Soluciones.Count() > 0)
                    {
                        var soluciones = dto.Soluciones.Select(x => new ProgramaGeneralProblemaFactorSubSolucionAsignada
                        {
                            IdProgramaGeneralProblemaDetalle = respuesta.Id,
                            IdProgramaGeneralProblemaFactorSubSolucion = x.IdProgramaGeneralProblemaFactorSubSolucion,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        });
                        var res = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Add(soluciones);
                        _unitOfWork.Commit();
                        resultado.Soluciones = _mapper.Map<List<ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO>>(res);
                    }



                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProgramaGeneralProblemaDetalleDTO Actualizar(ProgramaGeneralProblemaDetalleDTO dto, string usuario)
        {
            try
            {
                ProgramaGeneralProblemaDetalle? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.ProgramaGeneralProblemaDetalleRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.IdPgeneral = dto.IdPGeneral;
                            entidad.IdProgramaGeneralProblemaFactor = dto.IdProgramaGeneralProblemaFactor;
                            entidad.IdProgramaGeneralProblemaFactorDetalle = dto.IdProgramaGeneralProblemaFactorDetalle;
                            entidad.AplicaNombreDetalle = dto.AplicaNombreDetalle;
                            entidad.AplicaTituloDetalle = dto.AplicaTituloDetalle;
                            entidad.AplicaPieDePagina = dto.AplicaPieDePagina;
                            entidad.AplicaDescripcionSolucion = dto.AplicaDescripcionSolucion;
                            entidad.AplicaTituloSolucion = dto.AplicaTituloSolucion;
                            entidad.AplicaSubTituloSolucion = dto.AplicaSubTituloSolucion;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.ProgramaGeneralProblemaDetalleRepository.Update(entidad);
                            _unitOfWork.Commit();

                            var listasoluciones = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.ObtenerPorIdProblemaDetalle(entidad.Id).ToList();
                            if (listasoluciones != null && listasoluciones.Count() > 0)
                            {
                                if (dto.Soluciones != null && dto.Soluciones.Count() > 0)
                                {
                                    listasoluciones.RemoveAll(s => dto.Soluciones.Any(x => x.Id == s.Id));
                                }
                                if (listasoluciones.Count() > 0)
                                {
                                    _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Delete(listasoluciones.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                            }

                            if (dto.Soluciones != null && dto.Soluciones.Count() > 0)
                            {
                                dto.Soluciones.ForEach(solucion =>
                                {
                                    ProgramaGeneralProblemaFactorSubSolucionAsignada subsolucion;
                                    if (solucion.Id != 0 && _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Exist(solucion.Id))
                                    {
                                        subsolucion = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.ObtenerPorId(solucion.Id)!;
                                        subsolucion.IdProgramaGeneralProblemaFactorSubSolucion = solucion.IdProgramaGeneralProblemaFactorSubSolucion;
                                        subsolucion.UsuarioModificacion = usuario;
                                        subsolucion.FechaModificacion = DateTime.Now;
                                        _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Update(subsolucion);
                                        _unitOfWork.Commit();
                                    }
                                    else
                                    {
                                        subsolucion = new ProgramaGeneralProblemaFactorSubSolucionAsignada()
                                        {
                                            IdProgramaGeneralProblemaDetalle = entidad.Id,
                                            IdProgramaGeneralProblemaFactorSubSolucion = solucion.IdProgramaGeneralProblemaFactorSubSolucion,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };
                                        var resultado = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Add(subsolucion);
                                        _unitOfWork.Commit();
                                        solucion.Id = resultado.Id;
                                    }
                                });
                            }

                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProgramaGeneralProblemaDetalleObtener> Obtener(int idPGeneral)
        {
            var filas = _unitOfWork
                .ProgramaGeneralProblemaDetalleRepository
                .Obtener(idPGeneral)
                ?? Enumerable.Empty<ProblemaClienteByPGeneral>();


            var resultado = filas
                .GroupBy(x => x.Id)
                .Select(g =>
                {
                    var first = g.First();


                    var subsoluciones = g
                        .Where(r => r.IdProgramaGeneralProblemaFactorSubSolucion.HasValue)
                        .GroupBy(r => r.IdProgramaGeneralProblemaFactorSubSolucion!.Value)
                        .Select(sg => new ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO
                        {

                            Id = sg.Select(x => x.IdProgramaGeneralProblemaFactorSubSolucionAsignada)
                                   .FirstOrDefault() ?? 0,


                            IdProgramaGeneralProblemaDetalle = g.Key,


                            IdProgramaGeneralProblemaFactorSubSolucion = sg.Key
                        })
                        .ToList();

                    return new ProgramaGeneralProblemaDetalleObtener
                    {

                        Id = g.Key,


                        IdPGeneral = first.IdPGeneral,
                        IdProgramaGeneralProblemaFactor = first.IdProgramaGeneralProblemaFactor,


                        IdProgramaGeneralProblemaFactorDetalle = first.IdProgramaGeneralProblemaFactorDetalle,

                        AplicaTituloDetalle = first.AplicaTituloDetalle,
                        AplicaNombreDetalle = first.AplicaNombreDetalle,
                        AplicaPieDePagina = first.AplicaPieDePagina,

                        IdProgramaGeneralProblemaFactorSolucion = first.IdProgramaGeneralProblemaFactorSolucion,
                        AplicaDescripcionSolucion = first.AplicaDescripcionSolucion,
                        AplicaTituloSolucion = first.AplicaTituloSolucion,
                        AplicaSubTituloSolucion = first.AplicaSubTituloSolucion,

                        SubSoluciones = subsoluciones
                    };
                })
                .OrderBy(r => r.IdProgramaGeneralProblemaFactor)
                .ThenBy(r => r.IdProgramaGeneralProblemaFactorDetalle ?? int.MaxValue)
                .ToList();

            return resultado;
        }


        public bool Eliminar(int idPGeneralProblema, string usuario)
        {
            try
            {
                var entidad = _unitOfWork.ProgramaGeneralProblemaDetalleRepository.ObtenerPorId(idPGeneralProblema);
                if (entidad != null && entidad.Id != 0)
                {
                    entidad.UsuarioModificacion = usuario;
                    entidad.FechaModificacion = DateTime.Now;
                    var respuesta = _unitOfWork.ProgramaGeneralProblemaDetalleRepository.Update(entidad);
                    _unitOfWork.Commit();

                    var listasoluciones = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.ObtenerPorIdProblemaDetalle(entidad.Id).ToList();
                    if (listasoluciones != null && listasoluciones.Count() > 0)
                    {
                        listasoluciones.ForEach(solucion =>
                        {
                            ProgramaGeneralProblemaFactorSubSolucionAsignada subsolucion;
                            if (solucion.Id != 0 && _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Exist(solucion.Id))
                            {
                                subsolucion = _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.ObtenerPorId(solucion.Id)!;
                                subsolucion.UsuarioModificacion = usuario;
                                subsolucion.FechaModificacion = DateTime.Now;
                                _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository.Update(subsolucion);
                                _unitOfWork.Commit();
                            }
                        });
                    }
                    return true;
                }
                else
                    throw new BadRequestException("Entidad no encontrada");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<ProgramaGeneralProblemaDetalleObtenerAgenda> ObtenerProblemasClienteAgendaV6(int idPGeneral)
        {
            IEnumerable<ProblemaAgendaRow> rows =_unitOfWork.ProgramaGeneralProblemaDetalleRepository.ObtenerProblemasClienteAgendaV6(idPGeneral);

            var resultado =
                rows.GroupBy(r => new
                {
                    r.IdProgramaGeneralProblemaDetalle,
                    r.IdPGeneral,
                    r.IdProgramaGeneralProblemaFactor,
                    r.ProblemaClienteNombre,
                    r.IdProgramaGeneralProblemaFactorDetalle,
                    r.ProblemaClienteDetalleTabs,
                    r.ProblemaClienteDetalleTitulo,
                    r.ProblemaClienteDetallePiePagina,
                    r.AplicaTituloDetalle,
                    r.AplicaNombreDetalle,
                    r.AplicaPieDePagina,
                    r.IdProgramaGeneralProblemaFactorSolucion,
                    r.ProblemaClienteSolucionDescripcion,
                    r.ProblemaClienteSolucionTitulo,
                    r.ProblemaClienteSolucionSubTitulo,
                    r.AplicaDescripcionSolucion,
                    r.AplicaTituloSolucion,
                    r.AplicaSubTituloSolucion
                })
                .Select(g => new ProgramaGeneralProblemaDetalleObtenerAgenda
                {
                    IdProgramaGeneralProblemaDetalle = g.Key.IdProgramaGeneralProblemaDetalle,
                    IdPGeneral = g.Key.IdPGeneral,
                    IdProgramaGeneralProblemaFactor = g.Key.IdProgramaGeneralProblemaFactor,
                    ProblemaClienteNombre = g.Key.ProblemaClienteNombre,
                    IdProgramaGeneralProblemaFactorDetalle = g.Key.IdProgramaGeneralProblemaFactorDetalle,
                    ProblemaClienteDetalleTabs = g.Key.ProblemaClienteDetalleTabs,
                    ProblemaClienteDetalleTitulo = g.Key.ProblemaClienteDetalleTitulo,
                    ProblemaClienteDetallePiePagina = g.Key.ProblemaClienteDetallePiePagina,
                    AplicaTituloDetalle = g.Key.AplicaTituloDetalle,
                    AplicaNombreDetalle = g.Key.AplicaNombreDetalle,
                    AplicaPieDePagina = g.Key.AplicaPieDePagina,
                    IdProgramaGeneralProblemaFactorSolucion = g.Key.IdProgramaGeneralProblemaFactorSolucion,
                    ProblemaClienteSolucionDescripcion = g.Key.ProblemaClienteSolucionDescripcion,
                    ProblemaClienteSolucionTitulo = g.Key.ProblemaClienteSolucionTitulo,
                    ProblemaClienteSolucionSubTitulo = g.Key.ProblemaClienteSolucionSubTitulo,
                    AplicaDescripcionSolucion = g.Key.AplicaDescripcionSolucion,
                    AplicaTituloSolucion = g.Key.AplicaTituloSolucion,
                    AplicaSubTituloSolucion = g.Key.AplicaSubTituloSolucion,

  
                    SubSoluciones = g
                        .GroupBy(x => new
                        {
                            x.IdProgramaGeneralProblemaFactorSubSolucionAsignada,
                            x.IdProgramaGeneralProblemaFactorSubSolucion,
                            x.SubSolucion,
                            x.SubSolucionOrden,
                            x.SubSolucionNivel
                        })
                        .Select(x => new ProgramaGeneralProblemaFactorSubSolucionAsignadaAgendaDTO
                        {
                            IdProgramaGeneralProblemaFactorSubSolucionAsignada = x.Key.IdProgramaGeneralProblemaFactorSubSolucionAsignada,
                            IdProgramaGeneralProblemaDetalle = g.Key.IdProgramaGeneralProblemaDetalle,
                            IdProgramaGeneralProblemaFactorSubSolucion = x.Key.IdProgramaGeneralProblemaFactorSubSolucion,
                            SubSolucion = x.Key.SubSolucion,
                            SubSolucionOrden = x.Key.SubSolucionOrden,
                            SubSolucionNivel = x.Key.SubSolucionNivel
                        })
                        .OrderBy(x => x.SubSolucionOrden)
                        .ToList()
                })

                .OrderBy(x => x.IdProgramaGeneralProblemaFactor)
                .ThenBy(x => x.IdProgramaGeneralProblemaDetalle)
                .ToList();

            return resultado;

        }
    }

}
