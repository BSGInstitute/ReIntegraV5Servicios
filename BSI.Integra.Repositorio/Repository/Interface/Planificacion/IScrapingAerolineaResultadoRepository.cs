using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IScrapingAerolineaResultadoRepository : IGenericRepository<TScrapingAerolineaResultado>
    {
        #region Metodos Base
        TScrapingAerolineaResultado Add(ScrapingAerolineaResultado entidad);
        TScrapingAerolineaResultado Update(ScrapingAerolineaResultado entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TScrapingAerolineaResultado> Add(IEnumerable<ScrapingAerolineaResultado> listadoEntidad);
        IEnumerable<TScrapingAerolineaResultado> Update(IEnumerable<ScrapingAerolineaResultado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ScrapingAerolineaResultado? ObtenerPorId(int id);
    }
}
