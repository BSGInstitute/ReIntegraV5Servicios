using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPespecificoCronogramaGrupoRepository : IGenericRepository<TPespecificoCronograma>
    {
        #region Metodos Base
        TPespecificoCronograma Add(PespecificoCronogramaGrupo entidad);
        TPespecificoCronograma Update(PespecificoCronogramaGrupo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPespecificoCronograma> Add(IEnumerable<PespecificoCronogramaGrupo> listadoEntidad);
        IEnumerable<TPespecificoCronograma> Update(IEnumerable<PespecificoCronogramaGrupo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoCronogramaGrupo? ObtenerPorIdPespecificoPorIdPais(int idPespecifico, int idPais);
    }
}
