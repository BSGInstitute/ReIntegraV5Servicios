using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoImpuestoService
    {
        #region Metodos Base
        TipoImpuesto Add(TipoImpuestoRecibidoDTO entidad, string usuario);
        TipoImpuesto Update(TipoImpuestoRecibidoDTO entidad, string usuario);
        bool Delete(int id, string usuario);

        List<TipoImpuesto> Add(List<TipoImpuesto> listadoEntidad);
        List<TipoImpuesto> Update(List<TipoImpuesto> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoImpuestoDTO> ObtenerTipoImpuesto();
        IEnumerable<TipoImpuestoComboDTO> ObtenerCombo();
    }
}
