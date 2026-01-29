using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena comentarios, documentos adjuntos y respuestas asociadas a las solicitudes de tipos de descuento
    /// </summary>
    public partial class TTipoDescuentoSolicitudDetalle
    {
        /// <summary>
        /// Identificador único del detalle de solicitud
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la solicitud de descuento asociada
        /// </summary>
        public int IdTipoDescuentoSolicitud { get; set; }
        /// <summary>
        /// Comentario o justificación proporcionada en la solicitud de descuento
        /// </summary>
        public string? ComentarioSolicitud { get; set; }
        /// <summary>
        /// Nombre del archivo adjunto a la solicitud (si aplica)
        /// </summary>
        public string? NombreArchivoSolicitud { get; set; }
        /// <summary>
        /// Tipo MIME del archivo de solicitud (application/pdf, image/jpeg, etc.)
        /// </summary>
        public string? ContentTypeSolicitud { get; set; }
        /// <summary>
        /// Comentario de respuesta del aprobador sobre la solicitud
        /// </summary>
        public string? ComentarioRespuesta { get; set; }
        /// <summary>
        /// Nombre del archivo adjunto en la respuesta (si aplica)
        /// </summary>
        public string? NombreArchivoRespuesta { get; set; }
        /// <summary>
        /// Tipo MIME del archivo de respuesta (application/pdf, image/jpeg, etc.)
        /// </summary>
        public string? ContentTypeRespuesta { get; set; }
        /// <summary>
        /// Indicador de estado del registro (1 = Activo, 0 = Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario del sistema que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario del sistema que realizó la última modificación del registro
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
        /// Control de versión para concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TTipoDescuentoSolicitud IdTipoDescuentoSolicitudNavigation { get; set; } = null!;
    }
}
