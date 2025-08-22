using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoLandingPageService
    {

        #region Metodos Base
        TipoLandingPage Add(TipoLandingPageEnvio entidad);
        TipoLandingPage Update(TipoLandingPageEnvio entidad);
        bool Delete(int id, string usuario);

        List<TipoLandingPage> Add(List<TipoLandingPage> listadoEntidad);
        List<TipoLandingPage> Update(List<TipoLandingPage> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboTipoLandingPage> ObtenerCombo();
        IEnumerable<TipoLandingPage> ObtenerTipoLandingPage();

    }
}
