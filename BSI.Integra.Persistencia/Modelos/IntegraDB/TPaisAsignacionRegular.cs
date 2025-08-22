using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPaisAsignacionRegular
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        public int IdPais { get; set; }
        /// <summary>
        /// estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion 
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha creacion 
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Row version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// el di migracion
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
