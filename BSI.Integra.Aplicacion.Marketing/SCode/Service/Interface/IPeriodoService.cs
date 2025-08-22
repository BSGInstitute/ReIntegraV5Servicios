using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IPeriodoService
    {
        List<PeriodoFiltroDTO> ObtenerCombo();
        List<FiltroDTO> ObtenerIdPeriodoFechaActual();
        public List<FiltroIdNombreDTO> ObtenerUltimoPeriodo();
    }
}
