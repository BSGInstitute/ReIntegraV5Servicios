using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoCambioEntreMonedaRepository : IGenericRepository<TTipoCambioEntreMonedum>
    {
        #region Metodos Base
        TTipoCambioEntreMonedum Add(TipoCambioEntreMoneda entidad);
        TTipoCambioEntreMonedum Update(TipoCambioEntreMoneda entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoCambioEntreMonedum> Add(IEnumerable<TipoCambioEntreMoneda> listadoEntidad);
        IEnumerable<TTipoCambioEntreMonedum> Update(IEnumerable<TipoCambioEntreMoneda> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public object ObtenerParaFiltro();
        
     }
}
