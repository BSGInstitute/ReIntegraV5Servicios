using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Asigna actividad congelada a sesión específica de PEspecifico. Vincula actividades cabecera congeladas con sesiones académicas específicas. Define qué actividades congeladas aplican para qué sesiones.
    /// </summary>
    public partial class TGestionContactoActividadSesionCongeladum
    {
        public TGestionContactoActividadSesionCongeladum()
        {
            TGestionContactoActividadSesionCongeladaLogs = new HashSet<TGestionContactoActividadSesionCongeladaLog>();
        }

        /// <summary>
        /// Identificador único de la asignación de actividad congelada a sesión académica. Clave primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la actividad cabecera congelada que se asigna a la sesión. Clave foránea a T_GestionDocenteActividadCabeceraCongelada. La actividad congelada que será ejecutada en esta sesión.
        /// </summary>
        public int IdGestionDocenteActividadCabeceraCongelada { get; set; }
        /// <summary>
        /// Identificador de la sesión académica específica (PEspecifico). Referencia a T_PEspecificoSesion. Define a qué sesión académica se asigna la actividad. Ejemplo: Sesión 2026-1 de Matemática 101, Sección A.
        /// </summary>
        public int IdPespecificoSesion { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución de la asignación a sesión. Referencia a T_GestionDocenteEstadoEjecucion. Permite rastrear si la asignación está lista, en progreso, ejecutada, fallida, etc. NULLABLE - Puede ser nulo.
        /// </summary>
        public int? IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo de la asignación. 1 = Asignación activa (la actividad se ejecutará en esta sesión), 0 = Asignación inactiva (la actividad NO se ejecutará). Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó la asignación de la actividad a la sesión. Máximo 50 caracteres. Referencia al usuario que ejecutó la operación. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó la asignación. Máximo 50 caracteres. Se actualiza con cada cambio. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora exacta en UTC-5 (Hora Estándar de Perú) cuando se realizó la asignación. Marca el momento de vinculación entre actividad y sesión. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación de la asignación. Se actualiza automáticamente con cada cambio. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Número de versión para control de concurrencia optimista. Generado automáticamente por SQL Server (TIMESTAMP). Previene conflictos en actualizaciones simultáneas. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteActividadCabeceraCongeladum IdGestionDocenteActividadCabeceraCongeladaNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion? IdGestionDocenteEstadoEjecucionNavigation { get; set; }
        public virtual TPespecificoSesion IdPespecificoSesionNavigation { get; set; } = null!;
        public virtual ICollection<TGestionContactoActividadSesionCongeladaLog> TGestionContactoActividadSesionCongeladaLogs { get; set; }
    }
}
