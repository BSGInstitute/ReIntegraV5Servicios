using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralProyectoAplicacionProveedorRepository : IGenericRepository<TPgeneralProyectoAplicacionProveedor>
    {
        #region Metodos Base
        TPgeneralProyectoAplicacionProveedor Add(PgeneralProyectoAplicacionProveedor entidad);
        TPgeneralProyectoAplicacionProveedor Update(PgeneralProyectoAplicacionProveedor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralProyectoAplicacionProveedor> Add(IEnumerable<PgeneralProyectoAplicacionProveedor> listadoEntidad);
        IEnumerable<TPgeneralProyectoAplicacionProveedor> Update(IEnumerable<PgeneralProyectoAplicacionProveedor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralProyectoAplicacionProveedor? ObtenerPorIdProveedorIdPgeneralProyectoAplicacion(int idProveedor, int idPgeneralProyectoAplicacion);
    }
}
