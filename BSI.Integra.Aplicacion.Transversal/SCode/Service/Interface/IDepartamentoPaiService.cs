using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IDepartamentoPaiService
    {
        IEnumerable<ComboDTO> ObtenerCombo();
        CodigoDepartamentoDTO ObtenerCodigoPorId(int id);
    }
}
