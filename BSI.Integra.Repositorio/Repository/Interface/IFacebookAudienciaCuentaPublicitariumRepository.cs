using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFacebookAudienciaCuentaPublicitariumRepository : IGenericRepository<TFacebookAudienciaCuentaPublicitarium>
    {
        #region Metodos Base
        TFacebookAudienciaCuentaPublicitarium Add(FacebookAudienciaCuentaPublicitarium entidad);
        TFacebookAudienciaCuentaPublicitarium Update(FacebookAudienciaCuentaPublicitarium entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFacebookAudienciaCuentaPublicitarium> Add(IEnumerable<FacebookAudienciaCuentaPublicitarium> listadoEntidad);
        IEnumerable<TFacebookAudienciaCuentaPublicitarium> Update(IEnumerable<FacebookAudienciaCuentaPublicitarium> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
       
    }
} 