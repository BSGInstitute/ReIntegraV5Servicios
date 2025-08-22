using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TGrupoComponenteEvaluacion
    {
        public TGrupoComponenteEvaluacion()
        {
            TPuestoTrabajoPuntajeCalificacions = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
        }

        /// <summary>
        /// Primary key de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del grupo de componentes
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// nombre abreviado o parecido de componente
        /// </summary>
        public string? NombreAbreviado { get; set; }
        /// <summary>
        /// Indica si tiene Formula para calcular el puntaje
        /// </summary>
        public int? IdFormulaPuntaje { get; set; }
        /// <summary>
        /// Indica si requiere Centil
        /// </summary>
        public bool RequiereCentil { get; set; }
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
        /// Indica el Valor por el cual se va a multiplicar al momento de obtener una calificacion
        /// </summary>
        public decimal? Factor { get; set; }

        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacions { get; set; }
    }
}
