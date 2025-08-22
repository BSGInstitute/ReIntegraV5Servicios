namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class ReportePendientePeriodoFiltroDTO
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaCortePrevio { get; set; }
        public List<int> Modalidad { get; set; }
        public List<string> Coordinadora { get; set; }
        public ReportePendienteGeneralPeriodoDTO datosPrueba { get; set; }
    }


    public class ReportePendientePeriodoFiltroPruebaDTO
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaCortePrevio { get; set; }
        public List<int> Modalidad { get; set; }
        public List<string> Coordinadora { get; set; }
        public Guid? Identificador { get; set; }
    }

    public class ReportePendienteGeneralPeriodoDTO
    {
        public List<ReportePendientePeriodoyCoordinadorDTO> Periodo { get; set; }
        public List<ReportePendientePeriodoyCoordinadorDTO> Matriculados { get; set; }
        public List<ReportePendientesCambiosPorCoordinadorDTO>? Cambios { get; set; }
        public List<ReportePendientesDiferenciasDTO>? Diferencias { get; set; }
        public List<ReportePendientePeriodoyCoordinadorDTO> Cierre { get; set; }
        public List<ReportePendientePeriodoyCoordinadorDTO>? CierreOriginales { get; set; }
        public List<ReportePendientesModificacionesMesDTO>? ModificacionesMes { get; set; }

        public string FechaCierreActual { get; set; }
        public string FechaCierrePrevio { get; set; }
    }



    public class ReportePendiente
    {
        public string g { get; set; }
        public List<ReportePendienteDetalleFinalDTO> l { get; set; }
    }

}
