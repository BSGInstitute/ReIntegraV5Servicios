using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExaman
    {
        public TExaman()
        {
            TExamenAsignadoEvaluadors = new HashSet<TExamenAsignadoEvaluador>();
            TPuestoTrabajoPuntajeCalificacions = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
        }

        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del examen
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Titulo del examen
        /// </summary>
        public string? Titulo { get; set; }
        /// <summary>
        /// Flag si el examen requiere minutos
        /// </summary>
        public bool? RequiereTiempo { get; set; }
        /// <summary>
        /// Duracion en minutos del examen
        /// </summary>
        public int? DuracionMinutos { get; set; }
        /// <summary>
        /// Instrucciones del examen en formato HTML
        /// </summary>
        public string? Instrucciones { get; set; }
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
        /// LLave foranea con la tabla T_ExamenTest
        /// </summary>
        public int? IdExamenTest { get; set; }
        /// <summary>
        /// LLave foranea con la tabla T_ExamenConfiguracionFormato
        /// </summary>
        public int? IdExamenConfiguracionFormato { get; set; }
        /// <summary>
        /// LLave foranea con la tabla T_ExamenComportamiento
        /// </summary>
        public int? IdExamenComportamiento { get; set; }
        /// <summary>
        /// LLave foranea con la tabla T_ExamenConfigurarResultado
        /// </summary>
        public int? IdExamenConfigurarResultado { get; set; }
        /// <summary>
        /// LLave foranea con la tabla T_CriterioEvaluacionProceso
        /// </summary>
        public int? IdCriterioEvaluacionProceso { get; set; }
        /// <summary>
        /// LLave foranea con la tabla T_GrupoComponenteEvaluacion
        /// </summary>
        public int? IdGrupoComponenteEvaluacion { get; set; }
        /// <summary>
        /// true: si requiere centil false: si no requiere
        /// </summary>
        public bool? RequiereCentil { get; set; }
        /// <summary>
        /// Fk T_FormulaPuntaje
        /// </summary>
        public int? IdFormulaPuntaje { get; set; }
        /// <summary>
        /// Indica el Valor por el cual se va a multiplicar al momento de obtener una calificacion
        /// </summary>
        public decimal? Factor { get; set; }
        /// <summary>
        /// FK de T_CentroCosto para cursos de recuperación
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Cantidad de Días Configurados para Acceso Temporal de Postulantes a Nueva Aula Virtual
        /// </summary>
        public int? CantidadDiasAcceso { get; set; }

        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluadors { get; set; }
        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacions { get; set; }
    }
}
