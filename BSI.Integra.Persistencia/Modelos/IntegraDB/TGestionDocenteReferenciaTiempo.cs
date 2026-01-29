using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Define puntos de referencia temporal (ANTES_SESION, DESPUES_SESION)
    /// </summary>
    public partial class TGestionDocenteReferenciaTiempo
    {
        public TGestionDocenteReferenciaTiempo()
        {
            TGestionDocenteDisparadorReglaTiempoRelativoReferencia = new HashSet<TGestionDocenteDisparadorReglaTiempoRelativoReferencium>();
        }

        /// <summary>
        /// Identificador único de la referencia de tiempo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la referencia (ej: Antes de Sesión)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Código de la referencia (ej: ANTES_SESION)
        /// </summary>
        public string Codigo { get; set; } = null!;
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

        public virtual ICollection<TGestionDocenteDisparadorReglaTiempoRelativoReferencium> TGestionDocenteDisparadorReglaTiempoRelativoReferencia { get; set; }
    }
}
