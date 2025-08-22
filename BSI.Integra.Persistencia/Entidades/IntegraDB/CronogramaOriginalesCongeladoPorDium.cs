using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaOriginalesCongeladoPorDium : BaseIntegraEntity
    {
        public string? EstadoMatricula { get; set; }
        public string? Alumno { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public double? Cuota { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public string? Moneda { get; set; }
        public double? CuotaDolares { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? PeriodoPorFechaVencimiento { get; set; }
        public string? CoordinadoraAcademica { get; set; }
        public string? CoordinadoraCobranza { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaCongelamiento { get; set; }

    }
}
