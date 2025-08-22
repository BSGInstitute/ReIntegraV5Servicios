using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoCambioEntreMonedaService
    {
        #region Metodos Base
        TipoCambioEntreMoneda Add(TipoCambioEntreMoneda entidad);
        TipoCambioEntreMoneda Update(TipoCambioEntreMoneda entidad);
        bool Delete(int id, string usuario);

        List<TipoCambioEntreMoneda> Add(List<TipoCambioEntreMoneda> listadoEntidad);
        List<TipoCambioEntreMoneda> Update(List<TipoCambioEntreMoneda> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        public object ObtenerParaFiltro();


    }
}
