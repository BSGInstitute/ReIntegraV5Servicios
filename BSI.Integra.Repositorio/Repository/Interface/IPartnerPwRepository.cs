using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPartnerPwRepository : IGenericRepository<TPartnerPw>
    {
        #region Metodos Base
        TPartnerPw Add(PartnerPw entidad);
        TPartnerPw Update(PartnerPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPartnerPw> Add(IEnumerable<PartnerPw> listadoEntidad);
        IEnumerable<TPartnerPw> Update(IEnumerable<PartnerPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PartnerPwDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        PartnerPw? ObtenerPorId(int idPartner);
        public int ObtenerPartnerAnterior(int idPartner);
    }
}