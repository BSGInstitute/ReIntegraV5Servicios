using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface ITiempoCapacitacionService
    {
        #region Metodos Base
        TiempoCapacitacion Add(TiempoCapacitacion entidad);
        TiempoCapacitacion Update(TiempoCapacitacion entidad);
        bool Delete(int id, string usuario);

        List<TiempoCapacitacion> Add(List<TiempoCapacitacion> listadoEntidad);
        List<TiempoCapacitacion> Update(List<TiempoCapacitacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TiempoCapacitacionDTO> ObtenerTiempoCapacitacion();
        List<TiempoCapacitacionComboDTO> ObtenerCombo();
    }
}
