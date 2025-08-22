using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IInteraccionChatIntegraService
    {
        #region Metodos Base
        InteraccionChatIntegra Add(InteraccionChatIntegra entidad);
        InteraccionChatIntegra Update(InteraccionChatIntegra entidad);
        bool Delete(int id, string usuario);

        List<InteraccionChatIntegra> Add(List<InteraccionChatIntegra> listadoEntidad);
        List<InteraccionChatIntegra> Update(List<InteraccionChatIntegra> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion 
        IEnumerable<ReporteChatLogDTO> GenerarReporteChatLog(ChatReporteDTO chat); 
        IEnumerable<ReporteChatIntegraDTO> GenerarReporteChat(ChatReporteDTO chat);
        public string ObtenerNombreMes(int mes);
        public int ObtenerNumeroSemana(DateTime date);
        InteraccionChatIntegra ObtenerPorId(int id);

        // IEnumerable<InteraccionChatIntegraDTO> ObtenerInteraccionChatIntegra();

    }
}
