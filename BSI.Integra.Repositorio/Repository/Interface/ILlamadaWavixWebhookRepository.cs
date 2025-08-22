using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILlamadaWavixWebhookRepository : IGenericRepository<TLlamadaWavixWebhook>
    {
        #region Metodos Base
        TLlamadaWavixWebhook Add(LlamadaWavixWebhook entidad);
        TLlamadaWavixWebhook Update(LlamadaWavixWebhook entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TLlamadaWavixWebhook> Add(IEnumerable<LlamadaWavixWebhook> listadoEntidad);
        IEnumerable<TLlamadaWavixWebhook> Update(IEnumerable<LlamadaWavixWebhook> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        bool? GuardarLlamadaWebhook(LlamadaWavixWebHookDTO llamada);
        bool? GuardarLlamadaEntranteWebhook(LlamadaWavixEntranteDTO llamada);
    }
}
