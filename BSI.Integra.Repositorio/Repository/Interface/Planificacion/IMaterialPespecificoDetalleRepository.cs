using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialPespecificoDetalleRepository : IGenericRepository<TMaterialPespecificoDetalle>
    {
        #region Metodos Base
        TMaterialPespecificoDetalle Add(MaterialPespecificoDetalle entidad);
        TMaterialPespecificoDetalle Update(MaterialPespecificoDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialPespecificoDetalle> Add(IEnumerable<MaterialPespecificoDetalle> listadoEntidad);
        IEnumerable<TMaterialPespecificoDetalle> Update(IEnumerable<MaterialPespecificoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        MaterialPespecificoDetalle? ObtenerPorId(int id);
        List<MaterialPespecificoDetalle> ObtenerPorIds(List<int> ids);
        IEnumerable<MaterialPEspecificoDetalleCriterioDTO> ObtenerDetalleMaterialPEspecifico(int idMaterialPEspecifico, int idMaterialAccion, int idMaterialVersion);
        List<MaterialPespecificoDetalle> ObtenerPorMaterial(List<int> idMaterialPEspecifico, List<int> idMaterialVersion, List<int> idMaterialEstado);
        MaterialPEspecificoDetalleEnvioProveedorDTO ObtenerDetalleMaterialPEspecificoEnviarProveedor(int id);
        AsociarActualizarFurMaterialVersionDTO ObtenerFurAsociadoPorIdPEspecificoDetalle(int idMaterialPEspecificoDetalle);
        MaterialPEspecificoDetalleFurDTO ObtenerDetalleFur(int idMaterialPEspecificoDetalle);
    }
}
