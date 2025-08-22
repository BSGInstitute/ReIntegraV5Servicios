using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TComprobantePagoPorFur
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_ComprobantePago
        /// </summary>
        public int IdComprobantePago { get; set; }
        /// <summary>
        /// Llave foranes de la tabla T_Fur
        /// </summary>
        public int IdFur { get; set; }
        /// <summary>
        /// Monto del Fur
        /// </summary>
        public decimal Monto { get; set; }
        /// <summary>
        /// Estado de eliminado o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
