using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGmailCorreoArchivoAdjuntoRepository : IGenericRepository<TGmailCorreoArchivoAdjunto>
    {
        List<GmailCorreoArchivoAdjuntoDTO> obtenerCorreoArchivoAdjuntoPorId(int idCorreoArchivo);
    }
}
