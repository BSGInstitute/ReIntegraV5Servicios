using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadLogService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_OportunidadLog
    /// </summary>
    public class OportunidadLogService : IOportunidadLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadLog, OportunidadLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<OportunidadLogFinalizarActividadDTO, OportunidadLog>(MemberList.None);
                cfg.CreateMap<OportunidadLogDTO, OportunidadLog>(MemberList.None);
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogHistorialComentariosDTO> </returns>
        public IEnumerable<OportunidadLogHistorialComentariosDTO> ObtenerHistorialComentariosPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var oportunidadLogReporte = _unitOfWork.OportunidadLogRepository.ObtenerReporteSeguimientoYPersonalPorIdOportunidad(idOportunidad);
                var historialComentarios = (
                    from olog in oportunidadLogReporte
                    group olog by new
                    {
                        olog.FaseInicio,
                        olog.FaseDestino,
                        olog.FechaModificacion,
                        olog.FechaSiguienteLlamada,
                        olog.IdFaseOportunidad,
                        olog.IdFaseOportunidadIP,
                        olog.IdFaseOportunidadPF,
                        olog.IdFaseOportunidadIC,
                        olog.FechaEnvioFaseOportunidadPF,
                        olog.FechaPagoFaseOportunidadPF,
                        olog.FechaPagoFaseOportunidadIC,
                        olog.IdOcurrencia,
                        olog.IdEstadoOcurrencia,
                        olog.IdOportunidadLog,
                        olog.NombreActividad,
                        olog.NombreOcurrencia,
                        olog.ComentarioActividad,
                        olog.IdFaseOportunidadInicial,
                        olog.Personal
                    } into g
                    select new OportunidadLogHistorialComentariosDTO
                    {
                        FaseInicio = g.Key.FaseInicio,
                        FaseDestino = g.Key.FaseDestino,
                        FechaModificacion = g.Key.FechaModificacion,
                        FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                        IdFaseOportunidad = g.Key.IdFaseOportunidad,
                        IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                        IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                        IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                        FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                        FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                        FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                        IdOcurrencia = g.Key.IdOcurrencia,
                        IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                        IdOportunidadLog = g.Key.IdOportunidadLog,
                        NombreActividad = g.Key.NombreActividad,
                        NombreOcurrencia = g.Key.NombreOcurrencia,
                        ComentarioActividad = g.Key.ComentarioActividad,
                        IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                        Personal = g.Key.Personal,
                        LlamadasIntegra = g.Select(o => new LlamadaIntegraDTO
                        {
                            Id = o.IdCentralLLamada,
                            TiempoDuracion = o.TiempoDuracion,
                            TiempoDuracionMinutos = o.TiempoDuracionMinutos,
                            FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                            EstadoLlamada = o.EstadoLlamadaIntegra,
                            FechaFinLlamada = o.FechaFinLlamadaIntegra,
                            SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                            NombreGrabacion = o.NombreGrabacionIntegra,
                            Webphone = o.Webphone
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                        Llamadas3CX = g.Select(o => new Llamada3CXDTO
                        {
                            Id = o.IdTresCX,
                            TiempoDuracion = o.TiempoDuracion3CX,
                            FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                            EstadoLlamada = o.EstadoLlamadaTresCX,
                            FechaFinLlamada = o.FechaFinLlamadaTresCX,
                            SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                            NombreGrabacion = o.NombreGrabacionTresCX
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                    }).ToList();
                return historialComentarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Seguimiento Nuevo Webphone y las Actividades relacionadas a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteSeguimientoNWActividadDTO> </returns>
        public IEnumerable<ReporteSeguimientoNWActividadDTO> ObtenerReporteSeguimientoNWActividadesPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var seguimientoDetalle = ObtenerReporteSeguimientoNWDetallePorIdOportunidad(idOportunidad).ToList();
                var actividades = _unitOfWork.OportunidadLogRepository.ObtenerReporteActividadOcurrenciaPorIdOportunidad(idOportunidad);

                var reporteActividades = seguimientoDetalle.Select(s =>
                    new ReporteSeguimientoNWActividadDTO()
                    {
                        FaseInicio = s.FaseInicio,
                        FaseDestino = s.FaseDestino,
                        FechaModificacion = s.FechaModificacion,
                        FechaSiguienteLlamada = s.FechaSiguienteLlamada,
                        NombreActividad = s.NombreActividad,
                        NombreOcurrencia = s.NombreOcurrencia,
                        ComentarioActividad = s.ComentarioActividad,
                        TotalEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == 1 /*TODO: ValorEstatico.IdEstadoOcurrenciaEjecutado*/
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalNoEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == 2 /*TODO: ValorEstatico.IdEstadoOcurrenciaNoEjecutado*/
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalAsignacionManual = actividades
                            .Where(x => x.IdEstadoOcurrencia == 7/*TODO: ValorEstatico.IdEstadoOcurrenciaAsignacionManual*/
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        EstadoFase = ObtenerEstadoFaseOportunidadLog(
                                new EstadoFaseOportunidadLogDTO()
                                {
                                    IdFaseOportunidad = s.IdFaseOportunidad,
                                    IdFaseOportunidadIC = s.IdFaseOportunidadIC,
                                    IdFaseOportunidadIP = s.IdFaseOportunidadIP,
                                    IdFaseOportunidadPF = s.IdFaseOportunidadPF
                                }
                            ),
                        Estado = ObtenerEstadoActividadOportunidadLog(s.IdEstadoOcurrencia, s.IdOcurrencia),
                        FechaEnvio = s.IdFaseOportunidad == 22 ? s.FechaEnvioFaseOportunidadPF : null,
                        FechaPago = s.IdFaseOportunidad == 12 || s.IdFaseOportunidad == 22 ?
                            s.FechaPagoFaseOportunidadPF ?? s.FechaPagoFaseOportunidadIC : null,
                        LlamadasIntegra = s.LlamadasIntegra.OrderBy(p => p.FechaInicioLlamada).ToList(),
                        Llamadas3CX = s.Llamadas3CX.OrderBy(p => p.FechaInicioLlamada).ToList()
                    }
                );

                var codigoFase = _unitOfWork.OportunidadRepository.ObtenerCodigoFasePorIdOportunidad(idOportunidad);
                var actividadProgramada = new ReporteSeguimientoNWActividadDTO()
                {
                    FaseInicio = codigoFase.FaseInicio,
                    FechaSiguienteLlamada = codigoFase.FechaSiguienteLlamada,
                    Estado = "NO EJECUTADO",
                    TotalEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == 1 /*TODO: ValorEstatico.IdEstadoOcurrenciaEjecutado*/
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalNoEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == 2 /*TODO: ValorEstatico.IdEstadoOcurrenciaNoEjecutado*/
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalAsignacionManual = actividades
                            .Where(x => x.IdEstadoOcurrencia == 7
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                };
                reporteActividades = reporteActividades.Append(actividadProgramada);
                return reporteActividades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Seguimiento Nuevo Webphone y las Actividades relacionadas a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteSeguimientoNWActividadDTO> </returns>
        public List<ReporteSeguimientoNWActividadAlternoDTO> ObtenerReporteSeguimientoNWActividadesPorIdOportunidad3cx(int idOportunidad)
        {
            try
            {
                var seguimientoDetalle = ObtenerReporteSeguimientoNWDetallePorIdOportunidad3cx(idOportunidad);
                var actividades = _unitOfWork.OportunidadLogRepository.ObtenerReporteActividadOcurrenciaPorIdOportunidad(idOportunidad);
                var reporteActividades = new List<ReporteSeguimientoNWActividadAlternoDTO>();
                seguimientoDetalle.ForEach(s =>
                {
                    var item = new ReporteSeguimientoNWActividadAlternoDTO()
                    {
                        IdActividadDetalle = s.IdActividadDetalle,
                        FaseInicio = s.FaseInicio,
                        FaseDestino = s.FaseDestino,
                        FechaModificacion = s.FechaModificacion,
                        FechaSiguienteLlamada = s.FechaSiguienteLlamada,
                        NombreActividad = s.NombreActividad,
                        NombreOcurrencia = s.NombreOcurrencia,
                        ComentarioActividad = s.ComentarioActividad,
                        OtroMedio = s.OtroMedio,
                        EstadoSeguimientoWhatsApp = s.EstadoSeguimientoWhatsApp,
                        TotalEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalNoEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalAsignacionManual = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        EstadoFase = ObtenerEstadoFaseOportunidadLog(
                                new EstadoFaseOportunidadLogDTO()
                                {
                                    IdFaseOportunidad = s.IdFaseOportunidad,
                                    IdFaseOportunidadIC = s.IdFaseOportunidadIC,
                                    IdFaseOportunidadIP = s.IdFaseOportunidadIP,
                                    IdFaseOportunidadPF = s.IdFaseOportunidadPF
                                }
                            ),
                        Estado = ObtenerEstadoActividadOportunidadLog(s.IdEstadoOcurrencia, s.IdOcurrencia),
                        FechaEnvio = s.IdFaseOportunidad == FaseOportunidad.PF ? s.FechaEnvioFaseOportunidadPF : null,
                        FechaPago = s.IdFaseOportunidad == FaseOportunidad.IC || s.IdFaseOportunidad == FaseOportunidad.PF ?
                            s.FechaPagoFaseOportunidadPF ?? s.FechaPagoFaseOportunidadIC : null,
                    };
                    var llamadas = new List<LlamadaIntegra3cxDTO>();
                    if (s.LlamadasIntegra != null && s.LlamadasIntegra.Count() > 0)
                    {
                        llamadas.AddRange(s.LlamadasIntegra);
                    }
                    if (s.Llamadas3CX != null && s.Llamadas3CX.Count() > 0)
                    {
                        llamadas.AddRange(s.Llamadas3CX);
                    }
                    if (s.LlamadasRingover != null && s.LlamadasRingover.Count() > 0)
                    {
                        llamadas.AddRange(s.LlamadasRingover);
                    }
                    item.LlamadasIntegra3cx = llamadas.OrderBy(p => p.FechaInicioLlamada).ToList();
                    reporteActividades.Add(item);
                });

                var codigoFase = _unitOfWork.OportunidadRepository.ObtenerCodigoFasePorIdOportunidad(idOportunidad);
                var actividadProgramada = new ReporteSeguimientoNWActividadAlternoDTO()
                {
                    FaseInicio = codigoFase.FaseInicio,
                    FechaSiguienteLlamada = codigoFase.FechaSiguienteLlamada,
                    Estado = "NO EJECUTADO",
                    TotalEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalNoEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalAsignacionManual = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                };
                reporteActividades.Add(actividadProgramada);
                return reporteActividades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Seguimiento Nuevo Webphone y las Actividades relacionadas a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteSeguimientoNWActividadDTO> </returns>
        public List<ReporteSeguimientoNWActividadAlternoDTO> ObtenerReporteSeguimientoActividadesPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var seguimientoDetalle = ObtenerReporteSeguimientoDetallePorIdOportunidad(idOportunidad);
                var actividades = _unitOfWork.OportunidadLogRepository.ObtenerReporteActividadOcurrenciaPorIdOportunidad(idOportunidad);
                var reporteActividades = new List<ReporteSeguimientoNWActividadAlternoDTO>();
                seguimientoDetalle.ForEach(s =>
                {
                    var item = new ReporteSeguimientoNWActividadAlternoDTO()
                    {
                        IdActividadDetalle = s.IdActividadDetalle,
                        FaseInicio = s.FaseInicio,
                        FaseDestino = s.FaseDestino,
                        FechaModificacion = s.FechaModificacion,
                        FechaSiguienteLlamada = s.FechaSiguienteLlamada,
                        NombreActividad = s.NombreActividad,
                        NombreOcurrencia = s.NombreOcurrencia,
                        ComentarioActividad = s.ComentarioActividad,
                        OtroMedio = s.OtroMedio,
                        EstadoSeguimientoWhatsApp = s.EstadoSeguimientoWhatsApp,
                        TotalEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalNoEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        TotalAsignacionManual = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                && x.IdFaseActual == s.IdFaseOportunidadInicial
                                && x.FechaReal < s.FechaModificacion!.Value).Count(),
                        EstadoFase = ObtenerEstadoFaseOportunidadLog(
                                new EstadoFaseOportunidadLogDTO()
                                {
                                    IdFaseOportunidad = s.IdFaseOportunidad,
                                    IdFaseOportunidadIC = s.IdFaseOportunidadIC,
                                    IdFaseOportunidadIP = s.IdFaseOportunidadIP,
                                    IdFaseOportunidadPF = s.IdFaseOportunidadPF
                                }
                            ),
                        Estado = ObtenerEstadoActividadOportunidadLog(s.IdEstadoOcurrencia, s.IdOcurrencia),
                        FechaEnvio = s.IdFaseOportunidad == FaseOportunidad.PF ? s.FechaEnvioFaseOportunidadPF : null,
                        FechaPago = s.IdFaseOportunidad == FaseOportunidad.IC || s.IdFaseOportunidad == FaseOportunidad.PF ?
                            s.FechaPagoFaseOportunidadPF ?? s.FechaPagoFaseOportunidadIC : null,
                    };
                    item.LlamadasIntegra3cx = s.Llamadas.OrderBy(p => p.FechaInicioLlamada).ToList();
                    reporteActividades.Add(item);
                });

                var codigoFase = _unitOfWork.OportunidadRepository.ObtenerCodigoFasePorIdOportunidad(idOportunidad);
                var actividadProgramada = new ReporteSeguimientoNWActividadAlternoDTO()
                {
                    FaseInicio = codigoFase.FaseInicio,
                    FechaSiguienteLlamada = codigoFase.FechaSiguienteLlamada,
                    Estado = "NO EJECUTADO",
                    TotalEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalNoEjecutadas = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                    TotalAsignacionManual = actividades
                            .Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual
                                && x.IdFaseActual == codigoFase.IdFaseOportunidad).Count(),
                };
                reporteActividades.Add(actividadProgramada);
                return reporteActividades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 30/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogHistorialComentariosDTO> </returns>
        private IEnumerable<OportunidadLogReporteSeguimientoNWDetalleDTO> ObtenerReporteSeguimientoNWDetallePorIdOportunidad(int idOportunidad)
        {
            try
            {
                var logSeguimientoNW = _unitOfWork.OportunidadLogRepository.ObtenerOportunidadLogReporteSeguimientoNWPorIdOportunidad(idOportunidad);
                var seguimientoNWDetalle = (
                    from p in logSeguimientoNW
                    group p by new
                    {
                        p.FaseInicio,
                        p.FaseDestino,
                        p.FechaModificacion,
                        p.FechaSiguienteLlamada,
                        p.IdFaseOportunidad,
                        p.IdFaseOportunidadIP,
                        p.IdFaseOportunidadPF,
                        p.IdFaseOportunidadIC,
                        p.FechaEnvioFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadIC,
                        p.IdOcurrencia,
                        p.IdEstadoOcurrencia,
                        p.IdOportunidadLog,
                        p.NombreActividad,
                        p.NombreOcurrencia,
                        p.ComentarioActividad,
                        p.IdFaseOportunidadInicial
                    } into g
                    select new OportunidadLogReporteSeguimientoNWDetalleDTO
                    {
                        FaseInicio = g.Key.FaseInicio,
                        FaseDestino = g.Key.FaseDestino,
                        FechaModificacion = g.Key.FechaModificacion,
                        FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                        IdFaseOportunidad = g.Key.IdFaseOportunidad,
                        IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                        IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                        IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                        FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                        FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                        FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                        IdOcurrencia = g.Key.IdOcurrencia,
                        IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                        IdOportunidadLog = g.Key.IdOportunidadLog,
                        NombreActividad = g.Key.NombreActividad,
                        NombreOcurrencia = g.Key.NombreOcurrencia,
                        ComentarioActividad = g.Key.ComentarioActividad,
                        IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                        LlamadasIntegra = g.Select(o => new LlamadaIntegraDTO
                        {
                            Id = o.IdCentralLLamada,
                            TiempoDuracion = o.TiempoDuracion,
                            TiempoDuracionMinutos = o.TiempoDuracionMinutos,
                            FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                            EstadoLlamada = o.EstadoLlamadaIntegra,
                            FechaFinLlamada = o.FechaFinLlamadaIntegra,
                            SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                            NombreGrabacion = o.NombreGrabacionIntegra,
                            Webphone = o.Webphone
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                        Llamadas3CX = g.Select(o => new Llamada3CXDTO
                        {
                            Id = o.IdTresCX,
                            TiempoDuracion = o.TiempoDuracion3CX,
                            FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                            EstadoLlamada = o.EstadoLlamadaTresCX,
                            FechaFinLlamada = o.FechaFinLlamadaTresCX,
                            SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                            NombreGrabacion = o.NombreGrabacionTresCX
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                    }
                ).ToList();

                return seguimientoNWDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 30/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogHistorialComentariosDTO> </returns>
        public List<OportunidadLogReporteSeguimientoDetalleDTO> ObtenerReporteSeguimientoDetallePorIdOportunidad(int idOportunidad)
        {
            try
            {
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(oportunidad.IdPersonalAsignado == null ? 0 : oportunidad.IdPersonalAsignado.Value);
                var logSeguimientoNW = _unitOfWork.OportunidadLogRepository.ObtenerOportunidadLogReporteSeguimientoV5(idOportunidad, diferenciaHoraria == null ? 0 : (diferenciaHoraria.Valor == null ? 0 : diferenciaHoraria.Valor.Value));
                var seguimientoNWDetalle = (
                    from p in logSeguimientoNW
                    group p by new
                    {
                        p.IdActividadDetalle,
                        p.FaseInicio,
                        p.FaseDestino,
                        p.FechaModificacion,
                        p.FechaSiguienteLlamada,
                        p.IdFaseOportunidad,
                        p.IdFaseOportunidadIP,
                        p.IdFaseOportunidadPF,
                        p.IdFaseOportunidadIC,
                        p.FechaEnvioFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadIC,
                        p.IdOcurrencia,
                        p.IdEstadoOcurrencia,
                        p.IdOportunidadLog,
                        p.NombreActividad,
                        p.NombreOcurrencia,
                        p.ComentarioActividad,
                        p.IdFaseOportunidadInicial,
                        p.EstadoSeguimientoWhatsApp,
                        p.OtroMedio
                    } into g
                    select new OportunidadLogReporteSeguimientoDetalleDTO
                    {
                        IdActividadDetalle = g.Key.IdActividadDetalle,
                        FaseInicio = g.Key.FaseInicio,
                        FaseDestino = g.Key.FaseDestino,
                        FechaModificacion = g.Key.FechaModificacion,
                        FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                        IdFaseOportunidad = g.Key.IdFaseOportunidad,
                        IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                        IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                        IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                        FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                        FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                        FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                        IdOcurrencia = g.Key.IdOcurrencia,
                        IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                        IdOportunidadLog = g.Key.IdOportunidadLog,
                        NombreActividad = g.Key.NombreActividad,
                        NombreOcurrencia = g.Key.NombreOcurrencia,
                        ComentarioActividad = g.Key.ComentarioActividad,
                        IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                        OtroMedio = g.Key.OtroMedio,
                        EstadoSeguimientoWhatsApp = g.Key.EstadoSeguimientoWhatsApp,
                        Llamadas = g.Select(o => new LlamadaIntegra3cxDTO
                        {
                            Id = o.IdRegistroLlamada,
                            DuracionTimbrado = o.DuracionTimbrado,
                            DuracionContesto = o.DuracionContesto,
                            DuracionTimbradoMinutos = ((double)(o.DuracionTimbrado.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            DuracionContestoMinutos = ((double)(o.DuracionContesto.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            FechaInicioLlamada = o.FechaInicioLlamada,
                            FechaFinLlamada = o.FechaFinLlamada,
                            EstadoLlamada = o.EstadoLlamada,
                            SubEstadoLlamada = o.SubEstadoLlamada,
                            UrlGrabacion = o.UrlGrabacion,
                            UrlGrabacion2 = o.UrlGrabacion2,
                            NombreGrabacion = o.UrlGrabacion,
                            Webphone = o.WebphoneGrabacion,
                            OrigenLlamada = o.OrigenLlamada,
                            esLlamadaCalificada = o.esLlamadaCalificada,
                            esLlamadaTranscrita=o.esLlamadaTranscrita
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                    }
                ).ToList();
                return seguimientoNWDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 30/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogHistorialComentariosDTO> </returns>
        public List<OportunidadLogReporteSeguimientoNWDetalleAlternoDTO> ObtenerReporteSeguimientoNWDetallePorIdOportunidad3cx(int idOportunidad)
        {
            try
            {
                var logSeguimientoNW = _unitOfWork.OportunidadLogRepository.ObtenerOportunidadLogReporteSeguimientoNWAlterno3cx(idOportunidad);
                var seguimientoNWDetalle = (
                    from p in logSeguimientoNW
                    group p by new
                    {
                        p.IdActividadDetalle,
                        p.FaseInicio,
                        p.FaseDestino,
                        p.FechaModificacion,
                        p.FechaSiguienteLlamada,
                        p.IdFaseOportunidad,
                        p.IdFaseOportunidadIP,
                        p.IdFaseOportunidadPF,
                        p.IdFaseOportunidadIC,
                        p.FechaEnvioFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadPF,
                        p.FechaPagoFaseOportunidadIC,
                        p.IdOcurrencia,
                        p.IdEstadoOcurrencia,
                        p.IdOportunidadLog,
                        p.NombreActividad,
                        p.NombreOcurrencia,
                        p.ComentarioActividad,
                        p.IdFaseOportunidadInicial,
                        p.EstadoSeguimientoWhatsApp,
                        p.OtroMedio
                    } into g
                    select new OportunidadLogReporteSeguimientoNWDetalleAlternoDTO
                    {
                        IdActividadDetalle = g.Key.IdActividadDetalle,
                        FaseInicio = g.Key.FaseInicio,
                        FaseDestino = g.Key.FaseDestino,
                        FechaModificacion = g.Key.FechaModificacion,
                        FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                        IdFaseOportunidad = g.Key.IdFaseOportunidad,
                        IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                        IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                        IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                        FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                        FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                        FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                        IdOcurrencia = g.Key.IdOcurrencia,
                        IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                        IdOportunidadLog = g.Key.IdOportunidadLog,
                        NombreActividad = g.Key.NombreActividad,
                        NombreOcurrencia = g.Key.NombreOcurrencia,
                        ComentarioActividad = g.Key.ComentarioActividad,
                        IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                        OtroMedio = g.Key.OtroMedio,
                        EstadoSeguimientoWhatsApp = g.Key.EstadoSeguimientoWhatsApp,
                        LlamadasIntegra = g.Select(o => new LlamadaIntegra3cxDTO
                        {
                            Id = o.IdLW,
                            DuracionTimbrado = o.DuracionTimbradoLW,
                            DuracionContesto = o.DuracionContestoLW,
                            DuracionTimbradoMinutos = ((double)(o.DuracionTimbradoLW.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            DuracionContestoMinutos = ((double)(o.DuracionContestoLW.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            FechaInicioLlamada = o.FechaInicioLlamadaIntegra,
                            FechaFinLlamada = o.FechaFinLlamadaIntegra,
                            EstadoLlamada = o.EstadoLlamadaTresCXIntegra,
                            SubEstadoLlamada = o.SubEstadoLlamadaTresCXIntegra,
                            NombreGrabacion = o.NombreGrabacionIntegra,
                            Webphone = o.Webphone,
                            OrigenLlamada = "Integra"
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                        Llamadas3CX = g.Select(o => new LlamadaIntegra3cxDTO
                        {
                            Id = o.IdTresCxCC,
                            IdLlamadaCentral = o.IdLlamadaCentralTresCx,
                            DuracionTimbrado = o.DuracionTimbradoCentralTresCx,
                            DuracionContesto = o.DuracionContestoCentralTresCx,
                            DuracionTimbradoMinutos = ((double)(o.DuracionTimbradoCentralTresCx.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            DuracionContestoMinutos = ((double)(o.DuracionContestoCentralTresCx.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            FechaInicioLlamada = o.FechaInicioLlamadaTresCX,
                            FechaFinLlamada = o.FechaFinLlamadaTresCX,
                            EstadoLlamada = o.EstadoLlamadaTresCX,
                            SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                            NombreGrabacion = o.NombreGrabacionTresCX,
                            Webphone = o.WebphoneTresCx,
                            OrigenLlamada = "3cx"
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                        LlamadasRingover = g.Select(o => new LlamadaIntegra3cxDTO
                        {
                            Id = o.IdRingover,
                            IdLlamadaCentral = o.IdLlamadaCentralRingover,
                            DuracionTimbrado = o.DuracionTimbradoCentralRingover,
                            DuracionContesto = o.DuracionContestoCentralRingover,
                            DuracionTimbradoMinutos = ((double)(o.DuracionTimbradoCentralRingover.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            DuracionContestoMinutos = ((double)(o.DuracionContestoCentralRingover.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            FechaInicioLlamada = o.FechaInicioLlamadaRingover,
                            FechaFinLlamada = o.FechaFinLlamadaRingover,
                            EstadoLlamada = o.EstadoLlamadaRingover,
                            SubEstadoLlamada = o.SubEstadoLlamadaRingover,
                            NombreGrabacion = o.NombreGrabacionRingover,
                            Webphone = o.WebphoneRingover,
                            OrigenLlamada = "Ringover"
                        }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                    }
                ).ToList();
                return seguimientoNWDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Estado Fase de la Oportunidad dependiendo de los valores de IdFaseOportunidad
        /// </summary>
        /// <param name="olog">DTO que encapsula los IdFaseOportunidad</param>
        /// <returns> ValorStringDTO </returns>
        public string ObtenerEstadoFaseOportunidadLog(EstadoFaseOportunidadLogDTO olog)
        {
            string estadoFase = "-";
            const int estadoSolido = 1; //5
            const int estadoDudoso = 2;
            if (olog.IdFaseOportunidad == FaseOportunidad.IP)
            {
                if (olog.IdFaseOportunidadIP == estadoSolido)
                    estadoFase = "Solido";
                else if (olog.IdFaseOportunidadIP == estadoDudoso)
                    estadoFase = "Dudoso";
                else
                    estadoFase = "-";
            }
            else if (olog.IdFaseOportunidad == FaseOportunidad.PF)
            {
                if (olog.IdFaseOportunidadPF == estadoSolido)
                    estadoFase = "Solido";
                else if (olog.IdFaseOportunidadPF == estadoDudoso)
                    estadoFase = "Dudoso";
                else
                    estadoFase = "-";
            }
            else if (olog.IdFaseOportunidad == FaseOportunidad.IC)
            {
                if (olog.IdFaseOportunidadIC == estadoSolido)
                    estadoFase = "Solido";
                else if (olog.IdFaseOportunidadIC == estadoDudoso)
                    estadoFase = "Dudoso";
                else
                    estadoFase = "-";
            }
            return estadoFase;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Estado de la Actividad dependiendo de el EstadoOcurrencia y la Ocurrencia
        /// </summary>
        /// <param name="idEstadoOcurrencia">Id de EstadoOcurrencia</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> ValorStringDTO </returns>
        public string? ObtenerEstadoActividadOportunidadLog(int? idEstadoOcurrencia, int? idOcurrencia)
        {
            try
            {
                string? estado = null;
                if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado)
                    estado = "EJECUTADO";
                else if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado)
                {
                    bool reprogramadoManual = new int[] { 149, 162, 163, 164, 165, 168, 207, 209 }.Contains(idOcurrencia ?? 0);
                    if (reprogramadoManual)
                        estado = "REPROGRAMADO M.";
                    else
                        estado = "REPROGRAMADO AUT.";
                }
                else if (idEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual)
                    estado = "ASIGNACION";

                return estado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Margiory Ramirez Neyra
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle del Log  de Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ObtenerDetalleOportunidadDTO> </returns>
        public List<ObtenerDetalleOportunidadDTO> ObtenerDetalleOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadLogRepository.ObtenerDetalleOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
