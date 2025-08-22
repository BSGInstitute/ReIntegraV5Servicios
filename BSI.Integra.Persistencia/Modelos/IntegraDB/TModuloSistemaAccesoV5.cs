using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModuloSistemaAccesoV5
    {
        /// <summary>
        /// Es llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// llave foranea de T_UsuarioRol
        /// </summary>
        public int IdUsuarioRol { get; set; }
        /// <summary>
        /// Llave foranea de T_Usuario
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Llave foranea de T_ModuloSistema
        /// </summary>
        public int IdModuloSistema { get; set; }
        /// <summary>
        /// Campo de auditoria estado del dato
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo de auditoria usuario creacion del dato
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria usuario modificacion del dato
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria fecha creacion del dato
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo de auditoria fecha modificacion del dato
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de auditoria row version del dato
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria IdMigracion
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
