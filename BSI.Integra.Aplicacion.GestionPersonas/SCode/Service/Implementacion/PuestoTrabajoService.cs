using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.WebPages.Html;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PuestoTrabajoService : IPuestoTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PuestoTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajo, PuestoTrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajo, PuestoTrabajoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoInsertDTO, PuestoTrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoInsertDTO, TPuestoTrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPerfilPuestoTrabajo, PerfilPuestoTrabajo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public IEnumerable<PuestoTrabajoEnviarDTO> Obtener()
        {
            var listaPuestoTrabajoFecha = _unitOfWork.PuestoTrabajoRepository.ObtenerPuestoTrabajoRegistradoFechaModificacion();
            List<PuestoTrabajoEnviarDTO> listaFiltrada = new List<PuestoTrabajoEnviarDTO>();
            if (listaPuestoTrabajoFecha != null && listaPuestoTrabajoFecha.Count > 0)
            {
                var grupoPuestoTrabajo = listaPuestoTrabajoFecha.GroupBy(u => new { u.Id, u.Nombre, u.IdPersonalAreaTrabajo, u.IdPerfilPuestoTrabajo, u.PersonalAreaTrabajo })
                    .Select(group => new PuestoFechaDTO
                    {
                        Id = group.Key.Id,
                        Nombre = group.Key.Nombre,
                        PersonalAreaTrabajo = group.Key.PersonalAreaTrabajo,
                        IdPersonalAreaTrabajo = group.Key.IdPersonalAreaTrabajo,
                        IdPerfilPuestoTrabajo = group.Key.IdPerfilPuestoTrabajo,
                        ListaFechaModificacion = group.Select(x => new FechaModificacionDTO
                        {
                            PuestoTrabajoFechaModificacion = x.PuestoTrabajoFechaModificacion,
                            PerfilPuestoTrabajoFechaModificacion = x.PerfilPuestoTrabajoFechaModificacion,
                            PersonalAreaFechaModificacion = x.PersonalAreaFechaModificacion,
                            PuestoTrabajoCaracteristicaPersonalFechaModificacion = x.PuestoTrabajoCaracteristicaPersonalFechaModificacion,
                            PuestoTrabajoCursoComplementarioFechaModificacion = x.PuestoTrabajoCursoComplementarioFechaModificacion,
                            PuestoTrabajoExperienciaFechaModificacion = x.PuestoTrabajoExperienciaFechaModificacion,
                            PuestoTrabajoFormacionAcademicaFechaModificacion = x.PuestoTrabajoFormacionAcademicaFechaModificacion,
                            PuestoTrabajoFuncionFechaModificacion = x.PuestoTrabajoFuncionFechaModificacion,
                            PuestoTrabajoRelacionFechaModificacion = x.PuestoTrabajoRelacionFechaModificacion,
                            PuestoTrabajoRelacionDetalleFechaModificacion = x.PuestoTrabajoRelacionDetalleFechaModificacion,
                            PuestoTrabajoReporteFechaModificacion = x.PuestoTrabajoReporteFechaModificacion,
                            PuestoTrabajoPuntajeCalificacionFechaModificacion = x.PuestoTrabajoPuntajeCalificacionFechaModificacion,
                            ModuloSistemaPuestoTrabajoFechaModificacion = x.ModuloSistemaPuestoTrabajoFechaModificacion,
                            PuestoTrabajoUsuarioModificacion = x.PuestoTrabajoUsuarioModificacion,
                            PerfilPuestoTrabajoUsuarioModificacion = x.PerfilPuestoTrabajoUsuarioModificacion,
                            PersonalAreaUsuarioModificacion = x.PersonalAreaUsuarioModificacion,
                            PuestoTrabajoCaracteristicaPersonalUsuarioModificacion = x.PuestoTrabajoCaracteristicaPersonalUsuarioModificacion,
                            PuestoTrabajoCursoComplementarioUsuarioModificacion = x.PuestoTrabajoCursoComplementarioUsuarioModificacion,
                            PuestoTrabajoExperienciaUsuarioModificacion = x.PuestoTrabajoExperienciaUsuarioModificacion,
                            PuestoTrabajoFormacionAcademicaUsuarioModificacion = x.PuestoTrabajoFormacionAcademicaUsuarioModificacion,
                            PuestoTrabajoFuncionUsuarioModificacion = x.PuestoTrabajoFuncionUsuarioModificacion,
                            PuestoTrabajoRelacionUsuarioModificacion = x.PuestoTrabajoRelacionUsuarioModificacion,
                            PuestoTrabajoRelacionDetalleUsuarioModificacion = x.PuestoTrabajoRelacionDetalleUsuarioModificacion,
                            PuestoTrabajoReporteUsuarioModificacion = x.PuestoTrabajoReporteUsuarioModificacion,
                            PuestoTrabajoPuntajeCalificacionUsuarioModificacion = x.PuestoTrabajoPuntajeCalificacionUsuarioModificacion,
                            ModuloSistemaPuestoTrabajoUsuarioModificacion = x.ModuloSistemaPuestoTrabajoUsuarioModificacion
                        }).ToList()
                    }
                ).ToList();

                List<FechaUsuarioDTO> listaFechaModificacion;
                foreach (var item in grupoPuestoTrabajo)
                {
                    listaFechaModificacion = new List<FechaUsuarioDTO>();
                    List<FechaUsuarioDTO> listaPuestoTrabajoFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPerfilPuestoTrabajoFecha = item.ListaFechaModificacion.Where(x => x.PerfilPuestoTrabajoFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PerfilPuestoTrabajoFechaModificacion.GetValueOrDefault(), Usuario = x.PerfilPuestoTrabajoUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPersonalAreaFechaModificacion = item.ListaFechaModificacion.Where(x => x.PersonalAreaFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PersonalAreaFechaModificacion.GetValueOrDefault(), Usuario = x.PersonalAreaUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoCaracteristicaPersonalFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoCaracteristicaPersonalFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoCaracteristicaPersonalFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoCaracteristicaPersonalUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoCursoComplementarioFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoCursoComplementarioFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoCursoComplementarioFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoCursoComplementarioUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoExperienciaFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoExperienciaFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoExperienciaFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoExperienciaUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoFormacionAcademicaFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoFormacionAcademicaFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoFormacionAcademicaFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoFormacionAcademicaUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoFuncionFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoFuncionFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoFuncionFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoFuncionUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoRelacionFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoRelacionFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoRelacionFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoRelacionUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoRelacionDetalleFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoRelacionDetalleFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoRelacionDetalleFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoRelacionDetalleUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoReporteFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoReporteFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoReporteFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoReporteUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaPuestoTrabajoPuntajeCalificacionFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoPuntajeCalificacionFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoPuntajeCalificacionFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoPuntajeCalificacionUsuarioModificacion }).ToList();
                    List<FechaUsuarioDTO> listaModuloSistemaPuestoTrabajoFechaModificacion = item.ListaFechaModificacion.Where(x => x.ModuloSistemaPuestoTrabajoFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.ModuloSistemaPuestoTrabajoFechaModificacion.GetValueOrDefault(), Usuario = x.ModuloSistemaPuestoTrabajoUsuarioModificacion }).ToList();
                    listaFechaModificacion.AddRange(listaPuestoTrabajoFechaModificacion);
                    listaFechaModificacion.AddRange(listaPerfilPuestoTrabajoFecha);
                    listaFechaModificacion.AddRange(listaPersonalAreaFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoCaracteristicaPersonalFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoCursoComplementarioFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoExperienciaFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoFormacionAcademicaFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoFuncionFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoRelacionFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoRelacionDetalleFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoReporteFechaModificacion);
                    listaFechaModificacion.AddRange(listaPuestoTrabajoPuntajeCalificacionFechaModificacion);
                    listaFechaModificacion.AddRange(listaModuloSistemaPuestoTrabajoFechaModificacion);
                    PuestoTrabajoEnviarDTO registroPuestoTrabajo;
                    DateTime fechaReciente = listaFechaModificacion[0].Fecha;
                    string usuarioReciente = listaFechaModificacion[0].Usuario;
                    foreach (var item2 in listaFechaModificacion)
                    {
                        if (item2.Fecha > fechaReciente)
                        {
                            fechaReciente = item2.Fecha;
                            usuarioReciente = item2.Usuario;
                        }
                    }
                    var fechaRecienteLetras = fechaReciente.ToString("dd/M/yyy", CultureInfo.CreateSpecificCulture("es-PE"));

                    registroPuestoTrabajo = new PuestoTrabajoEnviarDTO()
                    {
                        Id = item.Id,
                        IdPersonalAreaTrabajo = item.IdPersonalAreaTrabajo,
                        PersonalAreaTrabajo = item.PersonalAreaTrabajo,
                        Nombre = item.Nombre,
                        IdPerfilPuestoTrabajo = item.IdPerfilPuestoTrabajo,
                        Objetivo = item.Objetivo,
                        Descripcion = item.Descripcion,
                        UsuarioModificacion = usuarioReciente,
                        FechaModificacion = fechaRecienteLetras
                    };
                    listaFiltrada.Add(registroPuestoTrabajo);
                }
            }
            return listaFiltrada.OrderByDescending(x => DateTime.ParseExact(x.FechaModificacion, "dd/M/yyyy", CultureInfo.CreateSpecificCulture("es-PE"))).ToList();
        }


        public IEnumerable<PuestoTrabajoModuloSistemaDTO> ObtenerGridAsignacionInterfaz(int idPuestoTrabajo)
        {
            try
            {


                var listaModuloSistema = _unitOfWork.ModuloSistemaV5Repository.ObtenerModulosGrupoModulo(); 
                var listaPuestoTrabajo =  _unitOfWork.PuestoTrabajoRepository.GetAll();
                var listaModuloPuestoTrabajo = _unitOfWork.ModuloSistemaPuestoTrabajoRepository.GetBy(x => x.IdPuestoTrabajo == idPuestoTrabajo).ToList();
               

                List<PuestoTrabajoModuloSistemaDTO> listaRegistroModuloAsignados = new List<PuestoTrabajoModuloSistemaDTO>();
                PuestoTrabajoModuloSistemaDTO registroModuloAsignados;
                if (listaPuestoTrabajo != null)
                {
                    foreach (var modulo in listaModuloSistema)
                    {
                        bool estadoModuloSistemaPuestoTrabajo = false;
                        var puestoTrabajoModulo = listaModuloPuestoTrabajo.Where(x => x.IdModuloSistema == modulo.Id).FirstOrDefault();
                        if (puestoTrabajoModulo != null)
                        {
                            estadoModuloSistemaPuestoTrabajo = puestoTrabajoModulo.Estado;
                        }

                        registroModuloAsignados = new PuestoTrabajoModuloSistemaDTO()
                        {
                            IdModuloSistema = modulo.Id,
                            ModuloSistema = modulo.Nombre,
                            IdModuloSistemaGrupo = modulo.IdModuloSistemaGrupo,
                            ModuloSistemaGrupo = modulo.ModuloSistemaGrupo,
                            NombreTipo = modulo.NombreTipo,
                            Url = modulo.Url,
                            Estado = estadoModuloSistemaPuestoTrabajo
                        };

                        listaRegistroModuloAsignados.Add(registroModuloAsignados);
                    }
                }
                if (listaRegistroModuloAsignados.Count > 0)
                {
                    listaRegistroModuloAsignados = listaRegistroModuloAsignados.OrderBy(x => x.ModuloSistemaGrupo).ToList();
                }
                return listaRegistroModuloAsignados;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ComboPuestoTrabajo ObtenerCombos()
        {
            try
            {
                ComboPuestoTrabajo respuesta = new ComboPuestoTrabajo()
                {
                    listaPersonalAreaTrabajo = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerTodoFiltroAreaTrabajo(),
                    listaPuestoTrabajo = _unitOfWork.PuestoTrabajoRepository.ObtenerCombo(),
                    listaTipoFuncion = _unitOfWork.PersonalTipoFuncionRepository.Obtener(),
                    listaFrecuenciaPuestoTrabajo = _unitOfWork.FrecuenciaPuestoTrabajoRepository.Obtener(),
                    listaTipoCompetenciaTecnica = _unitOfWork.TipoCompetenciaTecnicaRepository.ObtenerCombos(),
                    listaCompetenciaTecnica = _unitOfWork.CompetenciaTecnicaRepository.Obtener(),
                    listaNivelCompetenciaTecnica = _unitOfWork.NivelCompetenciaTecnicaRepository.Obtener(),
                    listaExperiencia = _unitOfWork.ExperienciaRepository.ObtenerCombo(),
                    listaTipoExperiencia = _unitOfWork.TipoExperienciaRepository.Obtener(),
                    listaSexo = _unitOfWork.SexoRepository.ObtenerCombo(),
                    listaEstadoCivil = _unitOfWork.EstadoCivilRepository.Obtener(),
                    listaTipoFormacion = _unitOfWork.TipoFormacionRepository.Obtener(),
                    listaNivelEstudio = _unitOfWork.NivelEstudioRepository.ObtenerCombo(),
                    listaAreaFormacion = _unitOfWork.AreaFormacionRepository.ObtenerCombo(),
                    listaCentroEstudio = _unitOfWork.CentroEstudioRepository.Obtener(),
                    listaGradoEstudio = _unitOfWork.GradoEstudioRepository.Obtener(),
                    listaRango = _unitOfWork.ProcesoSeleccionRangoRepository.Obtener(),

                };
                return _mapper.Map<ComboPuestoTrabajo>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PuestoTrabajoInsertDTO Insertar(PuestoTrabajoInsertDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PuestoTrabajo entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdPersonalAreaTrabajo = dto.IdPersonalAreaTrabajo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PuestoTrabajoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    var resultado = _mapper.Map<PuestoTrabajoInsertDTO>(respuesta);
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

        public PuestoTrabajoInsertDTO Actualizar(PuestoTrabajoInsertDTO dto, string usuario)
        {
            try
            {
                PuestoTrabajo? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PuestoTrabajoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.IdPersonalAreaTrabajo = dto.IdPersonalAreaTrabajo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PuestoTrabajoRepository.Update(entidad);
                            _unitOfWork.Commit();
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
        public ObtenerExamenDTO ObtenerPerfilPuestoTrabajo(int? idPerfilPuestoTrabajo)
        {
            try
            {
                var listaPuestoTrabajoRelacion = _unitOfWork.PerfilPuestoTrabajoRepository.ObtenerPuestoTrabajoRelacion(idPerfilPuestoTrabajo);
                var listaPuestoTrabajoRelacionCompuesto = listaPuestoTrabajoRelacion.GroupBy(x => new { x.Id, x.IdPerfilPuestoTrabajo }).Select(x => new PuestoTrabajoRelacionCompuestoDTO
                {
                    Id = x.Key.Id,
                    IdPerfilPuestoTrabajo = x.Key.IdPerfilPuestoTrabajo,
                    ListaPuestoDependencia = x.GroupBy(y => new { y.IdPuestoTrabajoRelacionDetalle, y.IdPuestoTrabajo_Dependencia, y.PuestoTrabajo_Dependencia }).Select(y => new FiltroIdNombrePKDTO
                    {
                        Id = y.Key.IdPuestoTrabajo_Dependencia == null ? 0 : y.Key.IdPuestoTrabajo_Dependencia.Value,
                        Nombre = y.Key.PuestoTrabajo_Dependencia,
                        PK = y.Key.IdPuestoTrabajoRelacionDetalle
                    }).ToList(),
                    ListaPuestoACargo = x.GroupBy(y => new { y.IdPuestoTrabajoRelacionDetalle, y.IdPuestoTrabajo_PuestoACargo, y.PuestoTrabajo_PuestoACargo }).Select(y => new FiltroIdNombrePKDTO
                    {
                        Id = y.Key.IdPuestoTrabajo_PuestoACargo == null ? 0 : y.Key.IdPuestoTrabajo_PuestoACargo.Value,
                        Nombre = y.Key.PuestoTrabajo_PuestoACargo,
                        PK = y.Key.IdPuestoTrabajoRelacionDetalle
                    }).ToList(),
                    ListaPuestoRelacionInterna = x.GroupBy(y => new { y.IdPuestoTrabajoRelacionDetalle, y.IdPersonalAreaTrabajo, y.PersonalAreaTrabajo }).Select(y => new FiltroIdNombrePKDTO
                    {
                        Id = y.Key.IdPersonalAreaTrabajo == null ? 0 : y.Key.IdPersonalAreaTrabajo.Value,
                        Nombre = y.Key.PersonalAreaTrabajo,
                        PK = y.Key.IdPuestoTrabajoRelacionDetalle
                    }).ToList()
                }).ToList();
                foreach (var item in listaPuestoTrabajoRelacionCompuesto)
                {
                    item.ListaPuestoACargo.RemoveAll(x => x.Id == 0);
                    item.ListaPuestoDependencia.RemoveAll(x => x.Id == 0);
                    item.ListaPuestoRelacionInterna.RemoveAll(x => x.Id == 0);
                }

                //FUNCIONES
                var listaPuestoTrabajoFuncion = _unitOfWork.PuestoTrabajoFuncionRepository.ObtenerPuestoTrabajoFuncion(idPerfilPuestoTrabajo);

                //REPORTE	
                var listaPuestoTrabajoReporte = _unitOfWork.PuestoTrabajoReporteRepository.ObtenerPuestoTrabajoReporte(idPerfilPuestoTrabajo);

                //CURSOS COMPLEMENTARIOS
                var listaPuestoTrabajoCursoComplementario = _unitOfWork.PuestoTrabajoCursoComplementarioRepository.ObtenerPuestoTrabajoCursoComplementario(idPerfilPuestoTrabajo);

                //EXPERIENCIA
                var listaPuestoTrabajoExperiencia = _unitOfWork.PuestoTrabajoExperienciaRepository.ObtenerPuestoTrabajoExperiencia(idPerfilPuestoTrabajo);

                //CARACTERISTICAS PERSONALES
                var listaPuestoTrabajoCaracteristicaPersonal = _unitOfWork.PuestoTrabajoCaracteristicaPersonalRepository.ObtenerPuestoTrabajoCaracteristicaPersonal(idPerfilPuestoTrabajo);
                //_repPuestoTrabajoCaracteristicaPersonal.ObtenerPuestoTrabajoCaracteristicaPersonal(IdPerfilPuestoTrabajo);

                //FORMACION ACADEMICA
                var listaPuestoTrabajoFormacionAcademica = _unitOfWork.PuestoTrabajoFormacionAcademicaRepository.ObtenerPuestoTrabajoFormacionAcademica(idPerfilPuestoTrabajo);

                var listaPuestoTrabajoFormacionAcademicaProcesada = listaPuestoTrabajoFormacionAcademica.Select(x => new PuestoTrabajoFormacionAcademicaDTO
                {
                    Id = x.Id,
                    IdPerfilPuestoTrabajo = x.IdPerfilPuestoTrabajo,
                    IdAreaFormacion = x.IdAreaFormacion.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
                    IdCentroEstudio = x.IdCentroEstudio.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
                    IdGradoEstudio = x.IdGradoEstudio.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
                    IdNivelEstudio = x.IdNivelEstudio.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
                    IdTipoFormacion = x.IdTipoFormacion.Split(",").Select(y => Convert.ToInt32(y)).ToList()
                }).ToList();

                var listaEvaluacion = _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.ObtenerNombreEvaluacionPuntaje();
                var ListaCalificacionTotal = listaEvaluacion.Where(x => x.CalificacionTotal == true).ToList();
                var ListaCalificacionAgrupadaIndependiente = listaEvaluacion.Where(x => x.CalificacionTotal == false).ToList();
                var ListaIndependiente = ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo == null || x.IdGrupo == 0).ToList();
                var ListaGrupo = ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo != null && x.IdGrupo != 0).ToList();
                List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionTotal = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
                List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionAgrupada = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
                List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionIndependiente = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();

                //Separa todas las Calificaciones Totales, se agrupa las evaluaciones y se colocan en nuevoCalificacionTotal
                if (ListaCalificacionTotal.Count() > 0)
                {
                    foreach (var item in ListaCalificacionTotal)
                    {
                        item.IdComponente = null;
                        item.IdGrupo = null;
                        item.NombreComponente = null;
                        item.NombreGrupo = null;
                        item.CalificaAgrupadoNoIndependiente = false;
                    }
                    nuevoCalificacionTotal = ListaCalificacionTotal.GroupBy(u => (u.IdEvaluacion, u.NombreEvaluacion))
                        .Select(group => new PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion,
                            IdGrupo = null,
                            IdComponente = null,
                            NombreComponente = null,
                            NombreGrupo = null,
                            NombreEvaluacion = group.Key.NombreEvaluacion,
                            CalificacionTotal = true,
                            Puntaje = null,
                            CalificaPorCentil = false,
                            IdProcesoSeleccionRango = 0,
                            EsCalificable = false
                        }).ToList();
                }

                //Separa todas las Calificaciones por Componente, se agrupa los Componentes y se colocan en nuevoCalificacionIndependiente
                if (ListaIndependiente.Count() > 0)
                {
                    foreach (var item in ListaIndependiente)
                    {
                        item.IdGrupo = null;
                        item.NombreGrupo = null;
                    }
                    nuevoCalificacionIndependiente = ListaIndependiente.GroupBy(u => (u.IdComponente, u.NombreComponente, u.IdEvaluacion, u.NombreEvaluacion))
                        .Select(group => new PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion,
                            IdGrupo = null,
                            IdComponente = group.Key.IdComponente,
                            NombreComponente = group.Key.NombreComponente,
                            NombreGrupo = null,
                            NombreEvaluacion = group.Key.NombreEvaluacion,
                            CalificacionTotal = false,
                            Puntaje = null,
                            CalificaPorCentil = false,
                            CalificaAgrupadoNoIndependiente = false,
                            IdProcesoSeleccionRango = 0,
                            EsCalificable = false
                        }).ToList();
                }

                //Separa todas las Calificaciones por Grupo, se agrupa los Grupos y se colocan en nuevoCalificacionAgrupada
                if (ListaGrupo.Count() > 0)
                {
                    foreach (var item in ListaGrupo)
                    {
                        item.IdComponente = null;
                        item.NombreComponente = null;
                    }
                    nuevoCalificacionAgrupada = ListaGrupo.GroupBy(u => (u.IdGrupo, u.NombreGrupo, u.IdEvaluacion, u.NombreEvaluacion))
                        .Select(group => new PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion,
                            IdGrupo = group.Key.IdGrupo,
                            IdComponente = null,
                            NombreComponente = null,
                            NombreGrupo = group.Key.NombreGrupo,
                            NombreEvaluacion = group.Key.NombreEvaluacion,
                            CalificacionTotal = false,
                            Puntaje = null,
                            CalificaPorCentil = false,
                            CalificaAgrupadoNoIndependiente = true,
                            IdProcesoSeleccionRango = 0,
                            EsCalificable = false
                        }).ToList();
                }
                List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> listaPuntajeCalificacionTotal = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
                listaPuntajeCalificacionTotal = nuevoCalificacionTotal.Concat(nuevoCalificacionAgrupada).Concat(nuevoCalificacionIndependiente).ToList();

                foreach (var item in listaPuntajeCalificacionTotal)
                {
                    if (item.IdEvaluacion != null && item.IdGrupo == null && item.IdComponente == null)
                    {

                        PuestoTrabajoPuntajeCalificacion evaluacionPje = new PuestoTrabajoPuntajeCalificacion();
                        evaluacionPje = _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacion(idPerfilPuestoTrabajo, item.IdEvaluacion);
                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                    if (item.IdGrupo != null)
                    {

                        PuestoTrabajoPuntajeCalificacion evaluacionPje = new PuestoTrabajoPuntajeCalificacion();
                        evaluacionPje = _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacionAndIdGrupoComponenteEvaluacion(idPerfilPuestoTrabajo, item.IdEvaluacion, item.IdGrupo);
                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                    if (item.IdComponente != null)
                    {
                        PuestoTrabajoPuntajeCalificacion evaluacionPje = new PuestoTrabajoPuntajeCalificacion();
                        evaluacionPje = _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacionANDIdComponente(idPerfilPuestoTrabajo, item.IdEvaluacion, item.IdComponente);
                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                }

                List<PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO> listaNombreEvaluacion = new List<PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO>();
                foreach (var item in listaPuntajeCalificacionTotal)
                {
                    if (listaPuntajeCalificacionTotal.Count > 0)
                    {
                        listaNombreEvaluacion.Add(new PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO
                        {
                            Id = item.IdEvaluacion.Value,
                            Nombre = item.NombreEvaluacion,
                            CalificacionTotal = item.CalificacionTotal,
                            CalificaAgrupadoNoIndependiente = item.CalificaAgrupadoNoIndependiente
                        });
                    }

                }
                var listaEvaluaciones = listaNombreEvaluacion.GroupBy(u => (u.Id, u.Nombre, u.CalificacionTotal, u.CalificaAgrupadoNoIndependiente))
                        .Select(group => new PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO
                        {
                            Id = group.Key.Id,
                            Nombre = group.Key.Nombre,
                            CalificacionTotal = group.Key.CalificacionTotal,
                            CalificaAgrupadoNoIndependiente = group.Key.CalificaAgrupadoNoIndependiente
                        }).ToList();

                var resultado = new ObtenerExamenDTO
                {
                    ListaPuestoTrabajoRelacion = listaPuestoTrabajoRelacionCompuesto,
                    ListaPuestoTrabajoFuncion = listaPuestoTrabajoFuncion,
                    ListaPuestoTrabajoReporte = listaPuestoTrabajoReporte,
                    ListaPuestoTrabajoCursoComplementario = listaPuestoTrabajoCursoComplementario,
                    ListaPuestoTrabajoExperiencia = listaPuestoTrabajoExperiencia,
                    ListaPuestoTrabajoCaracteristicaPersonal = listaPuestoTrabajoCaracteristicaPersonal,
                    ListaPuestoTrabajoFormacionAcademica = listaPuestoTrabajoFormacionAcademicaProcesada,
                    ListaEvaluacionesPuntajeCalificacion = listaPuntajeCalificacionTotal,
                    ListaEvaluaciones = listaEvaluaciones
                };
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<PuestoTrabajoVersionesDTO> ObtenerListaHistoricoPerfilPuestoTrabajo(int? idPuestoTrabajo)
        {

            try
            {
                List<PuestoTrabajoVersionesDTO> listaPerfilPuestoTrabajoHistorico;
                if (idPuestoTrabajo > 0)
                {
                    listaPerfilPuestoTrabajoHistorico = _unitOfWork.PerfilPuestoTrabajoRepository.ObtenerListaPerfilPuestoTrabajoHistorico(idPuestoTrabajo);
                }
                else
                {
                    listaPerfilPuestoTrabajoHistorico = new List<PuestoTrabajoVersionesDTO>();
                }

                return listaPerfilPuestoTrabajoHistorico;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public bool InsertarActualizarPerfilPuestoTrabajo(PerfilPuestoTrabajoInsertarActualizarDTO dto, string usuario)
        {
            try
            {
                if (_unitOfWork.PuestoTrabajoRepository.Exist(dto.IdPuestoTrabajo))
                {
                    var puestoTrabajo = _unitOfWork.PuestoTrabajoRepository.FirstById(dto.IdPuestoTrabajo);
                    PerfilPuestoTrabajo perfilPuestoTrabajo;
                    //Si es Usuario Aprobado, Crear siempre una nueva versión
                    if (dto.EsUsuarioAprobacion)
                    {
                        dto.CrearNuevaVersion = true;
                    }
                    if (dto.CrearNuevaVersion)
                    {
                        int version = 1;
                        if (dto.EsUsuarioAprobacion)
                        {
                            if (dto.IdPerfilPuestoTrabajo > 0)
                            {
                                var perfilPuestoTrabajoExistente = _unitOfWork.PerfilPuestoTrabajoRepository.FirstById(dto.IdPerfilPuestoTrabajo);
                                perfilPuestoTrabajoExistente.EsActual = false;
                                perfilPuestoTrabajoExistente.UsuarioModificacion = usuario;
                                perfilPuestoTrabajoExistente.FechaModificacion = DateTime.Now;
                                _unitOfWork.PerfilPuestoTrabajoRepository.Update(perfilPuestoTrabajoExistente);
                                _unitOfWork.Commit();

                                version = perfilPuestoTrabajoExistente.Version + 1;
                                //Eliminar Solicitudes Anteriores si existieran
                                var eliminarSolicitudesPendientes = _unitOfWork.PerfilPuestoTrabajoRepository.GetBy(x => x.IdPuestoTrabajo == dto.IdPuestoTrabajo && x.IdPerfilPuestoTrabajoEstadoSolicitud == 5).FirstOrDefault();
                                if (eliminarSolicitudesPendientes != null)
                                {
                                    eliminarSolicitudesPendientes.IdPerfilPuestoTrabajoEstadoSolicitud = 2;
                                    eliminarSolicitudesPendientes.IdPersonalAprobacion = dto.IdPersonal;
                                    eliminarSolicitudesPendientes.UsuarioModificacion = dto.Usuario;
                                    eliminarSolicitudesPendientes.FechaModificacion = DateTime.Now;
                                    _unitOfWork.PerfilPuestoTrabajoRepository.Update(eliminarSolicitudesPendientes);
                                    _unitOfWork.Commit();
                                }
                            }

                            perfilPuestoTrabajo = new PerfilPuestoTrabajo()
                            {
                                IdPuestoTrabajo = puestoTrabajo.Id,
                                Descripcion = dto.Descripcion,
                                Objetivo = dto.Objetivo,
                                EsActual = true,
                                Version = version,
                                Estado = true,
                                UsuarioModificacion = dto.Usuario,
                                UsuarioCreacion = dto.Usuario,
                                FechaModificacion = DateTime.Now,
                                FechaCreacion = DateTime.Now,
                                IdPersonalSolicitud = dto.IdPersonal,
                                FechaSolicitud = DateTime.Now,
                                IdPersonalAprobacion = dto.IdPersonal,
                                FechaAprobacion = DateTime.Now,
                                IdPerfilPuestoTrabajoEstadoSolicitud = 1
                            };
                            var perfil = _unitOfWork.PerfilPuestoTrabajoRepository.Add(perfilPuestoTrabajo);
                            _unitOfWork.Commit();
                            perfilPuestoTrabajo.Id = perfil.Id;
                          
                        }
                        else
                        {
                            var estado = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.FirstBy(x => x.Nombre.Equals("Solicitado"));
                            //_repPerfilPuestoTrabajoEstadoSolicitud.FirstBy(x => x.Nombre.Equals("Solicitado"));
                            int? idEstado = null;
                            if (estado != null)
                            {
                                idEstado = estado.Id;
                            }
                            if (dto.IdPerfilPuestoTrabajo > 0)
                            {
                                var perfilPuestoTrabajoExistente = _unitOfWork.PerfilPuestoTrabajoRepository.FirstById(dto.IdPerfilPuestoTrabajo);
                                version = perfilPuestoTrabajoExistente.Version + 1;
                            }

                            perfilPuestoTrabajo = new PerfilPuestoTrabajo()
                            {
                                IdPuestoTrabajo = puestoTrabajo.Id,
                                Descripcion = dto.Descripcion,
                                Objetivo = dto.Objetivo,
                                EsActual = false,
                                Version = version,
                                Estado = true,
                                UsuarioModificacion = dto.Usuario,
                                UsuarioCreacion = dto.Usuario,
                                FechaModificacion = DateTime.Now,
                                FechaCreacion = DateTime.Now,
                                IdPersonalSolicitud = dto.IdPersonal,
                                FechaSolicitud = DateTime.Now,
                                IdPerfilPuestoTrabajoEstadoSolicitud = idEstado
                            };
                            var perfil = _unitOfWork.PerfilPuestoTrabajoRepository.Add(perfilPuestoTrabajo);
                            _unitOfWork.Commit();
                            perfilPuestoTrabajo.Id = perfil.Id;
                            
                        }

                        if (dto.EstadoPuestoTrabajoCaracteristicaPersonal && dto.PuestoTrabajoCaracteristicaPersonal.Count > 0)
                        {
                            foreach (var item in dto.PuestoTrabajoCaracteristicaPersonal)
                            {
                                PuestoTrabajoCaracteristicaPersonal puestoTrabajoCaracteristicaPersonal = new PuestoTrabajoCaracteristicaPersonal
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    EdadMinima = item.EdadMinima,
                                    EdadMaxima = item.EdadMaxima,
                                    IdEstadoCivil = item.IdEstadoCivil,
                                    IdSexo = item.IdSexo,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoCaracteristicaPersonalRepository.Add(puestoTrabajoCaracteristicaPersonal);
                                _unitOfWork.Commit();

                            }
                        }

                        if (dto.EstadoPuestoTrabajoCursoComplementario && dto.PuestoTrabajoCursoComplementario.Count > 0)
                        {
                            foreach (var item in dto.PuestoTrabajoCursoComplementario)
                            {
                                PuestoTrabajoCursoComplementario puestoTrabajoCursoComplementario = new PuestoTrabajoCursoComplementario
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdCompetenciaTecnica = item.IdCompetenciaTecnica,
                                    IdNivelCompetenciaTecnica = item.IdNivelCompetenciaTecnica,
                                    IdTipoCompetenciaTecnica = item.IdTipoCompetenciaTecnica,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoCursoComplementarioRepository.Add(puestoTrabajoCursoComplementario);
                                _unitOfWork.Commit();
                                //_repPuestoTrabajoCursoComplementario.Insert(puestoTrabajoCursoComplementario);
                            }
                        }

                        if (dto.EstadoPuestoTrabajoExperiencia && dto.PuestoTrabajoExperiencia.Count > 0)
                        {
                            foreach (var item in dto.PuestoTrabajoExperiencia)
                            {
                                PuestoTrabajoExperiencia puestoTrabajoExperiencia = new PuestoTrabajoExperiencia
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdExperiencia = item.IdExperiencia,
                                    IdTipoExperiencia = item.IdTipoExperiencia,
                                    NumeroMinimo = item.NumeroMinimo,
                                    Periodo = item.Periodo,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoExperienciaRepository.Add(puestoTrabajoExperiencia);
                                _unitOfWork.Commit();
                            }
                        }

                        if (dto.EstadoPuestoTrabajoFormacionAcademica && dto.PuestoTrabajoFormacion.Count > 0)
                        {
                            foreach (var item in dto.PuestoTrabajoFormacion)
                            {
                                PuestoTrabajoFormacionAcademica puestoTrabajoFormacionAcademica = new PuestoTrabajoFormacionAcademica
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdAreaFormacion = item.IdAreaFormacion == null ? "" : string.Join(",", item.IdAreaFormacion.Select(x => x)),
                                    IdCentroEstudio = item.IdCentroEstudio == null ? "" : string.Join(",", item.IdCentroEstudio.Select(x => x)),
                                    IdGradoEstudio = item.IdGradoEstudio == null ? "" : string.Join(",", item.IdGradoEstudio.Select(x => x)),
                                    IdNivelEstudio = item.IdNivelEstudio == null ? "" : string.Join(",", item.IdNivelEstudio.Select(x => x)),
                                    IdTipoFormacion = item.IdTipoFormacion == null ? "" : string.Join(",", item.IdTipoFormacion.Select(x => x)),
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoFormacionAcademicaRepository.Add(puestoTrabajoFormacionAcademica);
                                _unitOfWork.Commit();
                            }
                        }

                        if (dto.EstadoPuestoTrabajoFuncion && dto.PuestoTrabajoFuncion.Count > 0)
                        {
                            foreach (var item in dto.PuestoTrabajoFuncion)
                            {
                                PuestoTrabajoFuncion puestoTrabajoFuncion = new PuestoTrabajoFuncion
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
                                    IdPersonalTipoFuncion = item.IdPersonalTipoFuncion,
                                    Nombre = item.Funcion,
                                    NroOrden = item.NroOrden,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoFuncionRepository.Add(puestoTrabajoFuncion);
                                _unitOfWork.Commit();
                                //_repPuestoTrabajoFuncion.Insert(puestoTrabajoFuncion);
                            }
                        }

                        if (dto.EstadoPuestoTrabajoRelacion && dto.PuestoTrabajoRelacion.Count > 0)
                        {
                            foreach (var item in dto.PuestoTrabajoRelacion)
                            {
                                PuestoTrabajoRelacion puestoTrabajoRelacion = new PuestoTrabajoRelacion()
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                var relacion =_unitOfWork.PuestoTrabajoRelacionRepository.Add(puestoTrabajoRelacion);
                                _unitOfWork.Commit();
                                puestoTrabajoRelacion.Id = relacion.Id;
                                //_repPuestoTrabajoRelacion.Insert(puestoTrabajoRelacion);

                                foreach (var rel in item.ListaPuestoRelacionInterna)
                                {
                                    PuestoTrabajoRelacionDetalle puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalle
                                    {
                                        IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
                                        IdPersonalAreaTrabajo = rel.Id,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.Usuario,
                                        UsuarioModificacion = dto.Usuario
                                    };
                                    _unitOfWork.PuestoTrabajoRelacionDetalleRepository.Add(puestoTrabajoRelacionDetalle);
                                    _unitOfWork.Commit();
                                }

                                foreach (var rel in item.ListaPuestoACargo)
                                {
                                    PuestoTrabajoRelacionDetalle puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalle
                                    {
                                        IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
                                        IdPuestoTrabajoPuestoAcargo = rel.Id,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.Usuario,
                                        UsuarioModificacion = dto.Usuario
                                    };
                                    _unitOfWork.PuestoTrabajoRelacionDetalleRepository.Add(puestoTrabajoRelacionDetalle);
                                    _unitOfWork.Commit();
                                }

                                foreach (var rel in item.ListaPuestoDependencia)
                                {
                                    PuestoTrabajoRelacionDetalle puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalle
                                    {
                                        IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
                                        IdPuestoTrabajoDependencia = rel.Id,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.Usuario,
                                        UsuarioModificacion = dto.Usuario
                                    };
                                    _unitOfWork.PuestoTrabajoRelacionDetalleRepository.Add(puestoTrabajoRelacionDetalle);
                                    _unitOfWork.Commit();
                                }
                            }
                        }

                        if (dto.EstadoPuestoTrabajoReporte && dto.PuestoTrabajoReporte.Count > 0)
                        {
                            foreach (var item in dto.PuestoTrabajoReporte)
                            {
                                PuestoTrabajoReporte puestoTrabajoReporte = new PuestoTrabajoReporte
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
                                    Nombre = item.Reporte,
                                    NroOrden = item.NroOrden,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoReporteRepository.Add(puestoTrabajoReporte);
                                _unitOfWork.Commit();

                            }
                        }

                        var listaPuntajePrevio = _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.GetBy(x => x.IdPerfilPuestoTrabajo == dto.IdPerfilPuestoTrabajo).ToList();
                        //_repPuestoTrabajoPuntajeCalificacion.GetBy(x => x.IdPerfilPuestoTrabajo == PerfilPuesto.IdPerfilPuestoTrabajo).ToList();

                        foreach (var item in listaPuntajePrevio)
                        {
                            PuestoTrabajoPuntajeCalificacion puntajeAnt = new PuestoTrabajoPuntajeCalificacion()
                            {
                                IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                IdExamen = item.IdExamen,
                                IdExamenTest = item.IdExamenTest,
                                IdGrupoComponenteEvaluacion = item.IdGrupoComponenteEvaluacion,
                                CalificaPorCentil = item.CalificaPorCentil,
                                PuntajeMinimo = item.PuntajeMinimo,
                                IdProcesoSeleccionRango = item.IdProcesoSeleccionRango,
                                EsCalificable = item.EsCalificable,
                                Estado = true,
                                UsuarioCreacion = dto.Usuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = dto.Usuario,
                                FechaModificacion = DateTime.Now
                            };
                            _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.Add(puntajeAnt);
                            _unitOfWork.Commit();

                        }

                        foreach (var item in dto.Puntaje.ListaPuntaje)
                        {
                            PuestoTrabajoPuntajeCalificacion puntaje = _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.BuscarPorIdPerfilAndIdEvaluacionAndIdGrupoAndIdComponente(perfilPuestoTrabajo.Id, item.IdEvaluacion, item.IdGrupo, item.IdComponente);
                            //_unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.FirstBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == item.IdComponente);
                            // _repPuestoTrabajoPuntajeCalificacion.FirstBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == item.IdComponente);
                            if (puntaje != null && puntaje.Id != null && puntaje.Id != 0)
                            {

                                puntaje.CalificaPorCentil = item.CalificaPorCentil;
                                puntaje.PuntajeMinimo = item.Puntaje;
                                puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
                                puntaje.EsCalificable = item.EsCalificable;

                                puntaje.UsuarioModificacion = dto.Usuario;
                                puntaje.FechaModificacion = DateTime.Now;
                                _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.Update(puntaje);
                                _unitOfWork.Commit();

                            }
                            else
                            {
                                puntaje = new PuestoTrabajoPuntajeCalificacion()
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdExamen = item.IdComponente,
                                    IdExamenTest = item.IdEvaluacion,
                                    IdGrupoComponenteEvaluacion = item.IdGrupo,
                                    CalificaPorCentil = item.CalificaPorCentil,
                                    PuntajeMinimo = item.Puntaje,
                                    IdProcesoSeleccionRango = item.IdProcesoSeleccionRango,
                                    EsCalificable = item.EsCalificable,
                                    Estado = true,
                                    UsuarioCreacion = dto.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    UsuarioModificacion = dto.Usuario,
                                    FechaModificacion = DateTime.Now
                                };
                                _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.Add(puntaje);
                                _unitOfWork.Commit();
                            }
                        }
          
                    }
                    else
                    {
                        perfilPuestoTrabajo = _unitOfWork.PerfilPuestoTrabajoRepository.ObtenerIdPuestoTrabajoANDIdPerfilPuestoTrabajoEstadoSolicitud(dto.IdPuestoTrabajo, 5);
                        //GetBy(x => x.IdPuestoTrabajo == dto.IdPuestoTrabajo && x.IdPerfilPuestoTrabajoEstadoSolicitud == 5).FirstOrDefault();
                        perfilPuestoTrabajo.Objetivo = dto.Objetivo;
                        perfilPuestoTrabajo.Descripcion = dto.Descripcion;
                        perfilPuestoTrabajo.UsuarioModificacion = dto.Usuario;
                        perfilPuestoTrabajo.FechaModificacion = DateTime.Now;
                        _unitOfWork.PerfilPuestoTrabajoRepository.Update(perfilPuestoTrabajo);
                        _unitOfWork.Commit();

                        dto.IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id;

                        if (dto.EstadoPuestoTrabajoCaracteristicaPersonal)
                        {
                            var listaPuestoTrabajoCaracteristicaPersonal = _unitOfWork.PuestoTrabajoCaracteristicaPersonalRepository.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            //_repPuestoTrabajoCaracteristicaPersonal.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            foreach (var item in listaPuestoTrabajoCaracteristicaPersonal)
                            {
                                if (!dto.PuestoTrabajoCaracteristicaPersonal.Any(x => x.Id == item.Id))
                                {
                                    _unitOfWork.PuestoTrabajoCaracteristicaPersonalRepository.Delete(item.Id, dto.Usuario);
                                    //_repPuestoTrabajoCaracteristicaPersonal.Delete(item.Id, dto.Usuario);
                                }

                            }
                            foreach (var item in dto.PuestoTrabajoCaracteristicaPersonal)
                            {
                                PuestoTrabajoCaracteristicaPersonal puestoTrabajoCaracteristicaPersonal = new PuestoTrabajoCaracteristicaPersonal
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    EdadMinima = item.EdadMinima,
                                    EdadMaxima = item.EdadMaxima,
                                    IdEstadoCivil = item.IdEstadoCivil,
                                    IdSexo = item.IdSexo,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoCaracteristicaPersonalRepository.Add(puestoTrabajoCaracteristicaPersonal);
                                _unitOfWork.Commit();
                                //_repPuestoTrabajoCaracteristicaPersonal.Insert(puestoTrabajoCaracteristicaPersonal);
                            }
                        }

                        if (dto.EstadoPuestoTrabajoCursoComplementario)
                        {
                            var listaPuestoTrabajoCursoComplementario = _unitOfWork.PuestoTrabajoCursoComplementarioRepository.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            //_repPuestoTrabajoCursoComplementario.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            foreach (var item in listaPuestoTrabajoCursoComplementario)
                            {
                                if (!dto.PuestoTrabajoCursoComplementario.Any(x => x.Id == item.Id))
                                {
                                    _unitOfWork.PuestoTrabajoCursoComplementarioRepository.Delete(item.Id, dto.Usuario);
                                    //_repPuestoTrabajoCursoComplementario.Delete(item.Id, dto.Usuario);
                                }
                            }

                            foreach (var item in dto.PuestoTrabajoCursoComplementario)
                            {
                                PuestoTrabajoCursoComplementario puestoTrabajoCursoComplementario = new PuestoTrabajoCursoComplementario
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdCompetenciaTecnica = item.IdCompetenciaTecnica,
                                    IdNivelCompetenciaTecnica = item.IdNivelCompetenciaTecnica,
                                    IdTipoCompetenciaTecnica = item.IdTipoCompetenciaTecnica,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoCursoComplementarioRepository.Add(puestoTrabajoCursoComplementario);
                                _unitOfWork.Commit();
                                //_repPuestoTrabajoCursoComplementario.Insert(puestoTrabajoCursoComplementario);
                            }
                        }

                        if (dto.EstadoPuestoTrabajoExperiencia)
                        {
                            var listaPuestoTrabajoExperiencia = _unitOfWork.PuestoTrabajoExperienciaRepository.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            //_repPuestoTrabajoExperiencia.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            foreach (var item in listaPuestoTrabajoExperiencia)
                            {
                                if (!dto.PuestoTrabajoExperiencia.Any(x => x.Id == item.Id))
                                {
                                    _unitOfWork.PuestoTrabajoExperienciaRepository.Delete(item.Id, dto.Usuario);
                                }
                            }
                            foreach (var item in dto.PuestoTrabajoExperiencia)
                            {
                                PuestoTrabajoExperiencia puestoTrabajoExperiencia = new PuestoTrabajoExperiencia
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdExperiencia = item.IdExperiencia,
                                    IdTipoExperiencia = item.IdTipoExperiencia,
                                    NumeroMinimo = item.NumeroMinimo,
                                    Periodo = item.Periodo,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoExperienciaRepository.Add(puestoTrabajoExperiencia);
                                _unitOfWork.Commit();
                            }
                        }

                        if (dto.EstadoPuestoTrabajoFormacionAcademica)
                        {
                            var listaPuestoTrabajoFormacion = _unitOfWork.PuestoTrabajoFormacionAcademicaRepository.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            //_repPuestoTrabajoFormacionAcademica.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            foreach (var item in listaPuestoTrabajoFormacion)
                            {
                                if (!dto.PuestoTrabajoFormacion.Any(x => x.Id == item.Id))
                                {
                                    _unitOfWork.PuestoTrabajoFormacionAcademicaRepository.Delete(item.Id, dto.Usuario);
                                }
                            }
                            foreach (var item in dto.PuestoTrabajoFormacion)
                            {
                                PuestoTrabajoFormacionAcademica puestoTrabajoFormacionAcademica = new PuestoTrabajoFormacionAcademica
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdAreaFormacion = item.IdAreaFormacion == null ? "" : string.Join(",", item.IdAreaFormacion.Select(x => x)),
                                    IdCentroEstudio = item.IdCentroEstudio == null ? "" : string.Join(",", item.IdCentroEstudio.Select(x => x)),
                                    IdGradoEstudio = item.IdGradoEstudio == null ? "" : string.Join(",", item.IdGradoEstudio.Select(x => x)),
                                    IdNivelEstudio = item.IdNivelEstudio == null ? "" : string.Join(",", item.IdNivelEstudio.Select(x => x)),
                                    IdTipoFormacion = item.IdTipoFormacion == null ? "" : string.Join(",", item.IdTipoFormacion.Select(x => x)),
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoFormacionAcademicaRepository.Add(puestoTrabajoFormacionAcademica);
                                _unitOfWork.Commit();
                            }
                        }

                        if (dto.EstadoPuestoTrabajoFuncion)
                        {
                            var listaPuestoTrabajoFuncion = _unitOfWork.PuestoTrabajoFuncionRepository.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            //_repPuestoTrabajoFuncion.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            foreach (var item in listaPuestoTrabajoFuncion)
                            {
                                if (!dto.PuestoTrabajoFuncion.Any(x => x.Id == item.Id))
                                {
                                    _unitOfWork.PuestoTrabajoFuncionRepository.Delete(item.Id, dto.Usuario);
                                }
                            }
                            foreach (var item in dto.PuestoTrabajoFuncion)
                            {
                                PuestoTrabajoFuncion puestoTrabajoFuncion = new PuestoTrabajoFuncion
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
                                    IdPersonalTipoFuncion = item.IdPersonalTipoFuncion,
                                    Nombre = item.Funcion,
                                    NroOrden = item.NroOrden,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoFuncionRepository.Add(puestoTrabajoFuncion);
                                _unitOfWork.Commit();
                            }
                        }

                        if (dto.EstadoPuestoTrabajoRelacion)
                        {
                            var listaPuestoTrabajoRelacion = _unitOfWork.PuestoTrabajoRelacionRepository.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            //_repPuestoTrabajoRelacion.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            foreach (var item in listaPuestoTrabajoRelacion)
                            {
                                if (!dto.PuestoTrabajoRelacion.Any(x => x.Id == item.Id))
                                {
                                    var list = _unitOfWork.PuestoTrabajoRelacionDetalleRepository.GetBy(x => x.IdPuestoTrabajoRelacion == item.Id);
                                    //_repPuestoTrabajoRelacionDetalle.GetBy(x => x.IdPuestoTrabajoRelacion == item.Id);
                                    foreach (var it in list)
                                    {
                                        _unitOfWork.PuestoTrabajoRelacionDetalleRepository.Delete(it.Id, dto.Usuario);
                                    }
                                    _unitOfWork.PuestoTrabajoRelacionRepository.Delete(item.Id, dto.Usuario);
                                }
                            }

                            foreach (var item in dto.PuestoTrabajoRelacion)
                            {
                                PuestoTrabajoRelacion puestoTrabajoRelacion = new PuestoTrabajoRelacion()
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoRelacionRepository.Add(puestoTrabajoRelacion);
                                _unitOfWork.Commit();

                                foreach (var rel in item.ListaPuestoRelacionInterna)
                                {
                                    PuestoTrabajoRelacionDetalle puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalle
                                    {
                                        IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
                                        IdPersonalAreaTrabajo = rel.Id,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.Usuario,
                                        UsuarioModificacion = dto.Usuario
                                    };
                                    _unitOfWork.PuestoTrabajoRelacionDetalleRepository.Add(puestoTrabajoRelacionDetalle);
                                    _unitOfWork.Commit();
                                }

                                foreach (var rel in item.ListaPuestoACargo)
                                {
                                    PuestoTrabajoRelacionDetalle puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalle
                                    {
                                        IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
                                        IdPuestoTrabajoPuestoAcargo = rel.Id,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.Usuario,
                                        UsuarioModificacion = dto.Usuario
                                    };
                                    _unitOfWork.PuestoTrabajoRelacionDetalleRepository.Add(puestoTrabajoRelacionDetalle);
                                    _unitOfWork.Commit();
                                }

                                foreach (var rel in item.ListaPuestoDependencia)
                                {
                                    PuestoTrabajoRelacionDetalle puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalle
                                    {
                                        IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
                                        IdPuestoTrabajoDependencia = rel.Id,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.Usuario,
                                        UsuarioModificacion = dto.Usuario
                                    };
                                    _unitOfWork.PuestoTrabajoRelacionDetalleRepository.Add(puestoTrabajoRelacionDetalle);
                                    _unitOfWork.Commit();
                                }
                            }

                        }

                        if (dto.EstadoPuestoTrabajoReporte)
                        {
                            var listaPuestoTrabajoReporte = _unitOfWork.PuestoTrabajoReporteRepository.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            //_repPuestoTrabajoReporte.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
                            foreach (var item in listaPuestoTrabajoReporte)
                            {
                                if (!dto.PuestoTrabajoReporte.Any(x => x.Id == item.Id))
                                {
                                    _unitOfWork.PuestoTrabajoReporteRepository.Delete(item.Id, dto.Usuario);
                                }
                            }
                            foreach (var item in dto.PuestoTrabajoReporte)
                            {
                                PuestoTrabajoReporte puestoTrabajoReporte = new PuestoTrabajoReporte
                                {
                                    IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
                                    IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
                                    Nombre = item.Reporte,
                                    NroOrden = item.NroOrden,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = dto.Usuario,
                                    UsuarioModificacion = dto.Usuario
                                };
                                _unitOfWork.PuestoTrabajoReporteRepository.Add(puestoTrabajoReporte);
                                _unitOfWork.Commit();
                            }
                        }


                        foreach (var item in dto.Puntaje.ListaPuntaje)
                        {

                            PuestoTrabajoPuntajeCalificacion puntaje = _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.BuscarPorIdPerfilAndIdEvaluacionAndIdGrupoAndIdComponente(perfilPuestoTrabajo.Id, item.IdEvaluacion, item.IdGrupo, item.IdComponente);
                            //_repPuestoTrabajoPuntajeCalificacion.FirstBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == item.IdComponente);
                            if (puntaje != null && puntaje.Id != null && puntaje.Id != 0)
                            {

                                puntaje.CalificaPorCentil = item.CalificaPorCentil;
                                puntaje.PuntajeMinimo = item.Puntaje;
                                puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
                                puntaje.EsCalificable = item.EsCalificable;

                                puntaje.UsuarioModificacion = dto.Usuario;
                                puntaje.FechaModificacion = DateTime.Now;
                                _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.Update(puntaje);
                                _unitOfWork.Commit();
                            }
                            else
                            {
                                puntaje = new PuestoTrabajoPuntajeCalificacion();
                                puntaje.IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id;
                                puntaje.IdExamen = item.IdComponente;
                                puntaje.IdExamenTest = item.IdEvaluacion;
                                puntaje.IdGrupoComponenteEvaluacion = item.IdGrupo;
                                puntaje.CalificaPorCentil = item.CalificaPorCentil;
                                puntaje.PuntajeMinimo = item.Puntaje;
                                puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
                                puntaje.EsCalificable = item.EsCalificable;

                                puntaje.Estado = true;
                                puntaje.UsuarioCreacion = dto.Usuario;
                                puntaje.FechaCreacion = DateTime.Now;
                                puntaje.UsuarioModificacion = dto.Usuario;
                                puntaje.FechaModificacion = DateTime.Now;
                                _unitOfWork.PuestoTrabajoPuntajeCalificacionRepository.Add(puntaje);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                    
                }
                _unitOfWork.Commit();
                return true;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.PuestoTrabajoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PuestoTrabajoRepository.Delete(id, usuario);

                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertarActualizarInterfaz( AsignarInterfazDTO ListaAsignar , string usuarioP)
        {
           
            try
            {
 
                var listaAsignados = _unitOfWork.ModuloSistemaPuestoTrabajoRepository.GetBy(x => x.IdPuestoTrabajo == ListaAsignar.Id).ToList();
         
                //var listaModuloPuestoTrabajo = _repPuestoTrabajo.ObtenerModulosPorPuestoTrabajo(ListaAsignar.Id);

                ModuloSistemaPuestoTrabajo agregar;
                foreach (var item in listaAsignados)
                {
                    var existeAsignacion = ListaAsignar.ListaAsignacion.Where(x => x.IdModuloSistema == item.IdModuloSistema).FirstOrDefault();
                    if (existeAsignacion == null)
                    {
                        var eliminar = _unitOfWork.ModuloSistemaPuestoTrabajoRepository.FirstById(item.Id);
                        _unitOfWork.ModuloSistemaPuestoTrabajoRepository.Delete(eliminar.Id, usuarioP);
                    }
                    else
                    {
                        ListaAsignar.ListaAsignacion.Remove(existeAsignacion);
                    }
                }
                foreach (var item in ListaAsignar.ListaAsignacion)
                {
                    agregar = new ModuloSistemaPuestoTrabajo()
                    {
                        IdModuloSistema = item.IdModuloSistema,
                        IdPuestoTrabajo = ListaAsignar.Id,
                        Estado = true,
                        UsuarioCreacion = usuarioP,
                        UsuarioModificacion = usuarioP,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.ModuloSistemaPuestoTrabajoRepository.Add(agregar);
                    _unitOfWork.Commit();
                }

                var listaPersonalCambio = _unitOfWork.PersonalPuestoSedeHistoricoRepository.GetBy(x => x.IdPuestoTrabajo == ListaAsignar.Id && x.Actual == true).ToList();
                foreach (var personal in listaPersonalCambio)
                {
                    //Asignación de módulos
                    var usuario = _unitOfWork.UsuarioRepository.GetBy(x => x.IdPersonal == personal.IdPersonal).FirstOrDefault();
                    if (usuario != null)
                    {
                        var listaModuloAnterior = _unitOfWork.ModuloSistemaAccesoRepository.GetBy(x => x.IdUsuario == usuario.Id).ToList();
                        var listaModuloNuevo = _unitOfWork.ModuloSistemaPuestoTrabajoRepository.GetBy(x => x.IdPuestoTrabajo == personal.IdPuestoTrabajo).ToList();
                        if (listaModuloAnterior.Count > 0)
                        {
                            foreach (var moduloAnterior in listaModuloAnterior)
                            {
                                _unitOfWork.ModuloSistemaAccesoRepository.Delete(moduloAnterior.Id, usuarioP);
                            }
                        }

                        if (listaModuloNuevo.Count > 0)
                        {
                            ModuloSistemaAccesoV5 agregarModulo;
                            foreach (var moduloNuevo in listaModuloNuevo)
                            {
                                agregarModulo = new ModuloSistemaAccesoV5()
                                {
                                    IdUsuarioRol = usuario.IdUsuarioRol,
                                    IdUsuario = usuario.Id,
                                    IdModuloSistema = moduloNuevo.IdModuloSistema,
                                    Estado = true,
                                    UsuarioCreacion = usuarioP,
                                    UsuarioModificacion = usuarioP,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                _unitOfWork.ModuloSistemaAccesoRepository.Add(agregarModulo);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AprobarRechazarVersionPerfilPuestoTrabajo(AprobacionRechazoPerfilPuestoTrabajoDTO dto, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var perfilPuestoTrabajo = _unitOfWork.PerfilPuestoTrabajoRepository.FirstById(dto.IdPerfilPuestoTrabajo);
                    if (perfilPuestoTrabajo != null)
                    {
                        int? idEstado = null;
                        if (dto.TipoBoton) //Aprobado
                        {
                            var listaPerfilesAnteriores = _unitOfWork.PerfilPuestoTrabajoRepository.GetBy(x => x.IdPuestoTrabajo == perfilPuestoTrabajo.IdPuestoTrabajo && x.EsActual == true).ToList();
                            foreach (var item in listaPerfilesAnteriores)
                            {
                                item.EsActual = false;
                                item.UsuarioModificacion = usuario;
                                item.FechaModificacion = DateTime.Now;
                                _unitOfWork.PerfilPuestoTrabajoRepository.Update(item);
                            }

                            perfilPuestoTrabajo.EsActual = true;
                            var estado = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.FirstBy(x => x.Nombre.Equals("Aprobado"));
                   
                            if (estado != null)
                            {
                                idEstado = estado.Id;
                            }
                            perfilPuestoTrabajo.IdPerfilPuestoTrabajoEstadoSolicitud = idEstado;
                        }
                        else //Rechazado
                        {
                            perfilPuestoTrabajo.EsActual = false;
                            var estado = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.FirstBy(x => x.Nombre.Equals("Rechazado"));
                            if (estado != null)
                            {
                                idEstado = estado.Id;
                            }
                            perfilPuestoTrabajo.IdPerfilPuestoTrabajoEstadoSolicitud = idEstado;
                        }
                        perfilPuestoTrabajo.IdPersonalAprobacion = dto.IdPersonal;
                        perfilPuestoTrabajo.FechaAprobacion = DateTime.Now;
                        perfilPuestoTrabajo.Observacion = dto.Observacion;
                        perfilPuestoTrabajo.UsuarioModificacion = usuario;
                        perfilPuestoTrabajo.FechaModificacion = DateTime.Now;
                        _unitOfWork.PerfilPuestoTrabajoRepository.Update(perfilPuestoTrabajo);
                        scope.Complete();

                        return true;
                    }
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PersonalAprobacionDTO> EsPersonalAprobacionVersion(int idPersonal)
        {

            try
            {
                List<PersonalAprobacionDTO> listaPerfilPuestoTrabajoHistorico;
                if (idPersonal > 0)
                {
                  
                    listaPerfilPuestoTrabajoHistorico = _unitOfWork.PerfilPuestoTrabajoRepository.ObtenerPersonalAprobacionVersion(idPersonal);
                }
                else
                {
                    listaPerfilPuestoTrabajoHistorico = new List<PersonalAprobacionDTO>();
                }

                return listaPerfilPuestoTrabajoHistorico;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<ComboDTO> ObtenerCombo ()
        {
            return _unitOfWork.PuestoTrabajoRepository.ObtenerCombo();
        }
    }
}