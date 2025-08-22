using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SemaforoFinanciero : BaseIntegraEntity
    {
        public int IdPais { get; set; }
        public bool Activo { get; set; }
        public List<SemaforoFinancieroDetalle>? Detalle { get; set; } = new List<SemaforoFinancieroDetalle>();
    }
}
