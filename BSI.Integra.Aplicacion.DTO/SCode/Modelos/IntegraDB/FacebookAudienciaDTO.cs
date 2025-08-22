using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FacebookAudienciaDTO
    {
        public int IdFiltroSegmento { get; set; }
        public string? FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Cuenta { get; set; }
        public string? Pais { get; set; }
        public string Usuario { get; set; }
        public List<FacebookAudienciaDatosAlumnoDTO> Alumnos { get; set; }

    }

    public class FacebookAudienciaBODTO
    {
        public int IdFiltroSegmento { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Subtipo { get; set; }
        public string RecursoArchivoCliente { get; set; }

    }
    public class IdCustomAudienceDTO
    {
        public string Id { get; set; }

    }
   
    public class DatosCiudadDTO
    {
        public int Id { get; set; }
        public string NombreCiudad { get; set; }
        public int IdPais { get; set; }
    }
    public class RespuestaFacebookDTO
    {
        public string audience_id { get; set; }
        public string session_id { get; set; }
        public string num_received { get; set; }
        public string num_invalid_entries { get; set; }
        public object invalid_entry_samples { get; set; }

    }
    public class FacebookAudienciaRespuestaApiDTO
    {
        public bool FlagAudiencia { get; set; }
        public bool FlagEmails { get; set; }
        public int EmailsRecibidos { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string MensajeError { get; set; }
    }
}
