using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ContactoConfiguracion : BaseIntegraEntity
    {
        public int? IdOrigen { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdTipoDatoRelacionado { get; set; }
    }
}
