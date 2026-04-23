using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas
{
    /// <summary>DTO de listado de actividad Bot IA con numeros asociados.</summary>
    public class ChatbotActividadBotIAListadoDTO
    {
        public int          IdChatbotActividad      { get; set; }
        public string       NombreChatbotActividad  { get; set; }
        public int          IdChatbotEsquema        { get; set; }
        public string       NombreChatbotEsquema    { get; set; }
        public int          IdMedioComunicacion     { get; set; }
        public string       NombreMedioComunicacion { get; set; }
        public bool         Estado                  { get; set; }
        public List<string> Numeros                 { get; set; } = new();
    }

    /// <summary>DTO interno para obtener los numeros vinculados a actividades.</summary>
    public class ChatbotActividadBotIANumeroDTO
    {
        public int    IdChatbotActividad { get; set; }
        public string NumeroWhatsApp     { get; set; }
    }

    /// <summary>DTO para insertar una nueva actividad Bot IA.</summary>
    public class InsertarChatbotActividadBotIADTO
    {
        [Required] public string       Nombre              { get; set; }
        [Required] public int          IdChatbotEsquema    { get; set; }
        [Required] public int          IdMedioComunicacion { get; set; }
                   public bool         Estado              { get; set; }
                   public List<string> Numeros             { get; set; } = new();
    }

    /// <summary>DTO para actualizar una actividad Bot IA existente.</summary>
    public class ActualizarChatbotActividadBotIADTO
    {
        [Required] public int          Id                  { get; set; }
        [Required] public string       Nombre              { get; set; }
        [Required] public int          IdChatbotEsquema    { get; set; }
        [Required] public int          IdMedioComunicacion { get; set; }
                   public bool         Estado              { get; set; }
                   public List<string> Numeros             { get; set; } = new();
    }

    /// <summary>DTO para eliminar una actividad Bot IA.</summary>
    public class EliminarChatbotActividadBotIADTO
    {
        [Required] public int Id { get; set; }
    }

    /// <summary>DTO para el catalogo de medios de comunicacion disponibles.</summary>
    public class MedioComunicacionDTO
    {
        [JsonProperty("Id")]
        public int    IdMedioComunicacion     { get; set; }

        [JsonProperty("Nombre")]
        public string NombreMedioComunicacion { get; set; }
    }
}
