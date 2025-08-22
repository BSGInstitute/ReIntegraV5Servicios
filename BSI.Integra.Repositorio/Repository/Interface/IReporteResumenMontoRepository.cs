using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteResumenMontoRepository 
    {
        public List<ReporteResumenMontosCambiosDTO> ObtenerReporteResumenMontosCambios(ReporteResumenMontosFiltroDTO FiltroPendiente);
        public List<ReporteResumenMontosDiferenciasDTO> ObtenerReporteResumenMontosDiferencias(ReporteResumenMontosFiltroDTO FiltroPendiente);
        public List<ReporteResumenMontosDTO> ObtenerReporteResumenMontos(ReporteResumenMontosFiltroDTO FiltroPendiente);
        public List<ReporteResumenMontosCierreDTO> ObtenerReporteResumenMontosCierre(ReporteResumenMontosFiltroDTO FiltroPendiente);
        public List<ReporteResumenMontosNuevosMatriculadosDTO> ObtenerReporteResumenMontosNuevosMatriculados(ReporteResumenMontosFiltroDTO FiltroPendiente);
    }
}
