using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IParametroSeoPwService
    {
        IEnumerable<ParametroSeoPwDTO> ObtenerCombo();
    }
}
