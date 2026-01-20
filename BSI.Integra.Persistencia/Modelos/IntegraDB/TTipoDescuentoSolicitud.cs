using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena solicitudes de aprobación para aplicar tipos de descuento a oportunidades comerciales
    /// </summary>
    public partial class TTipoDescuentoSolicitud
    {
        public TTipoDescuentoSolicitud()
        {
            TTipoDescuentoSolicitudDetalles = new HashSet<TTipoDescuentoSolicitudDetalle>();
        }

        /// <summary>
        /// Identificador único de la solicitud de descuento
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del tipo de descuento solicitado
        /// </summary>
        public int IdTipoDescuento { get; set; }
        /// <summary>
        /// Identificador de la oportunidad comercial a la que se aplica el descuento
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Identificador del estado actual de la solicitud (Pendiente, Aprobado, Rechazado)
        /// </summary>
        public int IdTipoDescuentoSolicitudEstado { get; set; }
        public int IdPersonalSolicitante { get; set; }
        /// <summary>
        /// Indicador de estado del registro (1 = Activo, 0 = Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario del sistema que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario del sistema que realizó la última modificación del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de versión para concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalSolicitanteNavigation { get; set; } = null!;
        public virtual TTipoDescuento IdTipoDescuentoNavigation { get; set; } = null!;
        public virtual TTipoDescuentoSolicitudEstado IdTipoDescuentoSolicitudEstadoNavigation { get; set; } = null!;
        public virtual ICollection<TTipoDescuentoSolicitudDetalle> TTipoDescuentoSolicitudDetalles { get; set; }
    }
}
