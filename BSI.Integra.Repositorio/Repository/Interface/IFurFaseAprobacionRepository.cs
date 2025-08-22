using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFurFaseAprobacionRepository : IGenericRepository<TFurFaseAprobacion>
    {
        #region Metodos Base
        TFurFaseAprobacion Add(FurFaseAprobacion entidad);
        TFurFaseAprobacion Update(FurFaseAprobacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFurFaseAprobacion> Add(IEnumerable<FurFaseAprobacion> listadoEntidad);
        IEnumerable<TFurFaseAprobacion> Update(IEnumerable<FurFaseAprobacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<Object> ObtenerCombo();

    }
}
