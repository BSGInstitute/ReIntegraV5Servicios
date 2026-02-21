using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Registro de actividades cabecera agregadas al flujo congelado. Rastrea qué actividades se han añadido a qué flujos y en qué orden de ejecución. Vincula actividades maestras con sus versiones congeladas dentro de un flujo específico.
    /// </summary>
    public partial class TGestionContactoFlujoActividadAgregadum
    {
        public TGestionContactoFlujoActividadAgregadum()
        {
            TGestionContactoFlujoActividadAgregadaLogs = new HashSet<TGestionContactoFlujoActividadAgregadaLog>();
        }

        /// <summary>
        /// Identificador único del registro de agregación de actividad a flujo. Clave primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del flujo congelado al cual se agregó la actividad. Clave foránea a T_GestionContactoFlujoCongelado. El flujo padre que contiene esta actividad. Todos los registros con el mismo IdGestionContactoFlujoCongelado pertenecen al mismo flujo.
        /// </summary>
        public int IdGestionContactoFlujoCongelado { get; set; }
        /// <summary>
        /// Identificador de la actividad cabecera maestra que se agregó. Referencia a T_GestionDocenteActividadCabecera (tabla de maestro). Permite rastrear la actividad original de la cual se creó la versión congelada. Propósito: auditoría y vinculación con maestro.
        /// </summary>
        public int IdGestionDocenteActividadCabecera { get; set; }
        /// <summary>
        /// Identificador de la copia congelada creada. Clave foránea a T_GestionDocenteActividadCabeceraCongelada. Apunta a tabla de congelamiento normal. Esta es la versión congelada que realmente se ejecutará dentro del flujo congelado.
        /// </summary>
        public int IdGestionDocenteActividadCabeceraCongelada { get; set; }
        /// <summary>
        /// Orden de la actividad agregada en el flujo. Número secuencial: 1 = Primera actividad a ejecutar, 2 = Segunda actividad, 3 = Tercera, etc. Define la secuencia de ejecución de las actividades en el flujo. Rango: 1-999. Debe ser único por cada IdGestionContactoFlujoCongelado.
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo de la agregación. 1 = Agregación activa (la actividad se ejecutará en el flujo), 0 = Agregación inactiva/eliminada (la actividad NO se ejecutará). Permite deshabilitar actividades sin eliminar el registro. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó la agregación de la actividad al flujo. Máximo 50 caracteres. Referencia al usuario que ejecutó la operación de agregar la actividad. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó el registro de agregación. Máximo 50 caracteres. Se actualiza con cambios (reorden, cambio de estado, etc.). Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora exacta en UTC-5 (Hora Estándar de Perú) cuando se agregó la actividad al flujo. Marca el momento de la operación. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación del registro de agregación. Se actualiza con cambios (reordenamiento, cambio de estado, etc.). Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Número de versión para control de concurrencia optimista. Generado automáticamente por SQL Server (TIMESTAMP). Previene conflictos cuando múltiples usuarios intentan modificar simultáneamente. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionContactoFlujoCongelado IdGestionContactoFlujoCongeladoNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadCabeceraCongeladum IdGestionDocenteActividadCabeceraCongeladaNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadCabecera IdGestionDocenteActividadCabeceraNavigation { get; set; } = null!;
        public virtual ICollection<TGestionContactoFlujoActividadAgregadaLog> TGestionContactoFlujoActividadAgregadaLogs { get; set; }
    }
}
