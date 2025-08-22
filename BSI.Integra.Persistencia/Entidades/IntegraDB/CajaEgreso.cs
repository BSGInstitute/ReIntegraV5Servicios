using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CajaEgreso : BaseIntegraEntity
    {
        public int? IdCajaPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public int? IdFur { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public int? IdCajaEgresoAprobado { get; set; }
        public bool EsEnviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdPersonalResponsable { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int? IdComprobantePago { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }
    }
}
