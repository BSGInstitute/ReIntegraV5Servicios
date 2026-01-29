using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Registra fechas y tiempos asociados a categorías de gestión docente
    /// </summary>
    public partial class TGestionDocenteCategoriaGeneralTiempo
    {
        /// <summary>
        /// Identificador único del registro de tiempo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteCategoria
        /// </summary>
        public int IdGestionDocenteCategoria { get; set; }
        /// <summary>
        /// Fecha y hora asociada a la categoría
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

        public virtual TGestionDocenteCategorium IdGestionDocenteCategoriaNavigation { get; set; } = null!;
    }
}
