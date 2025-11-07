using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    var argumentoDetalleMotivaciones = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacionNombre(item.Id);
                    
                    var argumentoDetalleDto = new ProgramaGeneralArgumentoDetalleDTO
                    {
                        Id = item.Id,
                        Detalle = item.Detalle,
                        Motivacion = new PGArgumentoDetalleMotivacionDTO
                        {
                            Id = argumentoDetalleMotivaciones.IdProgramaMotivacion,
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
                        var argumentoDetalleMotivaciones = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacionNombre(item.Id);
                        var argumentoDetalleDto = new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = ag.Id,
                            Detalle = ag.Detalle,
                            Motivacion = new PGArgumentoDetalleMotivacionDTO
                            {
                                Id = argumentoDetalleMotivaciones.IdProgramaMotivacion,
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

        public async Task<List<ProgramaGeneralArgumentoDTO>> ObtenerArgumentoMotivacion(int idPGeneral)
        {
            // Ejecución secuencial para evitar accesos concurrentes a recursos no thread-safe.
            var argumentos = (await _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerTodoProgramaGeneralAsync(idPGeneral)).ToList();

            foreach (var item in argumentos)
            {
                // Obtener modalidades por cada argumento
                var modalidades = await _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoModalidadAsync(item.Id);
                item.Modalidades = modalidades.Select(m => new ProgramaGeneralArgumentoModalidadDTO
                {
                    Id = m.Id,
                    IdModalidad = m.IdModalidadCurso,
                    Nombre = m.Nombre
                }).ToList();

                // Obtener detalles por cada argumento
                var detalles = await _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleAsync(item.Id);

                var detallesDtoList = new List<ProgramaGeneralArgumentoDetalleDTO>();
                foreach (var ag in detalles)
                {
                    // Obtener motivación por cada detalle
                    var motivacion = await _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacionAsync(ag.Id);

                    var detalleDto = new ProgramaGeneralArgumentoDetalleDTO
                    {
                        Id = ag.Id,
                        Detalle = ag.Detalle,
                        Motivacion = (motivacion != null)
                            ? new PGArgumentoDetalleMotivacionDTO
                            {
                                Id = motivacion.IdProgramaMotivacion,
                            }
                            : null
                    };
                    detallesDtoList.Add(detalleDto);
                }
                item.ArgumentoDetalle = detallesDtoList.OrderBy(d => d.Id).ToList();
            }

            return argumentos.OrderBy(a => a.Id).ToList();
        }

        public async Task<List<ConfiguracionProblemaJerarquicaDTO>> ObtenerProblemaCliente(int idPGeneral, int? idAlumno = null)
        {
            var factorRepo = (await _unitOfWork.ProgramaGeneralProblemaFactorRepository.ObtenerAsync())
                               .OrderBy(f => f.Id)
                               .ToList();

            var detalleRepo = (await _unitOfWork.ProgramaGeneralProblemaFactorDetalleRepository.ObtenerAsync())
                                .OrderBy(d => d.Id)
                                .ToList();

            var solucionRepo = (await _unitOfWork.ProgramaGeneralProblemaFactorSolucionRepository.ObtenerAsync())
                                  .OrderBy(sc => sc.Id)
                                  .ToList();

            var subSolucionesRepo = (await _unitOfWork.ProgramaGeneralProblemaFactorSubSolucionRepository.ObtenerAsync())
                                      .OrderBy(sst => sst.Id)
                                      .ToList();

            var factor = factorRepo.Select(f => new FactorDTO { Id = f.Id, Nombre = f.Nombre }).ToList();
            var detalle = detalleRepo.Select(d => new FactorDetalleDTO { Id = d.Id, Nombre = d.Nombre, Titulo = d.Titulo }).ToList();
            var solucion = solucionRepo.Select(s => new FactorSolucionDTO
            {
                Id = s.Id,
                Descripcion = s.Descripcion,
                Titulo = s.Titulo,
                SubTitulo = s.SubTitulo
            }).ToList();
            var subSoluciones = subSolucionesRepo.Select(ss => new SubSolucionDTO
            {
                Id = ss.Id,
                IdProgramaGeneralProblemaFactorSolucion = ss.IdProgramaGeneralProblemaFactorSolucion,
                Solucion = ss.Solucion,
                Orden = ss.Orden,
                Nivel = ss.Nivel
            }).ToList();

            var lookups = new
            {
                FactorLookup = factor.ToDictionary(f => f.Id),
                DetalleLookup = detalle.ToDictionary(d => d.Id),
                SolucionLookup = solucion.ToDictionary(s => s.Id),
                SubSolucionLookup = subSoluciones.ToDictionary(ss => ss.Id)
            };


            var catalogoFilas = (await _unitOfWork.ProgramaGeneralProblemaDetalleRepository
                                .ObtenerProblemaClienteAsync(idPGeneral))
                                ?? Enumerable.Empty<ProblemaClienteByPGeneral>();

            IEnumerable<ProblemaClienteByPGeneral> filasParaEnsamblar = catalogoFilas;
            var solucionesMarcadas = new HashSet<int>();

            if (idAlumno.HasValue)
            {
                var respuestasHistoricas = (await _unitOfWork.ProgramaGeneralProblemaDetalleRepository
                    .ObtenerHistorialRespuestasAsync(idPGeneral, idAlumno.Value))
                    .ToList();

                var solutionIdsParaMostrar = respuestasHistoricas
                    .Select(r => r.IdProgramaGeneralProblemaFactorSolucion)
                    .Distinct()
                    .ToHashSet();

                solucionesMarcadas = respuestasHistoricas
                    .Where(r => r.EsSolucionado)
                    .Select(r => r.IdProgramaGeneralProblemaFactorSolucion)
                    .ToHashSet();

                filasParaEnsamblar = catalogoFilas.Where(f =>
                    f.IdProgramaGeneralProblemaFactorSolucion.HasValue &&
                    solutionIdsParaMostrar.Contains(f.IdProgramaGeneralProblemaFactorSolucion.Value)
                );

            }

            var configuraciones = filasParaEnsamblar.GroupBy(x => x.Id).Select(g =>
            {
                var first = g.First();
                var subsolucionIds = g
                    .Where(r => r.IdProgramaGeneralProblemaFactorSubSolucion.HasValue)
                    .Select(r => r.IdProgramaGeneralProblemaFactorSubSolucion!.Value)
                    .Distinct()
                    .ToList();

                return new
                {
                    Config = new ProgramaGeneralProblemaDetalleObtener2
                    {
                        Id = g.Key,
                        IdPGeneral = first.IdPGeneral,
                        IdProgramaGeneralProblemaFactor = first.IdProgramaGeneralProblemaFactor,
                        IdProgramaGeneralProblemaFactorDetalle = first.IdProgramaGeneralProblemaFactorDetalle,
                        IdProgramaGeneralProblemaFactorSolucion = first.IdProgramaGeneralProblemaFactorSolucion,
                        SubSolucionIds = subsolucionIds,
                        AplicaTituloDetalle = first.AplicaTituloDetalle,
                        AplicaNombreDetalle = first.AplicaNombreDetalle,
                        AplicaPieDePagina = first.AplicaPieDePagina,
                        AplicaDescripcionSolucion = first.AplicaDescripcionSolucion,
                        AplicaTituloSolucion = first.AplicaTituloSolucion,
                        AplicaSubTituloSolucion = first.AplicaSubTituloSolucion
                    },
                    IdSolucion = first.IdProgramaGeneralProblemaFactorSolucion
                };
            }).ToList();

            var resultadoFinal = new List<ConfiguracionProblemaJerarquicaDTO>();

            foreach (var item in configuraciones)
            {
                var config = item.Config;

                var subSolucionesAnidadas = new List<SubSolucionDTO>();
                foreach (var subId in config.SubSolucionIds)
                {
                    if (lookups.SubSolucionLookup.TryGetValue(subId, out var subSolucionObj))
                    {
                        subSolucionesAnidadas.Add(subSolucionObj);
                    }
                }

                bool esSeleccionado = item.IdSolucion.HasValue &&
                                              solucionesMarcadas.Contains(item.IdSolucion.Value);

                var dto = new ConfiguracionProblemaJerarquicaDTO
                {
                    Id = config.Id,
                    IdPGeneral = config.IdPGeneral,
                    Factor = lookups.FactorLookup.TryGetValue(config.IdProgramaGeneralProblemaFactor, out var factorDto) ? factorDto : null,
                    Detalle = config.IdProgramaGeneralProblemaFactorDetalle.HasValue && lookups.DetalleLookup.TryGetValue(config.IdProgramaGeneralProblemaFactorDetalle.Value, out var detalleDto) ? detalleDto : null,
                    Solucion = config.IdProgramaGeneralProblemaFactorSolucion.HasValue && lookups.SolucionLookup.TryGetValue(config.IdProgramaGeneralProblemaFactorSolucion.Value, out var solucionDto) ? solucionDto : null,
                    SubSoluciones = subSolucionesAnidadas.OrderBy(s => s.Id).ToList(),
                    AplicaTituloDetalle = config.AplicaTituloDetalle,
                    AplicaNombreDetalle = config.AplicaNombreDetalle,
                    AplicaPieDePagina = config.AplicaPieDePagina,
                    AplicaDescripcionSolucion = config.AplicaDescripcionSolucion,
                    AplicaTituloSolucion = config.AplicaTituloSolucion,
                    AplicaSubTituloSolucion = config.AplicaSubTituloSolucion,

                    EsSeleccionado = esSeleccionado
                };
                resultadoFinal.Add(dto);
            }

            return resultadoFinal.OrderBy(r => r.Id).ToList();
        }
        public ArgumentoMotivacionProgramaGeneralDTO ObtenerArgumentoMotivacionByIdPGeneral(int idPGeneral, string motivacion)
        {
            try
            {
                var programaGArgumentos = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerTodo(idPGeneral);
                List<ArgumentoMotivacionEstructuraDTO> estructuraCurricular = new();
                List<ArgumentoMotivacionEstructuraDTO> garantiaDePrograma = new();
                List<ArgumentoMotivacionEstructuraDTO> demostracionDeValor = new();
                List<ArgumentoMotivacionEstructuraDTO> aspectosDiferenciadores = new();
                List<ArgumentoMotivacionEstructuraDTO> argumentosDePerdidaPotencial = new();

                string Normalizar(string? texto)
                {
                    if (string.IsNullOrWhiteSpace(texto))
                        return string.Empty;

                    var n = texto.ToLower().Trim().Normalize(NormalizationForm.FormD);
                    return new string(n.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
                }

                var motivacionResult = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerMotivacionesByDiccionario(Normalizar(motivacion));
                string? motivacionDescripcion = motivacionResult != null ? motivacionResult.Nombre : motivacion;

                var nombreMotivacion = Normalizar(motivacionDescripcion);

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
                        //var _motivacion = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacion(ag.Id);
                        var _motivacion = _unitOfWork.ProgramaGeneralArgumentoRepository.ObtenerProgramaGeneralArgumentoDetalleMotivacionNombre(ag.Id);
                        if (_motivacion == null) continue;
                        if (_motivacion.IdProgramaMotivacion != motivacionResult.Id) continue;
                        detalleDtoList.Add(new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = ag.Id,
                            Detalle = ag.Detalle,
                            Motivacion = new PGArgumentoDetalleMotivacionDTO
                            {
                                Id = _motivacion.IdProgramaMotivacion,
                                Nombre = _motivacion.NombreMotivacion
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
                    var nombre = Normalizar(item.Nombre);
                    nombre = Regex.Replace(nombre, @"[^a-z]", "");

                    switch (nombre)
                    {
                        case "estructuracurricular":
                            estructuraCurricular.Add(ConstruirArgumento(item));
                            break;
                        case "partetecnica":
                            estructuraCurricular.Add(ConstruirArgumento(item));
                            break;
                        case "garantiaconfiabilidad":
                            garantiaDePrograma.Add(ConstruirArgumento(item));
                            break;
                        case "demostraciondevalor":
                            demostracionDeValor.Add(ConstruirArgumento(item));
                            break;
                        case "aspectosdiferenciadores":
                            aspectosDiferenciadores.Add(ConstruirArgumento(item));
                            break;
                        case "argumentosdeperdidapotencial":
                            argumentosDePerdidaPotencial.Add(ConstruirArgumento(item));
                            break;
                    }
                }
                return new ArgumentoMotivacionProgramaGeneralDTO
                {
                    MotivacionPrincipal = motivacionDescripcion,
                    GarantiaDePrograma = garantiaDePrograma,
                    EstructuraCurricular = estructuraCurricular,
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
                            IdProgramaMotivacion = m.Motivacion.Id,
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
                        IdProgramaMotivacion = nuevo.Motivacion.Id,
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
                            IdProgramaMotivacion = dto.Motivacion.Id,
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
                        motivExist.IdProgramaMotivacion = dto.Motivacion.Id;
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
                            .ObtenerProgramaGeneralArgumentoDetalleMotivacionNombre(d.Id);

                        return new ProgramaGeneralArgumentoDetalleDTO
                        {
                            Id = d.Id,
                            Detalle = d.Detalle,
                            Motivacion = motiv == null
                                ? null
                                : new PGArgumentoDetalleMotivacionDTO
                                {
                                    Id = motiv.IdProgramaMotivacion,
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
        public bool InsertarArgumentoMotivacionSeleccion(ProgramaArgumentoMotivacionSeleccionDTO data,string usuario) 
        {
            try
            {
                var motivacionesSelectFiltro = data.SeleccionMotivacion
                 .Where(x => x.seleccionado)
                 .ToList();
                var result = _unitOfWork.OportunidadProgramaMotivacionSeleccionRepository
                    .ObtenerTodoByIdOportunidad(data.IdOportunidad);

                if (!motivacionesSelectFiltro.Any())
                {
                    if (result != null && result.Any())
                    {
                        foreach (var item in result)
                        {
                            _unitOfWork.OportunidadProgramaMotivacionSeleccionRepository.Delete(item.Id, usuario);
                        }
                        _unitOfWork.Commit();
                    }

                    return true;
                }

                if (motivacionesSelectFiltro.Count > 2)
                    throw new Exception("Solo puede seleccionar 2 motivaciones");
                var listaMotivaciones = _unitOfWork.ProgramaMotivacionRepository.ObtenerTodo();
                List<OportunidadProgramaMotivacionSeleccion> motivacionesSeleccionadas = new List<OportunidadProgramaMotivacionSeleccion>();
                foreach (var motivacion in motivacionesSelectFiltro)
                {
                    var entidad = _unitOfWork.ProgramaGeneralMotivacionRepository.ObtenerPorId(motivacion.IdMotivacion);
                    var nombre = entidad.Nombre
                    .ToLower()
                    .Trim()
                    .Normalize(NormalizationForm.FormD);
                    nombre = new string(nombre
                     .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                     .ToArray());

                    nombre = Regex.Replace(nombre, @"[^a-z]", "");
                    var motivacionCoincidente = listaMotivaciones.FirstOrDefault(m =>
                    {
                        var nombreLista = m.Descripcion
                            .ToLower()
                            .Trim()
                            .Normalize(NormalizationForm.FormD);
                        nombreLista = new string(nombreLista
                            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                            .ToArray());
                        nombreLista = Regex.Replace(nombreLista, @"[^a-z]", "");
                        return nombreLista == nombre;
                    });
                    if (motivacionCoincidente != null)
                    {
                        OportunidadProgramaMotivacionSeleccion opo = new()
                        {
                            IdOportunidad = data.IdOportunidad,
                            IdProgramaMotivacion = motivacionCoincidente.Id,
                            Estado = true,
                            FechaModificacion = DateTime.Now,
                            UsuarioModificacion = usuario
                        };
                        motivacionesSeleccionadas.Add(opo);
                    }
                }

                if (result == null || !result.Any())
                {
                    foreach (var m in motivacionesSeleccionadas)
                    {
                        m.FechaCreacion = DateTime.Now;
                        m.UsuarioCreacion = usuario;
                        _unitOfWork.OportunidadProgramaMotivacionSeleccionRepository.Add(m);
                    }
                    _unitOfWork.Commit();
                }
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
