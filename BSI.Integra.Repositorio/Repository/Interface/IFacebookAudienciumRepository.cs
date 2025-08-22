using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFacebookAudienciumRepository : IGenericRepository<TFacebookAudiencium>
    {
        #region Metodos Base
        TFacebookAudiencium Add(FacebookAudiencium entidad);
        TFacebookAudiencium Update(FacebookAudiencium entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFacebookAudiencium> Add(IEnumerable<FacebookAudiencium> listadoEntidad);
        IEnumerable<TFacebookAudiencium> Update(IEnumerable<FacebookAudiencium> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
      

    }
}