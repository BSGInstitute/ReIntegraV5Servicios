using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
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
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionAuto();
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerHistoricoLlamadaCompletoPorIdOportunidad(int idOportunidad);


        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionTranscripcionMasiva();
        IEnumerable<LlamadaProcesoAutoDTO> ObtenerDatosConfiguracionCalificacionMasiva();



        (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporte(ReporteCalificacionRequest req);
        (IEnumerable<LlamadaCalificadaRawDTO> Items, int Total) ObtenerReporteFase(ReporteCalificacionRequest req);

        IEnumerable<LlamadaCalificadaRawDTO> ObtenerDatosParaPromedioGlobal(ReporteCalificacionGlobalRequest request);
        IEnumerable<CalificacionFaseDTO> ObtenerCalificacionFase(int idLlamada, bool tipoCalificacion);
        IEnumerable<InformacionLlamadaTresCxDTO> ObtenerInformacionLlamada(int idLlamada);

        //bool ProcesarRecomendacionesBatch(CalificacionLlamadaAutomaticaDTO calificacionLlamada);













    }
}