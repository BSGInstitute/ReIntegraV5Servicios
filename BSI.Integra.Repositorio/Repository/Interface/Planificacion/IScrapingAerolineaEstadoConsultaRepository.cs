using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IScrapingAerolineaEstadoConsultaRepository : IGenericRepository<TScrapingAerolineaEstadoConsultum>
    {
        #region Metodos Base
        TScrapingAerolineaEstadoConsultum Add(ScrapingAerolineaEstadoConsulta entidad);
        TScrapingAerolineaEstadoConsultum Update(ScrapingAerolineaEstadoConsulta entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TScrapingAerolineaEstadoConsultum> Add(IEnumerable<ScrapingAerolineaEstadoConsulta> listadoEntidad);
        IEnumerable<TScrapingAerolineaEstadoConsultum> Update(IEnumerable<ScrapingAerolineaEstadoConsulta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ScrapingAerolineaEstadoConsulta? ObtenerPorId(int id);
    }
}
