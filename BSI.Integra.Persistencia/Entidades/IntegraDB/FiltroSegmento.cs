using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FiltroSegmento : BaseIntegraEntity
    {

        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public int? IdOperadorComparacionNroSolicitudInformacionPg { get; set; }

        public int? IdOperadorComparacionNroSolicitudInformacionArea { get; set; }

        public int? NroSolicitudInformacionArea { get; set; }

        public int? NroCorreosAbiertosMailChimp { get; set; }

        public int? NroSolicitudInformacionSubArea { get; set; }

        public int? IdOperadorComparacionNroClicksEnlaceMailChimp { get; set; }

        public int? IdOperadorComparacionNroSolicitudInformacionSubArea { get; set; }

        public int? NroCorreosNoAbiertosMailChimp { get; set; }

        public int? NroClicksEnlaceMailChimp { get; set; }

        public bool? EsSuscribirme { get; set; }

        public bool? EsDesuscribirme { get; set; }

        public int? IdOperadorComparacionNroCorreosNoAbiertosMailChimp { get; set; }

        public int? IdOperadorComparacionNroCorreosAbiertosMailChimp { get; set; }

        public int? NroSolicitudInformacionPg { get; set; }

        public int? IdMigracion { get; set; }

        public int? IdFiltroSegmentoTipoContacto { get; set; }

        public DateTime? FechaInicioCreacionUltimaOportunidad { get; set; }

        public DateTime? FechaFinCreacionUltimaOportunidad { get; set; }

        public DateTime? FechaInicioModificacionUltimaActividadDetalle { get; set; }

        public DateTime? FechaFinModificacionUltimaActividadDetalle { get; set; }

        public int? IdOperadorComparacionNroSolicitudInformacion { get; set; }

        public int? NroSolicitudInformacion { get; set; }

        public int? IdOperadorComparacionNroOportunidades { get; set; }

        public int? NroOportunidades { get; set; }

        public bool? EsRn2 { get; set; }

        public DateTime? FechaInicioProgramacionUltimaActividadDetalleRn2 { get; set; }

        public DateTime? FechaFinProgramacionUltimaActividadDetalleRn2 { get; set; }

        public DateTime? FechaInicioFormulario { get; set; }

        public DateTime? FechaFinFormulario { get; set; }

        public DateTime? FechaInicioChatIntegra { get; set; }

        public DateTime? FechaFinChatIntegra { get; set; }

        public int? IdOperadorComparacionTiempoMaximoRespuestaChatOnline { get; set; }

        public int? TiempoMaximoRespuestaChatOnline { get; set; }

        public int? IdOperadorComparacionNroPalabrasClienteChatOnline { get; set; }

        public int? NroPalabrasClienteChatOnline { get; set; }

        public int? IdOperadorComparacionTiempoPromedioRespuestaChatOnline { get; set; }

        public int? TiempoPromedioRespuestaChatOnline { get; set; }

        public int? IdOperadorComparacionNroPalabrasClienteChatOffline { get; set; }

        public int? NroPalabrasClienteChatOffline { get; set; }

        public DateTime? FechaInicioCorreo { get; set; }

        public DateTime? FechaFinCorreo { get; set; }

        public int? IdOperadorComparacionNroCorreosAbiertos { get; set; }

        public int? NroCorreosAbiertos { get; set; }

        public int? IdOperadorComparacionNroCorreosNoAbiertos { get; set; }

        public int? NroCorreosNoAbiertos { get; set; }

        public int? IdOperadorComparacionNroClicksEnlace { get; set; }

        public int? NroClicksEnlace { get; set; }

        public bool? ConsiderarFiltroGeneral { get; set; }

        public bool? ConsiderarFiltroEspecifico { get; set; }

        public bool? TieneVentaCruzada { get; set; }

        public int? IdOperadorComparacionNroTotalLineaCreditoVigente { get; set; }

        public int? NroTotalLineaCreditoVigente { get; set; }

        public int? IdOperadorComparacionMontoTotalLineaCreditoVigente { get; set; }

        public int? MontoTotalLineaCreditoVigente { get; set; }

        public int? IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente { get; set; }

        public int? MontoMaximoOtorgadoLineaCreditoVigente { get; set; }

        public int? IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente { get; set; }

        public int? MontoMinimoOtorgadoLineaCreditoVigente { get; set; }

        public int? IdOperadorComparacionNroTotalLineaCreditoVigenteVencida { get; set; }

        public int? NroTotalLineaCreditoVigenteVencida { get; set; }

        public int? IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida { get; set; }

        public int? MontoTotalLineaCreditoVigenteVencida { get; set; }

        public int? IdOperadorComparacionNroTcOtorgada { get; set; }

        public int? NroTcOtorgada { get; set; }

        public int? IdOperadorComparacionMontoTotalOtorgadoEnTcs { get; set; }

        public int? MontoTotalOtorgadoEnTcs { get; set; }

        public int? IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc { get; set; }

        public int? MontoMaximoOtorgadoEnUnaTc { get; set; }

        public int? IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc { get; set; }

        public int? MontoMinimoOtorgadoEnUnaTc { get; set; }

        public int? IdOperadorComparacionMontoDisponibleTotalEnTcs { get; set; }

        public int? MontoDisponibleTotalEnTcs { get; set; }

        public DateTime? FechaInicioLlamada { get; set; }

        public DateTime? FechaFinLlamada { get; set; }

        public int? IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad { get; set; }

        public int? DuracionPromedioLlamadaPorOportunidad { get; set; }

        public int? IdOperadorComparacionDuracionTotalLlamadaPorOportunidad { get; set; }

        public int? DuracionTotalLlamadaPorOportunidad { get; set; }

        public int? IdOperadorComparacionNroLlamada { get; set; }

        public int? NroLlamada { get; set; }

        public int? IdOperadorComparacionDuracionLlamada { get; set; }

        public int? DuracionLlamada { get; set; }

        public int? IdOperadorComparacionTasaEjecucionLlamada { get; set; }

        public int? TasaEjecucionLlamada { get; set; }

        public DateTime? FechaInicioInteraccionSitioWeb { get; set; }

        public DateTime? FechaFinInteraccionSitioWeb { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionTotalSitioWeb { get; set; }

        public int? TiempoVisualizacionTotalSitioWeb { get; set; }

        public int? IdOperadorComparacionNroClickEnlaceTodoSitioWeb { get; set; }

        public int? NroClickEnlaceTodoSitioWeb { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma { get; set; }

        public int? TiempoVisualizacionTotalPaginaPrograma { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas { get; set; }

        public int? TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas { get; set; }

        public int? IdOperadorComparacionNroClickEnlacePaginaPrograma { get; set; }

        public int? NroClickEnlacePaginaPrograma { get; set; }

        public bool? ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma { get; set; }

        public bool? ConsiderarClickBotonMatricularmePaginaPrograma { get; set; }

        public bool? ConsiderarClickBotonVersionPruebaPaginaPrograma { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus { get; set; }

        public int? TiempoVisualizacionTotalPaginaBscampus { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus { get; set; }

        public int? TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus { get; set; }

        public int? IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea { get; set; }

        public int? NroVisitasDirectorioTagAreaSubArea { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea { get; set; }

        public int? TiempoVisualizacionTotalDirectorioTagAreaSubArea { get; set; }

        public int? IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea { get; set; }

        public int? NroClickEnlaceDirectorioTagAreaSubArea { get; set; }

        public int? IdOperadorComparacionNroVisitasPaginaMisCursos { get; set; }

        public int? NroVisitasPaginaMisCursos { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos { get; set; }

        public int? TiempoVisualizacionTotalPaginaMisCursos { get; set; }

        public int? IdOperadorComparacionNroClickEnlacePaginaMisCursos { get; set; }

        public int? NroClickEnlacePaginaMisCursos { get; set; }

        public int? IdOperadorComparacionNroVisitaPaginaCursoDiplomado { get; set; }

        public int? NroVisitaPaginaCursoDiplomado { get; set; }

        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado { get; set; }

        public int? TiempoVisualizacionTotalPaginaCursoDiplomado { get; set; }

        public int? IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado { get; set; }

        public int? NroClicksEnlacePaginaCursoDiplomado { get; set; }

        public bool? ConsiderarClickFiltroPaginaCursoDiplomado { get; set; }

        public bool? ConsiderarOportunidadHistorica { get; set; }

        public bool? ConsiderarCategoriaDato { get; set; }

        public bool? ConsiderarInteraccionOfflineOnline { get; set; }

        public bool? ConsiderarInteraccionSitioWeb { get; set; }

        public bool? ConsiderarInteraccionFormularios { get; set; }

        public bool? ConsiderarInteraccionChatPw { get; set; }

        public bool? ConsiderarInteraccionCorreo { get; set; }

        public bool? ConsiderarHistorialFinanciero { get; set; }

        public bool? ConsiderarInteraccionWhatsApp { get; set; }

        public bool? ConsiderarInteraccionChatMessenger { get; set; }

        public bool? ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }

        public DateTime? FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }

        public DateTime? FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }

        public int? IdTiempoFrecuenciaMatriculaAlumno { get; set; }

        public int? CantidadTiempoMatriculaAlumno { get; set; }

        public bool? ConsiderarConMessengerValido { get; set; }

        public bool? ConsiderarConWhatsAppValido { get; set; }

        public bool? ConsiderarConEmailValido { get; set; }

        public int? IdTiempoFrecuenciaCumpleaniosContactoDentroDe { get; set; }

        public int? CantidadTiempoCumpleaniosContactoDentroDe { get; set; }

        public DateTime? FechaInicioMatriculaAlumno { get; set; }

        public DateTime? FechaFinMatriculaAlumno { get; set; }

        public bool? ConsiderarAlumnosAsignacionAutomaticaOperaciones { get; set; }

        public int? IdOperadorMedidaTiempoCreacionOportunidad { get; set; }

        public int? NroMedidaTiempoCreacionOportunidad { get; set; }

        public int? IdOperadorMedidaTiempoUltimaActividadEjecutada { get; set; }


        public int? NroMedidaTiempoUltimaActividadEjecutada { get; set; }


        public int? EnvioAutomaticoEstadoActividadDetalle { get; set; }

        public int? ConsiderarYaEnviados { get; set; }

        public bool? ConsiderarEnvioAutomatico { get; set; }

        public bool? AplicaSobreCreacionOportunidad { get; set; }

        public bool? AplicaSobreUltimaActividad { get; set; }

        public bool? ExcluirMatriculados { get; set; }
        public bool? ConsiderarUltimaOportunidad { get; set; }
        public bool? ConsiderarMayorProbabilidadInscripcion { get; set; }
        public bool? ConsiderarMayorProbabilidadInscripcionVentaCruzada { get; set; }
        public bool? ConsiderarProbabilidad { get; set; }
        public bool? ConsiderarProbabilidadVentaCruzada { get; set; }
        public bool? ConsiderarEmbudo { get; set; }
        public bool? ConsiderarExcluirCampania { get; set; }
        public DateTime? FechaInicioExclusionCampania { get; set; }
        public DateTime? FechaFinExclusionCampania { get; set; }
        public ICollection<CampaniaGeneral> CampaniaGenerals { get; set; }
        public   ICollection<FiltroSegmentoValorTipo> FiltroSegmentoValorTipos { get; set; }
        public   ICollection<FiltroSegmentoDetalle> FiltroSegmentoDetalles { get; set; }

    }

}
