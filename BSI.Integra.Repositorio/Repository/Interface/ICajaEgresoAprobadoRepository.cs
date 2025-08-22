using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICajaEgresoAprobadoRepository : IGenericRepository<TCajaEgresoAprobado>
    {
        #region Metodos Base
        TCajaEgresoAprobado Add(CajaEgresoAprobado entidad);
        TCajaEgresoAprobado Update(CajaEgresoAprobado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCajaEgresoAprobado> Add(IEnumerable<CajaEgresoAprobado> listadoEntidad);
        IEnumerable<TCajaEgresoAprobado> Update(IEnumerable<CajaEgresoAprobado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


    }
}
