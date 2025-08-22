using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILlamadaWebphoneCruceCentralRepository : IGenericRepository<TLlamadaWebphoneCruceCentral>
    {
        #region Metodos Base
        TLlamadaWebphoneCruceCentral Add(LlamadaWebphoneCruceCentral entidad);
        TLlamadaWebphoneCruceCentral Update(LlamadaWebphoneCruceCentral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TLlamadaWebphoneCruceCentral> Add(IEnumerable<LlamadaWebphoneCruceCentral> listadoEntidad);
        IEnumerable<TLlamadaWebphoneCruceCentral> Update(IEnumerable<LlamadaWebphoneCruceCentral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}