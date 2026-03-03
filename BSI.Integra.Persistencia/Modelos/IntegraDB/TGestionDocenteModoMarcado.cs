using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de modos de marcado de ocurrencias (Manual, Automático, MARM)
    /// </summary>
    public partial class TGestionDocenteModoMarcado
    {
        public TGestionDocenteModoMarcado()
        {
            TGestionDocenteOcurrencia = new HashSet<TGestionDocenteOcurrencium>();
            TGestionDocenteOcurrenciaCongelada = new HashSet<TGestionDocenteOcurrenciaCongeladum>();
        }

        /// <summary>
        /// Identificador único del modo de marcado
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del modo (ej: Manual, Automático, MARM)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción del modo de marcado
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

        public virtual ICollection<TGestionDocenteOcurrencium> TGestionDocenteOcurrencia { get; set; }
        public virtual ICollection<TGestionDocenteOcurrenciaCongeladum> TGestionDocenteOcurrenciaCongelada { get; set; }
    }
}
