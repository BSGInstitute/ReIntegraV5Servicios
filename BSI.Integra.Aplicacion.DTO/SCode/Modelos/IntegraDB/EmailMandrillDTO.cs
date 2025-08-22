using Mandrill.Models;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaEmailMandrillDTO
    {
        public string Asunto { get; set; } = " ";
        public string CuerpoHTML { get; set; } = " ";
        public List<EmailAttachment> ListaArchivosAdjuntos { get; set; } = new List<EmailAttachment>();
    }
    public class PlantillaWhatsAppCalculadoDTO
    {
        public string Descripcion { get; set; }
        public string Plantilla { get; set; } = "";
        public List<DatoPlantillaWhatsAppDTO> ListaEtiquetas { get; set; } = new List<DatoPlantillaWhatsAppDTO>();
    }
    public class PlantillaWhatsAppAccesosDTO
    {
        public string Plantilla { get; set; } = "";
        public List<DatoPlantillaWhatsAppDTO> ListaEtiquetas { get; set; } = new List<DatoPlantillaWhatsAppDTO>();
        public DatosAlumnoOportunidadDTO DatoAlumno { get; set; } = new DatosAlumnoOportunidadDTO();
    }
    public class PlantillaSmsCalculadoDTO
    {
        public string Cuerpo { get; set; } = "";
    }
}
