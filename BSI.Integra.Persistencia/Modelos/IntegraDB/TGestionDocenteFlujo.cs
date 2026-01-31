using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla principal que define flujos de trabajo para gestión docente (ej: Email Recordatorio Subir Notas)
    /// </summary>
    public partial class TGestionDocenteFlujo
    {
        public TGestionDocenteFlujo()
        {
            TGestionDocenteActividadCabeceraFlujos = new HashSet<TGestionDocenteActividadCabeceraFlujo>();
        }

        /// <summary>
        /// Identificador único del flujo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del flujo (ej: Email Recordatorio Subir Notas)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del flujo
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteEstado
        /// </summary>
        public int IdGestionDocenteEstado { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteCategoria
        /// </summary>
        public int IdGestionDocenteCategoria { get; set; }
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

        public virtual TGestionDocenteCategorium IdGestionDocenteCategoriaNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstado IdGestionDocenteEstadoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionDocenteActividadCabeceraFlujo> TGestionDocenteActividadCabeceraFlujos { get; set; }
    }
}
