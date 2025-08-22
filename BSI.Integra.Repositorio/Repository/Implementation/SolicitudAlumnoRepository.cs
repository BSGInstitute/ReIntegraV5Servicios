
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

        
    }
}
