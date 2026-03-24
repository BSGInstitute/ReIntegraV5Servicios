using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas
{
    /// <summary>DTO de listado de actividad Bot IA con numeros asociados.</summary>
    public class ChatbotActividadBotIAListadoDTO
    {
        public int          IdChatbotActividadBotIA     { get; set; }
        public string       NombreChatbotActividadBotIA { get; set; }
        public int          IdChatbotEsquema            { get; set; }
        public string       NombreChatbotEsquema        { get; set; }
        public string       Modulo                      { get; set; }
        public bool         Estado                      { get; set; }
        public List<string> Numeros                     { get; set; } = new();
    }

    /// <summary>DTO interno para obtener los numeros vinculados a actividades.</summary>
    public class ChatbotActividadBotIANumeroDTO
    {
        public int    IdChatbotActividadBotIA { get; set; }
        public string NumeroWhatsApp          { get; set; }
    }

    /// <summary>DTO para insertar una nueva actividad Bot IA.</summary>
    public class InsertarChatbotActividadBotIADTO
    {
        [Required] public string       Nombre           { get; set; }
        [Required] public int          IdChatbotEsquema { get; set; }
        [Required] public string       Modulo           { get; set; }
                   public bool         Estado           { get; set; }
                   public List<string> Numeros          { get; set; } = new();
    }

    /// <summary>DTO para actualizar una actividad Bot IA existente.</summary>
    public class ActualizarChatbotActividadBotIADTO
    {
        [Required] public int          Id               { get; set; }
        [Required] public string       Nombre           { get; set; }
        [Required] public int          IdChatbotEsquema { get; set; }
        [Required] public string       Modulo           { get; set; }
                   public bool         Estado           { get; set; }
                   public List<string> Numeros          { get; set; } = new();
    }

    /// <summary>DTO para eliminar una actividad Bot IA.</summary>
    public class EliminarChatbotActividadBotIADTO
    {
        [Required] public int Id { get; set; }
    }
}
