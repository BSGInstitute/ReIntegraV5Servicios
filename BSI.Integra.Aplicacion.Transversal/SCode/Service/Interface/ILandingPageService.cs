using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ILandingPageService
    {

        #region Metodos Base
        LandingPage Add(LandingPageEnvio entidad);
        LandingPage Update(LandingPageEnvio entidad);
        bool Delete(int id, string usuario);

        List<LandingPage> Add(List<LandingPage> listadoEntidad);
        List<LandingPage> Update(List<LandingPage> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<LandingPageCombo> ObtenerCombo();
        IEnumerable<LandingPage> ObtenerLandingPage();


        IEnumerable<LandingPageC> ObtenerLandingPageC();
    }
}
