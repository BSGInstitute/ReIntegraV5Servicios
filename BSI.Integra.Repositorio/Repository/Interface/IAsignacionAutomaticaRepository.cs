using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsignacionAutomaticaRepository : IGenericRepository<TAsignacionAutomatica>
    {
        #region Metodos Base
        TAsignacionAutomatica Add(AsignacionAutomatica entidad);
        TAsignacionAutomatica Update(AsignacionAutomatica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAsignacionAutomatica> Add(IEnumerable<AsignacionAutomatica> listadoEntidad);
        IEnumerable<TAsignacionAutomatica> Update(IEnumerable<AsignacionAutomatica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<ReporteLandingPagePortalFacebookDTO> ObtenerReporteLandingPagePortalFacebook(FiltroLandingPagePortalFacebookDTO filtros);
        public AsignacionAutomatica ObtenerPorIdFaseOportunidadPortal(string idFaseOportunidadPortal);
        public AsignacionAutomatica ObtenerPorId(int idAsignacionAutomatica);
        IEnumerable<AsignacionAutomaticaCompuestoImportadosDTO> ObtenerRegistrosImportados(FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador);
        IEnumerable<AsignacionAutomaticaCompuestoErroneosDTO> ObtenerRegistrosErroneos(FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador);
        bool ExisteAsignacionAutomatica(int Id);
        AsignacionAutomatica ObtenerAsignacionAutomaticaPorId(int Id);
        List<ReporteLandingPagePortalDTO> ObtenerReporteLandingPagePortal(FiltroLandingPagePortalDTO filtros);
    }
}