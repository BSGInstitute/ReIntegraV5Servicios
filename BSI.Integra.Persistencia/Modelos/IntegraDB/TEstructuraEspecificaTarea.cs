using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEstructuraEspecificaTarea
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key de T_EstructuraEspecifica
        /// </summary>
        public int IdEstructuraEspecifica { get; set; }
        /// <summary>
        /// Id de la tarea del alumno
        /// </summary>
        public int IdTarea { get; set; }
        /// <summary>
        /// Nombre de la tarea
        /// </summary>
        public string NombreTarea { get; set; } = null!;
        /// <summary>
        /// Es el orden del capitulo al cual pertenece la tarea
        /// </summary>
        public int OrdenCapitulo { get; set; }
        /// <summary>
        /// Es foreing key de T_DocumentoSeccionPw
        /// </summary>
        public int IdDocumentoSeccionPw { get; set; }
        /// <summary>
        /// Para saber si el registro fue eliminado de forma logica
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
        /// Tarea
        /// </summary>
        public string? Tarea { get; set; }
    }
}
