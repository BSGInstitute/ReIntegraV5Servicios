using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITarifarioDetalleAlternoService
    {
        #region Metodos Base
        TarifarioDetalleAlterno Add(TarifarioDetalleAlterno entidad);
        TarifarioDetalleAlterno Update(TarifarioDetalleAlterno entidad);
        bool Delete(int id, string usuario);

        List<TarifarioDetalleAlterno> Add(List<TarifarioDetalleAlterno> listadoEntidad);
        List<TarifarioDetalleAlterno> Update(List<TarifarioDetalleAlterno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        TarifarioDetalleAlterno ObtenerPorId(int id);
    }
}
