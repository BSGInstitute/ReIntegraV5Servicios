using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IOportunidadPreAsignadumService
    {
        #region Metodos Base
        OportunidadPreAsignadum Add(OportunidadPreAsignadum entidad);
        OportunidadPreAsignadum Update(OportunidadPreAsignadum entidad);
        bool Delete(int id, string usuario);

        List<OportunidadPreAsignadum> Add(List<OportunidadPreAsignadum> listadoEntidad);
        List<OportunidadPreAsignadum> Update(List<OportunidadPreAsignadum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool ProcesoAsignacionAutomatizada();
    }
}
