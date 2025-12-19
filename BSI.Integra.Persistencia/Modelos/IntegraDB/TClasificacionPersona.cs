using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TClasificacionPersona
    {
        public TClasificacionPersona()
        {
            TGestionContactoLogs = new HashSet<TGestionContactoLog>();
            TGestionContactos = new HashSet<TGestionContacto>();
        }

        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdTablaOriginal { get; set; }
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

        public virtual TPersona IdPersonaNavigation { get; set; } = null!;
        public virtual TTipoPersona IdTipoPersonaNavigation { get; set; } = null!;
        public virtual ICollection<TGestionContactoLog> TGestionContactoLogs { get; set; }
        public virtual ICollection<TGestionContacto> TGestionContactos { get; set; }
    }
}
