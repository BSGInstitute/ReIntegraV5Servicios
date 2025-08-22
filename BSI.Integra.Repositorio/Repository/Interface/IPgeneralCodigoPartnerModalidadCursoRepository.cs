using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralCodigoPartnerModalidadCursoRepository : IGenericRepository<TPgeneralCodigoPartnerModalidadCurso>
    {
        #region Metodos Base
        TPgeneralCodigoPartnerModalidadCurso Add(PgeneralCodigoPartnerModalidadCurso entidad);
        TPgeneralCodigoPartnerModalidadCurso Update(PgeneralCodigoPartnerModalidadCurso entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralCodigoPartnerModalidadCurso> Add(IEnumerable<PgeneralCodigoPartnerModalidadCurso> listadoEntidad);
        IEnumerable<TPgeneralCodigoPartnerModalidadCurso> Update(IEnumerable<PgeneralCodigoPartnerModalidadCurso> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralCodigoPartnerModalidadCurso? ObtenerPorIdModalidadCursoIdPgeneralCodigoPartner(int idModalidadCurso, int idPgeneralCodigoPartner);
    }
}
