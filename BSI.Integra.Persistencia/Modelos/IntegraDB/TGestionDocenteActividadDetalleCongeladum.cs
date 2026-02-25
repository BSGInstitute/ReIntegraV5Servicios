using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de la actividad detalle. Contiene la configuración específica de ejecución, incluyendo medios de comunicación y disparadores asociados. Incluye todos los campos del maestro más configuración de ejecución por sesión.
    /// </summary>
    public partial class TGestionDocenteActividadDetalleCongeladum
    {
        public TGestionDocenteActividadDetalleCongeladum()
        {
            TGestionDocenteActividadDetalleCongeladaLogs = new HashSet<TGestionDocenteActividadDetalleCongeladaLog>();
            TGestionDocenteDisparadorCongelados = new HashSet<TGestionDocenteDisparadorCongelado>();
            TGestionDocenteOcurrenciaCongelada = new HashSet<TGestionDocenteOcurrenciaCongeladum>();
        }

        /// <summary>
        /// Identificador único de la actividad detalle congelada. Clave primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la actividad cabecera congelada padre. Clave foránea que establece relación jerárquica con T_GestionDocenteActividadCabeceraCongelada.
        /// </summary>
        public int IdGestionDocenteActividadCabeceraCongelada { get; set; }
        /// <summary>
        /// Identificador de la actividad detalle original (sin congelar). Referencia el maestro para propósitos de auditoría.
        /// </summary>
        public int IdGestionDocenteActividadDetalle { get; set; }
        /// <summary>
        /// Identificador de la actividad cabecera original. Permite rastrear la estructura original completa para auditoría.
        /// </summary>
        public int IdGestionDocenteActividadCabecera { get; set; }
        /// <summary>
        /// Identificador del tipo de actividad detalle (email, SMS, llamada, mensaje, notificación, etc.). Define cómo se ejecutará la actividad. Referencia a T_GestionDocenteActividadDetalleTipo.
        /// </summary>
        public int IdGestionDocenteActividadDetalleTipo { get; set; }
        /// <summary>
        /// Identificador de la plantilla de medio de comunicación a usar (email template, SMS template, template de mensaje, etc.). Define el contenido y formato de envío. Referencia a mkt.T_PlantillaMedioComunicacion.
        /// </summary>
        public int IdPlantillaMedioComunicacion { get; set; }
        /// <summary>
        /// Identificador del disparador que inicia la ejecución de esta actividad detalle. Define cuándo ejecutar la actividad (fecha fija, tiempo relativo, ocurrencia previa, evento sistema). Referencia a T_GestionDocenteDisparadorDetalle.
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Nombre descriptivo de la actividad detalle. Máximo 300 caracteres. Ejemplo: &quot;Envío de Email de Recordatorio Semanal&quot; o &quot;Notificación SMS a Estudiantes&quot;.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Identificador del estado de ejecución actual (por ejecutar, en progreso, ejecutada, fallida, cancelada, reintentando). Referencia a T_GestionDocenteEstadoEjecucion. Indica progreso operacional.
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
        /// Último usuario que modificó el registro congelado. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 del congelamiento. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
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

        public virtual TGestionDocenteActividadCabeceraCongeladum IdGestionDocenteActividadCabeceraCongeladaNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadCabecera IdGestionDocenteActividadCabeceraNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadDetalle IdGestionDocenteActividadDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadDetalleTipo IdGestionDocenteActividadDetalleTipoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TPlantillaMedioComunicacion IdPlantillaMedioComunicacionNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteActividadDetalleCongeladaLog> TGestionDocenteActividadDetalleCongeladaLogs { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorCongelado> TGestionDocenteDisparadorCongelados { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladum> TGestionDocenteOcurrenciaCongelada { get; set; }
    }
}
