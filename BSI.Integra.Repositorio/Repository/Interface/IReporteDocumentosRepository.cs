using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteDocumentosRepository
    {
        public List<ReporteDocumentosDTO> ObtenerReporteDocumentos(ReporteDocumentosFiltroDTO FiltroControlDocumentos);


    }
}
