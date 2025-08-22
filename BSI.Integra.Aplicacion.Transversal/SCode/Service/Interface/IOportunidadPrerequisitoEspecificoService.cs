using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadPrerequisitoEspecificoService
    {
        #region Metodos Base
        OportunidadPrerequisitoEspecifico Add(OportunidadPrerequisitoEspecifico entidad);
        OportunidadPrerequisitoEspecifico Update(OportunidadPrerequisitoEspecifico entidad);
        bool Delete(int id, string usuario);

        List<OportunidadPrerequisitoEspecifico> Add(List<OportunidadPrerequisitoEspecifico> listadoEntidad);
        List<OportunidadPrerequisitoEspecifico> Update(List<OportunidadPrerequisitoEspecifico> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
