using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAdwordsApiPalabraClaveRepository : IGenericRepository<TAdwordsApiPalabraClave>
    {
        #region Metodos Base
        TAdwordsApiPalabraClave Add(AdwordsApiPalabraClave entidad);
        TAdwordsApiPalabraClave Update(AdwordsApiPalabraClave entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAdwordsApiPalabraClave> Add(IEnumerable<AdwordsApiPalabraClave> listadoEntidad);
        IEnumerable<TAdwordsApiPalabraClave> Update(IEnumerable<AdwordsApiPalabraClave> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        int InsertarPalabraClaveYretornarId(AdwordsApiPalabraClave entidad);
    }
}
