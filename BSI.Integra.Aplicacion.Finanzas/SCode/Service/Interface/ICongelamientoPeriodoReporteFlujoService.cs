using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICongelamientoPeriodoReporteFlujoService
    {
        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoPeriodoDTO> FlujoCongelamientoPeriodo);

    }
}
