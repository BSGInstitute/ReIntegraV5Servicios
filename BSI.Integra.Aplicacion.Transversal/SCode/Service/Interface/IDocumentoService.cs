using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDocumentoService
    {
        List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion);
        byte[] RepublicacionVistaPreviaCertificado(int IdPlantillaF, int IdPlantillaP, int IdOportunidad, TMatriculaCabeceraDatosCertificado solicitudCertificado, ref int Idplantillabase, ref string codigoCertificado);
        byte[] GenerarCertificadoIrca(int idPlantillaF, int idContenidoCertificadoIrca, int idPEspecifico, ref string codigoCertificado, ref int idPgeneral);
        byte[] GenerarVistaModeloCertificado(int idPlantillaF, int idPlantillaP, int idPgeneral);

    }
}
