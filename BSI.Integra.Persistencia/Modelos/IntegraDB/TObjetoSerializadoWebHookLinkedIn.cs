using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TObjetoSerializadoWebHookLinkedIn
    {
        /// <summary>
        /// Id de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Json Obtenido
        /// </summary>
        public string? Json { get; set; }
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
        /// row version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id migración
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
