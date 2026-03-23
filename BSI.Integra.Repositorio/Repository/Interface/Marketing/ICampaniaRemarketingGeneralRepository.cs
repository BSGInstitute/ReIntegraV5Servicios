using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface ICampaniaRemarketingGeneralRepository
    {
        List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania();
        List<RendimientoDiarioCampaniaDTO> ObtenerRendimientoCampanias(List<int> ids);
        List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados();
        bool InsertarCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request);
        bool ActualizarCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request);
        //DetallesCampaniaDTO VerDetallesCampania(int id);
        CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id);
        DetallesCampaniaDTO ObtenerDetallesGeneralesEnvio(int idCampaniaRemarketing);
        ElementoEstadoEnvio ObtenerEstadoEnvioCampaniaRemarketing(int idCampaniaRemarketing);
        bool EliminarCampania(int id, string usuario);

        // Métodos individuales para cada lista de CombosConfiguracionCampaniaDTO
        List<ElementoConfiguracionCampania> ObtenerMediosEnvio();
        List<ElementoConfiguracionCampania> ObtenerTiposMensaje();
        List<ElementoConfiguracionCampania> ObtenerLogicasEnvio();
        List<ElementoConfiguracionCampania> ObtenerCategoriaArgumento();

        List<int> ObtenerPrioridadesUnicas();
        List<AlumnoCorreoDTO> ObtenerAlumnosCorreosPorFiltroSegmento(int idFiltroSegmento);
        bool InsertarEstadosEnvioCampaniaMasivo(List<RemarketingEstadoCampaniaDTO> estados);
        bool ActualizarEstadoEnvioCampania(int idCampaniaRemarketing, int estadoEnvio, string usuario);

        // Métodos para envío programado
        List<CampaniaProgramadaParaEjecutarDTO> ObtenerCampaniasProgramadasParaEjecutar();

        // Canvas
        bool InsertarCampaniaCanvas(CampaniaCanvasDTO request, string usuario);
        bool ActualizarCampaniaCanvas(CampaniaCanvasDTO request, string usuario);
        CampaniaCanvasDTO ObtenerCampaniaCanvas(int idRemarketingCampaniaGeneral);
        bool EliminarCampaniaCanvas(int idRemarketingCampaniaGeneral, string usuario);

    }
}
