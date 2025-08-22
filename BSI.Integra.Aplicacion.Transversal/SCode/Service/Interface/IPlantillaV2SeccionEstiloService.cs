using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaV2SeccionEstiloService
    {

        #region Metodos Base
        PlantillaV2SeccionEstilo Add(PlantillaV2SeccionEstiloEnvio entidad);
        PlantillaV2SeccionEstilo Update(PlantillaV2SeccionEstiloEnvio entidad);
        bool Delete(int id, string usuario);

        List<PlantillaV2SeccionEstilo> Add(List<PlantillaV2SeccionEstilo> listadoEntidad);
        List<PlantillaV2SeccionEstilo> Update(List<PlantillaV2SeccionEstilo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaV2SeccionEstiloCombo> ObtenerCombo();
        IEnumerable<PlantillaV2SeccionEstilo> ObtenerPlantillaV2SeccionEstilo();



    }
}
