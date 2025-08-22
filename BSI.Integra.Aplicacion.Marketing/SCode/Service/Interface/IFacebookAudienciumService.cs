using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFacebookAudienciumService
    {
        #region Metodos Base
        FacebookAudiencium Add(FacebookAudiencium entidad);
        FacebookAudiencium Update(FacebookAudiencium entidad);
        bool Delete(int id, string usuario);

        List<FacebookAudiencium> Add(List<FacebookAudiencium> listadoEntidad);
        List<FacebookAudiencium> Update(List<FacebookAudiencium> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
       
    }
}
