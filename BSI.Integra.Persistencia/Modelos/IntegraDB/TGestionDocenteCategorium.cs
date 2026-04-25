using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de categorías para clasificar flujos y actividades de gestión docente (ej: Académico, Administrativo)
    /// </summary>
    public partial class TGestionDocenteCategorium
    {
        public TGestionDocenteCategorium()
        {
            TGestionContactoFlujoCongelados = new HashSet<TGestionContactoFlujoCongelado>();
            TGestionDocenteActividadCabeceraCongelada = new HashSet<TGestionDocenteActividadCabeceraCongeladum>();
            TGestionDocenteActividadCabeceras = new HashSet<TGestionDocenteActividadCabecera>();
            TGestionDocenteCategoriaGeneralTiempos = new HashSet<TGestionDocenteCategoriaGeneralTiempo>();
            TGestionDocenteFlujos = new HashSet<TGestionDocenteFlujo>();
        }

        /// <summary>
        /// Identificador único de la categoría
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la categoría
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción de la categoría
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

        public virtual ICollection<TGestionContactoFlujoCongelado> TGestionContactoFlujoCongelados { get; set; }
        public virtual ICollection<TGestionDocenteActividadCabeceraCongeladum> TGestionDocenteActividadCabeceraCongelada { get; set; }
        public virtual ICollection<TGestionDocenteActividadCabecera> TGestionDocenteActividadCabeceras { get; set; }
        public virtual ICollection<TGestionDocenteCategoriaGeneralTiempo> TGestionDocenteCategoriaGeneralTiempos { get; set; }
        public virtual ICollection<TGestionDocenteFlujo> TGestionDocenteFlujos { get; set; }
    }
}
