using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSemaforoFinancieroDetalle
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_SemaforoFinanciero
        /// </summary>
        public int IdSemaforoFinanciero { get; set; }
        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Mensaje
        /// </summary>
        public string Mensaje { get; set; } = null!;
        /// <summary>
        /// Color
        /// </summary>
        public string Color { get; set; } = null!;
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de Migracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
