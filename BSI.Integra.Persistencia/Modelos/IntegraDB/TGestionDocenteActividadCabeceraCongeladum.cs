using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de la actividad cabecera. Captura la estructura principal de la actividad al momento del congelamiento. Una actividad cabecera puede tener múltiples actividades detalle. Incluye todos los campos del maestro.
    /// </summary>
    public partial class TGestionDocenteActividadCabeceraCongeladum
    {
        public TGestionDocenteActividadCabeceraCongeladum()
        {
            TGestionContactoActividadSesionCongelada = new HashSet<TGestionContactoActividadSesionCongeladum>();
            TGestionContactoFlujoActividadAgregada = new HashSet<TGestionContactoFlujoActividadAgregadum>();
            TGestionDocenteActividadCabeceraCongeladaLogs = new HashSet<TGestionDocenteActividadCabeceraCongeladaLog>();
            TGestionDocenteActividadDetalleCongelada = new HashSet<TGestionDocenteActividadDetalleCongeladum>();
        }

        /// <summary>
        /// Identificador único de la actividad cabecera congelada. Clave primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la copia congelada del flujo al que pertenece esta actividad. Clave foránea que establece la relación padre con T_GestionContactoFlujoCongelado.
        /// </summary>
        public int IdGestionContactoFlujoCongelado { get; set; }
        /// <summary>
        /// Identificador de la actividad cabecera original (sin congelar) de la cual se realiza la copia. Referencia el maestro para propósitos de auditoría y rastrabilidad.
        /// </summary>
        public int IdGestionDocenteActividadCabecera { get; set; }
        /// <summary>
        /// Identificador del flujo original al cual pertenecía la actividad. Permite rastrear la jerarquía original completa para auditoría.
        /// </summary>
        public int IdGestionDocenteFlujo { get; set; }
        /// <summary>
        /// Nombre descriptivo de la actividad cabecera. Copia del nombre al momento del congelamiento. Máximo 300 caracteres. Ejemplo: &quot;Envío de Recordatorio de Asistencia&quot;.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada de qué realiza esta actividad en el flujo. Máximo 1000 caracteres. Opcional. Ejemplo: &quot;Actividad principal que coordina el envío de recordatorios a estudiantes con baja asistencia&quot;.
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Identificador del estado administrativo de la actividad (activa, suspendida, archivada, finalizada). Referencia a catálogo T_GestionDocenteEstado. Estado administrativo.
        /// </summary>
        public int IdGestionDocenteEstado { get; set; }
        /// <summary>
        /// Identificador de la categoría de la actividad (comunicación, seguimiento, evaluación, recordatorio). Referencia a catálogo T_GestionDocenteCategoria. Clasifica el tipo de actividad.
        /// </summary>
        public int IdGestionDocenteCategoria { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución (por ejecutar, en ejecución, ejecutada, fallida, cancelada, reintentando). Referencia a T_GestionDocenteEstadoEjecucion. Indica progreso de ejecución operacional.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. 1 = Activo, 0 = Inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento. Máximo 50 caracteres. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó este registro congelado. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 del congelamiento de la actividad. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Generado automáticamente. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionContactoFlujoCongelado IdGestionContactoFlujoCongeladoNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadCabecera IdGestionDocenteActividadCabeceraNavigation { get; set; } = null!;
        public virtual TGestionDocenteCategorium IdGestionDocenteCategoriaNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstado IdGestionDocenteEstadoNavigation { get; set; } = null!;
        public virtual TGestionDocenteFlujo IdGestionDocenteFlujoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionContactoActividadSesionCongeladum> TGestionContactoActividadSesionCongelada { get; set; }
        public virtual ICollection<TGestionContactoFlujoActividadAgregadum> TGestionContactoFlujoActividadAgregada { get; set; }
        public virtual ICollection<TGestionDocenteActividadCabeceraCongeladaLog> TGestionDocenteActividadCabeceraCongeladaLogs { get; set; }
        public virtual ICollection<TGestionDocenteActividadDetalleCongeladum> TGestionDocenteActividadDetalleCongelada { get; set; }
    }
}
