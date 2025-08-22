using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAnuncioFacebookMetricaService
    {
        #region Metodos Base
        AnuncioFacebookMetrica Add(AnuncioFacebookMetrica entidad);
        AnuncioFacebookMetrica Update(AnuncioFacebookMetrica entidad);
        bool Delete(int id, string usuario);

        List<AnuncioFacebookMetrica> Add(List<AnuncioFacebookMetrica> listadoEntidad);
        List<AnuncioFacebookMetrica> Update(List<AnuncioFacebookMetrica> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<ReporteAnuncioFacebookMetricaDiasDTO> EstructurarReporteAnuncioFacebook(int? idAreaCapacitacion);
        public object ObtenerCombosAnuncioFacebookMetrica();

        public object ActualizarMetricaFacebookAnuncioPorIntervaloFecha(AnuncioFacebookMetricaFechaDTO FechaFiltroDescarga);
        public object ObtenerInformacionBasica();
        public bool ActualizarMetricaIntegra(List<AnuncioFacebookMetricaDTO> datosActualizar, string usuario);
        public bool RegularizarJerarquiaFacebook(List<AnuncioFacebookMetricaDTO> datosActualizar, string usuario);
        public CampaniaFacebook MapeoCampaniaFacebookDTOBO(CampaniaFacebookDTO campaniaFacebook);
        public ConjuntoAnuncioFacebook MapeoConjuntoAnuncioFacebookDTOBO(ConjuntoAnuncioFacebookJerarquiaDTO conjuntoAnuncioFacebook, int? idCampaniaFacebook, string nombreCampaniaFacebook);
        public AnuncioFacebook MapeoAnuncioFacebookDTOBO(AnuncioFacebookDTO anuncioFacebook, int idConjuntoAnuncioFacebook);
        public AnuncioFacebookMetrica MapeoAnuncioFacebookMetricaDTOBO(AnuncioFacebookMetricaDTO anuncioFacebookMetrica, int idAnuncioFacebook, string usuario);
        public string EstructurarMensajeAnuncioFacebook(string cadenaFechaInicio, DateTime fechaInicio, DateTime fechaFin);
    }
}
