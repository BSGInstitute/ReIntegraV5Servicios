using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPlantillaBaseService
    {
        #region Metodos Base
        PlantillaBase Add(PlantillaBase entidad);
        PlantillaBase Update(PlantillaBase entidad);
        bool Delete(int id, string usuario);

        List<PlantillaBase> Add(List<PlantillaBase> listadoEntidad);
        List<PlantillaBase> Update(List<PlantillaBase> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PlantillaBaseDTO> ObtenerPlantillaBase();
        IEnumerable<PlantillaBaseComboDTO> ObtenerCombo();
        PlantillaBase ObtenerPorId(int idPlantillaBase);
        PlantillaBaseDTO ObtenerIdPorNombre(string nombre);
        SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechBienvenida(int idActividadDetalle, int idPlantillaBase);
        SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechDespedida(int idActividadDetalle, int idPlantillaBase);
        bool ExistePorId(int idPlantilla);
    }
}
