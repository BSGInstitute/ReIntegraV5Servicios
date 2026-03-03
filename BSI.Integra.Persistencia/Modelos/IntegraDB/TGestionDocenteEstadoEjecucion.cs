using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de estados de ejecución: EJECUTADA, POR_EJECUTAR, PENDIENTE, EN_PROGRESO, FALLIDA, CANCELADA, REINTENTANDO. Centraliza todos los estados que pueden tener las entidades en ejecución. Tabla maestra de referencia.
    /// </summary>
    public partial class TGestionDocenteEstadoEjecucion
    {
        public TGestionDocenteEstadoEjecucion()
        {
            TGestionContactoActividadSesionCongelada = new HashSet<TGestionContactoActividadSesionCongeladum>();
            TGestionContactoActividadSesionCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations = new HashSet<TGestionContactoActividadSesionCongeladaLog>();
            TGestionContactoActividadSesionCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations = new HashSet<TGestionContactoActividadSesionCongeladaLog>();
            TGestionContactoFlujoActividadAgregadaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations = new HashSet<TGestionContactoFlujoActividadAgregadaLog>();
            TGestionContactoFlujoActividadAgregadaLogIdGestionDocenteEstadoEjecucionNuevoNavigations = new HashSet<TGestionContactoFlujoActividadAgregadaLog>();
            TGestionContactoFlujoCongeladoLogIdGestionDocenteEstadoEjecucionAnteriorNavigations = new HashSet<TGestionContactoFlujoCongeladoLog>();
            TGestionContactoFlujoCongeladoLogIdGestionDocenteEstadoEjecucionNuevoNavigations = new HashSet<TGestionContactoFlujoCongeladoLog>();
            TGestionContactoFlujoCongelados = new HashSet<TGestionContactoFlujoCongelado>();
            TGestionDocenteActividadCabeceraCongelada = new HashSet<TGestionDocenteActividadCabeceraCongeladum>();
            TGestionDocenteActividadCabeceraCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations = new HashSet<TGestionDocenteActividadCabeceraCongeladaLog>();
            TGestionDocenteActividadCabeceraCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations = new HashSet<TGestionDocenteActividadCabeceraCongeladaLog>();
            TGestionDocenteActividadDetalleCongelada = new HashSet<TGestionDocenteActividadDetalleCongeladum>();
            TGestionDocenteActividadDetalleCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations = new HashSet<TGestionDocenteActividadDetalleCongeladaLog>();
            TGestionDocenteActividadDetalleCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations = new HashSet<TGestionDocenteActividadDetalleCongeladaLog>();
            TGestionDocenteDisparadorCongeladoLogIdGestionDocenteEstadoEjecucionAnteriorNavigations = new HashSet<TGestionDocenteDisparadorCongeladoLog>();
            TGestionDocenteDisparadorCongeladoLogIdGestionDocenteEstadoEjecucionNuevoNavigations = new HashSet<TGestionDocenteDisparadorCongeladoLog>();
            TGestionDocenteDisparadorCongelados = new HashSet<TGestionDocenteDisparadorCongelado>();
            TGestionDocenteDisparadorEventoTipoCongelados = new HashSet<TGestionDocenteDisparadorEventoTipoCongelado>();
            TGestionDocenteDisparadorOcurrenciaDetalleCongelados = new HashSet<TGestionDocenteDisparadorOcurrenciaDetalleCongelado>();
            TGestionDocenteDisparadorReglaTiempoFijoCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoFijoCongelado>();
            TGestionDocenteDisparadorReglaTiempoRelativoCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoCongelado>();
            TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelado>();
            TGestionDocenteOcurrenciaCongelada = new HashSet<TGestionDocenteOcurrenciaCongeladum>();
            TGestionDocenteOcurrenciaCongeladaIaConfiguracions = new HashSet<TGestionDocenteOcurrenciaCongeladaIaConfiguracion>();
            TGestionDocenteOcurrenciaCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations = new HashSet<TGestionDocenteOcurrenciaCongeladaLog>();
            TGestionDocenteOcurrenciaCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations = new HashSet<TGestionDocenteOcurrenciaCongeladaLog>();
            TGestionDocenteOcurrenciaIaEjemploCongelada = new HashSet<TGestionDocenteOcurrenciaIaEjemploCongeladum>();
        }

        /// <summary>
        /// Identificador único del estado de ejecución. Clave primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Código único e inmutable del estado. Valores válidos: EJECUTADA, POR_EJECUTAR, PENDIENTE, EN_PROGRESO, FALLIDA, CANCELADA, REINTENTANDO. Máximo 50 caracteres. Campo UNIQUE. Identificador textual usado en código.
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Nombre descriptivo legible del estado. Máximo 100 caracteres. Ejemplo: &quot;Ejecutada correctamente&quot;, &quot;Esperando ejecución&quot;, &quot;En proceso de ejecución&quot;, &quot;Falló en ejecución&quot;, etc.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del estado. Máximo 500 caracteres. Aclara qué significa el estado y cuándo se usa. Opcional. Ejemplo: &quot;La entidad se ejecutó exitosamente. No se requieren acciones adicionales.&quot;
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Indicador de si el registro está activo/disponible. 1 = Activo (puede usarse), 0 = Inactivo (obsoleto). Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro en el catálogo. Máximo 50 caracteres. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó el registro. Máximo 50 caracteres. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 de creación del registro. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría obligatorio.
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

        public virtual ICollection<TGestionContactoActividadSesionCongeladum> TGestionContactoActividadSesionCongelada { get; set; }
        public virtual ICollection<TGestionContactoActividadSesionCongeladaLog> TGestionContactoActividadSesionCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionContactoActividadSesionCongeladaLog> TGestionContactoActividadSesionCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations { get; set; }
        public virtual ICollection<TGestionContactoFlujoActividadAgregadaLog> TGestionContactoFlujoActividadAgregadaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionContactoFlujoActividadAgregadaLog> TGestionContactoFlujoActividadAgregadaLogIdGestionDocenteEstadoEjecucionNuevoNavigations { get; set; }
        public virtual ICollection<TGestionContactoFlujoCongeladoLog> TGestionContactoFlujoCongeladoLogIdGestionDocenteEstadoEjecucionAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionContactoFlujoCongeladoLog> TGestionContactoFlujoCongeladoLogIdGestionDocenteEstadoEjecucionNuevoNavigations { get; set; }
        public virtual ICollection<TGestionContactoFlujoCongelado> TGestionContactoFlujoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteActividadCabeceraCongeladum> TGestionDocenteActividadCabeceraCongelada { get; set; }
        public virtual ICollection<TGestionDocenteActividadCabeceraCongeladaLog> TGestionDocenteActividadCabeceraCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionDocenteActividadCabeceraCongeladaLog> TGestionDocenteActividadCabeceraCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations { get; set; }
        public virtual ICollection<TGestionDocenteActividadDetalleCongeladum> TGestionDocenteActividadDetalleCongelada { get; set; }
        public virtual ICollection<TGestionDocenteActividadDetalleCongeladaLog> TGestionDocenteActividadDetalleCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionDocenteActividadDetalleCongeladaLog> TGestionDocenteActividadDetalleCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorCongeladoLog> TGestionDocenteDisparadorCongeladoLogIdGestionDocenteEstadoEjecucionAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorCongeladoLog> TGestionDocenteDisparadorCongeladoLogIdGestionDocenteEstadoEjecucionNuevoNavigations { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorCongelado> TGestionDocenteDisparadorCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorEventoTipoCongelado> TGestionDocenteDisparadorEventoTipoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorOcurrenciaDetalleCongelado> TGestionDocenteDisparadorOcurrenciaDetalleCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoFijoCongelado> TGestionDocenteDisparadorReglaTiempoFijoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoCongelado> TGestionDocenteDisparadorReglaTiempoRelativoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelado> TGestionDocenteDisparadorReglaTiempoRelativoReferenciaCongelados { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladum> TGestionDocenteOcurrenciaCongelada { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladaIaConfiguracion> TGestionDocenteOcurrenciaCongeladaIaConfiguracions { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladaLog> TGestionDocenteOcurrenciaCongeladaLogIdGestionDocenteEstadoEjecucionAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladaLog> TGestionDocenteOcurrenciaCongeladaLogIdGestionDocenteEstadoEjecucionNuevoNavigations { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaIaEjemploCongeladum> TGestionDocenteOcurrenciaIaEjemploCongelada { get; set; }
    }
}
