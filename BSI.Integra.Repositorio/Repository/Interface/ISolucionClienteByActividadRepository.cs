using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolucionClienteByActividadRepository : IGenericRepository<TSolucionClienteByActividad>
    {
        #region Metodos Base
        TSolucionClienteByActividad Add(SolucionClienteByActividad entidad);
        TSolucionClienteByActividad Update(SolucionClienteByActividad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSolucionClienteByActividad> Add(IEnumerable<SolucionClienteByActividad> listadoEntidad);
        IEnumerable<TSolucionClienteByActividad> Update(IEnumerable<SolucionClienteByActividad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
