using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISemaforoFinancieroRepository : IGenericRepository<TSemaforoFinanciero>
    {
        #region Metodos Base
        TSemaforoFinanciero Add(SemaforoFinanciero entidad);
        TSemaforoFinanciero Update(SemaforoFinanciero entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSemaforoFinanciero> Add(IEnumerable<SemaforoFinanciero> listadoEntidad);
        IEnumerable<TSemaforoFinanciero> Update(IEnumerable<SemaforoFinanciero> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroDTO> ObtenerSemaforoFinanciero();
        IEnumerable<SemaforoFinancieroComboDTO> ObtenerCombo();
        bool InsertarNuevoSemaforo(SemaforoFinanciero objeto);
        SemaforoFinanciero ObtenerSemaforoPorId(int id);
    }
}
