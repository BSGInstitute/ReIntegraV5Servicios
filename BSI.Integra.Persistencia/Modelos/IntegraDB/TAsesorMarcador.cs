using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la información del marcador de integra por personal
    /// </summary>
    public partial class TAsesorMarcador
    {
        /// <summary>
        /// (PK) primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// (FK) Forreign Key de T_PErsonal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Indica si el marcador esta activo
        /// </summary>
        public bool MarcadorActivo { get; set; }
        /// <summary>
        /// Campo de auditoria Estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo de auditoria UsuarioCreacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria UsuarioModificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo de auditoria FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de auditoria RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria IdMigracion
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
