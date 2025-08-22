using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionAutomaticaService
    {
        #region Metodos Base
        AsignacionAutomatica Add(AsignacionAutomatica entidad);
        AsignacionAutomatica Update(AsignacionAutomatica entidad);
        bool Delete(int id, string usuario);

        List<AsignacionAutomatica> Add(List<AsignacionAutomatica> listadoEntidad);
        List<AsignacionAutomatica> Update(List<AsignacionAutomatica> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public AsignacionAutomatica AsignacionAutomatica { get; set; }
        public int IdClasificacionPersona { get; set; }
        public List<ReporteLandingPagePortalFacebookDTO> ObtenerReporteLandingPagePortalFacebook(FiltroLandingPagePortalFacebookDTO filtros);

        public AsignacionAutomatica ObtenerPorIdFaseOportunidadPortal(string idFaseOportunidadPortal);
        public AsignacionAutomatica ObtenerPorId(int idAsignacionAutomatica);
        List<AsignacionAutomaticaError> Validar();
        public bool AplicarConfiguracion(List<AsignacionAutomaticaConfiguracionDTO> inclusion, List<AsignacionAutomaticaConfiguracionDTO> exclusion);
        public AsignacionAutomatica ValidarRegistroFormularioAsignacionAutomaticaTemp(int idAsignacionAutomaticaTemp, Dictionary<int, string> listaPaises, Dictionary<string, OrigenesCategoriaOrigenDTO> listaOrigenes);
    }
}
