using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IChatDetalleIntegraRepository : IGenericRepository<TChatDetalleIntegra>
    {
        #region Metodos Base
        TChatDetalleIntegra Add(ChatDetalleIntegra entidad);
        TChatDetalleIntegra Update(ChatDetalleIntegra entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TChatDetalleIntegra> Add(IEnumerable<ChatDetalleIntegra> listadoEntidad);
        IEnumerable<TChatDetalleIntegra> Update(IEnumerable<ChatDetalleIntegra> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ChatDetalleIntegraDTO> ObtenerChatDetalleIntegra();
        IEnumerable<ChatDetalleIntegraComboDTO> ObtenerCombo();
        HistorialChatRecibidosDTO ObtenerHistorialChatRecibidos(int idPersonal, int idAlumno);
        List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccion(int idInteraccion);
        CursoOportunidadDTO ObtenerCursoOportunidad(int idOportunidad);
        List<ChatHistorialComercialDTO> ObtenerDetalleChatPorIdAlumno(int idAlumno,int idPGeneral);
        List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(int idAlumno);

        bool FinalizarChatAtc(int idMatriculaCabecera,string userName);
        bool FinalizarChatComercial(int idOportunidad, string userName);
        List<DatosSesionChatDTO> GetIdUltimaInteraccion(int idMatriculaCabecera);
        List<DatosSesionChatComercialDTO> GetIdUltimaInteraccionComercial(int idAlumno);
        ChatDetalleIntegra ObtenerPorIntegraChatYRemintente(int idInteraccionChatIntegra, string idRemitente);
        List<HistorialChatDetalleIntegraDTO> ObtenerHistorialChatDetalleIntegra(int idMatriculaCabecera);
        List<ChatActivoDetalleIntegraDTO> ObtenerChatsAcademicosHabilitadosCoordinadora(int IdCoordinadorAcademico, bool EsOnline);
    }
}