using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que registra información detallada sobre las actividades de seguimiento realizadas en la gestión de contactos
    /// </summary>
    public partial class TActividadDetalleGestionContacto
    {
        /// <summary>
        /// Identificador único de la actividad de detalle (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Foreign Key que referencia a la actividad cabecera
        /// </summary>
        public int? IdActividadCabecera { get; set; }
        /// <summary>
        /// Foreign Key que referencia al registro de gestión de contacto
        /// </summary>
        public int? IdGestionContacto { get; set; }
        /// <summary>
        /// Fecha y hora programada para la actividad
        /// </summary>
        public DateTime? FechaProgramada { get; set; }
        /// <summary>
        /// Fecha y hora real en que se ejecutó la actividad
        /// </summary>
        public DateTime? FechaReal { get; set; }
        /// <summary>
        /// Duración real de la actividad en minutos
        /// </summary>
        public int? DuracionReal { get; set; }
        /// <summary>
        /// Foreign Key que referencia al estado de la actividad de detalle
        /// </summary>
        public int IdEstadoActividadDetalle { get; set; }
        /// <summary>
        /// Comentario o nota sobre la actividad realizada
        /// </summary>
        public string? Comentario { get; set; }
        /// <summary>
        /// Foreign Key que referencia a la ocurrencia alternativa
        /// </summary>
        public int? IdOcurrenciaAlterno { get; set; }
        /// <summary>
        /// Foreign Key que referencia a la ocurrencia de actividad alternativa
        /// </summary>
        public int? IdOcurrenciaActividadAlterno { get; set; }
        /// <summary>
        /// Indica si el seguimiento se realiza por WhatsApp (1: Sí, 0: No, NULL: No aplica)
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

        public virtual TActividadCabecera? IdActividadCabeceraNavigation { get; set; }
        public virtual TEstadoActividadDetalle IdEstadoActividadDetalleNavigation { get; set; } = null!;
        public virtual TGestionContacto? IdGestionContactoNavigation { get; set; }
        public virtual TOcurrenciaActividadAlterno? IdOcurrenciaActividadAlternoNavigation { get; set; }
        public virtual TOcurrenciaAlterno? IdOcurrenciaAlternoNavigation { get; set; }
    }
}
