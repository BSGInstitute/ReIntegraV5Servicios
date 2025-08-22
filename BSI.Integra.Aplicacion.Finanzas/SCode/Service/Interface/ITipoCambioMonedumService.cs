using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoCambioMonedumService
    {
        #region Metodos Base
        TipoCambioMonedum Add(TipoCambioMonedum entidad);
        TipoCambioMonedum Update(TipoCambioMonedum entidad);
        bool Delete(int id, string usuario);

        List<TipoCambioMonedum> Add(List<TipoCambioMonedum> listadoEntidad);
        List<TipoCambioMonedum> Update(List<TipoCambioMonedum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoCambioMonedumDTO> ObtenerTipoCambioMonedum();
        object InsertarGeneral(FiltroTipoCambioMonedaDTO tipoCambioMonedaDTO);
        bool EliminarGeneral(int Id, string Usuario);
        object ActualizarGeneral(FiltroTipoCambioMonedaDTO TipoCambioMonedaDTO);

    }
}
