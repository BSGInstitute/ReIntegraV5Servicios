using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISemaforoFinancieroDetalleRepository : IGenericRepository<TSemaforoFinancieroDetalle>
    {
        #region Metodos Base
        TSemaforoFinancieroDetalle Add(SemaforoFinancieroDetalle entidad);
        TSemaforoFinancieroDetalle Update(SemaforoFinancieroDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSemaforoFinancieroDetalle> Add(IEnumerable<SemaforoFinancieroDetalle> listadoEntidad);
        IEnumerable<TSemaforoFinancieroDetalle> Update(IEnumerable<SemaforoFinancieroDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetalle();
        IEnumerable<SemaforoFinancieroDetalleComboDTO> ObtenerCombo();
        IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(int idSemaforoFinanciero);
        List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerVariables(int IdSemaforoFinancieroDetalle);
        bool InsertarNuevoSemaforoDetalle(SemaforoFinancieroDetalle objetoBO);
        SemaforoFinancieroDetalle ObtenerSemaforoFinancieroDetallePorId(int id);
    }
}
