namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class ReporteProductividadVentasHorasTrabajadasDTO
    {
        public int IdPersonal { get; set; }
        public string? NombrePersonal { get; set; }
        public string? CoordinadorAsesor { get; set; }
        public string? NombreJefe { get; set; }
        public DateTime? FechaPeriodo { get; set; }
        public string? PeriodoMarcacion { get; set; }

        public int? HorasTrabajadas { get; set; } = null;
        public int? DiasTrabajados { get; set; } = null;
        public decimal? TotalVendido { get; set; } 
        public string? Sede { get; set; }
    }
    public class ReporteProductividadVentasIndicadoresDTO
    {
        public string? PeriodoMarcacion { get; set; }

        public int DiasLaborables { get; set; }
        public decimal TotalVendido { get; set; }
        public int DiasCoordinador { get; set; }
        public int DiasAsesor { get; set; }
        public int NumeroIS { get; set; }
        public decimal PagoDolaresRecargasTelefonicas { get; set; }
        public decimal PagoDolaresTelefonosFijos { get; set; }
        public decimal PagoDolaresCapacitacionPersonal { get; set; }
        public decimal PagoDolaresTelefonosMoviles { get; set; }
        public decimal PagoDolaresPublicidad { get; set; }
        public decimal PagoDolaresSueldoAsesores { get; set; }
        public decimal PagoDolaresSueldoCoordinadores { get; set; }
        public decimal PagoDolaresGratificacionAsesores { get; set; }
        public decimal PagoDolaresGratificacionCoordinadores { get; set; }
        public decimal PagoDolaresCTSAsesores { get; set; }
        public decimal PagoDolaresCTSCoordinadores { get; set; }
        public decimal PagoDolaresParticipacionesAsesores { get; set; }
        public decimal PagoDolaresParticipacionesCoordinadores { get; set; }
        public decimal PagoDolaresSisPensionarioAsesores { get; set; }
        public decimal PagoDolaresSisPensionarioCoordinadores { get; set; }
        public decimal PagoDolaresEsSaludAsesores { get; set; }
        public decimal PagoDolaresEsSaludCoordinadores { get; set; }
        public decimal PagoDolaresRenta5Asesores { get; set; }
        public decimal PagoDolaresRenta5Coordinadores { get; set; }
        public decimal PagoDolaresComisiones { get; set; }
        public decimal PagoDolaresVacaciones { get; set; }
        public decimal PagoDolaresPremios { get; set; }
        public decimal PagoDolaresSueldoLiquidacion { get; set; }
        public decimal PagoDolaresGratificacionLiquidacion { get; set; }
        public decimal PagoDolaresCTSLiquidacion { get; set; }
        public int NumeroCoordinadores { get; set; }
        public int NumeroAsesores { get; set; }
        public decimal BeaticosVentas { get; set; }
    }
    public class ReporteIndicadoresProductividadVentasGeneralDTO
    {
        public List<ReporteProductividadVentasHorasTrabajadasDTO> HorasTrabajadas { get; set; }
        public List<ReporteProductividadVentasIndicadoresDTO> Indicadores { get; set; }
    }
    public class ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO
    {
        public string Periodo { get; set; }
        public List<ReporteProductividadVentasHorasTrabajadasDTO> DetalleFecha { get; set; }
    }

    public class ReporteProductividadVentasDetalleFinalDTO
    {
        public string Anterior { get; set; }
        public string Actual { get; set; }
        public string TipoMonto { get; set; }//programa
        public string Periodo { get; set; }//periodoporfechavencimiento
        public string Monto { get; set; }//total dolares
    }

    public class ReporteProductividadVentasDetalleDTO
    {

        public string Tipo { get; set; }
        public List<ReporteProductividadVentasDetallesMesesDTO> ListaMontosMeses { get; set; }
    }

    public class ReporteProductividadVentasDetallesMesesDTO
    {
        public string Mes { get; set; }
        public string Monto { get; set; }
        public string Diferencia { get; set; }

    }
    public class ReporteProductividadVentas
    {
        public string g { get; set; }
        public List<ReporteProductividadVentasDetalleFinalDTO> l { get; set; }
    }

    public class ReporteIndicadoresProductividadVentasCompuestoDTO
    {
        public List<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> ReporteProductividadHorasTrabajadas { get; set; }
        public List<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> ReporteProductividadToTalVendido { get; set; }
        public List<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> ReporteHorasTrabajadasProductividadEquipo { get; set; }
        public List<ReporteProductividadVentas> ReporteIndicadoresProductividad { get; set; }

    }
}
