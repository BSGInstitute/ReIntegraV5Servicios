using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteIngresoService
    {
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosVentas(FiltroFechaDTO Filtro);
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOperaciones(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerReporteIngresosOperacionesTipoCambio(FiltroFechaDTO Filtro);
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOtrosIngresos(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerPagosIngresos(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosterior(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnterior(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerPagosIngresosGestionCobranza(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerPagosTasasAcademicas(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnteriorConDeposito(FiltroFechaDTO Filtro);
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosteriorConDeposito(FiltroFechaDTO Filtro);
    }
}
