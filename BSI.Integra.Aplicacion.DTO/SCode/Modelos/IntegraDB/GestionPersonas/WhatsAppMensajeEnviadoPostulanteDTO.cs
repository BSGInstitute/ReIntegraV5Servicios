
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class WhatsAppMensajeEnviadoPostulanteDTO
    {
        public int Id { get; set; }
        public string? WaTo { get; set; }
        public string? WaId { get; set; }
        public string? WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string? WaRecipientType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaLink { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPostulante { get; set; }
        public string usuario { get; set; }
        public List<DatosPlantillaWhatsAppDTO>? datosPlantillaWhatsApp { get; set; }
    }

    public class WhatsAppMensajesPostulanteDTO
    {
        public string Numero { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int IdPais { get; set; }
        public int IdPostulante { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePostulante { get; set; }
    }

    public class GestionPersonasPlantillaWhatsAppDTO
    {
        public int IdPlantilla { get; set; }
        public int IdPersonal { get; set; }
        public int IdPostulante { get; set; }
        public string Usuario { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class WhatsAppHistorialMensajesPostulanteDTO
    {
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public int IdPais { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPostulante { get; set; }
        public int? Registro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePersonal { get; set; }
    }
}
