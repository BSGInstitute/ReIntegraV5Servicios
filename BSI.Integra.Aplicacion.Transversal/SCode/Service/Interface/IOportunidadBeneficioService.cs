using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadBeneficioService
    {
        #region Metodos Base
        OportunidadBeneficio Add(OportunidadBeneficio entidad);
        OportunidadBeneficio Update(OportunidadBeneficio entidad);
        bool Delete(int id, string usuario);

        List<OportunidadBeneficio> Add(List<OportunidadBeneficio> listadoEntidad);
        List<OportunidadBeneficio> Update(List<OportunidadBeneficio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
