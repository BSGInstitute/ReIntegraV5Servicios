using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaV2SeccionEstiloRepository : IGenericRepository<TPlantillaV2seccionEstilo>
    {
        #region Metodos Base
        TPlantillaV2seccionEstilo Add(PlantillaV2SeccionEstilo entidad);
        TPlantillaV2seccionEstilo Update(PlantillaV2SeccionEstilo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPlantillaV2seccionEstilo> Add(IEnumerable<PlantillaV2SeccionEstilo> listadoEntidad);
        IEnumerable<TPlantillaV2seccionEstilo> Update(IEnumerable<PlantillaV2SeccionEstilo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaV2SeccionEstiloCombo> ObtenerCombo();
        IEnumerable<PlantillaV2SeccionEstilo> ObtenerPlantillaV2SeccionEstilo();

    }
}
