using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IBloqueHorarioService
    {
        IEnumerable<BloqueHorarioProcesarBicDTO> ObtenerCombo();
    }
}
