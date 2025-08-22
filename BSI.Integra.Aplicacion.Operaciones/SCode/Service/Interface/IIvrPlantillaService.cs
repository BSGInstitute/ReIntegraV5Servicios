using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IIvrPlantillaService
    {
        #region Metodos Base
        IvrPlantilla Add(IvrPlantillaDTO data, string Usuario);
        IvrPlantilla Update(IvrPlantillaDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<IvrPlantilla> Add(List<IvrPlantilla> listadoEntidad);
        List<IvrPlantilla> Update(List<IvrPlantilla> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<IvrPlantillaDTO> ObtenerIvrPlantilla();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
