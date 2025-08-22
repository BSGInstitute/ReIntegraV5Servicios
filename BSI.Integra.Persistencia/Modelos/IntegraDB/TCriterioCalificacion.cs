using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCriterioCalificacion
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del criterio de calificacion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Nomenclatura o sigla
        /// </summary>
        public string Sigla { get; set; } = null!;
        /// <summary>
        /// Estado de como va el documento
        /// </summary>
        public bool EstadoDocumento { get; set; }
        /// <summary>
        /// Estado si es un documento original
        /// </summary>
        public bool DocOriginal { get; set; }
        /// <summary>
        /// Estado si es un documento pago
        /// </summary>
        public bool DocPasarela { get; set; }
        /// <summary>
        /// Estado si es un documento entregado
        /// </summary>
        public bool DocPasCancelados { get; set; }
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
        /// Sistema Automatico Fecha creacion
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
