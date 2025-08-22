using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface INotaService
    {
        List<NotaPresencialDTO> ListadoNotaPorMatriculaCabecera(int idMatriculaCabecera);
        List<NotaPresencialPromedioDTO> ListadoNotaPorMatriculaCabeceraPromedio(int idMatriculaCabecera);
    }
}
