using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IScrapingAerolineaConfiguracionRepository : IGenericRepository<TScrapingAerolineaConfiguracion>
    {
        #region Metodos Base
        TScrapingAerolineaConfiguracion Add(ScrapingAerolineaConfiguracion entidad);
        TScrapingAerolineaConfiguracion Update(ScrapingAerolineaConfiguracion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TScrapingAerolineaConfiguracion> Add(IEnumerable<ScrapingAerolineaConfiguracion> listadoEntidad);
        IEnumerable<TScrapingAerolineaConfiguracion> Update(IEnumerable<ScrapingAerolineaConfiguracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ScrapingAerolineaConfiguracion? ObtenerPorId(int id);
    }
}
