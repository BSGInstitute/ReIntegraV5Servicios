using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IFurFaseAprobacionService
    {
        #region Metodos Base
        FurFaseAprobacion Add(FurFaseAprobacion entidad);
        FurFaseAprobacion Update(FurFaseAprobacion entidad);
        bool Delete(int id, string usuario);

        List<FurFaseAprobacion> Add(List<FurFaseAprobacion> listadoEntidad);
        List<FurFaseAprobacion> Update(List<FurFaseAprobacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<Object> ObtenerCombo();
    }
}
