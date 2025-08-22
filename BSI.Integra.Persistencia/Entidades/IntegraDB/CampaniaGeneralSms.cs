using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CampaniaGeneralSms : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public TimeSpan? HoraEnvio { get; set; }
        public DateTime? FechaInicioEnvioSms { get; set; }


    }
}
