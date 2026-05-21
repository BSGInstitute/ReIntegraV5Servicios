using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena las versiones de formularios de evaluación para diferentes plataformas
    /// </summary>
    public partial class TVersionFormularioEvaluacionChatbot
    {
        public TVersionFormularioEvaluacionChatbot()
        {
            TFormularioAplicadoChatbots = new HashSet<TFormularioAplicadoChatbot>();
            TPreguntaEvaluacionChatbots = new HashSet<TPreguntaEvaluacionChatbot>();
        }

        /// <summary>
        /// Clave primaria (ID) de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo de la versión del formulario
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del formulario y su propósito
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Origen o plataforma del formulario (Portal, WhatsApp, etc.)
        /// </summary>
        public string Origen { get; set; } = null!;
        /// <summary>
        /// Número de versión actual del formulario
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Estado del registro (Activo = 1 / Inactivo = 0)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión de fila para concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Canal de comunicación del hilo (Portal Web, WhatsApp, etc.)
        /// </summary>
        public int? IdMedioComunicacion { get; set; }

        public virtual TMedioComunicacion? IdMedioComunicacionNavigation { get; set; }
        public virtual ICollection<TFormularioAplicadoChatbot> TFormularioAplicadoChatbots { get; set; }
        public virtual ICollection<TPreguntaEvaluacionChatbot> TPreguntaEvaluacionChatbots { get; set; }
    }
}
