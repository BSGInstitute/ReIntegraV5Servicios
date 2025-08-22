using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaBaseRepository : IGenericRepository<TPlantillaBase>
    {
        #region Metodos Base
        TPlantillaBase Add(PlantillaBase entidad);
        TPlantillaBase Update(PlantillaBase entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPlantillaBase> Add(IEnumerable<PlantillaBase> listadoEntidad);
        IEnumerable<TPlantillaBase> Update(IEnumerable<PlantillaBase> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaBaseDTO> ObtenerPlantillaBase();
        IEnumerable<PlantillaBaseComboDTO> ObtenerCombo();
        PlantillaBase? ObtenerPorId(int idPlantillaBase);
        PlantillaBaseDTO ObtenerIdPorNombre(string nombre);
        SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechBienvenida(int idActividadDetalle, int idPlantillaBase);
        SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechDespedida(int idActividadDetalle, int idPlantillaBase);
        IEnumerable<ComboPlantillaContratoDTO> ObtenerPlantillasContrato();
    }
}