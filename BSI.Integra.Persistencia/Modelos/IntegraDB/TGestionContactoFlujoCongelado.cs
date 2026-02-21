using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada del flujo de gestión de contactos para docentes. Incluye todos los campos del maestro más configuración de asignación. Captura el estado inmutable del flujo en un momento específico.
    /// </summary>
    public partial class TGestionContactoFlujoCongelado
    {
        public TGestionContactoFlujoCongelado()
        {
            TGestionContactoFlujoActividadAgregada = new HashSet<TGestionContactoFlujoActividadAgregadum>();
            TGestionContactoFlujoCongeladoLogs = new HashSet<TGestionContactoFlujoCongeladoLog>();
            TGestionDocenteActividadCabeceraCongelada = new HashSet<TGestionDocenteActividadCabeceraCongeladum>();
        }

        /// <summary>
        /// Identificador único de la copia congelada del flujo. Clave primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador único que referencia la relación entre contacto y flujo de docente. Campo UNIQUE. Garantiza que cada relación contacto-flujo tenga una única copia congelada. Clave foránea que vincula con tabla padre.
        /// </summary>
        public int IdGestionContactoDocenteFlujo { get; set; }
        /// <summary>
        /// Identificador del flujo docente original (sin congelar) del cual se crea esta copia. Referencia la configuración base del flujo. Permite rastrear el maestro.
        /// </summary>
        public int IdGestionDocenteFlujo { get; set; }
        /// <summary>
        /// Nombre descriptivo del flujo congelado. Copia del nombre original del flujo al momento del congelamiento. Máximo 300 caracteres. Ejemplo: &quot;Recordatorio de Asistencia Semanal&quot;.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del flujo congelado. Proporciona contexto adicional sobre el propósito y alcance del flujo. Máximo 1000 caracteres. Puede ser nula. Ejemplo: &quot;Flujo automático para recordar asistencia a estudiantes activos&quot;.
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Identificador del estado administrativo actual del flujo (activo, suspendido, finalizado, archivado, etc.). Referencia a catálogo T_GestionDocenteEstado. NO confundir con IdGestionDocenteEstadoEjecucion.
        /// </summary>
        public int IdGestionDocenteEstado { get; set; }
        /// <summary>
        /// Identificador de la categoría a la que pertenece el flujo (ej: seguimiento, recordatorio, evaluación, comunicación). Referencia a tabla catálogo T_GestionDocenteCategoria. Clasifica por tipo de flujo.
        /// </summary>
        public int IdGestionDocenteCategoria { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución actual del flujo (por ejecutar, en progreso, ejecutada, fallida, cancelada, etc.). Referencia a T_GestionDocenteEstadoEjecucion. Indica progreso de ejecución operacional.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo del registro. 1 = Activo (el flujo se puede usar), 0 = Inactivo (el flujo está deshabilitado). Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro del congelamiento. Máximo 50 caracteres. Referencia a usuario del sistema que ejecutó la operación de congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó este registro congelado. Máximo 50 caracteres. Se actualiza cada vez que hay cambios en el flujo congelado. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora exacta en UTC-5 (Hora Estándar de Perú) cuando se congeló el flujo. Marca el momento de captura del estado inmutable. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación del registro congelado. Se actualiza automáticamente con cada cambio. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Número de versión para control de concurrencia optimista. Generado automáticamente por SQL Server (TIMESTAMP). Previene conflictos en actualizaciones simultáneas. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionContactoDocenteFlujo IdGestionContactoDocenteFlujoNavigation { get; set; } = null!;
        public virtual TGestionDocenteCategorium IdGestionDocenteCategoriaNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstado IdGestionDocenteEstadoNavigation { get; set; } = null!;
        public virtual TGestionDocenteFlujo IdGestionDocenteFlujoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionContactoFlujoActividadAgregadum> TGestionContactoFlujoActividadAgregada { get; set; }
        public virtual ICollection<TGestionContactoFlujoCongeladoLog> TGestionContactoFlujoCongeladoLogs { get; set; }
        public virtual ICollection<TGestionDocenteActividadCabeceraCongeladum> TGestionDocenteActividadCabeceraCongelada { get; set; }
    }
}
