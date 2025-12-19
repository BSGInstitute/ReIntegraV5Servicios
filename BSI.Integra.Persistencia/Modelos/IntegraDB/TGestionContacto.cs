using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla para gestión de contactos y seguimiento de oportunidades comerciales
    /// </summary>
    public partial class TGestionContacto
    {
        public TGestionContacto()
        {
            TActividadDetalleGestionContactos = new HashSet<TActividadDetalleGestionContacto>();
            TGestionContactoLogs = new HashSet<TGestionContactoLog>();
        }

        /// <summary>
        /// Identificador único del registro (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Foreign Key que referencia al centro de costo
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Foreign Key que referencia al personal asignado para seguimiento
        /// </summary>
        public int? IdPersonalAsignado { get; set; }
        /// <summary>
        /// Foreign Key que referencia a la clasificación de la persona (Lead, Prospecto, Cliente, etc.)
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// Foreign Key que referencia a la fase actual de gestión de contacto en el embudo de ventas
        /// </summary>
        public int IdFaseGestionContacto { get; set; }
        /// <summary>
        /// Foreign Key que referencia al origen del contacto (web, Facebook, referido, etc.)
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Último comentario o nota registrada por el asesor comercial
        /// </summary>
        public string? UltimoComentario { get; set; }
        /// <summary>
        /// Foreign Key que referencia al estado actual del contacto en la gestión comercial
        /// </summary>
        public int IdEstadoGestionContacto { get; set; }
        /// <summary>
        /// Indica si el contacto está siendo seguido por WhatsApp (1: Sí, 0: No, NULL: No aplica)
        /// </summary>
        public bool? EstadoSeguimientoWhatsApp { get; set; }
        /// <summary>
        /// Estado del registro (1: Activo, 0: Eliminado/Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro
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
        /// Campo de sistema automático que guarda la versión del registro para control de concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TCentroCosto? IdCentroCostoNavigation { get; set; }
        public virtual TClasificacionPersona? IdClasificacionPersonaNavigation { get; set; }
        public virtual TEstadoGestionContacto IdEstadoGestionContactoNavigation { get; set; } = null!;
        public virtual TFaseGestionContacto IdFaseGestionContactoNavigation { get; set; } = null!;
        public virtual TOrigen? IdOrigenNavigation { get; set; }
        public virtual TPersonal? IdPersonalAsignadoNavigation { get; set; }
        public virtual ICollection<TActividadDetalleGestionContacto> TActividadDetalleGestionContactos { get; set; }
        public virtual ICollection<TGestionContactoLog> TGestionContactoLogs { get; set; }
    }
}
