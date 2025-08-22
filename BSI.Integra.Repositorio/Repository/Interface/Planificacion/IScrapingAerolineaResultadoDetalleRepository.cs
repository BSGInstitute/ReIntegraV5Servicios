using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IScrapingAerolineaResultadoDetalleRepository : IGenericRepository<TScrapingAerolineaResultadoDetalle>
    {
        #region Metodos Base
        TScrapingAerolineaResultadoDetalle Add(ScrapingAerolineaResultadoDetalle entidad);
        TScrapingAerolineaResultadoDetalle Update(ScrapingAerolineaResultadoDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TScrapingAerolineaResultadoDetalle> Add(IEnumerable<ScrapingAerolineaResultadoDetalle> listadoEntidad);
        IEnumerable<TScrapingAerolineaResultadoDetalle> Update(IEnumerable<ScrapingAerolineaResultadoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ScrapingAerolineaResultadoDetalle? ObtenerPorId(int id);
    }
}
