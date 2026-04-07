using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ParametrosEnviarMensajeDTO
    {
        public IList<IFormFile>? Files { get; set; }
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
        public string? GrupoEmail { get; set; }
    }
    public class ParametrosEnviarMensajePlaDTO
    {
        public IList<IFormFile>? Files { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdGestionContacto { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public string? Remitente { get; set; }
        public string? Destinatario { get; set; }
        public string? Asunto { get; set; }
        public string? Mensaje { get; set; }
        public string? DestinatarioCc { get; set; }
        public string? DestinatarioBcc { get; set; }
        public string? GrupoEmail { get; set; }
    }
    /// Autor: Joseph Llanque
    /// Fecha: 20/03/2026
    /// Versión: 1.0
    /// <summary>
    /// DTO de request para previsualizar un mensaje de planificación sin enviarlo.
    /// </summary>
    public class PreviewMensajePlaRequestDTO
    {
        public int IdPlantilla { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdClasificacionPersona { get; set; }
    }

    /// Autor: Joseph Llanque
    /// Fecha: 20/03/2026
    /// Versión: 1.0
    /// <summary>
    /// DTO de response con el asunto y cuerpo HTML de la plantilla procesada.
    /// </summary>
    public class PreviewMensajePlaResponseDTO
    {
        public string Asunto { get; set; }
        public string CuerpoHtml { get; set; }
    }

    public class PerfilProfesionalDTO
    {
        public int? IdAFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdTiempoExperiencia { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        public string? PrincipalResponsabilidad { get; set; }
        public int? IdAlumno { get; set; }


    }
}
