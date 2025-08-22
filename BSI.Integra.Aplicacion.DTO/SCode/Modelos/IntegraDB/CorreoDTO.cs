using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class CorreoInformacionOportunidadDTO
    {
        public int? IdActividadDetalle { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdClasificacionPersona { get; set; }
    }
    public class CorreoCabeceraDTO
    {
        public string? Remitente { get; set; }
        public string? Destinatario { get; set; }
        public string? Asunto { get; set; }
        public string Mensaje { get; set; }
        public string? DestinatarioCc { get; set; }
        public string? DestinatarioBcc { get; set; }
        public string? Usuario { get; set; }
        public string? GrupoEmail { get; set; }
        public bool? envioGrupo { get; set; }
    }
    public class CorreoAlumnoSpeechDTO
    {
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string EmailBody { get; set; }
    }
    public class CorreoInteraccionesAlumnoDTO
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Categoria { get; set; }
        public string Asunto { get; set; }
        public string Estado { get; set; }
        public string CorreoReceptor { get; set; }
        public string CorreoRemitente { get; set; }
        public string Remitente { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string MessageId { get; set; }
        public int Orden { get; set; }
    }
    public class EnviarMensajeGmailDTO
    {
        public int? IdActividadDetalle { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public string? Remitente { get; set; }
        public string? Destinatario { get; set; }
        public string? Asunto { get; set; }
        public string Mensaje { get; set; }
        public string? DestinatarioCc { get; set; }
        public string? DestinatarioBcc { get; set; }
        public string? Usuario { get; set; }
        public string? GrupoEmail { get; set; }
        public bool? envioGrupo { get; set; }
        public int IdAsesor { get; set; }
        public IList<IFormFile>? Files { get; set; }
    }
    public class CorreoAlumnoVentasDTO
    {
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string EmailBody { get; set; }
        public string Fecha { get; set; }
    }
    public class CorreosGrupoDTO
    {
        public int idCentroCosto { get; set; }
        public List<int> paquete { get; set; }
        public List<int> estado { get; set; }
        public List<int> subEstado { get; set; }
    }


    public class DatosOportunidadAccesosPortalDTO
    {
        public int idAlumno { get; set; }
        public int idPersonalAsignado { get; set; }
        public int idOportunidad { get; set; }
        public int idCentroCosto { get; set; }
        public string emailAsesor { get; set; }
        public string emailAlumno { get; set; }
        public string codigoAlumno { get; set; }
        public string usuario { get; set; }
    }

    public class AccesosMoodleDTO
    {
        public int idAlumno { get; set; }
        public string idMoodle { get; set; }
        public string usuarioMoodle { get; set; }
        public string passwordMoodle { get; set; }
    }

    public class RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO
    {
        public int? idPlantilla { get; set; }
        public string? numero { get; set; }

    }

    public class DatoPlantillaCorreoDTO
    {
        public string codigo { get; set; }
        public string texto { get; set; }
    }
}
