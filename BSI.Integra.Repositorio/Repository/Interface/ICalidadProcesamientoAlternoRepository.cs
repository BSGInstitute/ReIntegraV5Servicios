using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICalidadProcesamientoAlternoRepository : IGenericRepository<TCalidadProcesamientoAlterno>
    {
        #region Metodos Base
        TCalidadProcesamientoAlterno Add(CalidadProcesamientoAlterno entidad);
        TCalidadProcesamientoAlterno Update(CalidadProcesamientoAlterno entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCalidadProcesamientoAlterno> Add(IEnumerable<CalidadProcesamientoAlterno> listadoEntidad);
        IEnumerable<TCalidadProcesamientoAlterno> Update(IEnumerable<CalidadProcesamientoAlterno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
