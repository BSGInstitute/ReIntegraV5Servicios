using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las credenciales de acceso a la Google Places API, incluyendo el API Key necesario para autenticar las consultas de reseñas por sede
    /// </summary>
    public partial class TGooglePlacesCredencialApi
    {
        /// <summary>
        /// Identificador único de la credencial (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del servicio al que pertenece la credencial (ej: BSG Places API)
        /// </summary>
        public string NombreServicio { get; set; } = null!;
        /// <summary>
        /// API Key de Google Cloud para autenticar llamadas a la Places API (formato: AIzaSy...)
        /// </summary>
        public string ApiKey { get; set; } = null!;
        /// <summary>
        /// Descripción adicional de la credencial o su uso
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Client ID de la credencial OAuth 2.0 de Google Cloud para autenticar vía Business Profile API
        /// </summary>
        public string? Oauth2ClientId { get; set; }
        /// <summary>
        /// Client Secret de la credencial OAuth 2.0 de Google Cloud
        /// </summary>
        public string? Oauth2ClientSecret { get; set; }
        /// <summary>
        /// Refresh Token OAuth 2.0 para obtener Access Tokens automáticamente sin intervención humana
        /// </summary>
        public string? Oauth2RefreshToken { get; set; }
        /// <summary>
        /// Estado lógico del registro (1=Activo, 0=Inactivo)
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
    }
}
