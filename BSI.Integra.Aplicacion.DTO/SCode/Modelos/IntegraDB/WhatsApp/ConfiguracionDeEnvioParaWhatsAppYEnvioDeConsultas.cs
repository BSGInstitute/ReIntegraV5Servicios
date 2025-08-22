using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class ConfiguracionDeEnvioParaWhatsAppYEnvioDeConsultas {
        public int Id { get; set; }
        public int? IdPlantilla { get; set; }
        public DateTime FechaDeEnvio { get; set; }
        public DateTime FechaFinDeEnvio { get; set; }
        public TimeSpan HoraDeEnvio { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public int IdFiltradoWhastApp { get; set; }
        public List<PersonalEncargadoDeEnvioDeConsultum> DatosParaEnvio { get; set; }
    }

}
