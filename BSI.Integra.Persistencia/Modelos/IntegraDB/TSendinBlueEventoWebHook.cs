using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSendinBlueEventoWebHook
    {
        public TSendinBlueEventoWebHook()
        {
            TSendinBlueDataDeEventos = new HashSet<TSendinBlueDataDeEvento>();
        }

        /// <summary>
        /// Identificador unico del evento de webhook de sendingblue
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tipo de evento registrado por el webhook
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Estado del registro (activo o eliminado)
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

        public virtual ICollection<TSendinBlueDataDeEvento> TSendinBlueDataDeEventos { get; set; }
    }
}
