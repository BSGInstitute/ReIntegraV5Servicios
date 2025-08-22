using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IChatDetalleIntegraArchivoRepository : IGenericRepository<TChatDetalleIntegraArchivo>
    {
        #region Metodos Base
        TChatDetalleIntegraArchivo Add(ChatDetalleIntegraArchivo entidad);
        TChatDetalleIntegraArchivo Update(ChatDetalleIntegraArchivo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TChatDetalleIntegraArchivo> Add(IEnumerable<ChatDetalleIntegraArchivo> listadoEntidad);
        IEnumerable<TChatDetalleIntegraArchivo> Update(IEnumerable<ChatDetalleIntegraArchivo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ChatDetalleIntegraArchivoDTO> ObtenerChatDetalleIntegraArchivo();
        IEnumerable<ChatDetalleIntegraArchivoComboDTO> ObtenerCombo();
    }
}