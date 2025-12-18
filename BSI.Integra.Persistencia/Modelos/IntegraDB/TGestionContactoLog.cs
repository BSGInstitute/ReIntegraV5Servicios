using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena el historial de cambios y actividades realizadas en la gestión de contactos
    /// </summary>
    public partial class TGestionContactoLog
    {
        /// <summary>
        /// Identificador único del registro de log (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Foreign Key que referencia al registro de gestión de contacto (T_GestionContacto)
        /// </summary>
        public int? IdGestionContacto { get; set; }
        /// <summary>
        /// Snapshot del centro de costo en el momento del log
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Snapshot del personal asignado en el momento del log
        /// </summary>
        public int? IdPersonalAsignado { get; set; }
        /// <summary>
        /// Snapshot de la clasificación de persona en el momento del log
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// Fase de gestión de contacto anterior (antes del cambio)
        /// </summary>
        public int? IdFaseGestionContactoAnterior { get; set; }
        /// <summary>
        /// Fase de gestión de contacto actual (después del cambio)
        /// </summary>
        public int? IdFaseGestionContacto { get; set; }
        /// <summary>
        /// Snapshot del origen del contacto en el momento del log
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Snapshot del estado de gestión de contacto en el momento del log
        /// </summary>
        public int? IdEstadoGestionContacto { get; set; }
        /// <summary>
        /// Snapshot del estado de seguimiento por WhatsApp en el momento del log
        /// </summary>
        public bool? EstadoSeguimientoWhatsApp { get; set; }
        /// <summary>
        /// Fecha y hora en que se registró el evento en el log
        /// </summary>
        public DateTime? FechaLog { get; set; }
        /// <summary>
        /// Comentario o descripción del cambio realizado
        /// </summary>
        public string? Comentario { get; set; }
        /// <summary>
        /// Identificador del personal asignado anterior (antes del cambio)
        /// </summary>
        public int? IdPersonalAsignadoAnterior { get; set; }
        /// <summary>
        /// Identificador del centro de costo anterior (antes del cambio)
        /// </summary>
        public int? IdCentroCostoAnterior { get; set; }
        /// <summary>
        /// Fecha y hora de finalización del periodo del log
        /// </summary>
        public DateTime? FechaFinLog { get; set; }
        /// <summary>
        /// Fecha y hora del cambio de fase de gestión de contacto
        /// </summary>
        public DateTime? FechaCambioFaseContacto { get; set; }
        /// <summary>
        /// Indicador de cambio de fase (1: Hubo cambio, 0: No hubo cambio)
        /// </summary>
        public bool? CambioFaseContacto { get; set; }
        /// <summary>
        /// Fecha y hora del cambio de asesor
        /// </summary>
        public DateTime? FechaCambioAsesor { get; set; }
        /// <summary>
        /// Fecha y hora del cambio de asesor anterior
        /// </summary>
        public DateTime? FechaCambioAsesorAnterior { get; set; }
        /// <summary>
        /// Estado del registro de log (1: Activo, 0: Eliminado/Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro de log
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro de log
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro de log
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro de log
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automático que guarda la versión del registro para control de concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TCentroCosto? IdCentroCostoAnteriorNavigation { get; set; }
        public virtual TCentroCosto? IdCentroCostoNavigation { get; set; }
        public virtual TClasificacionPersona? IdClasificacionPersonaNavigation { get; set; }
        public virtual TEstadoGestionContacto? IdEstadoGestionContactoNavigation { get; set; }
        public virtual TFaseGestionContacto? IdFaseGestionContactoAnteriorNavigation { get; set; }
        public virtual TFaseGestionContacto? IdFaseGestionContactoNavigation { get; set; }
        public virtual TGestionContacto? IdGestionContactoNavigation { get; set; }
        public virtual TOrigen? IdOrigenNavigation { get; set; }
        public virtual TPersonal? IdPersonalAsignadoAnteriorNavigation { get; set; }
        public virtual TPersonal? IdPersonalAsignadoNavigation { get; set; }
    }
}
