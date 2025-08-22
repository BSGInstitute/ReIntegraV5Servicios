using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoRegistroArchivoStorageDTO;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRegistroArchivoStorageRepository : IGenericRepository<TRegistroArchivoStorage>
    {
        #region Metodos Base
        TRegistroArchivoStorage Add(RegistroArchivoStorage entidad);
        TRegistroArchivoStorage Update(RegistroArchivoStorage entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRegistroArchivoStorage> Add(IEnumerable<RegistroArchivoStorage> listadoEntidad);
        IEnumerable<TRegistroArchivoStorage> Update(IEnumerable<RegistroArchivoStorage> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RegistroArchivoStorageComboDTO> ObtenerCombo();
        IEnumerable<RegistroArchivoStorageDTO> ObtenerRegistroArchivoStorage();

        IEnumerable<RegistroArchivoStoragePermisosDTO> ObtenerTodoPorPermisosRegistroArchivoStorage(RegistroArchivoMostrarFiltroDTO registroArchivoMostrarFiltro);
        IEnumerable<RegistroArchivoMostrarFiltroDTO> ObtenerMostrarFiltroArchivoStorage();
        IEnumerable<ComboContenedorArchivoDTO> ObtenerContenedores(int IdPersonal);
        IEnumerable<ComboSubcontenedorArchivoDTO> ObtenerSubcontenedores(int IdPersonal);
        IEnumerable<ComboTipoSubcontenedorArchivoDTO> ObtenerTipoSubcontenedores(int IdPersonal);



        //TRegistroArchivoStorage InsertarNuevoRegistro(RegistroArchivoStorageRepositorio entidad);




    }
}
