using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de tipos de clasificación para entrenamiento de IA
    /// </summary>
    public partial class TGestionDocenteIaEntrenamientoClasificacionTipo
    {
        public TGestionDocenteIaEntrenamientoClasificacionTipo()
        {
            TGestionDocenteIaEntrenamientoEjemplos = new HashSet<TGestionDocenteIaEntrenamientoEjemplo>();
            TGestionDocenteOcurrenciaIaEjemploCongelada = new HashSet<TGestionDocenteOcurrenciaIaEjemploCongeladum>();
        }

        /// <summary>
        /// Identificador único del tipo de clasificación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del tipo de clasificación
        /// </summary>
        public string Nombre { get; set; } = null!;
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

        public virtual ICollection<TGestionDocenteIaEntrenamientoEjemplo> TGestionDocenteIaEntrenamientoEjemplos { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaIaEjemploCongeladum> TGestionDocenteOcurrenciaIaEjemploCongelada { get; set; }
    }
}
