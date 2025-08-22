using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoCambioColService
    {
        #region Metodos Base
        TipoCambioCol Add(TipoCambioCol entidad);
        TipoCambioCol Update(TipoCambioCol entidad);
        bool Delete(int id, string usuario);

        List<TipoCambioCol> Add(List<TipoCambioCol> listadoEntidad);
        List<TipoCambioCol> Update(List<TipoCambioCol> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        double ObtenerPesosDolaresUltimoTipoCambioColombia();
        object Obtener();
        TipoCambioColombiaDTO ObtenerPesosDolaresTipoCambioColombia(string fecha);
    }
}
