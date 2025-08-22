using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface INivelEstudioService
    {
        IEnumerable<NivelEstudioDTO> Obtener();
        IEnumerable<TipoFormacionDTO> ObtenerFormacion();
        NivelEstudioComboDTO Insertar(NivelEstudioComboDTO dto, string usuario);
        NivelEstudioComboDTO Actualizar(NivelEstudioComboDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
