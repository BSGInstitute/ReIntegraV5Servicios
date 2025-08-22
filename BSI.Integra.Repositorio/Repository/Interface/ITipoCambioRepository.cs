using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoCambioRepository : IGenericRepository<TTipoCambio>
    {
        #region Metodos Base
        TTipoCambio Add(TipoCambio entidad);
        TTipoCambio Update(TipoCambio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoCambio> Add(IEnumerable<TipoCambio> listadoEntidad);
        IEnumerable<TTipoCambio> Update(IEnumerable<TipoCambio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TipoCambioFechaDTO ObtenerTipoCambio(int tipoCambio);
        IEnumerable<TipoCambioReporteDTO> ObtenerTipoCambioFiltro(TipoCambioFiltroDTO filtro);
        IEnumerable<TipoCambioObtenerDTO> Obtener();

    }
}
