using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp
{
    public interface ICampaniaWhatsAppFiltradoService
    {
        Task<RespuestaGenerica> FiltradoDeDatosParaWhatsapp(CampaniaMailingWhatsAppFiltradoDTO.CampaniaWhatsAppFiltrado datosFiltro);
        RespuestaGenerica FiltradoDeDatosParaWhatsappObtenerData(int IdcampaniaGeneral, int Prioridad);
        RespuestaGenerica EliminacionLogicaDeFiltroWhatsApp(int IdcampaniaGeneral, string usuario);
        bool SendMail(string usuario, int IdcampaniaGeneral);
    }
}
