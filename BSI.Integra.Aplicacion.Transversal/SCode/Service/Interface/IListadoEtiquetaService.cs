using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IListadoEtiquetaService
    {
        string ObtenerTelefonoPersonal(string central, string anexo3Cx);
        string EtiquetaUrlFirmaCorreo(string email);
        string EtiquetaListaProgramasPorIdEtiqueta(int idOportunidad, int idEtiqueta);
        string ObtenerUrlVersion(string urlVersion);
        string EtiquetaCursoRelacionado(int idCentroCosto);
        string ObtenerUrlBrochurePrograma(string urlBrochurePrograma);
        string EtiquetaExpositor(int idPGeneral);
        string ObtenerDuracionAndHorario(int idPGeneral);
        string ObtenerUrlDocumentoCronograma(string urlDocumentoCronograma);
        List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion, bool conTitulo);

    }
}
