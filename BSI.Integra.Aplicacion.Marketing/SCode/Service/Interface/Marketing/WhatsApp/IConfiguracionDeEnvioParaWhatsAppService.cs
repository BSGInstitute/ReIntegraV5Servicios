using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp
{
    public interface IConfiguracionDeEnvioParaWhatsAppService
    {
        #region Metodos Base
        RespuestaGenerica Add(ConfiguracionDeEnvioParaWhatsAppCreate entidad, string usuario);
        RespuestaGenerica Update(ConfiguracionDeEnvioParaWhatsAppCreate entidad, string usuario);
        RespuestaGenerica Delete(int id, string usuario);

        RespuestaGenerica Add(List<ConfiguracionDeEnvioParaWhatsAppDTO> listadoEntidad, string usuario);
        RespuestaGenerica Update(List<ConfiguracionDeEnvioParaWhatsAppDTO> listadoEntidad, string usuario);
        RespuestaGenerica Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
