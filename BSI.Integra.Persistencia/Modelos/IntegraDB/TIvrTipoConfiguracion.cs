using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TIvrTipoConfiguracion
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Describe el tipo de configuracion
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria Estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo auditoria UsuarioCreacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria UsuarioModificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo auditoria FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo auditoria FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo auditoria RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
