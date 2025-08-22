using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDiferenciaHorariaRepository : IGenericRepository<TDiferenciaHorarium>
    {
        #region Metodos Base
        TDiferenciaHorarium Add(DiferenciaHorarium entidad);
        TDiferenciaHorarium Update(DiferenciaHorarium entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDiferenciaHorarium> Add(IEnumerable<DiferenciaHorarium> listadoEntidad);
        IEnumerable<TDiferenciaHorarium> Update(IEnumerable<DiferenciaHorarium> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DiferenciaHorarium> ObtenerPorIdPaisOrigen(int idPaisOrigen);
    }
}
