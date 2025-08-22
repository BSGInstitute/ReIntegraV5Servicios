using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralProyectoAplicacionRepository : IGenericRepository<TPgeneralProyectoAplicacion>
    {
        #region Metodos Base
        TPgeneralProyectoAplicacion Add(PgeneralProyectoAplicacion entidad);
        TPgeneralProyectoAplicacion Update(PgeneralProyectoAplicacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralProyectoAplicacion> Add(IEnumerable<PgeneralProyectoAplicacion> listadoEntidad);
        IEnumerable<TPgeneralProyectoAplicacion> Update(IEnumerable<PgeneralProyectoAplicacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PgeneralProyectoAplicacionAlternoDTO> ObtenerPgeneralProyectoAplicacionPorIdPGeneral(int idPGeneral);
        IEnumerable<PgeneralProyectoAplicacion> ObtenerPorIdPGeneral(int idPGeneral);
        PgeneralProyectoAplicacion? ObtenerPorId(int id);
    }
}
