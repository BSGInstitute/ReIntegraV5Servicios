using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoUrlBlockStorageDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IUrlBlockStorageService
    {
        #region Metodos Base

        UrlBlockStorage Update(UrlBlockStorage entidad);
        bool Delete(int id, string usuario);
        UrlBlockStorage Add(UrlBlockStorage entidad);

        List<UrlBlockStorage> Add(List<UrlBlockStorage> listadoEntidad);
        List<UrlBlockStorage> Update(List<UrlBlockStorage> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ContenedorArchivoCompletoDTO> ObtenerInformacionPorIdUrlSubcontenedor(int IdUrlSubContenedor);



        //IEnumerable<TipoInteraccionFiltroDTO> TiposInteraccionPorProcedenciaFiltro();
        //IEnumerable<TipoUrlBlockStorageFiltroDTO> TipoUrlBlockStorageFiltroTodo();

        //IEnumerable<UrlBlockStorageFiltroDTO> ObtenerCateoriaOrigenFiltro();




    }
}
