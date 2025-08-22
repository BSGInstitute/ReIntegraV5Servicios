using BSI.Integra.Aplicacion.DTO;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IEstadoSolicitudBeneficioService
    {
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
