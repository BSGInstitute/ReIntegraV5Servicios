using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCriterioEvaluacion
    {
        public TCriterioEvaluacion()
        {
            TCriterioEvaluacionModalidadCursos = new HashSet<TCriterioEvaluacionModalidadCurso>();
            TCriterioEvaluacionTipoPersonas = new HashSet<TCriterioEvaluacionTipoPersona>();
            TCriterioEvaluacionTipoProgramas = new HashSet<TCriterioEvaluacionTipoPrograma>();
            TEsquemaEvaluacionDetalles = new HashSet<TEsquemaEvaluacionDetalle>();
            TEsquemaEvaluacionPgeneralDetalles = new HashSet<TEsquemaEvaluacionPgeneralDetalle>();
            TParametroEvaluacions = new HashSet<TParametroEvaluacion>();
        }

        /// <summary>
        /// Id de Criterios
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del criterio de evaluacion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Foreing Key de T_CriterioEvaluacionCategoria
        /// </summary>
        public int IdCriterioEvaluacionCategoria { get; set; }
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
        /// Llave foranea con la tabla T_FormaCalculoEvaluacion, Forma de calculo del criterio si existiera mas de 1
        /// </summary>
        public int? IdFormaCalculoEvaluacion { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_FormaCalificacionEvaluacion, Forma de calificacion del criterio
        /// </summary>
        public int? IdFormaCalificacionEvaluacion { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_FormaCalificacionEvaluacion, Forma de calculo de la nota de los parametros
        /// </summary>
        public int? IdFormaCalculoEvaluacionParametro { get; set; }

        public virtual TCriterioEvaluacionCategorium IdCriterioEvaluacionCategoriaNavigation { get; set; } = null!;
        public virtual TFormaCalificacionEvaluacion? IdFormaCalificacionEvaluacionNavigation { get; set; }
        public virtual ICollection<TCriterioEvaluacionModalidadCurso> TCriterioEvaluacionModalidadCursos { get; set; }
        public virtual ICollection<TCriterioEvaluacionTipoPersona> TCriterioEvaluacionTipoPersonas { get; set; }
        public virtual ICollection<TCriterioEvaluacionTipoPrograma> TCriterioEvaluacionTipoProgramas { get; set; }
        public virtual ICollection<TEsquemaEvaluacionDetalle> TEsquemaEvaluacionDetalles { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralDetalle> TEsquemaEvaluacionPgeneralDetalles { get; set; }
        public virtual ICollection<TParametroEvaluacion> TParametroEvaluacions { get; set; }
    }
}
