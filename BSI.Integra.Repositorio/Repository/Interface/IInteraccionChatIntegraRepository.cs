using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IInteraccionChatIntegraRepository : IGenericRepository<TInteraccionChatIntegra>
    {
        #region Metodos Base
        TInteraccionChatIntegra Add(InteraccionChatIntegra entidad);
        TInteraccionChatIntegra Update(InteraccionChatIntegra entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TInteraccionChatIntegra> Add(IEnumerable<InteraccionChatIntegra> listadoEntidad);
        IEnumerable<TInteraccionChatIntegra> Update(IEnumerable<InteraccionChatIntegra> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ReporteChatLogDTO> GenerarReporteChatLog(ChatReporteDTO chat);
        IEnumerable<ReporteChatIntegraDTO> GenerarReporteChat(ChatReporteDTO chat);
        public string ObtenerNombreMes(int mes);
        public int ObtenerNumeroSemana(DateTime date);
        InteraccionChatIntegra ObtenerPorId(int id);
    }
}
