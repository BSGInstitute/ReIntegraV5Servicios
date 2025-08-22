using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadPrerequisitoGeneralService
    {
        #region Metodos Base
        OportunidadPrerequisitoGeneral Add(OportunidadPrerequisitoGeneral entidad);
        OportunidadPrerequisitoGeneral Update(OportunidadPrerequisitoGeneral entidad);
        bool Delete(int id, string usuario);

        List<OportunidadPrerequisitoGeneral> Add(List<OportunidadPrerequisitoGeneral> listadoEntidad);
        List<OportunidadPrerequisitoGeneral> Update(List<OportunidadPrerequisitoGeneral> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
