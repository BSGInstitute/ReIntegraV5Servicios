using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaV2Repository : IGenericRepository<TPlantillaV2>
    {
        #region Metodos Base
        TPlantillaV2 Add(PlantillaV2 entidad);
        TPlantillaV2 Update(PlantillaV2 entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPlantillaV2> Add(IEnumerable<PlantillaV2> listadoEntidad);
        IEnumerable<TPlantillaV2> Update(IEnumerable<PlantillaV2> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaV2Combo> ObtenerCombo();
        IEnumerable<PlantillaV2> ObtenerPlantillaV2();

    }
}
