using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface ICampaniaRemarketingGeneralRepository
    {
        List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania();
        List<object> ObtenerRendimientoListadoCampanias(List<int> ids);
        List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados();
        //List<ResultadoTextoGeneradoDTO> ObtenerResultadosGeneracionTextoPorCampania(string idLlamadaIA);
        bool InsertarCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request);
        bool ActualizarCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request);
        //DetallesCampaniaDTO VerDetallesCampania(int id);
        CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id);
        DetallesCampaniaDTO ObtenerDetallesGeneralesEnvio(int idCampaniaRemarketing);
        ElementoEstadoEnvio ObtenerEstadoEnvioCampaniaRemarketing(int idCampaniaRemarketing);
        bool EliminarCampania(int id, string usuario);
        //MensajeGeneradoDTO ObtenerMensajeGeneradoPorId(int id);

        // Métodos individuales para cada lista de CombosConfiguracionCampaniaDTO
        List<ElementoConfiguracionCampania> ObtenerMediosEnvio();
        List<ElementoConfiguracionCampania> ObtenerTiposMensaje();
        List<ElementoConfiguracionCampania> ObtenerLogicasEnvio();
        //List<ElementoConfiguracionCampania> ObtenerArgumentos();
        List<ElementoConfiguracionCampania> ObtenerCategoriaArgumento();
        List<int> ObtenerPrioridadesUnicas();
        List<AlumnoCorreoDTO> ObtenerAlumnosCorreosPorFiltroSegmento(int idFiltroSegmento);

        // Métodos para el envío masivo de correos
        //bool InsertarEstadoEnvioCampania(RemarketingEstadoCampaniaDTO estado);
        bool InsertarEstadosEnvioCampaniaMasivo(List<RemarketingEstadoCampaniaDTO> estados);
        bool ActualizarEstadoEnvioCampania(int idCampaniaRemarketing, int estadoEnvio, string usuario);

    }
}
