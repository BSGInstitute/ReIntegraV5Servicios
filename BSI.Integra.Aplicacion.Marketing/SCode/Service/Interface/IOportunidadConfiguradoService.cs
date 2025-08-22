using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IOportunidadConfiguradoService
    {
        #region Metodos Base
        OportunidadConfigurado Add(OportunidadConfigurado entidad);
        OportunidadConfigurado Update(OportunidadConfigurado entidad);
        bool Delete(int id, string usuario);

        List<OportunidadConfigurado> Add(List<OportunidadConfigurado> listadoEntidad);
        List<OportunidadConfigurado> Update(List<OportunidadConfigurado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
