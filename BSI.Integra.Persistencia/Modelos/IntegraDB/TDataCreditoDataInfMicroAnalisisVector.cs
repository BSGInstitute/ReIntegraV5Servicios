using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDataCreditoDataInfMicroAnalisisVector
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string? NombreSector { get; set; }
        public string? CuentaEntidad { get; set; }
        public string? CuentaNumeroCuenta { get; set; }
        public string? CuentaTipoCuenta { get; set; }
        public string? CuentaEstado { get; set; }
        public bool? ContieneDatos { get; set; }
        public DateTime? Fecha { get; set; }
        public string? SaldoDeudaTotalMora { get; set; }
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
