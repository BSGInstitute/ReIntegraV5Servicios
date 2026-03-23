using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Define eventos específicos del sistema que pueden disparar actividades (ej: Finalización de Sesión, Creación de Curso)
    /// </summary>
    public partial class TGestionDocenteDisparadorEventoTipo
    {
        public TGestionDocenteDisparadorEventoTipo()
        {
            TGestionDocenteDisparadorEventoTipoCongelados = new HashSet<TGestionDocenteDisparadorEventoTipoCongelado>();
        }

        /// <summary>
        /// Identificador único del evento
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteDisparadorDetalle
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Nombre del evento (ej: Finalización de Sesión)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción del evento
        /// </summary>
        public string? Descripcion { get; set; }
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
        public virtual ICollection<TGestionDocenteDisparadorEventoTipoCongelado> TGestionDocenteDisparadorEventoTipoCongelados { get; set; }
    }
}
