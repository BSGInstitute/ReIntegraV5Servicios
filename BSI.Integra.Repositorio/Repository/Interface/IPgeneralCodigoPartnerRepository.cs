
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralCodigoPartnerRepository : IGenericRepository<TPgeneralCodigoPartner>
    {
        #region Metodos Base
        TPgeneralCodigoPartner Add(PGeneralCodigoPartner entidad);
        TPgeneralCodigoPartner Update(PGeneralCodigoPartner entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralCodigoPartner> Add(IEnumerable<PGeneralCodigoPartner> listadoEntidad);
        IEnumerable<TPgeneralCodigoPartner> Update(IEnumerable<PGeneralCodigoPartner> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PgeneralCodigoPartnerAlternoDTO> ObtenerPgeneralCodigoPartnerPorIdPGeneral(int idPGeneral);
        IEnumerable<PGeneralCodigoPartner> ObtenerPorIdPGeneral(int idPGeneral);
        PGeneralCodigoPartner? ObtenerPorId(int id);
    }
}
