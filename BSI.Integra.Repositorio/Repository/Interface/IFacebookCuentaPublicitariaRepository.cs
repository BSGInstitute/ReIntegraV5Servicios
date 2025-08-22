using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFacebookCuentaPublicitariaRepository : IGenericRepository<TFacebookCuentaPublicitarium>
    {
        #region Metodos Base
        TFacebookCuentaPublicitarium Add(FacebookCuentaPublicitarium entidad);
        TFacebookCuentaPublicitarium Update(FacebookCuentaPublicitarium entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFacebookCuentaPublicitarium> Add(IEnumerable<FacebookCuentaPublicitarium> listadoEntidad);
        IEnumerable<TFacebookCuentaPublicitarium> Update(IEnumerable<FacebookCuentaPublicitarium> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ComboDTO> ObtenerCombo();
         List<FacebookCuentaPublicitariaDTO> ObtenerComboFacebookCuentaPublicitaria();


    }
}
