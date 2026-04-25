using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las reseñas de Google obtenidas vía Places API, vinculadas a la configuración de sede correspondiente, con control de visibilidad en frontend y auditoría completa
    /// </summary>
    public partial class TGoogleResena
    {
        /// <summary>
        /// Identificador único de la reseña (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK hacia la configuración de sede de Google Places (mkt.T_GooglePlacesConfiguracion)
        /// </summary>
        public int IdGooglePlacesConfiguracion { get; set; }
        /// <summary>
        /// Identificador único de la reseña en Google Places API, clave para deduplicación (formato: places/{placeId}/reviews/{reviewId})
        /// </summary>
        public string? IdentificadorResena { get; set; }
        /// <summary>
        /// Nombre del autor de la reseña en Google Maps
        /// </summary>
        public string NombreAutor { get; set; } = null!;
        /// <summary>
        /// URI de la foto de perfil del autor en Google
        /// </summary>
        public string? FotoAutor { get; set; }
        /// <summary>
        /// URI del perfil del autor en Google Maps
        /// </summary>
        public string? UriAutor { get; set; }
        /// <summary>
        /// Calificación de la reseña en escala 1 a 5 estrellas
        /// </summary>
        public int Valoracion { get; set; }
        /// <summary>
        /// Texto completo de la reseña escrita por el usuario
        /// </summary>
        public string? TextoResena { get; set; }
        /// <summary>
        /// Código de idioma de la reseña (ej: es, en)
        /// </summary>
        public string? IdiomaResena { get; set; }
        /// <summary>
        /// Fecha de publicación de la reseña en Google (publishTime)
        /// </summary>
        public DateTime? FechaResena { get; set; }
        /// <summary>
        /// Descripción de tiempo relativo de la reseña proporcionada por Google (ej: hace 2 semanas)
        /// </summary>
        public string? DescripcionTiempoRelativo { get; set; }
        /// <summary>
        /// URI de la reseña en Google Maps
        /// </summary>
        public string? UriGoogleMaps { get; set; }
        /// <summary>
        /// Controla si la reseña se muestra en el Homepage/Frontend (1=Visible, 0=Oculta)
        /// </summary>
        public bool Mostrar { get; set; }
        /// <summary>
        /// Estado lógico del registro (1=Activo, 0=Eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó por última vez el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de control de versiones (timestamp automático)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGooglePlacesConfiguracion IdGooglePlacesConfiguracionNavigation { get; set; } = null!;
    }
}
