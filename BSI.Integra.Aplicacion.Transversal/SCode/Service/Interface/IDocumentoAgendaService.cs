using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDocumentoAgendaService
    {
        IEnumerable<DocumentoAgendaSinAuditoriaDTO> ObtenerDocumentoAgendaSinAuditoria();
        DocumentoAgendaDetalleDTO ObtenerDocumentoAgendaDetallePorIdActividadDetalle(int idActividadDetalle);
        StringDTO ObtenerDocumentoAgendaUrlPorPais(int idDocumentoAgenda, int idPais);
        DocumentoAgendaDescargaDTO ObtenerUrlDocumentoAgenda(
            DocumentoAgendaDescargaDTO documento,
            PEspecificoPorIdCentroCostoDTO programaEspecifico,
            PGeneralAtributosPrincipalesDTO programaGeneral
        );
        DocumentoAgendaDescargaDTO ObtenerBytesDocumentoAgenda(
            DocumentoAgendaDescargaDTO documento,
            PEspecificoPorIdCentroCostoDTO programaEspecifico,
            PGeneralAtributosPrincipalesDTO programaGeneral,
            OportunidadCompuestoDTO oportunidad
        );
        ProgramaCuotasDetalleDTO ObtenerCronograma(int idMatriculaCabecera);
        List<string> ObtenerBeneficiosConfiguradosProgramaGeneral(int idPGeneral, int? idPais, int? idPaquete);
        List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneralVersion(int idPGeneral, string version);
        string ValidarDocumentoPerfil(List<PgeneralHijoDTO> listaCursos, PgeneralDocumentoSeccionDTO pgeneral);
        string GenerarVersion(string Id);
        List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion);
        List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneral(int idPGeneral);
        List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneralSpeech(int idPGeneral);
        Task<List<ProgramaGeneralSeccionDocumentoDTO>> ObtenerListaSeccionDocumentoProgramaGeneralAsync(int idPGeneral);
    }
}
