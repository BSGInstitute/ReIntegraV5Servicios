using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.IntegracionConIntegraDB.UpdateCampania;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueCampaniasEnvioApiDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueSendersDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue
{
    public interface ISendingblueService
    {
        RespuestaGenerica SendinblueCampania(GetEmailCampaignsDTO emailCampaigns);
        RespuestaGenerica MandarCampaniaPorId(long campaignId);
        RespuestaGenerica ConseguirContactos(long limit, long offset);
        Task<RespuestaGenerica> EliminarContactos(string email);
        Task<RespuestaGenerica> ActualizarContactos(SendingContactosDTO contacto);
        RespuestaGenerica CrearContactos(SendingContactosDTO contacto);
        RespuestaGenerica ListarFolders(long limit, long offset);
        RespuestaGenerica CrearFolder(string myFolderName);
        Task<RespuestaGenerica> ActualizarFolder(long idFolder, string name);
        Task<RespuestaGenerica> EliminarFolder(long folderId);
        RespuestaGenerica Listas(long limit, long offset);
        RespuestaGenerica CrearLista(int id, string myListName);
        RespuestaGenerica UpdateCantidadDeContactosLista(int idLista);
        Task<RespuestaGenerica> UpdateLista(ListUpdate list);
        Task<RespuestaGenerica> DeleteLista(int idList);
        RespuestaGenerica ListarContactos(long limit, long offset);
        RespuestaGenerica DetalleDeLista(long idlist);
        RespuestaGenerica AgregarContactosALista(CrearContactosListaDto nuevoCorreo);
        RespuestaGenerica EliminarContactosDeLista(CrearContactosListaDto CorreosEliminados);
        RespuestaGenerica ObtenerContactosPorLista(long idDelist, long limit, long offset);
        RespuestaGenerica ObtenerTodosLosatributos();
        RespuestaGenerica AgregarAtributos(string categoria, string nombre, List<CreateAttributeEnumeration> enumerations, string tipo);
        RespuestaGenerica CrearCampaignEmail(CrearCampaniaSendinblue campania);
        RespuestaGenerica AgregarCampaniaABTest(CrearCampaniaSendinblueABTest aBTest);
        RespuestaGenerica ActualizarCampania(UpdateCampaniaDTO aBTest, int idcampania);
        SendingblueObtenerSenders ObtenerSenders();
        SendingblueSendersRespuesta AgregarSender(SengindblueSenders sendersDTO);
        Task<RespuestaGenerica> ActualizarSender(SengindblueSenders sendersDTO, int idSender);
        Task<RespuestaGenerica> EliminarSender(int idSender);
        RespuestaGenerica ObtenerTemplate(int limit, int offset, string sort, bool estadoDeTemplate);
        RespuestaGenerica ObtenerCampaniaPorId(int id);
        Task<RespuestaGenerica> ActualziarCampania(int idCampania, string tipo);
        RespuestaGenerica CrearCampaignEmailHtmlContent(CrearCampaignEmailHtmlContentDTO campania);
        RespuestaGenerica AgregarCampaniaABTestHtmlContent(CrearCampaniaSendinblueABTestHtmlContent aBTest);
    }
}
