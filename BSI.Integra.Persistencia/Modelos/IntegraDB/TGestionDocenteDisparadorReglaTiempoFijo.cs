using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Define disparadores con fecha y hora específica
    /// </summary>
    public partial class TGestionDocenteDisparadorReglaTiempoFijo
    {
        /// <summary>
        /// Identificador único de la regla de tiempo fijo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteDisparadorReglaTiempo
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteDisparadorDetalle
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Fecha y hora específica del disparador
        /// </summary>
        public DateTime Fecha { get; set; }
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

        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorReglaTiempo IdGestionDocenteDisparadorReglaTiempoNavigation { get; set; } = null!;
    }
}
