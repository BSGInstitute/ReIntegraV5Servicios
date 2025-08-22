using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOcurrenciaAlternoService
    {
        #region Metodos Base
        OcurrenciaAlterno Add(OcurrenciaAlterno entidad);
        OcurrenciaAlterno Update(OcurrenciaAlterno entidad);
        bool Delete(int id, string usuario);

        List<OcurrenciaAlterno> Add(List<OcurrenciaAlterno> listadoEntidad);
        List<OcurrenciaAlterno> Update(List<OcurrenciaAlterno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<OcurrenciaAlternoDTO> ObtenerOcurrenciaAlterno();
        IEnumerable<OcurrenciaAlternoComboDTO> ObtenerCombo();
        OcurrenciaAlternoDTO ObtenerOcurrenciaPorActividad(int idOcurrencia);
    }
}
