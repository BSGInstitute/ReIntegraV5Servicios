using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEsquemaEvaluacion
    {
        public TEsquemaEvaluacion()
        {
            TEsquemaEvaluacionDetalles = new HashSet<TEsquemaEvaluacionDetalle>();
            TEsquemaEvaluacionPgenerals = new HashSet<TEsquemaEvaluacionPgeneral>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Llave foranea con la tabla T_FormaCalculoEvaluacion
        /// </summary>
        public int IdFormaCalculoEvaluacion { get; set; }
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
        public bool? EsModulo { get; set; }
        /// <summary>
        /// Clave foránea de la tabla pla.T_ModalidadCurso
        /// </summary>
        public int? IdModalidadCurso { get; set; }

        public virtual TFormaCalculoEvaluacion IdFormaCalculoEvaluacionNavigation { get; set; } = null!;
        public virtual ICollection<TEsquemaEvaluacionDetalle> TEsquemaEvaluacionDetalles { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneral> TEsquemaEvaluacionPgenerals { get; set; }
    }
}
