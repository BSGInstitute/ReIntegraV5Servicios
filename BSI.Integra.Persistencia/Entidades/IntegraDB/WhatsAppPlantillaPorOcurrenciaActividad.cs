using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppPlantillaPorOcurrenciaActividad : BaseIntegraEntity
    {
        public int IdOcurrenciaActividad { get; set; }
        public int IdPlantilla { get; set; }
        public int NumeroDiasSinContacto { get; set; }
    }
}
