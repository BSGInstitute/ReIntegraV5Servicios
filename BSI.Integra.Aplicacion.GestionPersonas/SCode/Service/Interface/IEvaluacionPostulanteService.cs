
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IEvaluacionPostulanteService
    {
        Task<EvaluacionPostulanteComboDTO> ObtenerCombosModulo();
        ResultadoReporteEvaluacionPostulante GenerarReporte(EvaluacionPostulanteFiltroReporteDTO filtroReporte);
        TipoEvaluacionDTO ObtenerTipoExamen(FiltroTipoExamenDTO filtro);
        List<EvaluacionesAsignadasEvaluadorDTO> ObtenerEvaluacionesAsignadasEvaluador(int idPostulante, int idProcesoSeleccion);
        PreguntaTestAgrupadoDTO ObtenerPreguntasRespuestasRealizadasTestEvaluador(TestInformacionDTO testInformacion);
        PreguntaTestAgrupadoDTO ObtenerPreguntasRespuestasTestEvaluador(TestInformacionDTO testInformacion);
        List<EvaluacionPortalPostulante> ObtenerEvaluacionesPortalPostulante(EvaluacionPostulanteFiltroReporteDTO filtro);
        bool ActualizacionManualEtapaExamenAsignado(CalificacionManualDTO dto, string usuario);
        bool EnviarRespuestasTest(RespuestaEvaluacionEvaluadorDTO RespuestaTest, string usuario);
        List<ReportePostulanteMatriculaDTO> ObtenerNotasMatriculaReporte(List<int> idsPostulantes);
        bool RestablecerNotas(EnvioDatosReestablecerDTO dto, string usuario);
        List<ReportePruebaDTO> GenerarReporteIntegra(EvaluacionPostulanteFiltroReporteDTO filtroReporte);
        (bool Respuesta, string Mensaje) EnviarAccesoAulaVirtualPostulante(EnviarAccesoPostulanteDTO dto, string usuario);
    }
}
