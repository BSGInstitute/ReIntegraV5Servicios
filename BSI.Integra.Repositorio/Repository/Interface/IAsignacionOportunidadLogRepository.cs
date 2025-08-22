using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsignacionOportunidadLogRepository : IGenericRepository<TAsignacionOportunidadLog>
    {
        #region Metodos Base
        TAsignacionOportunidadLog Add(AsignacionOportunidadLog entidad);
        TAsignacionOportunidadLog Update(AsignacionOportunidadLog entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TAsignacionOportunidadLog> Add(IEnumerable<AsignacionOportunidadLog> listadoEntidad);
        IEnumerable<TAsignacionOportunidadLog> Update(IEnumerable<AsignacionOportunidadLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
