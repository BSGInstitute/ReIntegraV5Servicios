
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICategoriaCiudadService
    {
        TroncalEntidadDTO InsertarTroncal(TroncalEntidadDTO dto, string usuario);
        TroncalEntidadDTO ActualizarTroncal(TroncalEntidadDTO dto, string usuario);
        IEnumerable<TroncalDTO> ObtenerTroncales();
        IEnumerable<ComboDTO> ObtenerCategoriaCombo();
        IEnumerable<ComboDTO> ObtenerCiudadBsCombo();
    }
}
