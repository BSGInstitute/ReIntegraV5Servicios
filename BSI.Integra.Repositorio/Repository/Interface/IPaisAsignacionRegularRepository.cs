using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPaisAsignacionRegularRepository : IGenericRepository<TPaisAsignacionRegular>
    {
        #region Metodos Base
        TPaisAsignacionRegular Add(PaisAsignacionRegular entidad);
        TPaisAsignacionRegular Update(PaisAsignacionRegular entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPaisAsignacionRegular> Add(IEnumerable<PaisAsignacionRegular> listadoEntidad);
        IEnumerable<TPaisAsignacionRegular> Update(IEnumerable<PaisAsignacionRegular> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}