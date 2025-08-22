using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlantillaPwRepository : IGenericRepository<TPlantillaPw>
    {
        #region Metodos Base
        TPlantillaPw Add(PlantillaPw entidad);
        TPlantillaPw Update(PlantillaPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPlantillaPw> Add(IEnumerable<PlantillaPw> listadoEntidad);
        IEnumerable<TPlantillaPw> Update(IEnumerable<PlantillaPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlantillaPwDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<PlantillaPwComboWhatsappDTO> ObtenerComboWhatsapp();
        PlantillaPw ObtenerPorId(int id);
    }
}