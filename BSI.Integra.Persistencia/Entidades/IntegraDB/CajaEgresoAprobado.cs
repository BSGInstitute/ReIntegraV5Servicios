using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CajaEgresoAprobado : BaseIntegraEntity
    {
        public int IdCaja { get; set; }
        public string CodigoRec { get; set; } = null!;
        public string Anho { get; set; } = null!;
        public string Detalle { get; set; } = null!;
        public string Observacion { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public DateTime FechaCreacionRegistro { get; set; }
    }
}
