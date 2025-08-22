using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Es el registro de los documentos
    /// </summary>
    public partial class TDocumento
    {
        /// <summary>
        /// Clave primaria de la tabla, es autoincrementable
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del documento 
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion del documento
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Identificador de la tabla de origen
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
