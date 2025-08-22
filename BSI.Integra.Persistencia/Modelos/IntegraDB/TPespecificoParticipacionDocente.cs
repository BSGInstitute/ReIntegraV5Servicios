using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoParticipacionDocente
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Pespecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Expositor
        /// </summary>
        public int? IdExpositor { get; set; }
        /// <summary>
        /// Indica si el docente confirmo su participacion
        /// </summary>
        public bool? ConfirmaParticipacion { get; set; }
        /// <summary>
        /// Fecha en la que se confirma la participacion en el sistema
        /// </summary>
        public DateTime? FechaConfirmacion { get; set; }
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Indica si el silabo ha sido aprobado
        /// </summary>
        public bool? EsSilaboAprobado { get; set; }
    }
}
