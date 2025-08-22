using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICondicionTipoPagoRepository : IGenericRepository<TCondicionTipoPago>
    {
        #region Metodos Base
        TCondicionTipoPago Add(CondicionTipoPago entidad);
        TCondicionTipoPago Update(CondicionTipoPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCondicionTipoPago> Add(IEnumerable<CondicionTipoPago> listadoEntidad);
        IEnumerable<TCondicionTipoPago> Update(IEnumerable<CondicionTipoPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
