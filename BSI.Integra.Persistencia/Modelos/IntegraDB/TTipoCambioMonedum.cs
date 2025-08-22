using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTipoCambioMonedum
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public double MonedaAdolar { get; set; }
        public double DolarAmoneda { get; set; }
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Llave foranea con T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Llave foranea con fin.TipoCambioCol
        /// </summary>
        public int? IdTipoCambioCol { get; set; }
        /// <summary>
        /// Llave Foranea con la tabla fin.T_TipoCambio
        /// </summary>
        public int? IdTipoCambio { get; set; }
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

        public virtual TMonedum IdMonedaNavigation { get; set; } = null!;
        public virtual TTipoCambioCol? IdTipoCambioColNavigation { get; set; }
    }
}
