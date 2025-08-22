using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOrigenIngresoCajaRepository : IGenericRepository<TOrigenIngresoCaja>
    {
        #region Metodos Base
        TOrigenIngresoCaja Add(OrigenIngresoCaja entidad);
        TOrigenIngresoCaja Update(OrigenIngresoCaja entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOrigenIngresoCaja> Add(IEnumerable<OrigenIngresoCaja> listadoEntidad);
        IEnumerable<TOrigenIngresoCaja> Update(IEnumerable<OrigenIngresoCaja> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OrigenIngresoCajaComboDTO> ObtenerCombo();
    }
}
