using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionAutomaticaConfiguracionService
    {
        #region Metodos Base
        AsignacionAutomaticaConfiguracion Add(AsignacionAutomaticaConfiguracion entidad);
        AsignacionAutomaticaConfiguracion Update(AsignacionAutomaticaConfiguracion entidad);
        bool Delete(int id, string usuario);

        List<AsignacionAutomaticaConfiguracion> Add(List<AsignacionAutomaticaConfiguracion> listadoEntidad);
        List<AsignacionAutomaticaConfiguracion> Update(List<AsignacionAutomaticaConfiguracion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
