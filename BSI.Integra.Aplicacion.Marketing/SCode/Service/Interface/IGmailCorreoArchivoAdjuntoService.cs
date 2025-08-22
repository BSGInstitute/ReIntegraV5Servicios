using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IGmailCorreoArchivoAdjuntoService
    {
        List<GmailCorreoArchivoAdjuntoDTO> obtenerCorreoArchivoAdjuntoPorId(int idCorreoArchivo);
    }
}
