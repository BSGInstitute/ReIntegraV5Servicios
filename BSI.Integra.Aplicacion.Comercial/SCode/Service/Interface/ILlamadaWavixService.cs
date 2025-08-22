using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ILlamadaWavixService
    {
        #region Metodos Base
        LlamadaWavixWebhook Add(LlamadaWavixWebHookDTO data, string Usuario);
        LlamadaWavixWebhook Update(LlamadaWavixWebHookDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<LlamadaWavixWebhook> Add(List<LlamadaWavixWebhook> listadoEntidad);
        List<LlamadaWavixWebhook> Update(List<LlamadaWavixWebhook> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool? GuardarLlamadaWebhook(LlamadaWavixWebHookDTO llamada);
    }
}
