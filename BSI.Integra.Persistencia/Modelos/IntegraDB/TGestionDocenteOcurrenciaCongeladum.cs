using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de ocurrencia. Una ocurrencia es un evento registrable que ocurre durante la ejecución de una actividad. Ejemplo: Asistencia registrada, Tarea completada, Problema resuelto. Incluye todos los campos del maestro.
    /// </summary>
    public partial class TGestionDocenteOcurrenciaCongeladum
    {
        public TGestionDocenteOcurrenciaCongeladum()
        {
            TGestionDocenteDisparadorCongelados = new HashSet<TGestionDocenteDisparadorCongelado>();
            TGestionDocenteDisparadorOcurrenciaDetalleCongelados = new HashSet<TGestionDocenteDisparadorOcurrenciaDetalleCongelado>();
            TGestionDocenteOcurrenciaCongeladaIaConfiguracions = new HashSet<TGestionDocenteOcurrenciaCongeladaIaConfiguracion>();
            TGestionDocenteOcurrenciaCongeladaLogs = new HashSet<TGestionDocenteOcurrenciaCongeladaLog>();
        }

        /// <summary>
        /// Identificador único. Clave primaria. Generado automáticamente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la actividad detalle congelada a la cual pertenece esta ocurrencia. Clave foránea a T_GestionDocenteActividadDetalleCongelada. Establece relación jerárquica.
        /// </summary>
        public int IdGestionDocenteActividadDetalleCongelada { get; set; }
        /// <summary>
        /// Identificador de la ocurrencia original (sin congelar). Referencia el maestro para auditoría.
        /// </summary>
        public int IdGestionDocenteOcurrencia { get; set; }
        /// <summary>
        /// Nombre descriptivo de la ocurrencia. Máximo 200 caracteres. Ejemplo: &quot;Asistencia confirmada&quot;, &quot;Tarea enviada&quot;, &quot;Evaluación completada&quot;, &quot;Problema resuelto&quot;.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada de qué representa esta ocurrencia. Máximo 500 caracteres. Opcional. Ejemplo: &quot;Marca que el estudiante asistió correctamente a la clase&quot;.
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Identificador del tipo de ocurrencia (POSITIVA, NEGATIVA, NEUTRAL). Referencia a T_GestionDocenteOcurrenciaTipo. Clasifica el resultado de la ocurrencia. POSITIVA = algo bueno ocurrió, NEGATIVA = algo malo, NEUTRAL = informativo.
        /// </summary>
        public int IdGestionDocenteOcurrenciaTipo { get; set; }
        /// <summary>
        /// Identificador de la actividad detalle original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteActividadDetalle { get; set; }
        /// <summary>
        /// Identificador del modo de marcado (MANUAL, AUTOMATICO, CONFIRMACION). Referencia a T_GestionDocenteModoMarcado. Define cómo se registra la ocurrencia. MANUAL = por usuario, AUTOMATICO = por sistema, CONFIRMACION = usuario confirma sis registró.
        /// </summary>
        public int IdGestionDocenteModoMarcado { get; set; }
        /// <summary>
        /// Indicador de si se requiere comentario para marcar la ocurrencia. 1 = Obligatorio proporcionador comentario, 0 = Opcional o no requerido.
        /// </summary>
        public bool RequiereComentario { get; set; }
        /// <summary>
        /// Indicador de si se requiere fecha y hora para marcar la ocurrencia. 1 = Obligatorio capturar fecha/hora, 0 = Opcional o no requerido.
        /// </summary>
        public bool RequiereFechaHora { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución. Referencia a T_GestionDocenteEstadoEjecucion.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 del congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteActividadDetalleCongeladum IdGestionDocenteActividadDetalleCongeladaNavigation { get; set; } = null!;
        public virtual TGestionDocenteActividadDetalle IdGestionDocenteActividadDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteModoMarcado IdGestionDocenteModoMarcadoNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrencium IdGestionDocenteOcurrenciaNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaTipo IdGestionDocenteOcurrenciaTipoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteDisparadorCongelado> TGestionDocenteDisparadorCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorOcurrenciaDetalleCongelado> TGestionDocenteDisparadorOcurrenciaDetalleCongelados { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladaIaConfiguracion> TGestionDocenteOcurrenciaCongeladaIaConfiguracions { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladaLog> TGestionDocenteOcurrenciaCongeladaLogs { get; set; }
    }
}
