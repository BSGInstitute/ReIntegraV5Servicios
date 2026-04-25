using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de ejemplo de entrenamiento IA. Son instancias de
    ///   texto etiquetadas (positivas/negativas) usadas para entrenar y validar el modelo de clasificación de IA.
    /// </summary>
    public partial class TGestionDocenteOcurrenciaIaEjemploCongeladum
    {
        /// <summary>
        /// Identificador único del ejemplo de entrenamiento IA congelado. Clave
        ///   primaria. Generado automáticamente por IDENTITY.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la configuración IA congelada a la cual pertenece
        ///   este ejemplo. Clave foránea a T_GestionDocenteOcurrenciaCongeladaIaConfiguracion. Establece relación jerárquica.
        /// </summary>
        public int IdGestionDocenteOcurrenciaCongeladaIaConfiguracion { get; set; }
        /// <summary>
        /// Identificador del ejemplo de entrenamiento original (sin congelar).
        ///   Referencia el maestro para auditoría. Permite rastrear la versión original.
        /// </summary>
        public int IdGestionDocenteIaEntrenamientoEjemplo { get; set; }
        /// <summary>
        /// Identificador de la configuración IA original (sin congelar).
        ///   Referencia el maestro para auditoría. Permite rastrear la configuración original.
        /// </summary>
        public int IdGestionDocenteOcurrenciaIaConfiguracion { get; set; }
        /// <summary>
        /// Identificador del tipo de clasificación del ejemplo. Valores válidos:
        ///    ENTRENAMIENTO (para entrenar el modelo), VALIDACION (para validar el modelo entrenado), PRUEBA (para pruebas
        ///   finales). Define el propósito del ejemplo.
        /// </summary>
        public int IdGestionDocenteIaEntrenamientoClasificacionTipo { get; set; }
        /// <summary>
        /// Texto del ejemplo usado para entrenamiento de la IA. Puede ser un
        ///   comentario, mensaje, o cualquier entrada que el modelo debe aprender a clasificar. Ejemplo: &quot;Excelente participación
        ///   en clase, muy buena asistencia.&quot; Puede ser muy extenso (TEXT).
        /// </summary>
        public string TextoEjemplo { get; set; } = null!;
        /// <summary>
        /// Indicador de clasificación correcta del ejemplo. 1 = Ejemplo positivo
        ///    (la ocurrencia SÍ se cumple), 0 = Ejemplo negativo (la ocurrencia NO se cumple). Used for training the AI model to
        ///   recognize correct vs incorrect cases.
        /// </summary>
        public bool EsPositivo { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución del ejemplo. Referencia a
        ///   T_GestionDocenteEstadoEjecucion. Indica si el ejemplo está pendiente, en uso, archivado, etc.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo del registro. 1 = Activo (el
        ///   ejemplo se usa para entrenamiento), 0 = Inactivo (el ejemplo está deshabilitado). Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento del ejemplo. Máximo 50
        ///   caracteres. Referencia al usuario del sistema que ejecutó la operación. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó este registro congelado. Máximo 50
        ///   caracteres. Se actualiza cada vez que hay cambios. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora exacta en UTC-5 (Hora Estándar de Perú) cuando se
        ///   congeló el ejemplo. Marca el momento de captura del estado. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría
        ///   obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación del registro
        ///   congelado. Se actualiza automáticamente con cada cambio. Formato: YYYY-MM-DD HH:MM:SS. Campo de auditoría
        ///   obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Número de versión para control de concurrencia optimista. Generado
        ///   automáticamente por SQL Server (TIMESTAMP). Previene conflictos en actualizaciones simultáneas. Campo de auditoría
        ///   obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteIaEntrenamientoClasificacionTipo IdGestionDocenteIaEntrenamientoClasificacionTipoNavigation { get; set; } = null!;
        public virtual TGestionDocenteIaEntrenamientoEjemplo IdGestionDocenteIaEntrenamientoEjemploNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaCongeladaIaConfiguracion IdGestionDocenteOcurrenciaCongeladaIaConfiguracionNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaIaConfiguracion IdGestionDocenteOcurrenciaIaConfiguracionNavigation { get; set; } = null!;
    }
}
