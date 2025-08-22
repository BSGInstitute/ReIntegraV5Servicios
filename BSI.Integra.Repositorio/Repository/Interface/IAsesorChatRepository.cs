using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsesorChatRepository : IGenericRepository<TAsesorChat>
    {
        #region Metodos Base
        TAsesorChat Add(AsesorChat entidad);
        TAsesorChat Update(AsesorChat entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAsesorChat> Add(IEnumerable<AsesorChat> listadoEntidad);
        IEnumerable<TAsesorChat> Update(IEnumerable<AsesorChat> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PersonalAlumnoDTO ObtenerOportunidadPorNumero(int idCentroCosto, string numero);
        List<AddresseeDTO> ListaPersonaNotificacion();
        PersonalAlumnoDTO BuscarAlumnoPorWebHook(string Celular);
        ChatAsignadoNoAsignadoCompuestoDTO ObtenerChatAsignadosNoAsignados(FiltroCompuestroGrillaDTO paginador);
        ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores(FiltroCompuestroGrillaDTO paginador);
        List<ChatAsignadoNoAsignadoDTO> ObtenerTodoChatAsignados();
        ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores2();
    }
}