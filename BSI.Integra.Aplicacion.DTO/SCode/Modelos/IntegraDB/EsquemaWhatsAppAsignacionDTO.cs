using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    // =============================================
    // CATÁLOGOS GLOBALES
    // =============================================

    /// <summary>
    /// DTO para Mensaje Exacto (palabras clave)
    /// </summary>
    public class MensajeExactoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    /// <summary>
    /// DTO para request de insertar/actualizar Mensaje Exacto
    /// </summary>
    public class MensajeExactoRequestDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    /// <summary>
    /// DTO para Fase del catálogo global
    /// </summary>
    public class FaseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    /// <summary>
    /// DTO para request de insertar/actualizar Fase
    /// </summary>
    public class FaseRequestDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    /// <summary>
    /// DTO para Perfil del catálogo global
    /// </summary>
    public class PerfilDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    /// <summary>
    /// DTO para request de insertar/actualizar Perfil
    /// </summary>
    public class PerfilRequestDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    // =============================================
    // ESQUEMA PRINCIPAL
    // =============================================

    /// <summary>
    /// DTO para listado de esquemas
    /// </summary>
    public class EsquemaWhatsAppAsignacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Restricciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    /// <summary>
    /// DTO para request de insertar/actualizar esquema
    /// </summary>
    public class EsquemaWhatsAppAsignacionRequestDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Restricciones { get; set; }
    }

    // =============================================
    // LECTURA DE MENSAJES
    // =============================================

    /// <summary>
    /// DTO para lectura de mensaje (clasificación)
    /// </summary>
    public class EsquemaLecturaMensajeDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public string ClasificacionTipoMensaje { get; set; } = null!;
        public string? PromptLectura { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int CantidadMensajesExactos { get; set; }
    }

    /// <summary>
    /// DTO detallado de lectura de mensaje con mensajes exactos incluidos
    /// </summary>
    public class EsquemaLecturaMensajeDetalleDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public string ClasificacionTipoMensaje { get; set; } = null!;
        public string? PromptLectura { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<MensajeExactoAsociadoDTO> MensajesExactos { get; set; } = new List<MensajeExactoAsociadoDTO>();
    }

    /// <summary>
    /// DTO para mensajes exactos asociados a una lectura de mensaje
    /// </summary>
    public class MensajeExactoAsociadoDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacionLecturaMensaje { get; set; }
        public int IdMensajeExacto { get; set; }
        public string NombreMensajeExacto { get; set; } = null!;
    }

    /// <summary>
    /// DTO para request de insertar/actualizar lectura de mensaje
    /// </summary>
    public class EsquemaLecturaMensajeRequestDTO
    {
        public int? Id { get; set; }
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public string ClasificacionTipoMensaje { get; set; } = null!;
        public string? PromptLectura { get; set; }
        public List<int> MensajesExactosIds { get; set; } = new List<int>();
    }

    // =============================================
    // INTERPRETAR INFORMACIÓN
    // =============================================

    /// <summary>
    /// DTO para interpretación de información
    /// </summary>
    public class EsquemaInterpretarInformacionDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int CantidadSubcategorias { get; set; }
    }

    /// <summary>
    /// DTO detallado de interpretación con clasificaciones y subcategorías
    /// </summary>
    public class EsquemaInterpretarInformacionDetalleDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<ClasificacionAsociadaDTO> Clasificaciones { get; set; } = new List<ClasificacionAsociadaDTO>();
        public List<SubcategoriaDetalleDTO> Subcategorias { get; set; } = new List<SubcategoriaDetalleDTO>();
    }

    /// <summary>
    /// DTO para clasificaciones asociadas a interpretación
    /// </summary>
    public class ClasificacionAsociadaDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacionInterpretarInformacion { get; set; }
        public int IdEsquemaWhatsAppAsignacionLecturaMensaje { get; set; }
        public string ClasificacionTipoMensaje { get; set; } = null!;
    }

    /// <summary>
    /// DTO detallado de subcategoría con fases y perfiles
    /// </summary>
    public class SubcategoriaDetalleDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacionInterpretarInformacion { get; set; }
        public string Nombre { get; set; } = null!;
        public bool TieneFaseMaxima { get; set; }
        public bool TienePerfil { get; set; }
        public List<FaseAsociadaDTO> Fases { get; set; } = new List<FaseAsociadaDTO>();
        public List<PerfilAsociadoDTO> Perfiles { get; set; } = new List<PerfilAsociadoDTO>();
    }

    /// <summary>
    /// DTO para fases asociadas a subcategoría
    /// </summary>
    public class FaseAsociadaDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacionSubcategoria { get; set; }
        public int IdFase { get; set; }
        public string NombreFase { get; set; } = null!;
    }

    /// <summary>
    /// DTO para perfiles asociados a subcategoría
    /// </summary>
    public class PerfilAsociadoDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacionSubcategoria { get; set; }
        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; } = null!;
    }

    /// <summary>
    /// DTO para subcategoría en request (sin IDs de asociaciones)
    /// </summary>
    public class SubcategoriaRequestDTO
    {
        public string Nombre { get; set; } = null!;
        public bool TieneFaseMaxima { get; set; }
        public bool TienePerfil { get; set; }
        public List<int> FasesIds { get; set; } = new List<int>();
        public List<int> PerfilesIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// DTO para request de insertar/actualizar interpretación de información
    /// </summary>
    public class EsquemaInterpretarInformacionRequestDTO
    {
        public int? Id { get; set; }
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public string Nombre { get; set; } = null!;
        public List<int> ClasificacionesIds { get; set; } = new List<int>();
        public List<SubcategoriaRequestDTO> Subcategorias { get; set; } = new List<SubcategoriaRequestDTO>();
    }

    // =============================================
    // RESPUESTAS
    // =============================================

    /// <summary>
    /// DTO para matriz de respuestas
    /// </summary>
    public class EsquemaRespuestaDTO
    {
        public int Id { get; set; }
        public int IdEsquemaWhatsAppAsignacionLecturaMensaje { get; set; }
        public string ClasificacionTipoMensaje { get; set; } = null!;
        public int? IdEsquemaWhatsAppAsignacionSubcategoria { get; set; }
        public string? NombreSubcategoria { get; set; }
        public string? ParametrosRespuesta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    /// <summary>
    /// DTO para request de insertar respuesta
    /// </summary>
    public class EsquemaRespuestaRequestDTO
    {
        public int IdEsquemaWhatsAppAsignacionLecturaMensaje { get; set; }
        public int? IdEsquemaWhatsAppAsignacionSubcategoria { get; set; }
        public string? ParametrosRespuesta { get; set; }
    }

    /// <summary>
    /// DTO para actualizar respuesta (solo parámetros)
    /// </summary>
    public class EsquemaRespuestaActualizarDTO
    {
        public int Id { get; set; }
        public string? ParametrosRespuesta { get; set; }
    }

    // =============================================
    // ACTIVIDAD (VINCULACIÓN)
    // =============================================

    /// <summary>
    /// DTO para actividad (vinculación de número con esquema)
    /// </summary>
    public class EsquemaActividadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdAsistenteMarketingWhatsAppAsignacion { get; set; }
        public string NumeroWhatsApp { get; set; } = null!;
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public string NombreEsquema { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    /// <summary>
    /// DTO para request de insertar/actualizar actividad
    /// </summary>
    public class EsquemaActividadRequestDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdAsistenteMarketingWhatsAppAsignacion { get; set; }
        public int IdEsquemaWhatsAppAsignacion { get; set; }
        public bool? Estado { get; set; }
    }

    /// <summary>
    /// DTO para actualizar solo el estado de actividad (switcher)
    /// </summary>
    public class EsquemaActividadEstadoDTO
    {
        public int Id { get; set; }
        public bool Estado { get; set; }
    }
}
