using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILandingPageRepository : IGenericRepository<TLandingPage>
    {
        #region Metodos Base
        TLandingPage Add(LandingPage entidad);
        TLandingPage Update(LandingPage entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TLandingPage> Add(IEnumerable<LandingPage> listadoEntidad);
        IEnumerable<TLandingPage> Update(IEnumerable<LandingPage> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<LandingPageCombo> ObtenerCombo();
        IEnumerable<LandingPage> ObtenerLandingPage();
        IEnumerable<LandingPageC> ObtenerLandingPageC();

    }
}
