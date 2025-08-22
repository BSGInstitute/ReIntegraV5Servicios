using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IMaterialTipoService
    {
        ListaCombosDTO ObtenerCombosModulo();
        IEnumerable<MaterialTipoAgrupadoDTO> Obtener();
        MaterialTipoAgrupadoDTO InsertarMaterialTipo(MaterialTipoAsociacionEntidadDTO dto, string usuario);
        MaterialTipoAgrupadoDTO ActualizarMaterialTipo(MaterialTipoAsociacionEntidadDTO dto, string usuario);
        bool EliminarMaterialTipo(int id, string usuario);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
