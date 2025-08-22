using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCajaEgreso
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CajaPorRendirCabecera
        /// </summary>
        public int? IdCajaPorRendirCabecera { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Caja
        /// </summary>
        public int IdCaja { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Fur
        /// </summary>
        public int? IdFur { get; set; }
        /// <summary>
        /// Descripcion del registro
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Llave foranea con la tabla T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Total del efectivo solicitado
        /// </summary>
        public decimal TotalEfectivo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CajaEgresoAprobado
        /// </summary>
        public int? IdCajaEgresoAprobado { get; set; }
        /// <summary>
        /// Si se ha enviado el registro
        /// </summary>
        public bool EsEnviado { get; set; }
        /// <summary>
        /// Fecha en que se envio el registro o solicitud
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Personal
        /// </summary>
        public int? IdPersonalResponsable { get; set; }
        public int IdPersonalSolicitante { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_ComprobantePago
        /// </summary>
        public int? IdComprobantePago { get; set; }
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
        /// <summary>
        /// LLave foranea con la tabla T_IdComprobantePagoPorFur
        /// </summary>
        public int? IdComprobantePagoPorFur { get; set; }
    }
}
