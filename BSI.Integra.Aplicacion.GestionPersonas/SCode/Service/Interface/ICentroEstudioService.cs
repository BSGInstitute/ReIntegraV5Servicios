using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface ICentroEstudioService
    {
         IEnumerable<CentroEstudioDTO> Obtener();
        CentroEstudioDTO Insertar(CentroEstudioDTO dto, string usuario);
        CentroEstudioDTO Actualizar(CentroEstudioDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

        IEnumerable<CentroEstudioComboDTO> ObtenerComboCentroEstudio();

        IEnumerable<CentroEstudioComboDTO> ObtenerListaEstadoEstudioCombo();
    }
}
