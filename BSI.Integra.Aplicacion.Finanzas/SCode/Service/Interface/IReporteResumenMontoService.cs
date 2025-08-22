using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteResumenMontoService
    {
        public List<ReporteResumenMontosCierreDTO> ObtenerReporteResumenMontosCierre(ReporteResumenMontosFiltroDTO FiltroPendiente);
        public List<ReporteResumenMontosDiferenciasDTO> ObtenerReporteResumenMontosDiferencias(ReporteResumenMontosFiltroDTO FiltroPendiente);
        public List<ReporteResumenMontosDTO> ObtenerReporteResumenMontos(ReporteResumenMontosFiltroDTO FiltroPendiente);
        public List<ReporteResumenMontosCambiosDTO> ObtenerReporteResumenMontosCambios(ReporteResumenMontosFiltroDTO FiltroPendiente);


        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoPeriodoActual(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoPeriodoCierre(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosVariacionMensual(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosNuevosMatriculados(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoPais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadPresencialPais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadOnlinePais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadAonlinePais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
        public IEnumerable<ReporteResumenMontos> GenerarReporteResumenMontosTotalizadoModalidadInHousePais(ReporteResumenMontosGeneralTotalDTO respuestaGeneral);
    }
}
