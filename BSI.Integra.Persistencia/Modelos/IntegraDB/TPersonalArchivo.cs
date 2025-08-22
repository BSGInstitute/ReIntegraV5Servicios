using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalArchivo
    {
        /// <summary>
        /// PK de Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre Original de archivo
        /// </summary>
        public string? NombreArchivo { get; set; }
        /// <summary>
        /// Ruta de Archivo en Blob storage
        /// </summary>
        public string? RutaArchivo { get; set; }
        /// <summary>
        /// Tipo de Archivo
        /// </summary>
        public string? MimeType { get; set; }
        /// <summary>
        /// Validación de archivo de tipo imagen
        /// </summary>
        public bool? EsImagen { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
