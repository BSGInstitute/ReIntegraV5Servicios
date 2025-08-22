using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAdworkCredencialApiService
    {
        #region Metodos Base
        AdworkCredencialApi Add(AdworkCredencialApi entidad);
        AdworkCredencialApi Update(AdworkCredencialApi entidad);
        bool Delete(int id, string usuario);

        List<AdworkCredencialApi> Add(List<AdworkCredencialApi> listadoEntidad);
        List<AdworkCredencialApi> Update(List<AdworkCredencialApi> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        public IEnumerable<ReporteAdwordsApiPalabrasClaveRespuestaDTO> GenerarReporte(FiltroReporteAdwordsApiVolumenBusquedaDTO FiltroReporteAdwordsApiVolumenBusquedaDTO, bool actualziacion, string correo);
    }
}
