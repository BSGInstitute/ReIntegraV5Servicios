using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla base para reglas de tiempo (FIJO o RELATIVO)
    /// </summary>
    public partial class TGestionDocenteDisparadorReglaTiempo
    {
        public TGestionDocenteDisparadorReglaTiempo()
        {
            TGestionDocenteDisparadorReglaTiempoFijos = new HashSet<TGestionDocenteDisparadorReglaTiempoFijo>();
            TGestionDocenteDisparadorReglaTiempoRelativos = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativo>();
        }

        /// <summary>
        /// Identificador único de la regla de tiempo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tipo de regla (FIJO o RELATIVO)
        /// </summary>
        public string TipoRegla { get; set; } = null!;
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

        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoFijo> TGestionDocenteDisparadorReglaTiempoFijos { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativo> TGestionDocenteDisparadorReglaTiempoRelativos { get; set; }
    }
}
