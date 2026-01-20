using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena los niveles de aprobación para tipos de descuento con configuración de jerarquías de autorización multinivel
    /// </summary>
    public partial class TTipoDescuentoNivelAprobacion
    {
        public TTipoDescuentoNivelAprobacion()
        {
            TTipoDescuentoNivelAprobacionRols = new HashSet<TTipoDescuentoNivelAprobacionRol>();
        }

        /// <summary>
        /// Identificador único autoincremental del nivel de aprobación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo del nivel de aprobación (Supervisor, Gerente, Director, Gerente General)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del nivel de aprobación, responsabilidades, alcance de autorización y límites aplicables
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Indicador de estado del registro (1 = Activo, 0 = Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario del sistema que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario del sistema que realizó la última modificación del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de versión para concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TTipoDescuentoNivelAprobacionRol> TTipoDescuentoNivelAprobacionRols { get; set; }
    }
}
