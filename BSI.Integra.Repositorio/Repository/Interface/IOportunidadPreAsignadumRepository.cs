using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadPreAsignadumRepository : IGenericRepository<TOportunidadPreAsignadum>
    {
        #region Metodos Base
        TOportunidadPreAsignadum Add(OportunidadPreAsignadum entidad);
        TOportunidadPreAsignadum Update(OportunidadPreAsignadum entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadPreAsignadum> Add(IEnumerable<OportunidadPreAsignadum> listadoEntidad);
        IEnumerable<TOportunidadPreAsignadum> Update(IEnumerable<OportunidadPreAsignadum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}