using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena formularios de LinkedIn recibidos a travez de la Api de LeadsLinkedIn
    /// </summary>
    public partial class TLinkedInForm
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nombre del Formulario
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Descripcion del Formulario
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Headline del Formulario
        /// </summary>
        public string? Headline { get; set; }
        /// <summary>
        /// Preguntas del Formulario
        /// </summary>
        public string? Questions { get; set; }
        /// <summary>
        /// Registros tales registrados del Formulario
        /// </summary>
        public int? PaddingStart { get; set; }
        /// <summary>
        /// Estado del Formulario
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Formulario
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Formulario
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Formulario
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Formulario
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
