using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBloqueHorarioProcesaOportunidadRepository : IGenericRepository<TBloqueHorarioProcesaOportunidad>
    {
        #region Metodos Base
        TBloqueHorarioProcesaOportunidad Add(BloqueHorarioProcesaOportunidad entidad);
        TBloqueHorarioProcesaOportunidad Update(BloqueHorarioProcesaOportunidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TBloqueHorarioProcesaOportunidad> Add(IEnumerable<BloqueHorarioProcesaOportunidad> listadoEntidad);
        IEnumerable<TBloqueHorarioProcesaOportunidad> Update(IEnumerable<BloqueHorarioProcesaOportunidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public BloqueHorarioProcesaOportunidad ObtenerConfiguracionPorDia(string dia);
        public BloqueHorarioProcesaOportunidad ObtenerConfiguracion(string dia);
    }
}
