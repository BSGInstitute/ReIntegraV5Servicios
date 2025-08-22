using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPgeneralParametroSeoPwRepository : IGenericRepository<TPgeneralParametroSeoPw>
    {
        #region Metodos Base
        TPgeneralParametroSeoPw Add(PgeneralParametroSeoPw entidad);
        TPgeneralParametroSeoPw Update(PgeneralParametroSeoPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralParametroSeoPw> Add(IEnumerable<PgeneralParametroSeoPw> listadoEntidad);
        IEnumerable<TPgeneralParametroSeoPw> Update(IEnumerable<PgeneralParametroSeoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion 
        IEnumerable<PgeneralParametroSeoPwDTO> ObtenerParametrosSEOPorIdPGeneral(int idPGeneral);
        IEnumerable<PgeneralParametroSeoPwDTO> ObtenerPgeneralParametroSeoPorIdPGeneral(int idPGeneral);
        IEnumerable<PgeneralParametroSeoPw> ObtenerPorIdPGeneral(int idPGeneral);
        PgeneralParametroSeoPw? ObtenerPorIdPGeneralIdParametroSeo(int idPGeneral, int idParametroSeo);
        PgeneralParametroSeoPw? ObtenerPorId(int id);
    }
}