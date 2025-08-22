using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PGeneralTipoDescuento : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool? FlagPromocion { get; set; }
    }
}
