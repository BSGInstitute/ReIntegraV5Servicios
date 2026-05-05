using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las reseñas de Facebook obtenidas vía Graph API, con control de visibilidad en el frontend y auditoría completa
    /// </summary>
    public partial class TFacebookResena
    {
        /// <summary>
        /// Identificador único de la reseña (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK hacia la configuración de la página de Facebook (mkt.T_FacebookConfiguracion)
        /// </summary>
        public int IdFacebookConfiguracion { get; set; }
        /// <summary>
        /// ID único de la historia/open graph story en Facebook (clave para deduplicación)
        /// </summary>
        public string? IdentificadorHistoria { get; set; }
        /// <summary>
        /// Indica si el usuario recomienda (1) o no recomienda (0) el negocio
        /// </summary>
        public bool? Recomienda { get; set; }
        /// <summary>
        /// Indica si la reseña tiene texto escrito
        /// </summary>
        public bool? TieneTexto { get; set; }
        /// <summary>
        /// Texto completo de la reseña escrita por el usuario
        /// </summary>
        public string? TextoResena { get; set; }
        /// <summary>
        /// Fecha de creación de la reseña en Facebook (formato UTC)
        /// </summary>
        public DateTime? FechaResena { get; set; }
        /// <summary>
        /// Controla si la reseña se muestra en el Homepage/Frontend
        /// </summary>
        public bool? Mostrar { get; set; }
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

        public virtual TFacebookConfiguracion IdFacebookConfiguracionNavigation { get; set; } = null!;
    }
}
