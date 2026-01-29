using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena ejemplos de texto para entrenar el modelo de IA en clasificación de ocurrencias
    /// </summary>
    public partial class TGestionDocenteIaEntrenamientoEjemplo
    {
        /// <summary>
        /// Identificador único del ejemplo de entrenamiento
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteOcurrenciaIaConfiguracion
        /// </summary>
        public int IdGestionDocenteOcurrenciaIaConfiguracion { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteIaEntrenamientoClasificacionTipo
        /// </summary>
        public int IdGestionDocenteIaEntrenamientoClasificacionTipo { get; set; }
        /// <summary>
        /// Texto de ejemplo para entrenamiento (ej: Sí, confirmo mi asistencia)
        /// </summary>
        public string TextoEjemplo { get; set; } = null!;
        /// <summary>
        /// Indica si es ejemplo positivo (1) o contraejemplo (0)
        /// </summary>
        public bool EsPositivo { get; set; }
        /// <summary>
        /// Estado del registro (1=Activo, 0=Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión de fila para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteIaEntrenamientoClasificacionTipo IdGestionDocenteIaEntrenamientoClasificacionTipoNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaIaConfiguracion IdGestionDocenteOcurrenciaIaConfiguracionNavigation { get; set; } = null!;
    }
}
