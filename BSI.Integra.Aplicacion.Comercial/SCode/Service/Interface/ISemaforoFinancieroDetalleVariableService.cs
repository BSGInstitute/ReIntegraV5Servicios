using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISemaforoFinancieroDetalleVariableService
    {
        #region Metodos Base
        SemaforoFinancieroDetalleVariable Add(SemaforoFinancieroDetalleVariable entidad);
        SemaforoFinancieroDetalleVariable Update(SemaforoFinancieroDetalleVariable entidad);
        bool Delete(int id, string usuario);
        List<SemaforoFinancieroDetalleVariable> Add(List<SemaforoFinancieroDetalleVariable> listadoEntidad);
        List<SemaforoFinancieroDetalleVariable> Update(List<SemaforoFinancieroDetalleVariable> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroDetalleVariableDTO> ObtenerSemaforoFinancieroDetalleVariable();
        IEnumerable<SemaforoFinancieroDetalleVariableComboDTO> ObtenerCombo();
        IEnumerable<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerSemaforoFinancieroDetalleVariablePorIdSemaforoFinancieroDetalle(int idSemaforoFinancieroDetalle);
        SemaforoFinancieroDetalleVariable ObtenerSemaforoDetalleVariablePorId(int idSemaforoDetalleVariable);
    }
}
