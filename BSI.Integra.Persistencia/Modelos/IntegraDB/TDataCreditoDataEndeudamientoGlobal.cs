using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDataCreditoDataEndeudamientoGlobal
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string? Calificacion { get; set; }
        public string? Fuente { get; set; }
        public string? SaldoPendiente { get; set; }
        public string? TipoCredito { get; set; }
        public string? Moneda { get; set; }
        public string? NumeroCreditos { get; set; }
        public string? Independiente { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string? EntidadNombre { get; set; }
        public string? EntidadNit { get; set; }
        public string? EntidadSector { get; set; }
        public string? GarantiaTipo { get; set; }
        public string? GarantiaValor { get; set; }
        public string? Llave { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
