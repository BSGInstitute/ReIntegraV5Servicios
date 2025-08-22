using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModoFurRepository : IGenericRepository<TModoFur>
    {
        #region Metodos Base
        TModoFur Add(ModoFurE entidad);
        TModoFur Update(ModoFurE entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModoFur> Add(IEnumerable<ModoFurE> listadoEntidad);
        IEnumerable<TModoFur> Update(IEnumerable<ModoFurE> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


    }
}
