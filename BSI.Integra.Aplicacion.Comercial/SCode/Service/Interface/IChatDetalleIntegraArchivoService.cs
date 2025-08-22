using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IChatDetalleIntegraArchivoService
    {
        #region Metodos Base
        ChatDetalleIntegraArchivo Add(ChatDetalleIntegraArchivo entidad);
        ChatDetalleIntegraArchivo Update(ChatDetalleIntegraArchivo entidad);
        bool Delete(int id, string usuario);

        List<ChatDetalleIntegraArchivo> Add(List<ChatDetalleIntegraArchivo> listadoEntidad);
        List<ChatDetalleIntegraArchivo> Update(List<ChatDetalleIntegraArchivo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ChatDetalleIntegraArchivoDTO> ObtenerChatDetalleIntegraArchivo();
        IEnumerable<ChatDetalleIntegraArchivoComboDTO> ObtenerCombo();
    }
}
