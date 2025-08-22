using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralCodigoPartnerVersionProgramaRepository : IGenericRepository<TPgeneralCodigoPartnerVersionPrograma>
    {
        #region Metodos Base
        TPgeneralCodigoPartnerVersionPrograma Add(PgeneralCodigoPartnerVersionPrograma entidad);
        TPgeneralCodigoPartnerVersionPrograma Update(PgeneralCodigoPartnerVersionPrograma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralCodigoPartnerVersionPrograma> Add(IEnumerable<PgeneralCodigoPartnerVersionPrograma> listadoEntidad);
        IEnumerable<TPgeneralCodigoPartnerVersionPrograma> Update(IEnumerable<PgeneralCodigoPartnerVersionPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralCodigoPartnerVersionPrograma? ObtenerPorIdVersionProgramaIdPgeneralCodigoPartner(int idVersionPrograma, int idPgeneralCodigoPartner);
    }
}
