using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPgeneralTagsPwRepository : IGenericRepository<TPgeneralTagsPw>
    {
        #region Metodos Base
        TPgeneralTagsPw Add(PgeneralTagsPw entidad);
        TPgeneralTagsPw Update(PgeneralTagsPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralTagsPw> Add(IEnumerable<PgeneralTagsPw> listadoEntidad);
        IEnumerable<TPgeneralTagsPw> Update(IEnumerable<PgeneralTagsPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<int> ObtenerIdsTagPorPGeneral(int idPgeneral);
        PgeneralTagsPw? ObtenerPorIdPGeneralyIdTagPw(int idPGeneral, int idTag);
        IEnumerable<PgeneralTagsPw> ObtenerPorIdTag(int idTag);
    }
}
