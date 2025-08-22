using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAdwordsApiPalabraClaveService
    {
        #region Metodos Base
        AdwordsApiPalabraClave Add(AdwordsApiPalabraClave entidad);
        AdwordsApiPalabraClave Update(AdwordsApiPalabraClave entidad);
        bool Delete(int id, string usuario);

        List<AdwordsApiPalabraClave> Add(List<AdwordsApiPalabraClave> listadoEntidad);
        List<AdwordsApiPalabraClave> Update(List<AdwordsApiPalabraClave> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
