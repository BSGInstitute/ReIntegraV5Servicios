using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de unidades de tiempo (Minutos, Horas, Días)
    /// </summary>
    public partial class TGestionDocenteUnidadTiempo
    {
        public TGestionDocenteUnidadTiempo()
        {
            TGestionDocenteDisparadorReglaTiempoRelativoCongelados = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoCongelado>();
            TGestionDocenteDisparadorReglaTiempoRelativos = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativo>();
        }

        /// <summary>
        /// Identificador único de la unidad de tiempo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la unidad de tiempo (ej: Minutos, Horas, Días)
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

        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoCongelado> TGestionDocenteDisparadorReglaTiempoRelativoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativo> TGestionDocenteDisparadorReglaTiempoRelativos { get; set; }
    }
}
