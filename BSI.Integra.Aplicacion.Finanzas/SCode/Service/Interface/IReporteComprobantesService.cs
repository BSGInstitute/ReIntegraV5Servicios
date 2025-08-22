using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteComprobantesService
    {
        public List<ReporteComprobantesDTO> ObtenerReporteComprobantes(int? idTipoAsociado);

        public List<ComboDTO> ObtenerTipo();
    }
}
