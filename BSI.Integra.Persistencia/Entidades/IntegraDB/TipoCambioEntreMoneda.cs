using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoCambioEntreMoneda : BaseIntegraEntity
    {
       
        public string Nombre { get; set; } = null!;
    
        public decimal? TipoCambio { get; set; }
    
       
        public int? IdMigracion { get; set; }
    }
}
