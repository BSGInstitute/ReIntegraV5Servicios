
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IMaterialAdicionalAulaVirtualService
    {
        IEnumerable<MaterialAdicionalAulaVirtualDTO> Obtener();
        MaterialAdicionalAulaVirtualDetalleDTO ObtenerDetalleMaterialAdicional(int idMaterialAdicional);
        MaterialAdicionalAulaVirtualDTO InsertarMaterialAdicional(MaterialAdicionalAulaVirtualEntidadDTO dto, string usuario);
        MaterialAdicionalAulaVirtualDTO ActualizarMaterialAdicional(MaterialAdicionalAulaVirtualEntidadDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
