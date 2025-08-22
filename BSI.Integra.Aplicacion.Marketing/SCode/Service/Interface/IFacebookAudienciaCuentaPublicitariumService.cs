using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFacebookAudienciaCuentaPublicitariumService
    {
        #region Metodos Base
        FacebookAudienciaCuentaPublicitarium Add(FacebookAudienciaCuentaPublicitarium entidad);
        FacebookAudienciaCuentaPublicitarium Update(FacebookAudienciaCuentaPublicitarium entidad);
        bool Delete(int id, string usuario);

        List<FacebookAudienciaCuentaPublicitarium> Add(List<FacebookAudienciaCuentaPublicitarium> listadoEntidad);
        List<FacebookAudienciaCuentaPublicitarium> Update(List<FacebookAudienciaCuentaPublicitarium> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
         }
}
