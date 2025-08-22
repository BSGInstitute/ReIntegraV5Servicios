using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTipoPagoCategorium
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CategoriaPrograma
        /// </summary>
        public int IdCategoriaPrograma { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_TipoPago
        /// </summary>
        public int IdTipoPago { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ModoPago
        /// </summary>
        public int IdModoPago { get; set; }
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
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual TCategoriaPrograma IdCategoriaProgramaNavigation { get; set; } = null!;
        public virtual TModoPago IdModoPagoNavigation { get; set; } = null!;
        public virtual TTipoPago IdTipoPagoNavigation { get; set; } = null!;
    }
}
