using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaV2SeccionRepository : IGenericRepository<TPlantillaV2seccion>
    {
        #region Metodos Base
        TPlantillaV2seccion Add(PlantillaV2Seccion entidad);
        TPlantillaV2seccion Update(PlantillaV2Seccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPlantillaV2seccion> Add(IEnumerable<PlantillaV2Seccion> listadoEntidad);
        IEnumerable<TPlantillaV2seccion> Update(IEnumerable<PlantillaV2Seccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaV2SeccionCombo> ObtenerCombo();
        IEnumerable<PlantillaV2Seccion> ObtenerPlantillaV2Seccion();

        List<PSTodo> ObtenerTodo(int id);

    }
}
