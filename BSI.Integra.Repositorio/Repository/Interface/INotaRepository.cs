using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface INotaRepository : IGenericRepository<TNotum>
    {
        List<NotaPresencialDTO> ListadoNotaPorMatriculaCabecera(int idMatriculaCabecera);
        List<NotaPresencialPromedioDTO> ListadoNotaPorMatriculaCabeceraPromedio(int idMatriculaCabecera);
        List<NotaPresencialPromedioEspecificoDTO> ListadoNotaPorMatriculaCabeceraPromedioIdEspecifico(int idMatriculaCabecera, int IdPEspecifico);
    }
}
