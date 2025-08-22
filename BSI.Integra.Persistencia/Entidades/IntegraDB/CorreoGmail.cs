using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CorreoGmail : BaseIntegraEntity
    {
        public long GmailCorreoId { get; set; }
        public int IdGmailFolder { get; set; }
        public string? Asunto { get; set; }
        public DateTime Fecha { get; set; }
        public string CuerpoHtml { get; set; } = null!;
        public bool EsLeido { get; set; }
        public string? NombreRemitente { get; set; }
        public string? EmailRemitente { get; set; }
        public string Destinatarios { get; set; } = null!;
        public string? EmailConCopiaOculta { get; set; }
        public string? EmailConCopia { get; set; }
        public bool AplicaCrearOportunidad { get; set; }
        public bool CumpleCriterioCrearOportunidad { get; set; }
        public bool SeCreoOportunidad { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public bool? EsDesuscritoCorrectamente { get; set; }
        public bool? EsMarcadoDesuscrito { get; set; }
        public bool? EsDescartado { get; set; }
        public int? IdPersonal { get; set; }

    }
}
