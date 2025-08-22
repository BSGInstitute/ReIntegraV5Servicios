using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DuplicarDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class FiltroSegmentoDTO
    {
        public int Id { get; set; }
        public int? IdFiltroSegmentoTipoContacto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdOperadorComparacionNroSolicitudInformacion { get; set; }
        public int? NroSolicitudInformacion { get; set; }
        public int? IdOperadorComparacionNroOportunidades { get; set; }
        public int? NroOportunidades { get; set; }

        public DateTime? FechaInicioCreacionUltimaOportunidad { get; set; }
        public DateTime? FechaFinCreacionUltimaOportunidad { get; set; }
        public DateTime? FechaInicioModificacionUltimaActividadDetalle { get; set; }
        public DateTime? FechaFinModificacionUltimaActividadDetalle { get; set; }

        public bool EsRn2 { get; set; }
        public DateTime? FechaInicioProgramacionUltimaActividadDetalleRn2 { get; set; }
        public DateTime? FechaFinProgramacionUltimaActividadDetalleRn2 { get; set; }

        //Fecha formulario
        public DateTime? FechaInicioFormulario { get; set; }
        public DateTime? FechaFinFormulario { get; set; }
        //Fecha chat
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
        //correo
        public DateTime? FechaInicioCorreo { get; set; }
        public DateTime? FechaFinCorreo { get; set; }
        public int? IdOperadorComparacionNroCorreosAbiertos { get; set; }
        public int? NroCorreosAbiertos { get; set; }
        public int? IdOperadorComparacionNroCorreosNoAbiertos { get; set; }
        public int? NroCorreosNoAbiertos { get; set; }
        public int? IdOperadorComparacionNroClicksEnlace { get; set; }
        public int? NroClicksEnlace { get; set; }
        public bool EsSuscribirme { get; set; }
        public bool EsDesuscribirme { get; set; }

        public int? IdOperadorComparacionNroCorreosAbiertosMailChimp { get; set; }
        public int? NroCorreosAbiertosMailChimp { get; set; }
        public int? IdOperadorComparacionNroCorreosNoAbiertosMailChimp { get; set; }
        public int? NroCorreosNoAbiertosMailChimp { get; set; }
        public int? IdOperadorComparacionNroClicksEnlaceMailChimp { get; set; }
        public int? NroClicksEnlaceMailChimp { get; set; }

        public bool ConsiderarFiltroGeneral { get; set; }
        public bool ConsiderarFiltroEspecifico { get; set; }
        public bool TieneVentaCruzada { get; set; }
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

        //Tab interaccion sitio web
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

        public int? IdOperadorComparacionNroSolicitudInformacionPg { get; set; }
        public int? NroSolicitudInformacionPg { get; set; }
        public int? IdOperadorComparacionNroSolicitudInformacionArea { get; set; }
        public int? NroSolicitudInformacionArea { get; set; }
        public int? IdOperadorComparacionNroSolicitudInformacionSubArea { get; set; }
        public int? NroSolicitudInformacionSubArea { get; set; }

        // Filtro Segmento Mailing General
        public int? IdCampaniaGeneral { get; set; }
        public int? TipoAsociacion { get; set; }
        public int? CantidadPeriodoSinRecibirCorreo { get; set; }
        public int? TipoPeriodoSinRecibirCorreo { get; set; }
        public int? IdProbabilidadOportunidad { get; set; }
        public int? NumeroSegmento { get; set; }
        public int? IdPrioridadMailChimpLista { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }

        //Considerar tabs
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
        public bool? ConsiderarEnvioAutomatico { get; set; }
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
        public bool? ExcluirMatriculados { get; set; }
        // TAB - ENVIO AUTOMATICO
        public bool? AplicaSobreCreacionOportunidad { get; set; }
        public int? IdOperadorMedidaTiempoCreacionOportunidad { get; set; }
        public int? NroMedidaTiempoCreacionOportunidad { get; set; }
        public bool? AplicaSobreUltimaActividad { get; set; }
        public int? IdOperadorMedidaTiempoUltimaActividadEjecutada { get; set; }
        public int? NroMedidaTiempoUltimaActividadEjecutada { get; set; }
        public int? EnvioAutomaticoEstadoActividadDetalle { get; set; }
        public bool? ConsiderarYaEnviados { get; set; }

        //Listas
        public List<FiltroSegmentoValorTipoDTO> ListaArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaSubArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaProgramaGeneral { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaProgramaEspecifico { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaOportunidadInicialFaseMaxima { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaOportunidadInicialFaseActual { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaOportunidadActualFaseMaxima { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaOportunidadActualFaseActual { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaPais { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaCiudad { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaTipoCategoriaOrigen { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaCategoriaOrigen { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaCargo { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaIndustria { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaAreaFormacion { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaAreaTrabajo { get; set; }


        public List<FiltroSegmentoValorTipoDTO> ListaTipoFormulario { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaTipoInteraccionFormulario { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaProbabilidadOportunidad { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaActividadLlamada { get; set; }

        public List<FiltroSegmentoValorTipoDTO> ListaVCArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaVCSubArea { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaVCPGeneral { get; set; }

        public List<FiltroSegmentoValorTipoDTO> ListaProbabilidadVentaCruzada { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaExcluirPorFiltroSegmento { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaExcluirPorConjuntoLista { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaExcluirPorCampaniaMailing { get; set; }

        public List<FiltroSegmentoValorTipoDTO> ListaActividadCabecera { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaOcurrencia { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaDocumentoAlumno { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaEstadoMatricula { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaSubEstadoMatricula { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaModalidadCurso { get; set; }

        public List<FiltroSegmentoDetalleDTO> ListaSesion { get; set; }
        public List<FiltroSegmentoDetalleDTO> ListaEstadoAcademico { get; set; }
        public List<FiltroSegmentoDetalleDTO> ListaEstadoPago { get; set; }
        public List<FiltroSegmentoDetalleDTO> ListaPorcentajeAvance { get; set; }

        public List<FiltroSegmentoDetalleDTO> ListaEstadoLlamada { get; set; }

        public List<FiltroSegmentoDetalleDTO> ListaSesionWebinar { get; set; }
        public List<FiltroSegmentoDetalleDTO> ListaTrabajoAlumno { get; set; }
        public List<FiltroSegmentoDetalleDTO> ListaTrabajoAlumnoFinal { get; set; }
        public List<FiltroSegmentoValorTipoDTO> ListaTarifario { get; set; }

        public List<FiltroSegmentoValorTipoDTO> ListaEnvioAutomaticoOportunidadFaseActual { get; set; }

        public string NombreUsuario { get; set; }
        public int? IdCampaniaMailing { get; set; }
        public int? IdCampaniaMailingLista { get; set; }

        //Conjunto lista
        public int? IdConjuntoListaDetalle { get; set; }
        public int? NroListasRepeticionContacto { get; set; }
        public int? NroEjecucion { get; set; }
        public FiltroSegmentoDTO()
        {
            ListaArea = new List<FiltroSegmentoValorTipoDTO>();
            ListaSubArea = new List<FiltroSegmentoValorTipoDTO>();
            ListaProgramaGeneral = new List<FiltroSegmentoValorTipoDTO>();
            ListaProgramaEspecifico = new List<FiltroSegmentoValorTipoDTO>();
            ListaOportunidadInicialFaseMaxima = new List<FiltroSegmentoValorTipoDTO>();
            ListaOportunidadInicialFaseActual = new List<FiltroSegmentoValorTipoDTO>();
            ListaOportunidadActualFaseMaxima = new List<FiltroSegmentoValorTipoDTO>();
            ListaOportunidadActualFaseActual = new List<FiltroSegmentoValorTipoDTO>();
            ListaPais = new List<FiltroSegmentoValorTipoDTO>();
            ListaCiudad = new List<FiltroSegmentoValorTipoDTO>();
            ListaTipoCategoriaOrigen = new List<FiltroSegmentoValorTipoDTO>();
            ListaCategoriaOrigen = new List<FiltroSegmentoValorTipoDTO>();
            ListaCargo = new List<FiltroSegmentoValorTipoDTO>();
            ListaIndustria = new List<FiltroSegmentoValorTipoDTO>();
            ListaAreaFormacion = new List<FiltroSegmentoValorTipoDTO>();
            ListaAreaTrabajo = new List<FiltroSegmentoValorTipoDTO>();


            ListaTipoFormulario = new List<FiltroSegmentoValorTipoDTO>();
            ListaTipoInteraccionFormulario = new List<FiltroSegmentoValorTipoDTO>();
            ListaProbabilidadOportunidad = new List<FiltroSegmentoValorTipoDTO>();
            ListaActividadLlamada = new List<FiltroSegmentoValorTipoDTO>();

            ListaVCArea = new List<FiltroSegmentoValorTipoDTO>();
            ListaVCSubArea = new List<FiltroSegmentoValorTipoDTO>();
            ListaProbabilidadVentaCruzada = new List<FiltroSegmentoValorTipoDTO>();
            ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado = new List<FiltroSegmentoValorTipoDTO>();
            ListaExcluirPorFiltroSegmento = new List<FiltroSegmentoValorTipoDTO>();
            ListaExcluirPorConjuntoLista = new List<FiltroSegmentoValorTipoDTO>();
            ListaExcluirPorCampaniaMailing = new List<FiltroSegmentoValorTipoDTO>();

            ListaActividadCabecera = new List<FiltroSegmentoValorTipoDTO>();
            ListaOcurrencia = new List<FiltroSegmentoValorTipoDTO>();
            ListaDocumentoAlumno = new List<FiltroSegmentoValorTipoDTO>();
            ListaEstadoMatricula = new List<FiltroSegmentoValorTipoDTO>();
            ListaSubEstadoMatricula = new List<FiltroSegmentoValorTipoDTO>();
            ListaModalidadCurso = new List<FiltroSegmentoValorTipoDTO>();

            ListaSesion = new List<FiltroSegmentoDetalleDTO>();
            ListaEstadoAcademico = new List<FiltroSegmentoDetalleDTO>();
            ListaEstadoPago = new List<FiltroSegmentoDetalleDTO>();
            ListaPorcentajeAvance = new List<FiltroSegmentoDetalleDTO>();
            ListaEstadoLlamada = new List<FiltroSegmentoDetalleDTO>();

            ListaSesionWebinar = new List<FiltroSegmentoDetalleDTO>();
            ListaTrabajoAlumno = new List<FiltroSegmentoDetalleDTO>();
            ListaTrabajoAlumnoFinal = new List<FiltroSegmentoDetalleDTO>();
            ListaTarifario = new List<FiltroSegmentoValorTipoDTO>();

            ListaEnvioAutomaticoOportunidadFaseActual = new List<FiltroSegmentoValorTipoDTO>();
        }
    }

}
