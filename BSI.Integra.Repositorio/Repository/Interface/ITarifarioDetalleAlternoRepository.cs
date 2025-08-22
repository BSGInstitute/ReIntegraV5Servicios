using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITarifarioDetalleAlternoRepository : IGenericRepository<TTarifarioDetalleAlterno>
    {
        #region Metodos Base
        TTarifarioDetalleAlterno Add(TarifarioDetalleAlterno entidad);
        TTarifarioDetalleAlterno Update(TarifarioDetalleAlterno entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTarifarioDetalleAlterno> Add(IEnumerable<TarifarioDetalleAlterno> listadoEntidad);
        IEnumerable<TTarifarioDetalleAlterno> Update(IEnumerable<TarifarioDetalleAlterno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TarifarioDetalleAlterno ObtenerPorId(int id);
    }
}
