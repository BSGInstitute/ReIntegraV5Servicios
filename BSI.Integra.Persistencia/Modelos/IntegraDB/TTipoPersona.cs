using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTipoPersona
    {
        public TTipoPersona()
        {
            TClasificacionPersonas = new HashSet<TClasificacionPersona>();
            TCriterioEvaluacionTipoPersonas = new HashSet<TCriterioEvaluacionTipoPersona>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string NombreTablaOriginal { get; set; } = null!;
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

        public virtual ICollection<TClasificacionPersona> TClasificacionPersonas { get; set; }
        public virtual ICollection<TCriterioEvaluacionTipoPersona> TCriterioEvaluacionTipoPersonas { get; set; }
    }
}
