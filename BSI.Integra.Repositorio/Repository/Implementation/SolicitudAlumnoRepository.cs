
using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolicitudAlumnoRepository
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2023
    /// <summary>
    /// Gestión general de T_SolicitudAlumno
    /// </summary>
    public class SolicitudAlumnoRepository : GenericRepository<TSolicitudAlumno>, ISolicitudAlumnoRepository
    {
        private Mapper _mapper;

        public SolicitudAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitudAlumno, SolicitudAlumno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSolicitudAlumno MapeoEntidad(SolicitudAlumno entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitudAlumno modelo = _mapper.Map<TSolicitudAlumno>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudAlumno Add(SolicitudAlumno entidad)
        {
            try
            {
                var SolicitudAlumno = MapeoEntidad(entidad);
                base.Insert(SolicitudAlumno);
                return SolicitudAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudAlumno Update(SolicitudAlumno entidad)
        {
            try
            {
                var SolicitudAlumno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudAlumno.RowVersion = entidadExistente.RowVersion;
                base.Update(SolicitudAlumno);
                return SolicitudAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TSolicitudAlumno> Add(IEnumerable<SolicitudAlumno> listadoEntidad)
        {
            try
            {
                List<TSolicitudAlumno> listado = new List<TSolicitudAlumno>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TSolicitudAlumno> Update(IEnumerable<SolicitudAlumno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitudAlumno> listado = new List<TSolicitudAlumno>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes de alumno segun area
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorArea(int idPersonal)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudPorArea", new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes de alumno segun personal
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorPersonal(int idPersonal)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudPorPersonal", new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes del alumno por filtro.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltro(FiltroSolicitudesDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudPorFiltro", new { FiltroSolicitud.IdPersonal, FiltroSolicitud.TipoFiltro, FiltroSolicitud.Filtro1, FiltroSolicitud.Filtro2 });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes del alumno por filtro reporte
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumno(FiltroSolicitudAlumnoDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                FiltroSolicitudAlumnoReporteDTO filtroFinal = new FiltroSolicitudAlumnoReporteDTO();
                if (FiltroSolicitud.IdEstadoSolicitud.Count() > 0)
                {
                    filtroFinal.IdEstadoSolicitud = String.Join(",", FiltroSolicitud.IdEstadoSolicitud);
                }
                if (FiltroSolicitud.FechaInicio != null)
                {
                    filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio.Value.Date;
                }

                if (FiltroSolicitud.FechaFin != null)
                {
                    filtroFinal.FechaFin = FiltroSolicitud.FechaFin.Value.Date.AddDays(1).AddSeconds(-1);
                }
                filtroFinal.IdMatriculaCabecera = FiltroSolicitud.IdMatriculaCabecera;
                //filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio;
                //filtroFinal.FechaFin = FiltroSolicitud.FechaFin;
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudAlumno", new { filtroFinal.IdMatriculaCabecera, filtroFinal.IdEstadoSolicitud, filtroFinal.FechaInicio, filtroFinal.FechaFin });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes del alumno por filtro reporte
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumnoRevision(FiltroSolicitudAlumnoDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                FiltroSolicitudAlumnoReporteDTO filtroFinal = new FiltroSolicitudAlumnoReporteDTO();
                if (FiltroSolicitud.IdEstadoSolicitud.Count() > 0)
                {
                    filtroFinal.IdEstadoSolicitud = String.Join(",", FiltroSolicitud.IdEstadoSolicitud);
                }
                if (FiltroSolicitud.FechaInicio != null)
                {
                    filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio.Value.Date;
                }

                if (FiltroSolicitud.FechaFin != null)
                {
                    filtroFinal.FechaFin = FiltroSolicitud.FechaFin.Value.Date.AddDays(1).AddSeconds(-1);
                }
                filtroFinal.IdMatriculaCabecera = FiltroSolicitud.IdMatriculaCabecera;
                filtroFinal.IdUsuario = FiltroSolicitud.IdUsuario;
                //filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio;
                //filtroFinal.FechaFin = FiltroSolicitud.FechaFin;
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudAlumnoRevision", new { filtroFinal.IdMatriculaCabecera, filtroFinal.IdEstadoSolicitud, filtroFinal.FechaInicio, filtroFinal.FechaFin , filtroFinal.IdUsuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes del alumno por filtro reporte
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAlumnoGestion(FiltroSolicitudAlumnoDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                FiltroSolicitudAlumnoReporteDTO filtroFinal = new FiltroSolicitudAlumnoReporteDTO();
                if (FiltroSolicitud.FechaInicio != null)
                {
                    filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio.Value.Date;
                }

                if (FiltroSolicitud.FechaFin != null)
                {
                    filtroFinal.FechaFin = FiltroSolicitud.FechaFin.Value.Date.AddDays(1).AddSeconds(-1);
                }
                //nuevoFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                //nuevoFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                if (FiltroSolicitud.IdEstadoSolicitud.Count() > 0)
                {
                    filtroFinal.IdEstadoSolicitud = String.Join(",", FiltroSolicitud.IdEstadoSolicitud);
                }
                filtroFinal.IdMatriculaCabecera = FiltroSolicitud.IdMatriculaCabecera;
                filtroFinal.IdUsuario = FiltroSolicitud.IdUsuario;
                //filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio;
                //filtroFinal.FechaFin = FiltroSolicitud.FechaFin;
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudAlumnoGestion", new { filtroFinal.IdMatriculaCabecera, filtroFinal.IdEstadoSolicitud, filtroFinal.FechaInicio, filtroFinal.FechaFin, filtroFinal.IdUsuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las solicitudes del alumno por filtro reporte
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudAlumnoFiltradaDTO </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolicitudesDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudPorFiltroReporte", new { FiltroSolicitud.IdPersonal, FiltroSolicitud.TipoFiltro, FiltroSolicitud.Filtro1, FiltroSolicitud.Filtro2 });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph LLanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener por Id.
        /// </summary>
        /// <param name="SolicitudAlumno"> Datos actualizar </param>
        /// <returns> RevisarSolicitudAlumnoDTO </returns>
        public SolicitudAlumno ObtenerPorId(int id)
        {
            try
            {
                SolicitudAlumno respuesta = new SolicitudAlumno();
                var query = @"SELECT Id,
                                    IdEstadoSolicitud,
                                    IdPersonal,
                                    IdSolicitud,
                                    IdMatriculaCabecera,
                                    IdPEspescifico,
                                    DetalleSolicitud,
                                    ContentTypeSolicitante,
                                    NombreArchivoSolicitante,
                                    ContentTypeSolucion,
                                    NombreArchivoSolucion,
                                    ComentarioSolucion,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    IdControlSolicitudOrigen
                            FROM ope.T_SolicitudAlumno
                            WHERE Estado = 1
                                  AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<SolicitudAlumno>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 23/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del historial de solicitudes por alumno.
        /// </summary>
        /// <param name="id"> Id matricula y idPespecifico </param>
        /// <returns> Solicitud </returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> obtenerSolicitudesAlumno()
        {
            try
            {
                var rpta = new List<SolicitudAlumnoFiltradaDTO>();
                var query = @" SELECT id,
                                      IdMatriculaCabecera,
                                      CodigoMatricula,
                                      NombreAlumno,
                                      IdPEspecifico,
                                      NombrePEspecifico,
                                      IdCentroCosto,
                                      CentroCosto,
                                      IdPGeneral,
                                      PGeneral,
                                      DetalleSolicitud,
                                      TipoSolicitud,
                                      Prioridad,
                                      IdSolicitud,
                                      NombreSolicitud,
                                      IdTipoReporte,
                                      NombreTipoReporte,
                                      IdSolicitudCategoria,
                                      NombreSolicitudCategoria,
                                      IdSubCategoria,
                                      NombreSubCategoria,
                                      IdSolicitante,
                                      NombreSolicitante,
                                      IdAreaSolicitante,
                                      AreaSolicitante,
                                      IdAreaRevision,
                                      AreaRevision,
                                      NombreArchivoSolicitante,
                                      IdPersonalRevision,
                                      PersonalRevision,
                                      IdAreaSolucion,
                                      AreaSolucion,
                                      IdPersonalSolucion,
                                      PersonalSolucion,
                                      FechaRegistro,
                                      ComentarioSolucion,
                                      NombreArchivoSolucion,
                                      IdEstadoSolicitud,
                                      EstadoSolicitud,
                                      Email 
                                      FROM ope.V_ObtenerFiltroSolicitud 
                                      ORDER BY FechaRegistro desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del historial de solicitudes por alumno.
        /// </summary>
        /// <param name="id"> Id matricula y idPespecifico </param>
        /// <returns> Solicitud </returns>
        public IEnumerable<SolicitudLogDTO> obtenerLogSolicitudes(int IdSolicitud)
        {
            try
            {
                var rpta = new List<SolicitudLogDTO>();
                var query = @"SELECT IdSolicitudAlumno,IdMatriculaCabecera, Campo, ValorNuevo, ValorAnterior, FechaModificacion,UsuarioModificacion 
                            FROM ope.V_SolicitudAlumnoLog
                            WHERE IdSolicitudAlumno = @IdSolicitud
                             ORDER BY FechaModificacion desc";
                var resultado = _dapperRepository.QueryDapper(query, new { IdSolicitud = IdSolicitud });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudLogDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 12/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene personal que esté dentro de Solicitud de alumnos.
        /// </summary>
        /// <param> </param>
        /// <returns> Solicitud </returns>
        public IEnumerable<SolicitudPersonalAlumnoDTO> ObtenerPersonalSolicitanteAlumno()
        {
            try
            {
                var rpta = new List<SolicitudPersonalAlumnoDTO>();
                var query = "SELECT * FROM ope.V_ObtenerPersonalSolicitanteAlumnos ORDER BY Personal";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudPersonalAlumnoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 12/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene personal que esté dentro de Solicitud de alumnos, filtrado por Id's.
        /// </summary>
        /// <param name="id"> Lista con id's de personal asignado </param>
        /// <returns> Solicitud </returns>
        public IEnumerable<SolicitudPersonalSolucionAlumnoDTO> ObtenerPersonalSolucionSolicitudAlumno(List<int> IdPersonal)
        {
            try
            {
                var rpta = new List<SolicitudPersonalSolucionAlumnoDTO>();
                var resultado = "";
                if (IdPersonal != null) {
                    if (IdPersonal.Count > 0) {
                        var query = "SELECT * FROM ope.V_ObtenerPersonalSolucionSolicitudAlumnos WHERE IdAreaTrabajo IN @Ids ORDER BY Personal";
                        resultado = _dapperRepository.QueryDapper(query, new { Ids = IdPersonal });
                    }
                    else
                    {
                        var query = "SELECT * FROM ope.V_ObtenerPersonalSolucionSolicitudAlumnos ORDER BY Personal";
                        resultado = _dapperRepository.QueryDapper(query, null);
                    }
                    if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    {
                        rpta = JsonConvert.DeserializeObject<List<SolicitudPersonalSolucionAlumnoDTO>>(resultado);
                    }
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 15/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las solicitudes de solicitud de alumno según parámetros
        /// </summary>
        /// <param> </param>
        /// <returns> ReporteSolicitudAlumnoDTO </returns>
        public IEnumerable<ReporteSolicitudAlumnoDTO> ObtenerReporteSolicitudesPorFiltroAlumno(FiltroReporteSolicitudAlumnoDTO FiltroReporteSolicitud)
        {
            try
            {
                List<ReporteSolicitudAlumnoDTO> rpta = new List<ReporteSolicitudAlumnoDTO>();
                FiltroReporteSolicitudAlumnoReporteDTO filtroFinal = new FiltroReporteSolicitudAlumnoReporteDTO();
                if(FiltroReporteSolicitud.IdMatriculaCabecera == 0)
                {
                    filtroFinal.IdMatriculaCabecera = null;
                }
                else
                {
                    filtroFinal.IdMatriculaCabecera = FiltroReporteSolicitud.IdMatriculaCabecera;
                }
                if (FiltroReporteSolicitud.IdEstadoSolicitud.Count() > 0)
                {
                    filtroFinal.IdEstadoSolicitud = String.Join(",", FiltroReporteSolicitud.IdEstadoSolicitud);
                }
                if (FiltroReporteSolicitud.IdSolicitante.Count() > 0)
                {
                    filtroFinal.IdSolicitante = String.Join(",", FiltroReporteSolicitud.IdSolicitante);
                }
                if (FiltroReporteSolicitud.IdOrigen.Count() > 0)
                {
                    filtroFinal.IdOrigen = String.Join(",", FiltroReporteSolicitud.IdOrigen);
                }
                if (FiltroReporteSolicitud.IdAreaSolucion.Count() > 0)
                {
                    filtroFinal.IdAreaSolucion = String.Join(",", FiltroReporteSolicitud.IdAreaSolucion);
                }
                if (FiltroReporteSolicitud.IdPersonalSolucion.Count() > 0)
                {
                    filtroFinal.IdPersonalSolucion = String.Join(",", FiltroReporteSolicitud.IdPersonalSolucion);
                }
                if (FiltroReporteSolicitud.FechaInicio != null)
                {
                    filtroFinal.FechaInicio = FiltroReporteSolicitud.FechaInicio.Value.Date;
                }
                if (FiltroReporteSolicitud.FechaFin != null)
                {
                    filtroFinal.FechaFin = FiltroReporteSolicitud.FechaFin.Value.Date.AddDays(1).AddSeconds(-1);
                }

                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerReporteSolicitudAlumno", new {
                    filtroFinal.IdMatriculaCabecera,
                    filtroFinal.IdEstadoSolicitud,
                    filtroFinal.IdSolicitante,
                    filtroFinal.IdOrigen,
                    filtroFinal.IdAreaSolucion,
                    filtroFinal.IdPersonalSolucion,
                    filtroFinal.FechaInicio,
                    filtroFinal.FechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteSolicitudAlumnoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 30/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene arbol de solicitudes alumno
        /// </summary>
        /// <param> </param>
        /// <returns> TipoSolicitudEstructuradaDTO </returns>
        public List<TipoSolicitudEstructuraDTO> ObtenerEstructuraSolicitudesPlana()
        {
            try
            {
                var rpta = new List<TipoSolicitudEstructuraDTO>();

                // Usar stored procedure en lugar de consulta directa
                var resultado = _dapperRepository.QuerySPDapper(
                    "ope.SP_SolicitudEstructura_Obtener",
                    null
                );

                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoSolicitudEstructuraDTO>>(
                        resultado
                    );
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener estructura de solicitudes: {ex.Message}");
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 30/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la solicitud activa del alumno
        /// </summary>
        /// <param> </param>
        /// <returns> TipoSolicitudEstructuradaDTO </returns>
        public RespuestaVerificacionSolicitudDTO VerificarSolicitudActivaAlumno(
            VerificarSolicitudAlumnoDTO filtro
        )
        {
            try
            {
                var rpta = new RespuestaVerificacionSolicitudDTO();

                
                var estadosActivos = new List<int> { 1, 2, 3, 5, 6 };
                var estadosActivosStr = string.Join(",", estadosActivos);

                // Llamar al SP con todos los parámetros del filtro
                var resultado = _dapperRepository.QuerySPDapper(
                    "ope.SP_ChatBotObtenerSolicitudAlumno",
                    new
                    {
                        IdAlumno = filtro.IdAlumno,
                        IdSolicitudTipoReporte = filtro.IdSolicitudTipoReporte,
                        IdSolicitudCategoria = filtro.IdSolicitudCategoria,
                        IdSolicitudProblema = filtro.IdSolicitudProblema,
                        IdPGeneral = filtro.IdPGeneral,
                        IdPEspecifico = filtro.IdPEspecifico,
                        IdEstadoSolicitud = estadosActivosStr,
                        FechaInicio = (DateTime?)null,
                        FechaFin = (DateTime?)null,
                    }
                );

                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    var solicitudes = JsonConvert.DeserializeObject<
                        List<SolicitudAlumnoRevisionDTO>
                    >(resultado);

                    if (solicitudes != null && solicitudes.Any())
                    {
                        // El SP ya filtró por todos los criterios, tomamos la primera coincidencia
                        var solicitudActiva = solicitudes.FirstOrDefault();

                        if (solicitudActiva != null)
                        {
                            var tiempoPasadoHoras = (int)
                                Math.Round(
                                    (DateTime.Now - solicitudActiva.FechaRegistro).TotalHours
                                );

                            rpta.Mensaje =
                                "El alumno ya tiene una solicitud activa para este tipo de solicitud.";
                            rpta.ExisteSolicitud = true;
                            rpta.TiempoPasadoHoras = tiempoPasadoHoras;
                            rpta.EstadoSolicitud = solicitudActiva.NombreEstadoSolicitud;
                            rpta.NombreControlSolicitudOrigen = solicitudActiva.NombreControlSolicitudOrigen;
                            rpta.Error = null;
                        }
                        else
                        {
                            rpta.Mensaje =
                                "No se encontró ninguna solicitud activa para el alumno y tipo de solicitud indicado.";
                            rpta.ExisteSolicitud = false;
                            rpta.TiempoPasadoHoras = null;
                            rpta.EstadoSolicitud = null;
                            rpta.NombreControlSolicitudOrigen = null;
                            rpta.Error = null;
                        }
                    }
                    else
                    {
                        rpta.Mensaje =
                            "No se encontró ninguna solicitud activa para el alumno y tipo de solicitud indicado.";
                        rpta.ExisteSolicitud = false;
                        rpta.TiempoPasadoHoras = null;
                        rpta.EstadoSolicitud = null;
                        rpta.NombreControlSolicitudOrigen = null;
                        rpta.Error = null;
                    }
                }
                else
                {
                    rpta.Mensaje =
                        "No se encontró ninguna solicitud activa para el alumno y tipo de solicitud indicado.";
                    rpta.ExisteSolicitud = false;
                    rpta.TiempoPasadoHoras = null;
                    rpta.EstadoSolicitud = null;
                    rpta.NombreControlSolicitudOrigen = null;
                    rpta.Error = null;
                }

                return rpta;
            }
            catch (Exception ex)
            {
                return new RespuestaVerificacionSolicitudDTO
                {
                    Mensaje = null,
                    ExisteSolicitud = null,
                    TiempoPasadoHoras = null,
                    EstadoSolicitud = null,
                    NombreControlSolicitudOrigen = null,
                    Error = new ErrorDetalleDTO
                    {
                        Descripción = "Hubo problemas al calcular el tiempo",
                        Exception = ex.ToString(),
                    },
                };
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 31/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos necesarios de BD para crear una solicitud de alumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <param name="idPEspecifico">ID del programa específico</param>
        /// <returns>Tupla con IdMatriculaCabecera, IdPersonal y posibles errores</returns>
        public (
            int? IdMatriculaCabecera,
            int? IdPersonal,
            string ErrorDescripcion,
            string ErrorException
        ) ObtenerDatosParaSolicitudAlumno(int idAlumno, int idPEspecifico)
        {
            try
            {
                // Paso 1: Obtener IdMatriculaCabecera activa
                var queryMatricula =
                    @"
                    SELECT TOP 1 MC.Id
                    FROM fin.T_MatriculaCabecera MC
                    WHERE MC.IdAlumno = @IdAlumno
                        AND MC.IdPEspecifico = @IdPEspecifico
                        AND MC.Estado = 1
                    ORDER BY MC.FechaCreacion DESC";

                var parametrosMatricula = new
                {
                    IdAlumno = idAlumno,
                    IdPEspecifico = idPEspecifico,
                };

                var resultadoMatricula = _dapperRepository.QueryDapper(
                    queryMatricula,
                    parametrosMatricula
                );

                if (
                    string.IsNullOrWhiteSpace(resultadoMatricula)
                    || resultadoMatricula.Contains("[]")
                )
                {
                    return (
                        null,
                        null,
                        "No se encontró una matrícula activa para el alumno con el programa especificado",
                        "No existe matrícula activa"
                    );
                }

                var matriculas = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(
                    resultadoMatricula
                );
                var idMatriculaCabecera = Convert.ToInt32(matriculas[0]["Id"]);

                // Paso 2: Obtener IdPersonal asignado usando los SPs en cascada
                int idPersonal = 0;

                var resultadoPersonalClasificacion = _dapperRepository.QuerySPDapper(
                    "ope.SP_ObtenerPersonalAsignadoMatricula",
                    new { IdMatriculaCabecera = idMatriculaCabecera }
                );

                if (
                    !string.IsNullOrWhiteSpace(resultadoPersonalClasificacion)
                    && !resultadoPersonalClasificacion.Contains("[]")
                    && !resultadoPersonalClasificacion.Contains("null")
                )
                {
                    var personalesClasificacion = JsonConvert.DeserializeObject<
                        List<Dictionary<string, object>>
                    >(resultadoPersonalClasificacion);

                    if (personalesClasificacion != null && personalesClasificacion.Any())
                    {
                        // Intentar con ambos nombres de campo (PascalCase y camelCase)
                        var diccionario = personalesClasificacion[0];
                        if (diccionario.ContainsKey("IdPersonal_Asignado"))
                        {
                            idPersonal = Convert.ToInt32(diccionario["IdPersonal_Asignado"]);
                        }
                        else if (diccionario.ContainsKey("idPersonal_Asignado"))
                        {
                            idPersonal = Convert.ToInt32(diccionario["idPersonal_Asignado"]);
                        }
                    }
                }

                // Si no se encontró personal, retornar error
                if (idPersonal == 0)
                {
                    return (
                        null,
                        null,
                        "No se encontró un personal asignado para la matrícula del alumno",
                        "No existe personal asignado"
                    );
                }

                // Retornar los datos obtenidos de BD
                return (idMatriculaCabecera, idPersonal, null, null);
            }
            catch (Exception ex)
            {
                return (null, null, "Error al obtener datos de la base de datos", ex.ToString());
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las solicitudes del alumno filtradas por asesor/revisor asignado.
        /// </summary>
        /// <param name="FiltroSolicitud">Filtro con IdPersonalRevision y otros parámetros</param>
        /// <returns>SolicitudAlumnoFiltradaDTO</returns>
        public IEnumerable<SolicitudAlumnoFiltradaDTO> ObtenerSolicitudesPorFiltroAsesor(FiltroSolicitudAlumnoPorAsesorDTO FiltroSolicitud)
        {
            try
            {
                List<SolicitudAlumnoFiltradaDTO> rpta = new List<SolicitudAlumnoFiltradaDTO>();
                FiltroSolicitudAlumnoPorAsesorReporteDTO filtroFinal = new FiltroSolicitudAlumnoPorAsesorReporteDTO();

                if (FiltroSolicitud.IdEstadoSolicitud != null && FiltroSolicitud.IdEstadoSolicitud.Count() > 0)
                {
                    filtroFinal.IdEstadoSolicitud = String.Join(",", FiltroSolicitud.IdEstadoSolicitud);
                }
                if (FiltroSolicitud.FechaInicio != null)
                {
                    filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio.Value.Date;
                }
                if (FiltroSolicitud.FechaFin != null)
                {
                    filtroFinal.FechaFin = FiltroSolicitud.FechaFin.Value.Date.AddDays(1).AddSeconds(-1);
                }
                filtroFinal.IdPersonalRevision = FiltroSolicitud.IdPersonalRevision;

                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudAlumnoPorAsesor", new { filtroFinal.IdPersonalRevision, filtroFinal.IdEstadoSolicitud, filtroFinal.FechaInicio, filtroFinal.FechaFin });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 15/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene solicitudes agrupadas: Derivadas (no resueltas de Atención al Cliente) y Resueltas (de otras áreas)
        /// </summary>
        /// <param name="FiltroSolicitud">Filtro con IdPersonalRevision y otros parámetros</param>
        /// <returns>RespuestaSolicitudesAlumnoDTO con solicitudes agrupadas</returns>
        public RespuestaSolicitudesAlumnoDTO ObtenerSolicitudesAgrupadasPorAsesor(FiltroSolicitudAlumnoPorAsesorDTO FiltroSolicitud)
        {
            try
            {
                const int ID_AREA_ATENCION_CLIENTE = 3;
                var estadosResueltos = new List<int> { 7, 8 };

                var respuesta = new RespuestaSolicitudesAlumnoDTO();
                List<SolicitudAlumnoFiltradaDTO> todasLasSolicitudes = new List<SolicitudAlumnoFiltradaDTO>();
                FiltroSolicitudAlumnoPorAsesorReporteDTO filtroFinal = new FiltroSolicitudAlumnoPorAsesorReporteDTO();

                if (FiltroSolicitud.FechaInicio != null)
                {
                    filtroFinal.FechaInicio = FiltroSolicitud.FechaInicio.Value.Date;
                }
                if (FiltroSolicitud.FechaFin != null)
                {
                    filtroFinal.FechaFin = FiltroSolicitud.FechaFin.Value.Date.AddDays(1).AddSeconds(-1);
                }
                filtroFinal.IdPersonalRevision = FiltroSolicitud.IdPersonalRevision;

                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudAlumnoPorAsesor", new { filtroFinal.IdPersonalRevision, filtroFinal.IdEstadoSolicitud, filtroFinal.FechaInicio, filtroFinal.FechaFin });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    todasLasSolicitudes = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }

                // Solicitudes Derivadas: NO resueltas y área de solución = Atención al Cliente (3)
                respuesta.SolicitudesDerivadas = todasLasSolicitudes
                    .Where(s => !estadosResueltos.Contains(s.IdEstadoSolicitud) && s.IdPersonalAreaTrabajo_Solucion == ID_AREA_ATENCION_CLIENTE)
                    .ToList();

                // Solicitudes Resueltas: Resueltas (7 u 8) y área de solución != Atención al Cliente
                respuesta.SolicitudesResueltas = todasLasSolicitudes
                    .Where(s => estadosResueltos.Contains(s.IdEstadoSolicitud) && s.IdPersonalAreaTrabajo_Solucion != ID_AREA_ATENCION_CLIENTE)
                    .ToList();

                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 20/01/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de una solicitud de alumno
        /// </summary>
        /// <param name="idSolicitud">ID de la solicitud a actualizar</param>
        /// <returns>True si se actualizó correctamente</returns>
        public bool ActualizarEstadoSolicitud(int idSolicitud)
        {
            try
            {
                var query = @"UPDATE ope.T_SolicitudAlumno
                              SET Visualizado = 1
                              WHERE Id = @idSolicitud";
                if (idSolicitud != null ) {
                    var resultadoMatricula = _dapperRepository.QueryDapper(query, new { idSolicitud });
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
