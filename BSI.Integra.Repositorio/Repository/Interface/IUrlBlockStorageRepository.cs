using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoUrlBlockStorageDTO;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IUrlBlockStorageRepository : IGenericRepository<TUrlBlockStorage>
    {
        #region Metodos Base
        TUrlBlockStorage Add(UrlBlockStorage entidad);
        TUrlBlockStorage Update(UrlBlockStorage entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TUrlBlockStorage> Add(IEnumerable<UrlBlockStorage> listadoEntidad);
        IEnumerable<TUrlBlockStorage> Update(IEnumerable<UrlBlockStorage> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


        IEnumerable<ContenedorArchivoCompletoDTO> ObtenerInformacionPorIdUrlSubcontenedor(int IdUrlSubContenedor);



        //IEnumerable<TipoInteraccionFiltroDTO> TiposInteraccionPorProcedenciaFiltro();
        //IEnumerable<TipoUrlBlockStorageFiltroDTO> TipoUrlBlockStorageFiltroTodo();

        // IEnumerable<UrlBlockStorageFiltroDTO> ObtenerCateoriaOrigenFiltro();
    }
}
