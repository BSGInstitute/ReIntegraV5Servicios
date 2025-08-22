using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IErrorRepository : IGenericRepository<TError>
    {
        #region Metodos Base
        TError Add(Error entidad);
        TError Update(Error entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TError> Add(IEnumerable<Error> listadoEntidad);
        IEnumerable<TError> Update(IEnumerable<Error> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ErrorDTO> ObtenerTodosErroresSistema();
    }
}
