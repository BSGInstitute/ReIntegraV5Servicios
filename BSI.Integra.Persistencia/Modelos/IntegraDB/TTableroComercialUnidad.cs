using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTableroComercialUnidad
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la unidad de resultado
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Valor de la unidad
        /// </summary>
        public int Valor { get; set; }
        /// <summary>
        /// Simbolo de la unidad
        /// </summary>
        public string? Simbolo { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modifico
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
