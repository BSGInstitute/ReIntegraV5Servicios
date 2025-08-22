using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteDocumentosService
    {
        public ReporteDocumentosCompuestoDTO ObtenerReporteDocumentos(ReporteDocumentosFiltroDTO FiltroControlDocumentos);

    }
}
