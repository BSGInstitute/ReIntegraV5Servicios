using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralForoAsignacionProveedorRepository : IGenericRepository<TPgeneralForoAsignacionProveedor>
    {
        #region Metodos Base
        TPgeneralForoAsignacionProveedor Add(PgeneralForoAsignacionProveedor entidad);
        TPgeneralForoAsignacionProveedor Update(PgeneralForoAsignacionProveedor entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPgeneralForoAsignacionProveedor> Add(IEnumerable<PgeneralForoAsignacionProveedor> listadoEntidad);
        IEnumerable<TPgeneralForoAsignacionProveedor> Update(IEnumerable<PgeneralForoAsignacionProveedor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PgeneralForoAsignacionProveedor> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
