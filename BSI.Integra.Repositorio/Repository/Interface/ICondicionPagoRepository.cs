using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICondicionPagoRepository : IGenericRepository<TCondicionPago>
    {
        #region Metodos Base
        TCondicionPago Add(CondicionPago entidad);
        TCondicionPago Update(CondicionPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCondicionPago> Add(IEnumerable<CondicionPago> listadoEntidad);
        IEnumerable<TCondicionPago> Update(IEnumerable<CondicionPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
