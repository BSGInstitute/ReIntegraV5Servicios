using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoDTO
    {
        public List<ProgramaDocumentosDTO> ListaDocumentos { get; set; } = new List<ProgramaDocumentosDTO>();
        public PEspecificoDTO PEspecifico { get; set; } = new PEspecificoDTO();
        public DatosOportunidadDocumentosCompuestoDTO Oportunidad { get; set; } = new DatosOportunidadDocumentosCompuestoDTO();
        public CertificadoGeneradoAutomaticoContenido contenidoCertificado { get; set; } = new CertificadoGeneradoAutomaticoContenido();
        public string Contenido = string.Empty;
    }
}
