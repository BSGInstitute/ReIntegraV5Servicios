using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena la realacion de  Criterios y Lineamientos de Evaluacion
    /// </summary>
    public partial class TCriterioLineamientoDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key de T_CriterioEvaluacion
        /// </summary>
        public int IdCriterioLineamiento { get; set; }
        /// <summary>
        /// Es foreing key de T_LineamientoEvaluacion
        /// </summary>
        public int IdLineamientoEvaluacion { get; set; }
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

        public virtual TCriterioLineamiento IdCriterioLineamientoNavigation { get; set; } = null!;
        public virtual TLineamientoEvaluacion IdLineamientoEvaluacionNavigation { get; set; } = null!;
    }
}
