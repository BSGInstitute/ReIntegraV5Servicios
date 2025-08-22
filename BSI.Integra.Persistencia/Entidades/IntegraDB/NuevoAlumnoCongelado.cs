using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class NuevoAlumnoCongelado : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public decimal Mora { get; set; }
        public decimal MontoPagado { get; set; }
        public bool Cancelado { get; set; }
        public string TipoCuota { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public DateTime FechaPago { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public int IdPeriodo { get; set; }
    }
}
