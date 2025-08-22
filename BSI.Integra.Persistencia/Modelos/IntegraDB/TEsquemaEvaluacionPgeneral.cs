using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEsquemaEvaluacionPgeneral
    {
        public TEsquemaEvaluacionPgeneral()
        {
            TEsquemaEvaluacionPgeneralDetalles = new HashSet<TEsquemaEvaluacionPgeneralDetalle>();
            TEsquemaEvaluacionPgeneralModalidads = new HashSet<TEsquemaEvaluacionPgeneralModalidad>();
            TEsquemaEvaluacionPgeneralProveedors = new HashSet<TEsquemaEvaluacionPgeneralProveedor>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_EsquemaEvaluacion
        /// </summary>
        public int IdEsquemaEvaluacion { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
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
        /// Fecha de inicio de la vigencia del esquema
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha de fin de la vigencia del esquema
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// Tipo de esquema (Normal o Predeterminado)
        /// </summary>
        public bool? EsquemaPredeterminado { get; set; }

        public virtual TEsquemaEvaluacion IdEsquemaEvaluacionNavigation { get; set; } = null!;
        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual ICollection<TEsquemaEvaluacionPgeneralDetalle> TEsquemaEvaluacionPgeneralDetalles { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralModalidad> TEsquemaEvaluacionPgeneralModalidads { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralProveedor> TEsquemaEvaluacionPgeneralProveedors { get; set; }
    }
}
