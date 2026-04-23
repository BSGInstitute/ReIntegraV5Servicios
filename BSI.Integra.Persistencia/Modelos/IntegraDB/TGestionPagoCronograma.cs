using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena las cuotas de pago asociadas a una gestion de pago. Depende de T_GestionPago
    /// </summary>
    public partial class TGestionPagoCronograma
    {
        public TGestionPagoCronograma()
        {
            TGestionPagoArchivos = new HashSet<TGestionPagoArchivo>();
        }

        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_GestionPago
        /// </summary>
        public int IdGestionPago { get; set; }
        /// <summary>
        /// Numero secuencial de la cuota
        /// </summary>
        public int NumeroCuota { get; set; }
        /// <summary>
        /// Monto de la cuota
        /// </summary>
        public decimal MontoCuota { get; set; }
        /// <summary>
        /// Fecha de vencimiento de la cuota
        /// </summary>
        public DateTime? FechaVencimiento { get; set; }
        /// <summary>
        /// Fecha probable de pago estimada por Finanzas
        /// </summary>
        public DateTime? FechaProbablePago { get; set; }
        /// <summary>
        /// Fecha en que se realizo el pago efectivo
        /// </summary>
        public DateTime? FechaRealPago { get; set; }
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

        public virtual TGestionPago IdGestionPagoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionPagoArchivo> TGestionPagoArchivos { get; set; }
    }
}
