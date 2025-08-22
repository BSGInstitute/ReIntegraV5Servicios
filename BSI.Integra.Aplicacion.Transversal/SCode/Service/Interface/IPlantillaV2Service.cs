using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaV2Service
    {

        #region Metodos Base
        PlantillaV2 Add(PlantillaV2Envio entidad);
        PlantillaV2 Update(PlantillaV2Envio entidad);
        bool Delete(int id, string usuario);

        List<PlantillaV2> Add(List<PlantillaV2> listadoEntidad);
        List<PlantillaV2> Update(List<PlantillaV2> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaV2Combo> ObtenerCombo();
        IEnumerable<PlantillaV2> ObtenerPlantillaV2();



    }
}
