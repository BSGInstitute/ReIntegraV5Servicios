using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ITipoContratoService
    {
        #region Metodos Base
        TipoContrato Add(TipoContratoDTO data, string Usuario);
        TipoContrato Update(TipoContratoDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<TipoContrato> Add(List<TipoContrato> listadoEntidad);
        List<TipoContrato> Update(List<TipoContrato> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoContratoDTO> ObtenerTipoContrato();
        IEnumerable<TipoContratoComboDTO> ObtenerCombo();
    }
}
