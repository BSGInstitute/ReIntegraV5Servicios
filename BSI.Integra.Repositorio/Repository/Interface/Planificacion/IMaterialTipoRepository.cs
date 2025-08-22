using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialTipoRepository : IGenericRepository<TMaterialTipo>
    {
        #region Metodos Base
        TMaterialTipo Add(MaterialTipo entidad);
        IEnumerable<TMaterialTipo> Add(IEnumerable<MaterialTipo> listadoEntidad);
        TMaterialTipo Update(MaterialTipo entidad);
        IEnumerable<TMaterialTipo> Update(IEnumerable<MaterialTipo> listadoEntidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialTipoDetalleDTO> Obtener();
        IEnumerable<MaterialTipoDetalleDTO> ObtenerRelacionesPorId(int idTipoDocumento);
        MaterialTipo ObtenerPorId(int idTipoMaterial);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
