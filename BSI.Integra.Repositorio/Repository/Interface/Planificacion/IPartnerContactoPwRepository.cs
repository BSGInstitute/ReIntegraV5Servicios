using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPartnerContactoPwRepository : IGenericRepository<TPartnerContactoPw>
    {
        #region Metodos Base
        TPartnerContactoPw Add(PartnerContactoPw entidad);
        TPartnerContactoPw Update(PartnerContactoPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPartnerContactoPw> Add(IEnumerable<PartnerContactoPw> listadoEntidad);
        IEnumerable<TPartnerContactoPw> Update(IEnumerable<PartnerContactoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PartnerContactoPw? ObtenerPorId(int id);
        IEnumerable<PartnerContactoPw> ObtenerPorIdPartner(int idPartner);
    }
}
