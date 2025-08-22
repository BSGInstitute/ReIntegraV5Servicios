using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISemaforoFinancieroDetalleService
    {
        #region Metodos Base
        SemaforoFinancieroDetalle Add(SemaforoFinancieroDetalle entidad);
        SemaforoFinancieroDetalle Update(SemaforoFinancieroDetalle entidad);
        bool Delete(int id, string usuario);
        List<SemaforoFinancieroDetalle> Add(List<SemaforoFinancieroDetalle> listadoEntidad);
        List<SemaforoFinancieroDetalle> Update(List<SemaforoFinancieroDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetalle();
        IEnumerable<SemaforoFinancieroDetalleComboDTO> ObtenerCombo();
        IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(int idSemaforoFinanciero);
        List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerVariables(int IdSemaforoFinancieroDetalle);
        bool InsertarNuevoSemaforoDetalle(SemaforoFinancieroDetalle objetoBO);
        SemaforoFinancieroDetalle ObtenerSemaforoFinancieroDetallePorId(int id);
    }
}
