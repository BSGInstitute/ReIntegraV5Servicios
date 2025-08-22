using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPgeneralAsubPgeneralVersionProgramaRepository : IGenericRepository<TPgeneralAsubPgeneralVersionPrograma>
    {
        #region Metodos Base
        TPgeneralAsubPgeneralVersionPrograma Add(PgeneralAsubPgeneralVersionPrograma entidad);
        TPgeneralAsubPgeneralVersionPrograma Update(PgeneralAsubPgeneralVersionPrograma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPgeneralAsubPgeneralVersionPrograma> Add(IEnumerable<PgeneralAsubPgeneralVersionPrograma> listadoEntidad);
        IEnumerable<TPgeneralAsubPgeneralVersionPrograma> Update(IEnumerable<PgeneralAsubPgeneralVersionPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralAsubPgeneralVersionPrograma? ObtenerPorId(int id);
        IEnumerable<PgeneralAsubPgeneralVersionPrograma> ObtenerPorIds(List<int> id);
        IEnumerable<PgeneralAsubPgeneralVersionPrograma> ObtenerPoridPgeneralASubPgeneral(int idPgeneralASubPgeneral);
    }
}
