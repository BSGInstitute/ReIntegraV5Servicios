using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IOrigenDatoCalidadService
    {
        #region Metodos Base
        OrigenDatoCalidad Add(OrigenDatoCalidad entidad);
        OrigenDatoCalidad Update(OrigenDatoCalidad entidad);
        bool Delete(int id, string usuario);

        List<OrigenDatoCalidad> Add(List<OrigenDatoCalidad> listadoEntidad);
        List<OrigenDatoCalidad> Update(List<OrigenDatoCalidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
