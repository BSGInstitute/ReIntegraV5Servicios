using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoCambioMonedumRepository : IGenericRepository<TTipoCambioMonedum>
    {
        #region Metodos Base
        TTipoCambioMonedum Add(TipoCambioMonedum entidad);
        TTipoCambioMonedum Update(TipoCambioMonedum entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoCambioMonedum> Add(IEnumerable<TipoCambioMonedum> listadoEntidad);
        IEnumerable<TTipoCambioMonedum> Update(IEnumerable<TipoCambioMonedum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoCambioMonedumDTO> ObtenerTipoCambioMonedum();
        public TipoCambioFechaDTO ObtenerTasaCambioMoneda(int idMoneda);
        public TTipoCambioMonedum ObtenerPorFecha(int idMoneda);
    }
}
