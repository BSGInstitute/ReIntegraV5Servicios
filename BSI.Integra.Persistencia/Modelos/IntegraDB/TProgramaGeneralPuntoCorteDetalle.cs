using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralPuntoCorteDetalle
    {
        /// <summary>
        /// Es PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_ProgramaGeneralPuntoCorte
        /// </summary>
        public int IdProgramaGeneralPuntoCorte { get; set; }
        /// <summary>
        /// FK de T_PuntoCorte
        /// </summary>
        public int IdPuntoCorte { get; set; }
        /// <summary>
        /// Si es tipo A, B, C, etc.
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Descripcion opcional
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Valor minimo
        /// </summary>
        public decimal? ValorMinimo { get; set; }
        /// <summary>
        /// Valor maximo
        /// </summary>
        public decimal? ValorMaximo { get; set; }
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
    }
}
