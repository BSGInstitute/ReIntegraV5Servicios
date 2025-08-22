using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISemaforoFinancieroVariableRepository : IGenericRepository<TSemaforoFinancieroVariable>
    {
        #region Metodos Base
        TSemaforoFinancieroVariable Add(SemaforoFinancieroVariable entidad);
        TSemaforoFinancieroVariable Update(SemaforoFinancieroVariable entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSemaforoFinancieroVariable> Add(IEnumerable<SemaforoFinancieroVariable> listadoEntidad);
        IEnumerable<TSemaforoFinancieroVariable> Update(IEnumerable<SemaforoFinancieroVariable> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroVariableDTO> ObtenerSemaforoFinancieroVariable();
        IEnumerable<SemaforoFinancieroVariableComboDTO> ObtenerCombo();
    }
}
