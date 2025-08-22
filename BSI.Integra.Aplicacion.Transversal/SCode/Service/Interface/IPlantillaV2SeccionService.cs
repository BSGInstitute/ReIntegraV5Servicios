using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaV2SeccionService
    {

        #region Metodos Base
        PlantillaV2Seccion Add(PlantillaV2SeccionEnvio entidad);
        PlantillaV2Seccion Update(PlantillaV2SeccionEnvio entidad);
        bool Delete(int id, string usuario);

        List<PlantillaV2Seccion> Add(List<PlantillaV2Seccion> listadoEntidad);
        List<PlantillaV2Seccion> Update(List<PlantillaV2Seccion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaV2SeccionCombo> ObtenerCombo();
        IEnumerable<PlantillaV2Seccion> ObtenerPlantillaV2Seccion();
        List<PSSeccion> ObtenerTodo(int id);





    }
}
