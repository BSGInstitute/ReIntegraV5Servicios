using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena los archivos adjuntos de la gestion de pago. NULL en IdGestionPagoCronograma indica archivo de cabecera, con valor indica voucher de cuota
    /// </summary>
    public partial class TGestionPagoArchivo
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_GestionPago
        /// </summary>
        public int IdGestionPago { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_GestionPagoCronograma. NULL si es archivo de cabecera, con valor si es voucher de cuota
        /// </summary>
        public int? IdGestionPagoCronograma { get; set; }
        /// <summary>
        /// Nombre fisico del archivo almacenado en blob storage
        /// </summary>
        public string NombreArchivo { get; set; } = null!;
        /// <summary>
        /// Tipo MIME del archivo (application/pdf, image/jpeg, etc.)
        /// </summary>
        public string ContentTypeArchivo { get; set; } = null!;
        /// <summary>
        /// Estado del registro (1: activo, 0: eliminado)
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
        /// Indica si el archivo corresponde a un Comprobante (1) o a Otros Archivos (0). Aplica solo a archivos de cabecera (IdGestionPagoCronograma NULL). Lo setea el backend según el endpoint usado
        /// </summary>
        public bool EsComprobante { get; set; }

        public virtual TGestionPagoCronograma? IdGestionPagoCronogramaNavigation { get; set; }
        public virtual TGestionPago IdGestionPagoNavigation { get; set; } = null!;
    }
}
