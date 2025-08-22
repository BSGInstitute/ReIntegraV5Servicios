using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoLandingPageRepository : IGenericRepository<TTipoLandingPage>
    {
        #region Metodos Base
        TTipoLandingPage Add(TipoLandingPage entidad);
        TTipoLandingPage Update(TipoLandingPage entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoLandingPage> Add(IEnumerable<TipoLandingPage> listadoEntidad);
        IEnumerable<TTipoLandingPage> Update(IEnumerable<TipoLandingPage> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboTipoLandingPage> ObtenerCombo();
        IEnumerable<TipoLandingPage> ObtenerTipoLandingPage();

    }
}
