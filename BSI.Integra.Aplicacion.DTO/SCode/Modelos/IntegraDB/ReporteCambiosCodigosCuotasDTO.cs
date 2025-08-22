namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteCambiosDTO
    {
        public int IdCronogramaMod { get; set; }
        public int IdAlumno { get; set; }
        public string Modalidad { get; set; }
        public DateTime FechaCambio { get; set; }
        public string Ciudad { get; set; }
        public string Programa { get; set; }
        public string Alumno { get; set; }
        public int IdCentroCosto { get; set; }
        public string CodigoAlumno { get; set; }
        public int IdMatricula { get; set; }
        public string Observaciones { get; set; }
        public string RealizadoPor { get; set; }
        public string MensajeSistema { get; set; }
        public string SolicitadoPor { get; set; }
        public string AprobadoPor { get; set; }
        public string Observaciones2 { get; set; }

    }

    public class ReporteCodigosDTO
    {
        public int IdAlumno { get; set; }
        public string Modalidad { get; set; }
        public string Ciudad { get; set; }
        public string Programa { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Codigo { get; set; }
        public int IdMatricula { get; set; }
        public string Alumno { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class ReporteCuotasDTO
    {
        public int IdAlumno { get; set; }
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string Modalidad { get; set; }
        public string Ciudad { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Codigo { get; set; }
        public string Alumno { get; set; }
        public DateTime FechaCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Cuota_SubCuota { get; set; }
        public string MonedaPago { get; set; }
        public string EstadoCuota { get; set; }

    }

    public class ReporteCambioProgramaDTO
    {
        public DateTime Fecha { get; set; }
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string CentroCostoAnterior { get; set; }
        public string CentroCostoNuevo { get; set; }

    }

    public class ReporteCambiosCodigosCuotasFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMatricula { get; set; }
    }
}
