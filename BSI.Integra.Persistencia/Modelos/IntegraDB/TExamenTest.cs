using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExamenTest
    {
        public TExamenTest()
        {
            TPuestoTrabajoPuntajeCalificacions = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
        }

        /// <summary>
        /// Clave primario del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Nombre Abreviado del registro
        /// </summary>
        public string? NombreAbreviado { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Indica si el examen ha sido calificado por el postulante
        /// </summary>
        public bool EsCalificadoPorPostulante { get; set; }
        /// <summary>
        /// Define si se va a mostrar los componentes de Evaluacion todo junto en un solo Examen
        /// </summary>
        public bool MostrarEvaluacionAgrupado { get; set; }
        /// <summary>
        /// Define si la evaluacion se mostrara por grupos
        /// </summary>
        public bool MostrarEvaluacionPorGrupo { get; set; }
        /// <summary>
        /// Define si la evaluacion se muestra componente por componente
        /// </summary>
        public bool MostrarEvaluacionPorComponente { get; set; }
        /// <summary>
        /// Indica si se va a utilizar Centil en la evaluacion
        /// </summary>
        public bool RequiereCentil { get; set; }
        /// <summary>
        /// Clave Foranea de gp.T_FormulaPuntaje
        /// </summary>
        public int? IdFormulaPuntaje { get; set; }
        /// <summary>
        /// Define si se Califica la evaluacion o es una evaluacion sin calificar
        /// </summary>
        public bool CalificarEvaluacion { get; set; }
        /// <summary>
        /// Indica si la Calificacion es Agrupada por Componentes o Independiente por cada Componente
        /// </summary>
        public bool EsCalificacionAgrupada { get; set; }
        /// <summary>
        /// Indica el Valor por el cual se va a multiplicar al momento de obtener una calificacion
        /// </summary>
        public decimal? Factor { get; set; }
        /// <summary>
        /// Primary -Key de tabla gp.T_EvaluacionCategoria
        /// </summary>
        public int? IdEvaluacionCategoria { get; set; }

        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacions { get; set; }
    }
}
