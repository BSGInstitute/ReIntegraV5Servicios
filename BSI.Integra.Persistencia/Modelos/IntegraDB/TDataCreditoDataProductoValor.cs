using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDataCreditoDataProductoValor
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string? Producto { get; set; }
        public string? Valor1 { get; set; }
        public string? Valor2 { get; set; }
        public string? Valor3 { get; set; }
        public string? Valor4 { get; set; }
        public string? Valor5 { get; set; }
        public string? Valor6 { get; set; }
        public string? Valor7 { get; set; }
        public string? Valor8 { get; set; }
        public string? Valor9 { get; set; }
        public string? Valor10 { get; set; }
        public string? Valor1smlv { get; set; }
        public string? Valor2smlv { get; set; }
        public string? Valor3smlv { get; set; }
        public string? Valor4smlv { get; set; }
        public string? Valor5smlv { get; set; }
        public string? Valor6smlv { get; set; }
        public string? Valor7smlv { get; set; }
        public string? Valor8smlv { get; set; }
        public string? Valor9smlv { get; set; }
        public string? Valor10smlv { get; set; }
        public string? Razon1 { get; set; }
        public string? Razon2 { get; set; }
        public string? Razon3 { get; set; }
        public string? Razon4 { get; set; }
        public string? Razon5 { get; set; }
        public string? Razon6 { get; set; }
        public string? Razon7 { get; set; }
        public string? Razon8 { get; set; }
        public string? Razon9 { get; set; }
        public string? Razon10 { get; set; }
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
