using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPartnerBeneficioPwRepository : IGenericRepository<TPartnerBeneficioPw>
    {
        #region Metodos Base
        TPartnerBeneficioPw Add(PartnerBeneficioPw entidad);
        TPartnerBeneficioPw Update(PartnerBeneficioPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPartnerBeneficioPw> Add(IEnumerable<PartnerBeneficioPw> listadoEntidad);
        IEnumerable<TPartnerBeneficioPw> Update(IEnumerable<PartnerBeneficioPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PartnerBeneficioPw? ObtenerPorId(int id);
        IEnumerable<PartnerBeneficioPw> ObtenerPorIdPartner(int idPartner);
    }
}
