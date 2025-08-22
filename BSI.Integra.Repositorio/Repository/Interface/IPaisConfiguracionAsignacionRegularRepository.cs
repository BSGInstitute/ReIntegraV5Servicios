using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPaisConfiguracionAsignacionRegularRepository : IGenericRepository<TPaisConfiguracionAsignacionRegular>
    {
        #region Metodos Base
        TPaisConfiguracionAsignacionRegular Add(PaisConfiguracionAsignacionRegular entidad);
        TPaisConfiguracionAsignacionRegular Update(PaisConfiguracionAsignacionRegular entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPaisConfiguracionAsignacionRegular> Add(IEnumerable<PaisConfiguracionAsignacionRegular> listadoEntidad);
        IEnumerable<TPaisConfiguracionAsignacionRegular> Update(IEnumerable<PaisConfiguracionAsignacionRegular> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}