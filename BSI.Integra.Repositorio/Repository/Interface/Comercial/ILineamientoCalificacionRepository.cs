using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface ILineamientoCalificacionRepository : IGenericRepository<TLineamientoCalificacion>
    {
        #region Metodos Base
        TLineamientoCalificacion Add(LineamientoCalificacion entidad);
        TLineamientoCalificacion Update(LineamientoCalificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TLineamientoCalificacion> Add(IEnumerable<LineamientoCalificacion> listadoEntidad);
        IEnumerable<TLineamientoCalificacion> Update(IEnumerable<LineamientoCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        LineamientoCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<LineamientoCalificacion> ObtenerLineamiento();
        IEnumerable<ConfiguracionEsquemaCalificacionDTO> EsquemaCalificacionConfigurado();
        IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamada();
        IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionMasivaActiva();
        IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionMasivaActiva();
        IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionAuto();
        IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionTranscripcionAuto();

        IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamiento(int idLlamada);
        IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionLineamientoHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx);
        IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionPuntoGeneralHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx);
        IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamientoGeneral(int idLlamada);

        IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionAutomaticaLineamiento(int idLlamada);
        bool GuardarCalificacionLlamada(CalificacionLlamadaManualDTO calificacionLlamada);
        bool CalificarLlamadaAutomaticamente(CalificacionLlamadaAutomaticaDTO calificacionLlamada);
        bool ActualizarEstadoCalificacionLlamada(EstadoLlamadaCalificadaDTO estadoLlamada);



        bool CongelarConfiguracion(CongelamientoConfiguracionDTO congelamientoConfiguracion);
        bool ActivarConfiguracion(CongelamientoConfiguracionActivaDTO activarConfiguracion);
        bool ConfigurarPanelAutomatico(ConfiguracionTranscripcionDTO configuracion);
        bool ConfigurarPanelAutomaticoCalificacion(ConfiguracionTranscripcionDTO configuracion);
        bool ConfigurarPanelCalificacionAuto(ConfiguracionTranscripcionDTO configuracion);
        bool ConfigurarPanelTranscripcionAuto(ConfiguracionTranscripcionDTO configuracion);
        bool ActivarConfiguracionTranscripcionAuto(ConfiguracionActivoProcesoDTO configuracion);
        bool ActivarConfiguracionCalificacionAuto(ConfiguracionActivoProcesoDTO configuracion);


        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionTranscripcionAuto();
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionTranscripcionAutoAtencionCliente(int idPersonalAreaTrabajo);

        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionAuto();
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosEvaluacionLLamadaCalificacionAuto(int idPersonalAreaTrabajo);
        InformacionMatriculaAlumnoDTO ObtenerInformacionMatriculaAlumno(int idMatriculaCabecera);

        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionAtencionCliente();

        LlamadaProcesoAutoDTO ObtenerDatosConfiguracionCalificacionPorIdLlamada(int idLlamada);

        IEnumerable<LlamadaProcesoAutoDTO> ObtenerHistoricoLlamadaCompletoPorIdOportunidad(int idOportunidad);


        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionTranscripcionMasiva();
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionMasiva();



        (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporte(ReporteCalificacionRequest req);
        (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporteVentas(ReporteCalificacionRequestV2 req);
        (IEnumerable<LlamadaCalificadaAtencionClienteRawDTO> Items, int Total) ObtenerReporteAtencionCliente(ReporteCalificacionRequestV2 req);
        (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReportePorArea(ReporteCalificacionAreaRequest req);

        (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporteFase(ReporteCalificacionRequest req);
        (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporteFaseVentas(ReporteCalificacionRequestV2 req);

        IEnumerable<LlamadaCalificadaRawDTO> ObtenerDatosParaPromedioGlobal(ReporteCalificacionGlobalRequest request);
        IEnumerable<LlamadaCalificadaRawDTO> ObtenerDatosParaPromedioGlobalVentas(ReporteCalificacionGlobalRequestV2 request);
        IEnumerable<LlamadaCalificadaAtencionClienteRawDTO> ObtenerDatosParaPromedioGlobalAtencionCliente(ReporteCalificacionGlobalRequestV2 request);
        IEnumerable<CalificacionFaseDTO> ObtenerCalificacionFase(int idLlamada, bool tipoCalificacion);
        IEnumerable<InformacionLlamadaTresCxDTO> ObtenerInformacionLlamada(int idLlamada);

        Task<InsertRecomendacionResultDTO> ProcesarRecomendacionesBatch(RecomendacionLlamadaDTO calificacionLlamada);

        IEnumerable<LlamadaWebphoneOcurrenciaDTO> ObtenerOcurrenciaRegistrada(int IdOportunidad);
        IEnumerable<TransicionCambioFaseOportunidadDTO> ObtenerConfiguracionCambioFaseOportunidad(int idFaseOrigen, int idFaseDestino);
        IEnumerable<PuntosCriticosLlamadaDiaDto> ObtenerPuntosCriticosPorDia();
        bool InsertarCongelamientoPuntoCritico(int idPersonal, DateTime fechaGeneracion, string resultadoPuntoCritico);
        IEnumerable<PuntoCriticoResumenDiarioDTO> ObtenerPuntoCriticoDiario(int IdPersonal,DateTime fechaGeneracion);
        Task<List<MotivacionRawDTO>> ObtenerMotivacionesPorIdPGeneralAsync(int idPGeneral);
        Task<List<ObjeccionClienteRawDTO>> ObtenerObjecionesClientesPorIdPGeneralAsync(int idPGeneral);
        IEnumerable<EvaluacionLlamadaDetalleDTO> ObtenerDetallesEvaluacionPorLlamada(int idLlamada);
        IEnumerable<EvaluacionPuntoGeneralDetalleDTO> ObtenerDetallesEvaluacionPuntosGeneralesPorLlamada(int idLlamada);
        IEnumerable<ConfiguracionEsquemaCalificacionPorLlamdaDTO> HistorialVersionCalificacionPorLlamada(int idLlamada);
        IEnumerable<HistoricoOcurrenciaAtencionClienteDto> ObtenerOcurrenciaRegistradaV2(int idOportunidad, int idPersonalAreaTrabajo);
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerHistoricoLlamadaCompletoPorIdOportunidadV2(int IdOportunidad, int IdPersonalAreaTrabajo);
        LlamadaProcesoAutoDTO ObtenerDatosConfiguracionCalificacionPorIdLlamadaV2(int idLlamada, int IdPersonalAreaTrabajo);
        IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamadaV2(int idPersonalAreaTrabajo);
        List<EvaluacionLlamadaJerarquicaDTO> ObtenerDataConfiguracionPorVersion(int idEvaluacionLlamadaConfiguracionVersion, int idPersonalAreaTrabajo);
        IEnumerable<ConfiguracionVigenteJerarquiaDTO> ObtenerConfiguracionPorVersion(int idVersion, int idPersonalAreaTrabajo);
        IEnumerable<ConfiguracionVigenteJerarquiaDTO> ObtenerConfiguracionVigentePorArea(int idPersonalAreaTrabajo);
        IEnumerable<ConfiguracionEsquemaCalificacionPorLlamdaDTO> HistorialVersionCalificacionPorLlamadav2(int idLlamada);

        // Métodos para tablas temporales - Calificación en tiempo real
        bool GuardarCalificacionLlamadaTemporal(CalificacionLlamadaManualTemporalDTO calificacionTemporal);
        IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamientoTemporal(int idActividadDetalle, int numeroLlamada);

        // Métodos de Validación de Matrícula
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosValidacionMatriculaPendiente();
        IEnumerable<ValidacionMatriculaLineamientoDTO> ObtenerValidacionMatriculaLineamiento(int idOportunidad, int tipoValidacionMatricula);
        IEnumerable<ValidacionMatriculaInformacionLlamadaRawDTO> ObtenerValidacionMatriculaInformacionLlamada(int idOportunidad);
    }
}
