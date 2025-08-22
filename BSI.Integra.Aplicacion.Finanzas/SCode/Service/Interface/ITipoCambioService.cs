using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoCambioService
    {
        #region Metodos Base
        TipoCambio Add(TipoCambio entidad);
        TipoCambio Update(TipoCambio entidad);
        bool Delete(int id, string usuario);

        List<TipoCambio> Add(List<TipoCambio> listadoEntidad);
        List<TipoCambio> Update(List<TipoCambio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoCambioObtenerDTO> Obtener();
        TipoCambioFechaDTO ObtenerTipoCambio(int tipoCambio);
        IEnumerable<TipoCambioReporteDTO> ObtenerTipoCambioFiltro(TipoCambioFiltroDTO filtro);
    }
}
