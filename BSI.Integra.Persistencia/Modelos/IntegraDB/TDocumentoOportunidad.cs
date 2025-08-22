using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDocumentoOportunidad
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_Alumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// FK de T_Oportunidad
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Nombre del archivo subido
        /// </summary>
        public string NombreArchivo { get; set; } = null!;
        /// <summary>
        /// Ruta del archivo en el blobstorage
        /// </summary>
        public string Ruta { get; set; } = null!;
        /// <summary>
        /// Comentario del archivo
        /// </summary>
        public string? Comentario { get; set; }
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
        /// <summary>
        /// FK de T_ClasificacionPersona
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// FK de T_DocumentoOportunidadTipo
        /// </summary>
        public int? IdDocumentoOportunidadTipo { get; set; }
    }
}
