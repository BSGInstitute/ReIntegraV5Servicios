using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion; 
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICrucigramaProgramaCapacitacionDetalleRepository : IGenericRepository<TCrucigramaProgramaCapacitacionDetalle>
    {
        #region Metodos Base
        TCrucigramaProgramaCapacitacionDetalle Add(CrucigramaProgramaCapacitacionDetalle entidad);
        TCrucigramaProgramaCapacitacionDetalle Update(CrucigramaProgramaCapacitacionDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCrucigramaProgramaCapacitacionDetalle> Add(IEnumerable<CrucigramaProgramaCapacitacionDetalle> listadoEntidad);
        IEnumerable<TCrucigramaProgramaCapacitacionDetalle> Update(IEnumerable<CrucigramaProgramaCapacitacionDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        CrucigramaProgramaCapacitacionDetalle? ObtenerPorId(int id);
        List<CrucigramaProgramaCapacitacionDetalle> ObtenerPorIdCrucigramaProgramaCapacitacionDetalle(int idCrucigramaProgramaCapacitacionDetalle);
    }
}
