using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSemaforoFinancieroDetalleVariable
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk de T_SemaforFinancieroDetalle
        /// </summary>
        public int IdSemaforoFinancieroDetalle { get; set; }
        /// <summary>
        /// Nombre de la variable
        /// </summary>
        public int IdSemaforoFinancieroVariable { get; set; }
        /// <summary>
        /// Fk de T_OperadorComparacion
        /// </summary>
        public decimal? ValorMinimo { get; set; }
        /// <summary>
        /// Fk de T_Moneda
        /// </summary>
        public int? IdMoneda { get; set; }
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
        /// Id de migracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Cantidad
        /// </summary>
        public decimal? ValorMaximo { get; set; }
    }
}
