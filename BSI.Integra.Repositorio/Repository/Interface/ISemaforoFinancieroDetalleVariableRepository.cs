using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISemaforoFinancieroDetalleVariableRepository : IGenericRepository<TSemaforoFinancieroDetalleVariable>
    {
        #region Metodos Base
        TSemaforoFinancieroDetalleVariable Add(SemaforoFinancieroDetalleVariable entidad);
        TSemaforoFinancieroDetalleVariable Update(SemaforoFinancieroDetalleVariable entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSemaforoFinancieroDetalleVariable> Add(IEnumerable<SemaforoFinancieroDetalleVariable> listadoEntidad);
        IEnumerable<TSemaforoFinancieroDetalleVariable> Update(IEnumerable<SemaforoFinancieroDetalleVariable> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroDetalleVariableDTO> ObtenerSemaforoFinancieroDetalleVariable();
        IEnumerable<SemaforoFinancieroDetalleVariableComboDTO> ObtenerCombo();
        IEnumerable<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerSemaforoFinancieroDetalleVariablePorIdSemaforoFinancieroDetalle(int idSemaforoFinancieroDetalle);
        SemaforoFinancieroDetalleVariable ObtenerSemaforoDetalleVariablePorId(int idSemaforoDetalleVariable);
    }
}
