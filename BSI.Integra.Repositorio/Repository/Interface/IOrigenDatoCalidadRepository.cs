using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOrigenDatoCalidadRepository : IGenericRepository<TOrigenDatoCalidad>
    {
        #region Metodos Base
        TOrigenDatoCalidad Add(OrigenDatoCalidad entidad);
        TOrigenDatoCalidad Update(OrigenDatoCalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOrigenDatoCalidad> Add(IEnumerable<OrigenDatoCalidad> listadoEntidad);
        IEnumerable<TOrigenDatoCalidad> Update(IEnumerable<OrigenDatoCalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}
