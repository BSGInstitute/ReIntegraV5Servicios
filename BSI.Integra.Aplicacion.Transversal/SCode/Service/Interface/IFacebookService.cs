using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFacebookService
    {
        #region Metodos Base

        #endregion
        List<CampaniaFacebookDTO> DescargarCampaniayPadre(List<string> listaCampania);
        List<AnuncioFacebookMetricaDTO> DescargarMetricaFacebookAnuncio(string fechaInicio, string fechaFin);
        List<ConjuntoAnuncioFacebookJerarquiaDTO> DescargarConjuntoAnuncioyPadre(List<string> listaConjuntoAnuncio);
        List<AnuncioFacebookDTO> DescargarAnuncioyPadre(List<string> listaAnuncio);

        List<DetalleMensajeDTO> DescargarConversacionPorIdPagina(string idPagina, string token);
        List<DetalleMensajeDTO> DescargarConversacionPorIdUsuario(string idPagina, string idUsuario, string token);

    }
}
