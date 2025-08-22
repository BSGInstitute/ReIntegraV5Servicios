using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTipoPrograma
    {
        public TTipoPrograma()
        {
            TCriterioEvaluacionTipoProgramas = new HashSet<TCriterioEvaluacionTipoPrograma>();
            TPgenerals = new HashSet<TPgeneral>();
        }

        /// <summary>
        /// clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de tipo de programa
        /// </summary>
        public string Nombre { get; set; } = null!;
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
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla origen
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TCriterioEvaluacionTipoPrograma> TCriterioEvaluacionTipoProgramas { get; set; }
        public virtual ICollection<TPgeneral> TPgenerals { get; set; }
    }
}
