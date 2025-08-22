using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoRegistroArchivoStorageDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IRegistroArchivoStorageService
    {
        #region Metodos Base

        RegistroArchivoStorage Update(RegistroArchivoStorage entidad);
        bool Delete(int id, string usuario);
        RegistroArchivoStorage Add(RegistroArchivoStorage entidad);

        List<RegistroArchivoStorage> Add(List<RegistroArchivoStorage> listadoEntidad);
        List<RegistroArchivoStorage> Update(List<RegistroArchivoStorage> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RegistroArchivoStorageComboDTO> ObtenerCombo();
        IEnumerable<RegistroArchivoStorageDTO> ObtenerRegistroArchivoStorage();

        IEnumerable<RegistroArchivoStoragePermisosDTO> ObtenerTodoPorPermisosRegistroArchivoStorage(RegistroArchivoMostrarFiltroDTO registroArchivoMostrarFiltro);
        IEnumerable<RegistroArchivoMostrarFiltroDTO> ObtenerMostrarFiltroArchivoStorage();

        IEnumerable<ComboContenedorArchivoDTO> ObtenerContenedores(int IdPersonal);
        IEnumerable<ComboSubcontenedorArchivoDTO> ObtenerSubcontRegistroArchivoStorageenedores(int IdPersonal);
        IEnumerable<ComboTipoSubcontenedorArchivoDTO> ObtenerTipoSubcontenedores(int IdPersonal);
        RegistroArchivoStorageRepositorio InsertarNuevoRegistro(RegistroArchivoStorageRepositorio entidad);


        string SubirArchivo(byte[] archivo, string mimeType, string nombreArchivo, string rutaCompleta, string rutaBlob);
        string RegistroArchivoStorageSubirArchivo(RegistroArchivoStorageSubirArchivoDTO registroSubirArchivo);


        IEnumerable<RegistroArchivoObtenerUrlComboDTO> ObtenerComboFirma();
        IEnumerable<RegistroArchivoObtenerUrlComboDTO> ObtenerRegistroArchivoStoragePorIdUrlSubContenedor(int idUrlSubContenedor);


        //IEnumerable<TipoInteraccionFiltroDTO> TiposInteraccionPorProcedenciaFiltro();
        //IEnumerable<TipoRegistroArchivoStorageFiltroDTO> TipoRegistroArchivoStorageFiltroTodo();

        //IEnumerable<RegistroArchivoStorageFiltroDTO> ObtenerCateoriaOrigenFiltro();




    }
}
