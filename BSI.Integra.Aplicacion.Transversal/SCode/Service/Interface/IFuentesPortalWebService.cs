using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFuentesPortalWebService
    {

        #region Metodos Base
        FuentesPortalWeb Add(FuentesPortalWebEnvio entidad);
        FuentesPortalWeb Update(FuentesPortalWebEnvio entidad);
        bool Delete(int id, string usuario);

        List<FuentesPortalWeb> Add(List<FuentesPortalWeb> listadoEntidad);
        List<FuentesPortalWeb> Update(List<FuentesPortalWeb> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<comboFuentes> ObtenerCombo();
        IEnumerable<FuentesPortalWeb> ObtenerFuentesPortalWeb();

    }
}
