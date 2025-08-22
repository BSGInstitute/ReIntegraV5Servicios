using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CampaniaGeneralWhatsApp : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public TimeSpan? HoraEnvio { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }


    }
}
