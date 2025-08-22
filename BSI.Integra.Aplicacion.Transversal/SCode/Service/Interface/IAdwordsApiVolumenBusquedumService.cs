using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAdwordsApiVolumenBusquedumService
    {
        #region Metodos Base
        AdwordsApiVolumenBusquedum Add(AdwordsApiVolumenBusquedum entidad);
        AdwordsApiVolumenBusquedum Update(AdwordsApiVolumenBusquedum entidad);
        bool Delete(int id, string usuario);

        List<AdwordsApiVolumenBusquedum> Add(List<AdwordsApiVolumenBusquedum> listadoEntidad);
        List<AdwordsApiVolumenBusquedum> Update(List<AdwordsApiVolumenBusquedum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
