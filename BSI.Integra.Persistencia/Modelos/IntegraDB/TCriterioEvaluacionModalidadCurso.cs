using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCriterioEvaluacionModalidadCurso
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key de T_CriterioEvaluacion
        /// </summary>
        public int IdCriterioEvaluacion { get; set; }
        /// <summary>
        /// Foreing Key de T_ModalidadCurso
        /// </summary>
        public int IdModalidadCurso { get; set; }
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

        public virtual TCriterioEvaluacion IdCriterioEvaluacionNavigation { get; set; } = null!;
        public virtual TModalidadCurso IdModalidadCursoNavigation { get; set; } = null!;
    }
}
