using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProcesoSeleccionPuntajeCalificacion
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de gp.T_ConfiguracionAsignacionEvaluacion
        /// </summary>
        public int IdProcesoSeleccion { get; set; }
        /// <summary>
        /// FK e gp.T_ExamenTest
        /// </summary>
        public int? IdExamenTest { get; set; }
        /// <summary>
        /// FK de gp.T_GrupoComponenteEvaluacion
        /// </summary>
        public int? IdGrupoComponenteEvaluacion { get; set; }
        /// <summary>
        /// FK de gp.T_Examen
        /// </summary>
        public int? IdExamen { get; set; }
        /// <summary>
        /// Verififica si el puntaje califica por Centil o por Puntaje directo
        /// </summary>
        public bool CalificaPorCentil { get; set; }
        /// <summary>
        /// Puntaje minimo con el cual se califica un componente,grupo de componentes o evaluacion
        /// </summary>
        public decimal? PuntajeMinimo { get; set; }
        /// <summary>
        /// FK de la tabla gp.T_ProcesoSeleccionRango
        /// </summary>
        public int? IdProcesoSeleccionRango { get; set; }
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
        /// Indica si la calificacion debe de aplicarse o no
        /// </summary>
        public bool EsCalificable { get; set; }
    }
}
