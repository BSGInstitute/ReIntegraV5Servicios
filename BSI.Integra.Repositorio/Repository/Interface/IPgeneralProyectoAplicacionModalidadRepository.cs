using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralProyectoAplicacionModalidadRepository : IGenericRepository<TPgeneralProyectoAplicacionModalidad>
    {
        #region Metodos Base
        TPgeneralProyectoAplicacionModalidad Add(PgeneralProyectoAplicacionModalidad entidad);
        TPgeneralProyectoAplicacionModalidad Update(PgeneralProyectoAplicacionModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralProyectoAplicacionModalidad> Add(IEnumerable<PgeneralProyectoAplicacionModalidad> listadoEntidad);
        IEnumerable<TPgeneralProyectoAplicacionModalidad> Update(IEnumerable<PgeneralProyectoAplicacionModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralProyectoAplicacionModalidad? ObtenerPorIdModalidadCursoIdPgeneralProyectoAplicacion(int idModalidadCurso, int idPgeneralProyectoAplicacion);
    }
}
