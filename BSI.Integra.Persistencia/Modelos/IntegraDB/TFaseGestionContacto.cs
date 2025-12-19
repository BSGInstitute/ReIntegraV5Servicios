using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo que contiene las fases del proceso de gestión de contactos (Lead, Prospecto, Cliente, etc.)
    /// </summary>
    public partial class TFaseGestionContacto
    {
        public TFaseGestionContacto()
        {
            TGestionContactoLogIdFaseGestionContactoAnteriorNavigations = new HashSet<TGestionContactoLog>();
            TGestionContactoLogIdFaseGestionContactoNavigations = new HashSet<TGestionContactoLog>();
            TGestionContactos = new HashSet<TGestionContacto>();
        }

        /// <summary>
        /// Identificador único de la fase de gestión de contacto (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Código único identificador de la fase (ej: LEAD, PROSP, CLI)
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Nombre descriptivo de la fase del proceso de gestión de contactos
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Descripción detallada de la fase y sus características
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (1: Activo, 0: Eliminado/Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automático que guarda la versión del registro para control de concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TGestionContactoLog> TGestionContactoLogIdFaseGestionContactoAnteriorNavigations { get; set; }
        public virtual ICollection<TGestionContactoLog> TGestionContactoLogIdFaseGestionContactoNavigations { get; set; }
        public virtual ICollection<TGestionContacto> TGestionContactos { get; set; }
    }
}
