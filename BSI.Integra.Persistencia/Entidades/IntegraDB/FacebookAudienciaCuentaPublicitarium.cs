using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookAudienciaCuentaPublicitarium : BaseIntegraEntity
    {
        public int IdFacebookAudiencia { get; set; }

        public int IdFacebookCuentaPublicitaria { get; set; }

        public string Subtipo { get; set; } = null!;

        public string Origen { get; set; } = null!;

        public int? IdConjuntoListaDetalle { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
