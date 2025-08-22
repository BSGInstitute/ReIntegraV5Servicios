using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ISedeTrabajoService
    {
        public IEnumerable<SedeTrabajoComboDTO> ObtenerSedeTrabajoCombo();
    }
}
