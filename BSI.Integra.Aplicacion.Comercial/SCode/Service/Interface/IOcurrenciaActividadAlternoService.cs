using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IOcurrenciaActividadAlternoService
    {
        #region Metodos Base
        OcurrenciaActividadAlterno Add(OcurrenciaActividadAlterno entidad);
        OcurrenciaActividadAlterno Update(OcurrenciaActividadAlterno entidad);
        bool Delete(int id, string usuario);

        List<OcurrenciaActividadAlterno> Add(List<OcurrenciaActividadAlterno> listadoEntidad);
        List<OcurrenciaActividadAlterno> Update(List<OcurrenciaActividadAlterno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OcurrenciaActividadAlternoDTO> ObtenerOcurrenciaActividadAlterno();
        IEnumerable<OcurrenciaActividadAlternoComboDTO> ObtenerCombo();
        IEnumerable<ArbolOcurenciaAlternoDTO> ObtenerArbolOcurrenciaAlterno(int idActividadCabecera, int idOcurrenciaPadre);
        ArbolOcurenciaAlternoDTO ObtenerOcurrenciaMarcador(int idActividadCabecera);
        OcurenciaActividadCompletoDTO ObtenerOcurrenciaActividadPorId(int? idOcurrenciaActividad);
    }
}
