using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Registra el flujo documental y financiero del pago de un comprobante completo. Relacion 1:1 con T_ComprobantePago
    /// </summary>
    public partial class TGestionPago
    {
        public TGestionPago()
        {
            TGestionPagoArchivos = new HashSet<TGestionPagoArchivo>();
            TGestionPagoCronogramas = new HashSet<TGestionPagoCronograma>();
        }

        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_ComprobantePago. Relacion 1:1 con UNIQUE
        /// </summary>
        public int IdComprobantePago { get; set; }
        /// <summary>
        /// Indica si el servicio fue validado por Operaciones (PO)
        /// </summary>
        public bool? ServicioValidado { get; set; }
        /// <summary>
        /// Fecha en que Operaciones registra la solicitud de pago
        /// </summary>
        public DateTime? FechaSolicitud { get; set; }
        /// <summary>
        /// Observaciones a la documentacion registradas durante la revision
        /// </summary>
        public string? ObservacionDocumentacion { get; set; }
        /// <summary>
        /// Respuesta al levantamiento de observaciones
        /// </summary>
        public string? LevantamientoObservacion { get; set; }
        /// <summary>
        /// Check de conformidad otorgada por Finanzas (FN)
        /// </summary>
        public bool? ConformidadFinanzas { get; set; }
        /// <summary>
        /// Observaciones a la programacion de pago registradas por Finanzas
        /// </summary>
        public string? ObservacionProgramacionPago { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_ModalidadPago (Total, Parcial)
        /// </summary>
        public int IdModalidadPago { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_PagoEstado (Solicitado, Observado, Pendiente, Pagado)
        /// </summary>
        public int IdPagoEstado { get; set; }
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

        public virtual TComprobantePago IdComprobantePagoNavigation { get; set; } = null!;
        public virtual TModalidadPago IdModalidadPagoNavigation { get; set; } = null!;
        public virtual TPagoEstado IdPagoEstadoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionPagoArchivo> TGestionPagoArchivos { get; set; }
        public virtual ICollection<TGestionPagoCronograma> TGestionPagoCronogramas { get; set; }
    }
}
