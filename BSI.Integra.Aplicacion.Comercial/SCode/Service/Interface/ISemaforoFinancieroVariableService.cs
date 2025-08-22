using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISemaforoFinancieroVariableService
    {
        #region Metodos Base
        SemaforoFinancieroVariable Add(SemaforoFinancieroVariable entidad);
        SemaforoFinancieroVariable Update(SemaforoFinancieroVariable entidad);
        bool Delete(int id, string usuario);
        List<SemaforoFinancieroVariable> Add(List<SemaforoFinancieroVariable> listadoEntidad);
        List<SemaforoFinancieroVariable> Update(List<SemaforoFinancieroVariable> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroVariableDTO> ObtenerSemaforoFinancieroVariable();
        IEnumerable<SemaforoFinancieroVariableComboDTO> ObtenerCombo();
    }
}
