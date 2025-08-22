using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IFrecuenciaService
    {
        public List<DatosFrecuenciaGeneralDTO> ObtenerListaFrecuenciaActividad();
        public List<DatosFrecuenciaGeneralDTO> ObtenerFrecuenciaReporteDocumentos();

    }
}
