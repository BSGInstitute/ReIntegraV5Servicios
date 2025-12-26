using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de estados de cabecera de gestión de contacto
    /// </summary>
    public partial class TEstadoCabeceraGestionContacto
    {
        public TEstadoCabeceraGestionContacto()
        {
            TSubEstadoCabeceraGestionContactos = new HashSet<TSubEstadoCabeceraGestionContacto>();
        }

        /// <summary>
        /// Identificador único del estado (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción del estado de matrícula
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado del registro (1: Activo, 0: Eliminado/Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro
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
        /// Campo de sistema automático que guarda la versión del registro para control de concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Identificador de migración de datos
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Indicador si el estado está activo para uso
        /// </summary>
        public bool? Activo { get; set; }

        public virtual ICollection<TSubEstadoCabeceraGestionContacto> TSubEstadoCabeceraGestionContactos { get; set; }
    }
}
