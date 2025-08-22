using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Google.Api.Ads.AdWords.v201809;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoCambioColRepository : IGenericRepository<TTipoCambioCol>
    {
        #region Metodos Base
        TTipoCambioCol Add(TipoCambioCol entidad);
        TTipoCambioCol Update(TipoCambioCol entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoCambioCol> Add(IEnumerable<TipoCambioCol> listadoEntidad);
        IEnumerable<TTipoCambioCol> Update(IEnumerable<TipoCambioCol> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        double ObtenerPesosDolaresUltimoTipoCambioColombia();
        object Obtener();
        TipoCambioColombiaDTO ObtenerPesosDolaresTipoCambioColombia(string fecha);
    }
}
